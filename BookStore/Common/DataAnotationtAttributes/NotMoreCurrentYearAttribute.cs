using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Common.DataAnotationtAttributes
{
    public class NotMoreCurrentYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            if ((int)value > DateTime.Now.Year)
            {
                return new ValidationResult("PublishedOn can not be more than curent year");
            }
            return ValidationResult.Success;
        }
    }
}
