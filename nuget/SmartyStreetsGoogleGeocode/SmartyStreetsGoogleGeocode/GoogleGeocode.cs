using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;

namespace SmartyStreetsGoogleGeocode
{
    public class GoogleGeocode
    {
        public static GeoPoint callGoogleGeocoder(GeocodeInput options, AuthOptions authOptions)
        {
            GeoPoint geoPoint = new GeoPoint();
            string apiKey = authOptions.GoogleApiKey;
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?key={1}&address={0}&sensor=true", Uri.EscapeDataString(options.address), apiKey);

            WebClient wc = new WebClient();

            try
            {
                string result = wc.DownloadString(requestUri);
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(result);

                if (obj.status != "OK")
                {
                    throw new ApplicationException(obj.error_message.ToString());
                }

                geoPoint.Latitude = obj.results[0].geometry.location.lat;
                geoPoint.Longitude = obj.results[0].geometry.location.lng;

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return geoPoint;
        }
    }
}
