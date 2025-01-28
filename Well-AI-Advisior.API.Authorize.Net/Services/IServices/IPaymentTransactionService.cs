using AuthorizeNet.Api.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI_Advisior.API.Authorize.Net.Model;

namespace Well_AI_Advisior.API.Authorize.Net.Services.IServices
{
    public interface IPaymentTransactionService
    {
        ANetApiResponse CreditCardCharges(CreditCardType creditCardType, TransactionRequest transactionRequest,string accountType);

        ANetApiResponse RefundTransaction(CreditCard CreditCardDetails, decimal TransactionAmount, string TransactionID, string accountType);

        ANetApiResponse VoidTransaction(string TransactionID, string accountType);

        ANetApiResponse DebitBankAccount(BankAccountType bankAccountType, decimal amount, string accountType);

        ANetApiResponse CreditBankAccount(BankAccountType bankAccountType, decimal amount, string TransactionID, string accountType);


    }
}
