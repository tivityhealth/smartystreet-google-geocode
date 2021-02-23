using SmartyStreetsGoogleGeocode;
using System;
using Xunit;

namespace XUnitTestSmartyStreetsGoogleGeocode
{
    public class UnitTest1
    {
        [Fact]
        public void TestZipApiWithZipCodeReturnsResult()
        {
            string zip = "85225";
            GeocodeInput dObj = new GeocodeInput(zip, null, null);
            SgGeocoder sggeocoder = new SgGeocoder(new AuthOptions { 
                SmartyStreetsAuthId = Environment.GetEnvironmentVariable("SmartyStreets_AuthId"),
                SmartyStreetsAuthToken = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"),
                GoogleApiKey = Environment.GetEnvironmentVariable("Google_Api_Key")
            });
            GeoPoint gp = sggeocoder.GetLatLng(dObj);

            Assert.Equal("(33.31666" + "\u00B0" + "N,-111.83182" + "\u00B0" + "E)", gp.ToString());
        }

        [Fact]
        public void TestUsStreetApiReturnsResult()
        {
            //LaunchSettingsFixture.SetEnvVariable();
            string address = "155 E Frye Rd Chandler AZ";
            GeocodeInput dObj = new GeocodeInput(address);

            SgGeocoder sggeocoder = new SgGeocoder(new AuthOptions
            {
                SmartyStreetsAuthId = Environment.GetEnvironmentVariable("SmartyStreets_AuthId"),
                SmartyStreetsAuthToken = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"),
                GoogleApiKey = Environment.GetEnvironmentVariable("Google_Api_Key")
            });
            GeoPoint gp = sggeocoder.GetLatLng(dObj);

            Assert.Equal("(33.32371" + "\u00B0" + "N,-111.83018" + "\u00B0" + "E)", gp.ToString());
        }

        [Fact]
        public void TestGoogleGeocodeReturnsResult()
        {
            //LaunchSettingsFixture.SetEnvVariable();
            string add = "LA Fitness, Arizona Ave Chandler AZ";
            GeocodeInput dObj = new GeocodeInput(add);

            SgGeocoder sggeocoder = new SgGeocoder(new AuthOptions
            {
                SmartyStreetsAuthId = Environment.GetEnvironmentVariable("SmartyStreets_AuthId"),
                SmartyStreetsAuthToken = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"),
                GoogleApiKey = Environment.GetEnvironmentVariable("Google_Api_Key")
            });
            GeoPoint gp = sggeocoder.GetLatLng(dObj);

            Assert.Equal("(33.248528" + "\u00B0" + "N,-111.8381307" + "\u00B0" + "E)", gp.ToString());
        }

        [Fact]
        public void TestInvalidZipCodeThrowsException()
        {
            //LaunchSettingsFixture.SetEnvVariable();
            string zip = "abcde";
            GeocodeInput dObj = new GeocodeInput(zip, null, null);
            SgGeocoder sggeocoder = new SgGeocoder(new AuthOptions
            {
                SmartyStreetsAuthId = Environment.GetEnvironmentVariable("SmartyStreets_AuthId"),
                SmartyStreetsAuthToken = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"),
                GoogleApiKey = Environment.GetEnvironmentVariable("Google_Api_Key")
            });
            Exception ex = Assert.Throws<ApplicationException>(() => sggeocoder.GetLatLng(dObj));

            Assert.Equal("You must provide a ZIP Code and/or City/State combination. Calling Google Geocoder", ex.Message);
        }

        [Fact]
        public void TestZipApiWithCityStateReturnsResult()
        {
            //LaunchSettingsFixture.SetEnvVariable();
            string city = "Chandler";
            string state = "AZ";
            GeocodeInput dObj = new GeocodeInput(null, city, state);

            SgGeocoder sggeocoder = new SgGeocoder(new AuthOptions
            {
                SmartyStreetsAuthId = Environment.GetEnvironmentVariable("SmartyStreets_AuthId"),
                SmartyStreetsAuthToken = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"),
                GoogleApiKey = Environment.GetEnvironmentVariable("Google_Api_Key")
            });

            GeoPoint gp = sggeocoder.GetLatLng(dObj);

            Assert.Equal("(33.32212" + "\u00B0" + "N,-111.87374" + "\u00B0" + "E)", gp.ToString());
        }

        [Fact]
        public void TestZipApiWithStateOnlyThrowsException()
        {
            //LaunchSettingsFixture.SetEnvVariable();
            string state = "AZ";
            GeocodeInput dObj = new GeocodeInput(null, null, state);

            SgGeocoder sggeocoder = new SgGeocoder(new AuthOptions
            {
                SmartyStreetsAuthId = Environment.GetEnvironmentVariable("SmartyStreets_AuthId"),
                SmartyStreetsAuthToken = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"),
                GoogleApiKey = Environment.GetEnvironmentVariable("Google_Api_Key")
            });
            Exception ex = Assert.Throws<ArgumentNullException>(() => sggeocoder.GetLatLng(dObj));

            Assert.Equal("City/State cannot be null or empty (Parameter 'CityState')", ex.Message);
        }

        [Fact]
        public void TestZipApiWithCityOnlyThrowsException()
        {
            //LaunchSettingsFixture.SetEnvVariable();
            string city = "Chandler";
            GeocodeInput dObj = new GeocodeInput(null, city, null);

            SgGeocoder sggeocoder = new SgGeocoder(new AuthOptions
            {
                SmartyStreetsAuthId = Environment.GetEnvironmentVariable("SmartyStreets_AuthId"),
                SmartyStreetsAuthToken = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"),
                GoogleApiKey = Environment.GetEnvironmentVariable("Google_Api_Key")
            });
            Exception ex = Assert.Throws<ArgumentNullException>(() => sggeocoder.GetLatLng(dObj));

            Assert.Equal("City/State cannot be null or empty (Parameter 'CityState')", ex.Message);
        }

        [Fact]
        public void TestNullArguments()
        {
            GeocodeInput dObj = new GeocodeInput(null, null, null);

            SgGeocoder sggeocoder = new SgGeocoder(new AuthOptions
            {
                SmartyStreetsAuthId = Environment.GetEnvironmentVariable("SmartyStreets_AuthId"),
                SmartyStreetsAuthToken = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"),
                GoogleApiKey = Environment.GetEnvironmentVariable("Google_Api_Key")
            });
            Exception ex = Assert.Throws<ArgumentNullException>(() => sggeocoder.GetLatLng(dObj));

            Assert.Equal("Arguments cannot be null or empty (Parameter 'GeocodeInput')", ex.Message);
        }

        [Fact]
        public void TestEmptyArguments()
        {
            GeocodeInput dObj = new GeocodeInput("");

            SgGeocoder sggeocoder = new SgGeocoder(new AuthOptions
            {
                SmartyStreetsAuthId = Environment.GetEnvironmentVariable("SmartyStreets_AuthId"),
                SmartyStreetsAuthToken = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"),
                GoogleApiKey = Environment.GetEnvironmentVariable("Google_Api_Key")
            });
            Exception ex = Assert.Throws<ArgumentNullException>(() => sggeocoder.GetLatLng(dObj));

            Assert.Equal("Arguments cannot be null or empty (Parameter 'GeocodeInput')", ex.Message);
        }
    }
}
