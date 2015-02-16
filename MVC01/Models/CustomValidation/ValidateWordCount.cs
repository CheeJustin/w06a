using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC01.Models.CustomValidation
{
    public class ValidateWordCount : ValidationAttribute
    {
        private readonly int _limit;

        public ValidateWordCount(int limit) : base("{0} contains too many words. Limit is " + limit + ".")
        {
            _limit = limit;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessage = FormatErrorMessage(validationContext.DisplayName);

            // Split input into an array.
            if (value == null)
                return new ValidationResult(errorMessage);

            string[] words = value.ToString().Split();

            // Remove empty values in array from extra white space
            words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (words.Length > _limit)
                return new ValidationResult(errorMessage);
            return ValidationResult.Success;
        }
    }
}