
using System;
using System.ComponentModel.DataAnnotations;
namespace ReserveSpot
{
    public class StartDateLessThanOrEqualToEndDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty("EndDate");
            if (property == null)
            {
                return new ValidationResult("Invalid property name.");
            }

            var endDate = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (value != null && (DateTime)value > endDate)
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }

}
