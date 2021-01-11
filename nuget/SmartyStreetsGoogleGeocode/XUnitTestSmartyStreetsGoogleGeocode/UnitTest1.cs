using Newtonsoft.Json;
using SmartyStreets;
using SmartyStreetsGoogleGeocode;
using System;
using System.IO;
using System.Net;
using Xunit;

namespace XUnitTestSmartyStreetsGoogleGeocode
{
    public class UnitTest1
    {
        [Fact]
        public void TestZipApiWithZipCodeReturnsResult()
        {
            Environment.SetEnvironmentVariable("SmartyStreets_AuthId", Environment.GetEnvironmentVariable("SmartyStreets_AuthId"));
            Environment.SetEnvironmentVariable("SmartyStreets_AuthToken", Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"));
            string zip = "85225";            
            GeocodeInput dObj = new GeocodeInput(zip, null, null);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.31666"+ "\u00B0" + "N,-111.83182" + "\u00B0" + "E)", gp.ToString());
        }

        [Fact]
        public void TestUsStreetApiReturnsResult()
        {
            Environment.SetEnvironmentVariable("SmartyStreets_AuthId", Environment.GetEnvironmentVariable("SmartyStreets_AuthId"));
            Environment.SetEnvironmentVariable("SmartyStreets_AuthToken", Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"));
            string address = "155 E Frye Rd Chandler AZ";
            GeocodeInput dObj = new GeocodeInput(address);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.32371"+ "\u00B0" + "N,-111.83018"+ "\u00B0" + "E)", gp.ToString());
        }

        [Fact]
        public void TestGoogleGeocodeReturnsResult()
        {
            Environment.SetEnvironmentVariable("SmartyStreets_AuthId", Environment.GetEnvironmentVariable("SmartyStreets_AuthId"));
            Environment.SetEnvironmentVariable("SmartyStreets_AuthToken", Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"));
            Environment.SetEnvironmentVariable("Google_Api_Key", Environment.GetEnvironmentVariable("Google_Api_Key"));
            string add = "LA Fitness, Arizona Ave Chandler AZ";
            GeocodeInput dObj = new GeocodeInput(add);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.248528"+ "\u00B0" + "N,-111.8381307"+ "\u00B0" + "E)", gp.ToString());
        }

        [Fact]
        public void TestInvalidZipCodeThrowsException()
        {
            Environment.SetEnvironmentVariable("SmartyStreets_AuthId", Environment.GetEnvironmentVariable("SmartyStreets_AuthId"));
            Environment.SetEnvironmentVariable("SmartyStreets_AuthToken", Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"));
            string zip = "abcde";
            GeocodeInput dObj = new GeocodeInput(zip, null, null);

            Exception ex = Assert.Throws<ApplicationException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("You must provide a ZIP Code and/or City/State combination. Calling Google Geocoder", ex.Message);
        }

        [Fact]
        public void TestInvalidKeyThrowsException()
        {
            Environment.SetEnvironmentVariable("SmartyStreets_AuthId", "1234567890");
            Environment.SetEnvironmentVariable("SmartyStreets_AuthToken", "1234567890");
            string zip = "85225";
            GeocodeInput dObj = new GeocodeInput(zip, null, null);

            Exception ex = Assert.Throws<BadCredentialsException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("Unauthorized: The credentials were provided incorrectly or did not match any existing, active credentials.", ex.Message);
        }

        [Fact]
        public void TestZipApiWithCityStateReturnsResult()
        {
            Environment.SetEnvironmentVariable("SmartyStreets_AuthId", Environment.GetEnvironmentVariable("SmartyStreets_AuthId"));
            Environment.SetEnvironmentVariable("SmartyStreets_AuthToken", Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"));
            string city = "Chandler";
            string state = "AZ";
            GeocodeInput dObj = new GeocodeInput(null, city, state);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.32212"+ "\u00B0" + "N,-111.87374"+ "\u00B0" + "E)", gp.ToString());
        }

        [Fact]
        public void TestZipApiWithStateOnlyThrowsException()
        {
            Environment.SetEnvironmentVariable("SmartyStreets_AuthId", Environment.GetEnvironmentVariable("SmartyStreets_AuthId"));
            Environment.SetEnvironmentVariable("SmartyStreets_AuthToken", Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"));
            string state = "AZ";
            GeocodeInput dObj = new GeocodeInput(null, null, state);

            Exception ex = Assert.Throws<ArgumentNullException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("City/State cannot be null or empty (Parameter 'CityState')", ex.Message);
        }

        [Fact]
        public void TestZipApiWithCityOnlyThrowsException()
        {
            Environment.SetEnvironmentVariable("SmartyStreets_AuthId", Environment.GetEnvironmentVariable("SmartyStreets_AuthId"));
            Environment.SetEnvironmentVariable("SmartyStreets_AuthToken", Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"));
            string city = "Chandler";
            GeocodeInput dObj = new GeocodeInput(null, city, null);

            Exception ex = Assert.Throws<ArgumentNullException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("City/State cannot be null or empty (Parameter 'CityState')", ex.Message);
        }

        [Fact]
        public void TestNullArguments()
        {
            GeocodeInput dObj = new GeocodeInput(null, null, null);

            Exception ex = Assert.Throws<ArgumentNullException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("Arguments cannot be null or empty (Parameter 'GeocodeInput')", ex.Message);
        }

        [Fact]
        public void TestEmptyArguments()
        {
            GeocodeInput dObj = new GeocodeInput("");

            Exception ex = Assert.Throws<ArgumentNullException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("Arguments cannot be null or empty (Parameter 'GeocodeInput')", ex.Message);
        }
    }
}
