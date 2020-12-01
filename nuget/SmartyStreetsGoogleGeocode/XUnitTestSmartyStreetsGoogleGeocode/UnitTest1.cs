using SmartyStreets;
using SmartyStreetsGoogleGeocode;
using System;
using System.Net;
using Xunit;

namespace XUnitTestSmartyStreetsGoogleGeocode
{
    public class UnitTest1
    {
        //Name tests
        const string auth_id = "a4e58b8c-6a2c-de84-3c6e-4e7b130f7134";
        const string auth_token = "LdPmUE3hz0MNZBMgAJIV";
        [Fact]
        public void TestZipApiWithZipCodeReturnsResult()
        {
            string zip = "85225";
            GeocodeInput dObj = new GeocodeInput(auth_id, auth_token, zip, null, null);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.31666°N,-111.83182°E)", gp.ToString());
        }

        [Fact]
        public void TestUsStreetApiReturnsResult()
        {
            string address = "155 E Frye Rd Chandler AZ";
            GeocodeInput dObj = new GeocodeInput(auth_id, auth_token, address);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.32371°N,-111.83018°E)", gp.ToString());
        }

        [Fact]
        public void TestGoogleGeocodeReturnsResult()
        {
            string add = "LA Fitness, Arizona Ave Chandler AZ";
            GeocodeInput dObj = new GeocodeInput(auth_id, auth_token, add);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.248528°N,-111.8381307°E)", gp.ToString());
        }

        [Fact]
        public void TestInvalidZipCodeThrowsException()
        {
            //Should call google or throw exception
            string zip = "abcde";
            GeocodeInput dObj = new GeocodeInput(auth_id, auth_token, zip, null, null);

            Exception ex = Assert.Throws<ApplicationException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("You must provide a ZIP Code and/or City/State combination. Calling Google Geocoder", ex.Message);
        }

        [Fact]
        public void TestInvalidKeyThrowsException()
        {
            string invalid_auth_id = "abcdef12345";
            string zip = "85225";
            GeocodeInput dObj = new GeocodeInput(invalid_auth_id, auth_token, zip, null, null);

            Exception ex = Assert.Throws<BadCredentialsException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("Unauthorized: The credentials were provided incorrectly or did not match any existing, active credentials.", ex.Message);
        }

        [Fact]
        public void TestZipApiWithCityStateReturnsResult()
        {
            string city = "Chandler";
            string state = "AZ";
            GeocodeInput dObj = new GeocodeInput(auth_id, auth_token, null, city, state);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.32212°N,-111.87374°E)", gp.ToString());
        }

        [Fact]
        public void TestZipApiWithStateOnlyThrowsException()
        {
            string state = "AZ";
            GeocodeInput dObj = new GeocodeInput(auth_id, auth_token, null, null, state);

            Exception ex = Assert.Throws<ArgumentNullException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("City/State cannot be null or empty (Parameter 'CityState')", ex.Message);
        }

        [Fact]
        public void TestZipApiWithCityOnlyThrowsException()
        {
            string city = "Chandler";
            GeocodeInput dObj = new GeocodeInput(auth_id, auth_token, null, city, null);

            Exception ex = Assert.Throws<ArgumentNullException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("City/State cannot be null or empty (Parameter 'CityState')", ex.Message);
        }

        [Fact]
        public void TestNullArguments()
        {
            GeocodeInput dObj = new GeocodeInput(auth_id, auth_token, null, null, null);

            Exception ex = Assert.Throws<ArgumentNullException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("Arguments cannot be null or empty (Parameter 'GeocodeInput')", ex.Message);
        }

        [Fact]
        public void TestEmptyArguments()
        {
            GeocodeInput dObj = new GeocodeInput(auth_id, auth_token, "");

            Exception ex = Assert.Throws<ArgumentNullException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("Arguments cannot be null or empty (Parameter 'GeocodeInput')", ex.Message);
        }
    }
}
