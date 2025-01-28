using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Well_AI_Advisior.API.Authorize.Net.Model;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;
using Well_AI.Advisor.API.Authorize;
using System.Linq;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Well_AI_Advisior.API.Authorize.Net.Services
{
    public class RecurringBillingService : IRecurringBillingService
    {
        private readonly IAuthenticationService _authenticationRepository;
        public RecurringBillingService(IAuthenticationService authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public ANetApiResponse CancelSubscription(string subscriptionId, string accountType)
        {
            ARBCancelSubscriptionResponse response = new ARBCancelSubscriptionResponse();
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION : AuthorizeNet.Environment.SANDBOX;
            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            // define the merchant information (authentication / transaction id)
            if (authontiactionDetail != null)
            {
                var apiCredentials = JsonConvert.DeserializeObject(authontiactionDetail.Value);
                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = apiCredentials });
                AuthonticateJsonModel authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = authRequest.BodyParameter.LoginId,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = authRequest.BodyParameter.TransactionKey,
                };

                var request = new ARBCancelSubscriptionRequest { subscriptionId = subscriptionId };
                var controller = new ARBCancelSubscriptionController(request);                          // instantiate the controller that will call the service
                controller.Execute();

                response = controller.GetApiResponse();                   // get the response from the service (errors contained if any)
            }

            // validate response
            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                return response;
            }
            else if (response != null)
            {
                return response;
            }

            return response;
        }

        public SubscriptionResponse CreateSubscription(CreditCardType cctype, SubscriptionType subscriptionType, string accountType)
        {
            SubscriptionResponse apiResponse = new SubscriptionResponse();
            ARBCreateSubscriptionResponse response = new ARBCreateSubscriptionResponse();
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION: AuthorizeNet.Environment.SANDBOX;
            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            if (authontiactionDetail != null)
            {
                var apiCredentials = JsonConvert.DeserializeObject(authontiactionDetail.Value);
                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = apiCredentials });
                AuthonticateJsonModel authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = authRequest.BodyParameter.LoginId,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = authRequest.BodyParameter.TransactionKey,
                };

                paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();

                interval.length = subscriptionType.paymentSchedule.interval.length;                        // months can be indicated between 1 and 12
                interval.unit = (ARBSubscriptionUnitEnum)subscriptionType.paymentSchedule.interval.unit;

                paymentScheduleType schedule = new paymentScheduleType
                {
                    interval = interval,
                    startDate = DateTime.Now.AddDays(1),      // start date should be tomorrow
                    totalOccurrences = subscriptionType.paymentSchedule.totalOccurrences,                          // 999 indicates no end date
                    trialOccurrences = subscriptionType.paymentSchedule.trialOccurrences
                };

                #region Payment Information
                
                string creditCardNumber = DecryptData(Convert.ToString(cctype.CardNumber));
                string cardCode = DecryptData(Convert.ToString(cctype.CardCode));
                //string expirationDate = DecryptData(Convert.ToString(cctype.ExpirationDate));
                string[] expirationDateArray = cctype.ExpirationDate.Split("-");
                string ExpireMonth = ""; //DecryptData(Carddetails.ExpireMonth);
                string ExpireYear = "";// DecryptData(Carddetails.ExpireYear);
                if (expirationDateArray.Length > 0)
                {
                    ExpireYear = DecryptData(expirationDateArray[1]);
                    ExpireMonth = DecryptData(expirationDateArray[0]);
                }                
                var creditCard = new creditCardType
                {
                    cardNumber = creditCardNumber,//cctype.CardNumber,
                    expirationDate = ExpireMonth+@"-"+ ExpireYear,//cctype.ExpirationDate
                    cardCode= cardCode
                };

                //standard api call to retrieve response
                paymentType cc = new paymentType { Item = creditCard };
                #endregion

                nameAndAddressType addressInfo = new nameAndAddressType()
                {
                    firstName = subscriptionType.billTo.FirstName,
                    lastName = subscriptionType.billTo.LastName
                };

                ARBSubscriptionType _subscriptionType = new ARBSubscriptionType()
                {
                    amount = subscriptionType.amount,
                    trialAmount = subscriptionType.trialAmount,
                    paymentSchedule = schedule,
                    billTo = addressInfo,
                    payment = cc
                };
                var request = new ARBCreateSubscriptionRequest { subscription = _subscriptionType };
                var controller = new ARBCreateSubscriptionController(request);          // instantiate the controller that will call the service
                controller.Execute();
                response = controller.GetApiResponse();   // get the response from the service (errors contained if any)
                if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response != null && response.messages.message != null)
                    {
                        apiResponse.SubscriptionId = response.subscriptionId;
                        apiResponse.Message = response.messages.message[0].text;

                    }
                }
                else
                {
                    apiResponse.Message = response.messages.message[0].text;
                }
                return apiResponse;
            }
            else
            {
                apiResponse.Message = response.messages.message[0].text;

            }
            return apiResponse;
        }

        public ANetApiResponse GetListOfSubscriptions(string accountType)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION: AuthorizeNet.Environment.SANDBOX;
            ARBGetSubscriptionListResponse response = new ARBGetSubscriptionListResponse();
            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            // define the merchant information (authentication / transaction id)
            if (authontiactionDetail != null)
            {
                var apiCredentials = JsonConvert.DeserializeObject(authontiactionDetail.Value);
                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = apiCredentials });
                AuthonticateJsonModel authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = authRequest.BodyParameter.LoginId,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = authRequest.BodyParameter.TransactionKey,
                };

                var request = new ARBGetSubscriptionListRequest { searchType = ARBGetSubscriptionListSearchTypeEnum.subscriptionActive };    // only gets active subscriptions

                var controller = new ARBGetSubscriptionListController(request);          // instantiate the controller that will call the service
                controller.Execute();

                 response = controller.GetApiResponse();   // get the response from the service (errors contained if any)

                // validate response
            }
            else
            {
                response.messages.message[0].text = "Something went wrong. Pleasae check subscription details and proceed";
            }
            return response;
        }

        public SubscriptionResponse GetSubscriptionStatus(string subscriptionId, string accountType)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION: AuthorizeNet.Environment.SANDBOX;
            SubscriptionResponse apiResponse = new SubscriptionResponse();
            ARBGetSubscriptionStatusResponse response = new ARBGetSubscriptionStatusResponse();
            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            if (authontiactionDetail != null)
            {
                var apiCredentials = JsonConvert.DeserializeObject(authontiactionDetail.Value);
                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = apiCredentials });
                AuthonticateJsonModel authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                    name = authRequest.BodyParameter.LoginId,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = authRequest.BodyParameter.TransactionKey,
                };

            var request = new ARBGetSubscriptionStatusRequest { subscriptionId = subscriptionId };

            var controller = new ARBGetSubscriptionStatusController(request);                          // instantiate the controller that will call the service
            controller.Execute();

             response = controller.GetApiResponse();                   // get the response from the service (errors contained if any)

                // validate response
                if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response != null && response.messages.message != null)
                    {
                        
                        apiResponse.Message = response.messages.message[0].text;

                    }
                }
                else
                {
                    apiResponse.Message = response.messages.message[0].text;
                }
                return apiResponse;
            }
            else
            {
                apiResponse.Message = response.messages.message[0].text;

            }
            return apiResponse;
        }

        public ANetApiResponse UpdateSubscription(CreditCardType cctype, SubscriptionType subscriptionType, string subscriptionId, string accountType)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION: AuthorizeNet.Environment.SANDBOX;
            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            ARBUpdateSubscriptionResponse response = new ARBUpdateSubscriptionResponse();
            // define the merchant information (authentication / transaction id)
            if (authontiactionDetail != null)
            {
                var apiCredentials = JsonConvert.DeserializeObject(authontiactionDetail.Value);
                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = apiCredentials });
                AuthonticateJsonModel authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = authRequest.BodyParameter.LoginId,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = authRequest.BodyParameter.TransactionKey,
                };

                paymentScheduleType schedule = new paymentScheduleType
                {
                    //startDate = DateTime.Parse(subscriptionType.paymentSchedule.startDate),      // start date should be tomorrow
                    totalOccurrences = subscriptionType.paymentSchedule.totalOccurrences    // 999 indicates no end date
                };

                #region Payment Information
                string creditCardNumber = DecryptData(Convert.ToString(cctype.CardNumber));
                string expirationDate = DecryptData(Convert.ToString(cctype.ExpirationDate));
                var creditCard = new creditCardType
                {
                    cardNumber = creditCardNumber,//cctype.CardNumber,
                    expirationDate = expirationDate//cctype.ExpirationDate
                };

                //standard api call to retrieve response
                paymentType cc = new paymentType { Item = creditCard };
                #endregion

                nameAndAddressType addressInfo = new nameAndAddressType()
                {
                    firstName = subscriptionType.billTo.FirstName,
                    lastName = subscriptionType.billTo.LastName
                };

                //customerProfileIdType customerProfile = new customerProfileIdType()
                //{
                //    customerProfileId = "1232312",
                //    customerPaymentProfileId = "2132132",
                //    customerAddressId = "1233432"
                //};

                ARBSubscriptionType _subscriptionType = new ARBSubscriptionType()
                {
                    amount = subscriptionType.amount,
                    paymentSchedule = schedule,
                    billTo = addressInfo,
                    payment = cc
                    //You can pass a profile to update subscription
                    //,profile = customerProfile
                };

                //Please change the subscriptionId according to your request
                var request = new ARBUpdateSubscriptionRequest { subscription = _subscriptionType, subscriptionId = subscriptionId };
                var controller = new ARBUpdateSubscriptionController(request);
                controller.Execute();
                response = controller.GetApiResponse();
                   
            }
            else
            {
                response.messages.message[0].text = "Something went wrong. Pleasae check subscription details and proceed";
              
            }

            return response;
        }

        public ANetApiResponse GetSubscription(string subscriptionId, string accountType)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION: AuthorizeNet.Environment.SANDBOX;
            SubscriptionResponse apiResponse = new SubscriptionResponse();
            ARBGetSubscriptionResponse response = new ARBGetSubscriptionResponse();
            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            // define the merchant information (authentication / transaction id)
            if (authontiactionDetail != null)
            {
                var apiCredentials = JsonConvert.DeserializeObject(authontiactionDetail.Value);
                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = apiCredentials });
                AuthonticateJsonModel authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = authRequest.BodyParameter.LoginId,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = authRequest.BodyParameter.TransactionKey,
                };

                var request = new ARBGetSubscriptionRequest { subscriptionId = subscriptionId };

                var controller = new ARBGetSubscriptionController(request);                          // instantiate the controller that will call the service
                controller.Execute();

                response = controller.GetApiResponse();                   // get the response from the service (errors contained if any)

                // validate response
            }
            else
            {
                response.messages.message[0].text = "Something went wrong. Pleasae check subscription details and proceed";
            }
            
            return response;
        }

        public SubscriptionResponse GetTransactionListRequest(string accountType)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION : AuthorizeNet.Environment.SANDBOX;
            SubscriptionResponse apiResponse = new SubscriptionResponse();
            AuthorizeNet.Api.Contracts.V1.getTransactionListResponse response = new getTransactionListResponse();

            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            if (authontiactionDetail != null)
            {
                var apiCredentials = JsonConvert.DeserializeObject(authontiactionDetail.Value);
                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = apiCredentials });
                AuthonticateJsonModel authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = authRequest.BodyParameter.LoginId,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = authRequest.BodyParameter.TransactionKey,
                };

                var request = new getTransactionListRequest { batchId = "8410840" };

                var controller = new AuthorizeNet.Api.Controllers.getTransactionListController(request);                          // instantiate the controller that will call the service
                controller.Execute();

                response = controller.GetApiResponse();                   // get the response from the service (errors contained if any)

                // validate response
                if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response != null && response.messages.message != null)
                    {
                        apiResponse.Message = response.messages.message[0].text;
                    }
                }
                else
                {
                    apiResponse.Message = response.messages.message[0].text;
                }
                return apiResponse;
            }
            else
            {
                apiResponse.Message = response.messages.message[0].text;

            }
            return apiResponse;
        }

        public ANetApiResponse GetTransactionListRequestStatus(string accountType , string _batchId)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION : AuthorizeNet.Environment.SANDBOX;
            ANetApiResponse apiResponse = new ANetApiResponse();
            AuthorizeNet.Api.Contracts.V1.getTransactionListResponse response = new getTransactionListResponse();

            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            if (authontiactionDetail != null)
            {
                var apiCredentials = JsonConvert.DeserializeObject(authontiactionDetail.Value);
                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = apiCredentials });
                AuthonticateJsonModel authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = authRequest.BodyParameter.LoginId,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = authRequest.BodyParameter.TransactionKey,
                };



                var request = new getTransactionListRequest { batchId = _batchId };
                var controller = new AuthorizeNet.Api.Controllers.getTransactionListController(request);                          // instantiate the controller that will call the service
                controller.Execute();

                response = controller.GetApiResponse();                   // get the response from the service (errors contained if any)

                // validate response
                response = controller.GetApiResponse();                   // get the response from the service (errors contained if any)

                // validate response
            }
            else
            {
                response.messages.message[0].text = "Something went wrong. Pleasae check subscription details and proceed";
            }

            return response;
        }

        public ANetApiResponse GetSettledBatchListStatus(string accountType, DateTime Firstdate, DateTime Lastdate)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION : AuthorizeNet.Environment.SANDBOX;
            ANetApiResponse apiResponse = new ANetApiResponse();
            AuthorizeNet.Api.Contracts.V1.getSettledBatchListResponse response = new getSettledBatchListResponse();

            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            if (authontiactionDetail != null)
            {
                var apiCredentials = JsonConvert.DeserializeObject(authontiactionDetail.Value);
                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = apiCredentials });
                AuthonticateJsonModel authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = authRequest.BodyParameter.LoginId,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = authRequest.BodyParameter.TransactionKey,
                };
           
                var request = new getSettledBatchListRequest { firstSettlementDate = Firstdate, lastSettlementDate = Lastdate };
                var controller = new AuthorizeNet.Api.Controllers.getSettledBatchListController(request);                          // instantiate the controller that will call the service
                controller.Execute();

                response = controller.GetApiResponse();                   // get the response from the service (errors contained if any)

                // validate response
                response = controller.GetApiResponse();                   // get the response from the service (errors contained if any)

                // validate response
            }
            else
            {
                response.messages.message[0].text = "Something went wrong. Pleasae check subscription details and proceed";
            }

            return response;
        }

        private async Task<string> EncryptData(String CardNum)
        {
            try
            {
                var CardNumBytes = Encoding.UTF32.GetBytes(Convert.ToString(CardNum));
                var CardHashCode = Convert.ToBase64String(CardNumBytes);
                return await Task.FromResult(CardHashCode);
            }
            catch (Exception ex)
            {
                //CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                //customErrorHandler.WriteError(ex, "PaymentMethod EncryptData", null);
                return CardNum;
            }
        }

        private string DecryptData(string EncodeData)
        {
            try
            {
                if (!string.IsNullOrEmpty(EncodeData) && !EncodeData.All(Char.IsDigit))
                {
                    var CardDetailBytes = Convert.FromBase64String(EncodeData);

                    var CardDetail = Encoding.UTF32.GetString(CardDetailBytes);

                    return CardDetail;
                }

                return EncodeData;
            }
            catch (Exception ex)
            {
                //CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                //customErrorHandler.WriteError(ex, "PaymentMethod DecryptData", null);
                return EncodeData;
            }
        }

        public ANetApiResponse UpdateSubscriptionApi(CreditCardType cctype, SubscriptionType subscriptionType, string subscriptionId, string accountType)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION : AuthorizeNet.Environment.SANDBOX;
            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            ARBUpdateSubscriptionResponse response = new ARBUpdateSubscriptionResponse();
            // define the merchant information (authentication / transaction id)
            if (authontiactionDetail != null)
            {
                var apiCredentials = JsonConvert.DeserializeObject(authontiactionDetail.Value);
                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = apiCredentials });
                AuthonticateJsonModel authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = authRequest.BodyParameter.LoginId,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = authRequest.BodyParameter.TransactionKey,
                };

                paymentScheduleType schedule = new paymentScheduleType
                {
                    //startDate = DateTime.Parse(subscriptionType.paymentSchedule.startDate),      // start date should be tomorrow
                    totalOccurrences = subscriptionType.paymentSchedule.totalOccurrences    // 999 indicates no end date
                };

                #region Payment Information
                string creditCardNumber = DecryptData(Convert.ToString(cctype.CardNumber));
                string expirationDate = DecryptData(Convert.ToString(cctype.ExpirationDate));
                var creditCard = new creditCardType
                {
                    cardNumber = creditCardNumber,//cctype.CardNumber,
                    expirationDate = expirationDate//cctype.ExpirationDate
                };

                //standard api call to retrieve response
                paymentType cc = new paymentType { Item = creditCard };
                #endregion

                nameAndAddressType addressInfo = new nameAndAddressType()
                {
                    firstName = subscriptionType.billTo.FirstName,
                    lastName = subscriptionType.billTo.LastName,
                    company = subscriptionType.billTo.Company,
                    address = subscriptionType.billTo.Address,
                    city = subscriptionType.billTo.City,
                    state = subscriptionType.billTo.State,
                    zip = subscriptionType.billTo.Zip,
                    country = subscriptionType.billTo.Country
                };

                nameAndAddressType addressInfo1 = new nameAndAddressType()
                {
                    firstName = subscriptionType.billTo.FirstName,
                    lastName = subscriptionType.billTo.LastName,
                    company = subscriptionType.billTo.Company,
                    address = subscriptionType.billTo.Address,
                    city = subscriptionType.billTo.City,
                    state = subscriptionType.billTo.State,
                    zip = subscriptionType.billTo.Zip,
                    country = subscriptionType.billTo.Country
                };

                //customerProfileIdType customerProfile = new customerProfileIdType()
                //{
                //    customerProfileId = "1232312",
                //    customerPaymentProfileId = "2132132",
                //    customerAddressId = "1233432"
                //};

                ARBSubscriptionType _subscriptionType = new ARBSubscriptionType()
                {
                    amount = subscriptionType.amount,
                    paymentSchedule = schedule,
                    billTo = addressInfo,
                    shipTo = addressInfo1,
                    payment = cc

                };

                var request = new ARBUpdateSubscriptionRequest { subscription = _subscriptionType, subscriptionId = subscriptionId };
                var controller = new ARBUpdateSubscriptionController(request);
                controller.Execute();
                response = controller.GetApiResponse();

            }
            else
            {
                response.messages.message[0].text = "Something went wrong. Pleasae check subscription details and proceed";

            }

            return response;
        }
    }
}
