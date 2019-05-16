using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Common.Extensions
{
    public static class IdentityResultExtension
    {
        public static void CheckResult(this IdentityResult result)
        {
            if (result.Succeeded == false)
            {
                throw new ValidationException(result.Errors.Select(x => new KeyValuePair<string, string>(x.Code, x.Description)).ToList());
            }
        }
    }
}
