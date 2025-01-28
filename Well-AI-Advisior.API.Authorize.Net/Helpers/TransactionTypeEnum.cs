using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.Authorize.Net.Helpers
{
    
    public enum TransactionTypeEnum
    {
        authOnlyTransaction = 0,
        authCaptureTransaction = 1,
        captureOnlyTransaction = 2,
        refundTransaction = 3,
        priorAuthCaptureTransaction = 4,
        voidTransaction = 5,
        getDetailsTransaction = 6,
        authOnlyContinueTransaction = 7,
        authCaptureContinueTransaction = 8
    }
}
