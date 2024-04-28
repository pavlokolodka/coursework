using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ReserveSpot.Domain
{
    abstract public class JSONDao<Entity>
    {
        private string filePath;
        public JSONDao(string path)
        {
            filePath = path;
        }
        protected List<Entity> LoadEntitites()
        {
            if (!File.Exists(filePath))
            {
                return new List<Entity>();
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Entity>>(json);
        }

        protected bool ValidateEntity(Entity entity)
        {
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(entity, context, results, true);

            if (!isValid)
            {
                var errorMessages = results.Select(r => string.Join(Environment.NewLine, r.MemberNames.Select(m => $"{m}: {r.ErrorMessage}")));

                throw new ValidationException(string.Join(Environment.NewLine, errorMessages));
            }

            return true;
        }


        protected void SaveEntities(List<Entity> entities)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(entities, settings);
            string directoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(filePath, json);
        }
    }
}
