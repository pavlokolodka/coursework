using System.ComponentModel.DataAnnotations;


namespace Web.Shared
{
    public class ModelValidator
    {
        

    public static List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            return validationResults;
        }

}
}
