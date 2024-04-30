using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveSpot.Domain.Common
{
    public class Validator
    {
        public static Guid? StringToGuild(string id)
        {
            string format = "D"; // D represents the format 00000000-0000-0000-0000-000000000000
            if (!Guid.TryParseExact(id, format, out Guid guid))
            {
                return null;
            }

            return guid;
        }
    }
}
