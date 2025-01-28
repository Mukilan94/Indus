using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Well_AI.Advisor.Administration.TokenProvider
{
    public class CustomPasswordResetTokenProvider<TUser>: DataProtectorTokenProvider<TUser> where TUser : class
    {
        public CustomPasswordResetTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomPasswordResetTokenProviderProviderOptions> options, ILogger<DataProtectorTokenProvider<TUser>> logger) :base(dataProtectionProvider,options, logger)
        { }
    }

    public class CustomPasswordResetTokenProviderProviderOptions
    : DataProtectionTokenProviderOptions
    { }
}
