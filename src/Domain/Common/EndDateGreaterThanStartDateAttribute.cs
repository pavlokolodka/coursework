using System.ComponentModel.DataAnnotations;

namespace ReserveSpot
{
    public class EndDateGreaterThanStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty("StartDate"); 
       
            var startDate = (DateTime)(property.GetValue(validationContext.ObjectInstance) ?? DateTime.MinValue);

            if (value != null && (DateTime)value < startDate)
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
