
using System;
using System.ComponentModel.DataAnnotations;
namespace ReserveSpot
{
    public class StartDateLessThanEndDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty("EndDate");
         
            var endDate = (DateTime)(property.GetValue(validationContext.ObjectInstance) ?? DateTime.MaxValue);

            //add days -1 to compare starting from 00:00:00
            if (value != null && ((DateTime)value > endDate || ((DateTime)value) < DateTime.Now.AddDays(-1)))
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }

}
