using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public class SgGeocoder : ISgGeocoder
    {
        private GeocodeInput _geoInput;

        public SgGeocoder(GeocodeInput geoInput)
        {
            _geoInput = geoInput;
        }
        public GeoPoint CallSgGeocoder()
        {
            GeoPoint result;

            if (_geoInput.IsNull())
            {
                throw new ArgumentNullException("GeocodeInput", "Arguments cannot be null or empty");
            }

            if (_geoInput.address != null && (_geoInput.zipcode == null || _geoInput.state == null || _geoInput.city == null))
            {
                result = UsStreetApi.CallUsStreet(_geoInput);
            }
            else
            {
                //Zip fail should throw an exception. Do not call Google geocoder if fails
                if (_geoInput.zipcode == null)
                {
                    if (_geoInput.city == null || _geoInput.state == null)
                    {
                        throw new ArgumentNullException("CityState", "City/State cannot be null or empty");
                    }
                }
                result = ZipApi.CallZip(_geoInput);
            }

            if (result == null)
            {
                if (_geoInput.address == null)
                {
                    _geoInput.address = _geoInput.city + " " + _geoInput.state + " " + _geoInput.zipcode;
                }
                result = GoogleGeocode.callGoogleGeocoder(_geoInput.address);
            }

            return result;
        }
    }
}