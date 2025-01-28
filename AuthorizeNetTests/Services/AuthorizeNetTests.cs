using Microsoft.VisualStudio.TestTools.UnitTesting;
using Well_AI_Advisior.API.Authorize.Net.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Well_AI_Advisior.API.Authorize.Net;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;
using Well_AI_Advisior.API.Authorize.Net.Model;

namespace AuthorizeNetTests
{
    [TestClass()]
    public class AuthorizeNetTests
    {
        [TestMethod()]
        public void GetCreditCardCharges()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IPaymentTransactionService>();
            CreditCardType ctype = new CreditCardType();
            TransactionRequest tRequest = new TransactionRequest();
            CustomerAddressType cType = new CustomerAddressType();
          
            cType.FirstName = "John";
            cType.LastName = "Doe";
            cType.Address = "123 My St";
            cType.City = "OurTown";
            cType.Zip = "98004";
            tRequest.amount = 10;
            tRequest.billTo = cType;
            ctype.CardNumber = "4111111111111111";
            ctype.ExpirationDate = "1028";
            ctype.CardCode = "123";

            var tasks = service.CreditCardCharges(ctype, tRequest,"SandBox");

            //Assert.IsTrue(tasks == 31);
            //Console.WriteLine("The VechicleList Count is equal to 31");
            //Console.ReadLine();
        }

        [TestMethod()]
        public void CreditBankAccount()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IPaymentTransactionService>();
            BankAccountType btype = new BankAccountType();
            decimal amount = 5;
            string transactionID = "0";
            btype.RoutingNumber = "121042882";
            btype.AccountNumber = "12673456789";
            btype.NameOnAccount = "John Doe";
            btype.BankName = "Wells Fargo Bank NA";
            var tasks = service.CreditBankAccount(btype, amount, transactionID, "SandBox");
        }

        [TestMethod()]
        public void DebitBankAccount()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IPaymentTransactionService>();
            BankAccountType btype = new BankAccountType();
            decimal amount = 5;
            Random rand = new Random();
            int randomAccountNumber = rand.Next(10000, int.MaxValue);
            btype.RoutingNumber = "121042882";
            btype.AccountNumber = randomAccountNumber.ToString();
            btype.NameOnAccount = "John Doe";
            btype.BankName = "Wells Fargo Bank NA";
            var tasks = service.DebitBankAccount(btype, amount, "SandBox");
        }

        [TestMethod()]
        public void RefundTransaction()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IPaymentTransactionService>();
            CreditCard c = new CreditCard();

            decimal amount = 5;
            c.cardNumber = "0015";
            c.expirationDate = "XXXX";
            string transactionId = "1234567890";
            var tasks = service.RefundTransaction(c, amount, transactionId, "SandBox");

        }

        [TestMethod()]
        public void VoidTransaction()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IPaymentTransactionService>();
            string transactionId = "1234567890";
            var tasks = service.VoidTransaction(transactionId, "SandBox");
        }

        [TestMethod()]
        public void CancelSubscription()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
            string subscriptionId = "100748";
            var tasks = service.CancelSubscription(subscriptionId, "SandBox");
        }

        [TestMethod()]
        public void GetSubscription()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
           
            string subscriptionId = "6743948";
            
            var tasks = service.GetSubscription(subscriptionId, "SandBox");
        }

        [TestMethod()]
        public void GetSubscriptionStatus()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
            string subscriptionId = "6743948";
            var tasks = service.GetSubscriptionStatus(subscriptionId, "SandBox");
        }

        [TestMethod()]
        public void GetListOfSubscriptions()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
            var tasks = service.GetListOfSubscriptions("SandBox");
            //Assert.Fail();
        }

        [TestMethod()]
        public void CreateSubscription()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
            Interval interval = new Interval();
            interval.length = 1;
            interval.unit = 1;
            PaymentSchedule paySechedule = new PaymentSchedule();
            paySechedule.totalOccurrences = 12;
            paySechedule.trialOccurrences = 1;
            var creditCard = new CreditCardType
            {
                CardNumber = "6011000000000012",
                ExpirationDate = "2020-12"
            };
            CustomerAddressType addressType = new CustomerAddressType();
            addressType.FirstName = "John";
            addressType.LastName = "Smith";
            PaymentType _payment = new PaymentType();
            _payment.Item = creditCard;
            SubscriptionType subType = new SubscriptionType();
            subType.amount = 5;
            subType.trialAmount = 35.55m;
            subType.paymentSchedule = paySechedule;
            subType.paymentSchedule.interval = new Interval();
            subType.paymentSchedule.interval = interval;
            subType.billTo = new CustomerAddressType();
            subType.billTo = addressType;
            subType.payment = _payment;
            //subType.payment
            var tasks = service.CreateSubscription(creditCard, subType, "SandBox");


        }
        

        [TestMethod()]
        public void UpdateSubscription()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
            Interval interval = new Interval();
            interval.length = 1;
            interval.unit = 0;
            PaymentSchedule paySechedule = new PaymentSchedule();
            paySechedule.totalOccurrences = 12;
            paySechedule.trialOccurrences = 1;

            /* paySechedule.startDate -- 
               Start date should be greater
               than or equal to subscription created date
               Parikshit */
           
            paySechedule.startDate= "2020-09-30";
            var creditCard = new CreditCardType
            {
                CardNumber = "4111111111111111",
                ExpirationDate = "2020-12"
            };
            CustomerAddressType addressType = new CustomerAddressType();
            addressType.FirstName = "John";
            addressType.LastName = "Smith";
            PaymentType _payment = new PaymentType();
            _payment.Item = creditCard;
            SubscriptionType subType = new SubscriptionType();
            subType.amount = 1000.00m;
            subType.paymentSchedule = paySechedule;
            subType.paymentSchedule.interval = new Interval();
            subType.paymentSchedule.interval = interval;
            subType.billTo = addressType;
            subType.payment = _payment;
            string subscriptionId = "6743948";
            //subType.payment
            var tasks = service.UpdateSubscription(creditCard, subType, subscriptionId, "SandBox");
        }
    }
}