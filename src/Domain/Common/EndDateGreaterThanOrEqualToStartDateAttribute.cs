using System.ComponentModel.DataAnnotations;

namespace ReserveSpot
{
    public class EndDateGreaterThanOrEqualToStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty("StartDate");
            if (property == null)
            {
                return new ValidationResult("Invalid property name.");
            }

            var startDate = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (value != null && (DateTime)value < startDate)
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
