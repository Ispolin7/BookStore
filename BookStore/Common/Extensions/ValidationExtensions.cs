using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Common
{
    public static class ValidationExtensions
    {
        public static void ThrowIfInvalid(this ValidationResult result)
        {
            if (result.IsValid == false)
            {
                throw new ModelStateException(result.Errors.Select(x => new KeyValuePair<string, string>(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
