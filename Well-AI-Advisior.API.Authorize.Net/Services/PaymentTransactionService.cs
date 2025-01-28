using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Authorize.Net.Helpers;
using Well_AI_Advisior.API.Authorize.Net.Helpers;
using Well_AI_Advisior.API.Authorize.Net.Model;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;

namespace Well_AI_Advisior.API.Authorize.Net.Services
{
    public class PaymentTransactionService : IPaymentTransactionService
    {

        private readonly IAuthenticationService _authenticationRepository;        
        public PaymentTransactionService(IAuthenticationService authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;

        }
        
        public ANetApiResponse CreditBankAccount(BankAccountType bankAccountType, decimal amount,string transactionID, string accountType)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = accountType == "Production" ? AuthorizeNet.Environment.PRODUCTION: AuthorizeNet.Environment.SANDBOX;
            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            // Extract the login details
            ApiHeaderParameter requestParam = JsonConvert.DeserializeObject<ApiHeaderParameter>(authontiactionDetail.Value);
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = requestParam.LoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = requestParam.TransactionKey,
            };
            var bankAccount = new bankAccountType
            {
                accountType = bankAccountTypeEnum.checking,
                routingNumber = bankAccountType.RoutingNumber,
                accountNumber = bankAccountType.AccountNumber,
                nameOnAccount = bankAccountType.NameOnAccount,
                echeckType = echeckTypeEnum.WEB,   // change based on how you take the payment (web, telephone, etc)
                bankName = bankAccountType.BankName,
                // checkNumber     = "101"                 // needed if echeckType is "ARC" or "BOC"
            };

            // standard api call to retrieve response
            var paymentType = new paymentType { Item = bankAccount };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.refundTransaction.ToString(),    // refund type
                payment = paymentType,
                amount = amount,
                refTransId = transactionID
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    return response;
                }
                else
                {
                    return response;
                }
            }
            else
            {
                return response;
            }

        }

        public ANetApiResponse DebitBankAccount(BankAccountType BankAccountType, decimal Amount, string accountType)
        {

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            // Extract the login details
            ApiHeaderParameter requestParam = JsonConvert.DeserializeObject<ApiHeaderParameter>(authontiactionDetail.Value);
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = requestParam.LoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = requestParam.TransactionKey,
            };

            var bankAccount = new bankAccountType
            {
                accountType = bankAccountTypeEnum.checking,
                routingNumber = BankAccountType.RoutingNumber,
                accountNumber = BankAccountType.AccountNumber.ToString(),
                nameOnAccount = BankAccountType.NameOnAccount,
                echeckType = echeckTypeEnum.WEB,   // change based on how you take the payment (web, telephone, etc)
                bankName = BankAccountType.BankName,
                // checkNumber     = "101"                 // needed if echeckType is "ARC" or "BOC"
            };
            var paymentType = new paymentType { Item = bankAccount };
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // refund type
                payment = paymentType,
                amount = Amount
            };
            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    return response;
                }
                else
                {
                    return response;
                }
            }
            else
            {
                return response;
            }

        }

        public ANetApiResponse CreditCardCharges(CreditCardType creditCardType, TransactionRequest transRequest, string accountType)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            // Extract the login details
            ApiHeaderParameter requestParam = JsonConvert.DeserializeObject<ApiHeaderParameter>(authontiactionDetail.Value);
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = requestParam.LoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = requestParam.TransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = creditCardType.CardNumber,
                expirationDate = creditCardType.ExpirationDate,
                cardCode = creditCardType.CardCode
            };

            var billingAddress = new customerAddressType
            {
                firstName = transRequest.billTo.FirstName,
                lastName = transRequest.billTo.LastName,
                address = transRequest.billTo.Address,
                city = transRequest.billTo.City,
                zip = transRequest.billTo.Zip
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard, dataSource = "", };

            //// Add line Items
            //var lineItems = new lineItemType[2];
            //lineItems[0] = new lineItemType { itemId = "1", name = "t-shirt", quantity = 2, unitPrice = new Decimal(15.00) };
            //lineItems[1] = new lineItemType { itemId = "2", name = "snowboard", quantity = 1, unitPrice = new Decimal(450.00) };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card

                amount = transRequest.amount,
                payment = paymentType,
                billTo = billingAddress,
                //lineItems = lineItems
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    return response;
                }
                else
                {
                    return response;

                }
            }
            else
            {
                return response;
            }
        }

        public ANetApiResponse RefundTransaction(CreditCard creditCardDetails, decimal TransactionAmount, string TransactionID, string accountType)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            // Extract the login details
            ApiHeaderParameter requestParam = JsonConvert.DeserializeObject<ApiHeaderParameter>(authontiactionDetail.Value);
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = requestParam.LoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = requestParam.TransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = creditCardDetails.cardNumber,
                expirationDate = creditCardDetails.expirationDate
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.refundTransaction.ToString(),    // refund type
                payment = paymentType,
                amount = TransactionAmount,
                refTransId = TransactionID
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        return response;
                    }
                    else
                    {
                        return response;
                    }
                }
                else
                {
                    return response;
                }
            }
            else
            {
                return response;
            }


        }

        public ANetApiResponse VoidTransaction(string TransactionID, string accountType)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ConfigurationModel authontiactionDetail = _authenticationRepository.AuthenticateApi();
            // Extract the login details
            ApiHeaderParameter requestParam = JsonConvert.DeserializeObject<ApiHeaderParameter>(authontiactionDetail.Value);
            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = requestParam.LoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = requestParam.TransactionKey,
            };

            var transactionRequest = new transactionRequestType
            {
                transactionType = TransactionTypeEnum.voidTransaction.ToString(),    // refund type
                refTransId = TransactionID
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                        Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                        Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                        Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                        Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                    }
                    else
                    {
                        Console.WriteLine("Failed Transaction.");
                        if (response.transactionResponse.errors != null)
                        {
                            Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                            Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed Transaction.");
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                        Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                    else
                    {
                        Console.WriteLine("Error Code: " + response.messages.message[0].code);
                        Console.WriteLine("Error message: " + response.messages.message[0].text);
                    }
                }
            }
            else
            {
                Console.WriteLine("Null Response.");
            }

            return response;
        }
    }
}
