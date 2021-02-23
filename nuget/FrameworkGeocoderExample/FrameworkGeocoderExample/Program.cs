using System;
using System.Configuration;
using SmartyStreetsGoogleGeocode;

namespace FrameworkGeocoderExample
{
    class Program
    {
        static void Main(string[] args)
        {
            GeocodeInput geocodeInput = new GeocodeInput("85225", null, null);

            SgGeocoder sgGeocoder = new SgGeocoder(new AuthOptions
            {
                SmartyStreetsAuthId = ConfigurationManager.AppSettings.Get("SmartyStreets_AuthId"),
                SmartyStreetsAuthToken = ConfigurationManager.AppSettings.Get("SmartyStreets_AuthToken"),
                GoogleApiKey = ConfigurationManager.AppSettings.Get("Google_Api_Key"),
            });
            GeoPoint result = sgGeocoder.GetLatLng(geocodeInput);

            Console.WriteLine(result);
        }
    }
}
