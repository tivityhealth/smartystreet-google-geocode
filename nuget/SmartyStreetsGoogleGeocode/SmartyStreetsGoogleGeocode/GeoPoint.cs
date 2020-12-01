using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public class GeoPoint
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public GeoPoint()
        {
            Latitude = 0;
            Longitude = 0;
        }

        public GeoPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return String.Format("({0}°N,{1}°E)", Latitude, Longitude);
        }
        
    }
}
