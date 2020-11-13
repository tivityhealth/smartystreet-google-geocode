using Newtonsoft.Json;
using System;
using System.Net;

namespace SmartyStreetsGoogleGeocode
{
    public class GoogleGeocode
    {
        const String apiKey = "AIzaSyDafu_-z71kEUS5uuELV2mf252rMIgED54";

        public static GeoPoint callGoogleGeocoder(string address)
        {
            GeoPoint geoPoint = new GeoPoint();
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?key={1}&address={0}&sensor=true", Uri.EscapeDataString(address), apiKey);

            WebClient wc = new WebClient();

            string result = wc.DownloadString(requestUri);

            dynamic obj = JsonConvert.DeserializeObject<dynamic>(result);

            if (obj.status != "OK")
            {
                throw new ApplicationException("Could not get geocoding information from Google");
            }

            geoPoint.Latitude = obj.results[0].geometry.location.lat;
            geoPoint.Longitude = obj.results[0].geometry.location.lng;

            return geoPoint;
        }
    }
}
