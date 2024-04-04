using System.Diagnostics;

namespace ReserveSpot
{
    public class UserCode : AbstractEntity
    {
        public int Code { get; set; }
       
        public UserCode()
        {
                     
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
