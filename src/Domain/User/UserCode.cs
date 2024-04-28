using Newtonsoft.Json;

namespace ReserveSpot.Domain
{
    public class UserCode : AbstractEntity
    {
        [JsonProperty]
        public int? Code { get; private set; }
       
        public UserCode()
        {
            Code = null;          
        }

        public int GenerateUserCode() {
            Random random = new Random();
            int sixDigitNumber = random.Next(100000, 1000000);
            Code = sixDigitNumber;
            return sixDigitNumber;
        }

        public bool ValidateUserCode()
        {
            return UpdatedAt.AddHours(24) > DateTime.Now; 
        }
    }
}
