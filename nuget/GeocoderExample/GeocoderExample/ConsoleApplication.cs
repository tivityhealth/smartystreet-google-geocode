using SmartyStreetsGoogleGeocode;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeocoderExample
{
    public class ConsoleApplication
    {
        private readonly ISgGeocoder _sgGeocoder;
        public ConsoleApplication(ISgGeocoder sgGeocoder)
        {
            _sgGeocoder = sgGeocoder;
        }

        // Application starting point
        public void Run()
        {
            //All the logic here
            GeocodeInput geoInput = new GeocodeInput("85225", null, null);
            var result = _sgGeocoder.CallSgGeocoder(geoInput);
            Console.WriteLine(result);
        }
    }
}
