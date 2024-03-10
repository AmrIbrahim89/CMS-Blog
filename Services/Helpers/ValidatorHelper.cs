using System.ComponentModel.DataAnnotations;

namespace Services.Helpers
{
    internal static class ValidatorHelper
    {
        internal static void ModelValidation(object modelToValidate)
        {
            ValidationContext context = new(modelToValidate);
            List<ValidationResult> validationResults = new();
            bool isValid = Validator.TryValidateObject(modelToValidate, context, validationResults, true);

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
