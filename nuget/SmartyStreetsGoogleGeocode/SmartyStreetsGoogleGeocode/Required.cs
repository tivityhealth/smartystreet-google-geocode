using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public static class Validations
    {
        public static void Required(string label, object value)
        {
            if (value == null)
            {
                throw new ApplicationException($"{label} is required");
            }

            if (value is string && ((string)value).Trim() == "")
            {
                throw new ApplicationException($"{label} is required");
            }

        }
    }
}
