using System.ComponentModel.DataAnnotations;

namespace Web.Shared.Dto
{
    public class EndDateGreaterThanStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty("StartDate"); 
            /*if (property == null)
            {
                return new ValidationResult("Invalid property name.");
            }*/

            var startDate = (DateTime)(property.GetValue(validationContext.ObjectInstance) ?? DateTime.MinValue);

            if (value != null && (DateTime)value < startDate.AddDays(1))
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
