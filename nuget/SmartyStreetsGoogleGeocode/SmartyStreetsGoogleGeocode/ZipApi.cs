using System;
using System.IO;
using SmartyStreets;
using SmartyStreets.USZipCodeApi;

namespace SmartyStreetsGoogleGeocode
{
    class ZipApi
    {
        public static GeoPoint CallZip(GeocodeInput options)
        {
            // You don't have to store your keys in environment variables, but we recommend it.
            var authId = Runtime.SmartyStreetsAuthId;
            var authToken = Runtime.SmartyStreetsAuthToken;
            var client = new ClientBuilder(authId, authToken).BuildUsZipCodeApiClient();

            // Documentation for input fields can be found at:
            // https://smartystreets.com/docs/us-zipcode-api#input-fields

            var lookup = new Lookup
            {
                City = options.city,
                State = options.state,
                ZipCode = options.zipcode,
            };

            try
            {
                client.Send(lookup);
            }
            catch (SmartyException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }

            var result = lookup.Result;

            if (result.Status == "invalid_zipcode" || result.Status == "invalid_state" || result.Status == "invalid_city")
            {
                throw new ApplicationException("Invalid Zip or State or City");
            }
            else if (result.Status == "conflict")
            {
                throw new ApplicationException("Conflicting ZIP Code/City/State information.");
            }
            else if (result.Status == "blank")
            {
                throw new ApplicationException("You must provide a ZIP Code and/or City/State combination. Calling Google Geocoder");
            }

            var zipCodes = result.ZipCodes[0];

            if (zipCodes == null)
            {
                return null;
            }

            GeoPoint geoPoint = new GeoPoint();
            geoPoint.Latitude = zipCodes.Latitude;
            geoPoint.Longitude = zipCodes.Longitude;

            return geoPoint;
        }
    }
}
