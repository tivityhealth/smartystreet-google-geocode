using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public class SgGeocoder
    {
        public static GeoPoint CallSgGeocoder(GeocodeInput geoInput)
        {
            GeoPoint result;

            if (geoInput.IsNull())
            {
                throw new ArgumentNullException("GeocodeInput", "Arguments cannot be null or empty");
            }
            
            if (geoInput.address != null && (geoInput.zipcode == null || geoInput.state == null || geoInput.city == null))
            {                
                result = UsStreetApi.CallUsStreet(geoInput);
            }
            else
            {
                //Zip fail should throw an exception. Do not call Google geocoder if fails
                if(geoInput.zipcode == null)
                {
                    if(geoInput.city == null || geoInput.state == null)
                    {
                        throw new ArgumentNullException("CityState", "City/State cannot be null or empty");
                    }
                }
                result = ZipApi.CallZip(geoInput);
            }

            if (result == null)
            {
                if(geoInput.address == null)
                {
                    geoInput.address = geoInput.city + " " + geoInput.state + " " + geoInput.zipcode;
                }
                result = GoogleGeocode.callGoogleGeocoder(geoInput.address);
            }

            return result;
        }
    }
}