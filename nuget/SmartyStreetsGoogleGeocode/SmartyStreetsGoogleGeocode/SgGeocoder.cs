using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public class SgGeocoder
    {
        public static GeoPoint CallSgGeocoder(AddressObject options)
        {
            if (options.zipcode != null || (options.state != null && options.city != null))
            {
                return ZipApi.CallZip(options);
            }
            else if (options.address != null)
            {
                return UsStreetApi.CallUsStreet(options);
            }

            return null;
        }
    }
}
