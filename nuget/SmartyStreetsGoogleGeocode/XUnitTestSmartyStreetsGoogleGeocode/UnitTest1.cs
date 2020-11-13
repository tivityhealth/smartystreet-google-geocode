using SmartyStreets;
using SmartyStreetsGoogleGeocode;
using System;
using Xunit;

namespace XUnitTestSmartyStreetsGoogleGeocode
{
    public class UnitTest1
    {
        const string auth_id = "a4e58b8c-6a2c-de84-3c6e-4e7b130f7134";
        const string auth_token = "LdPmUE3hz0MNZBMgAJIV";
        [Fact]
        public void TestZip()
        {
            string zip = "85225";
            AddressObject dObj = new AddressObject(auth_id, auth_token, zip, null, null);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.31666°N,-111.83182°E)", gp.ToString());
        }

        [Fact]
        public void TestUsStreet()
        {
            string address = "155 E Frye Rd Chandler AZ";
            AddressObject dObj = new AddressObject(auth_id, auth_token, address);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.32371°N,-111.83018°E)", gp.ToString());
        }

        [Fact]
        public void TestGoogleGeocode()
        {
            string add = "1448 S Spectrum Blvd Chandler AZ";
            AddressObject dObj = new AddressObject(auth_id, auth_token, add);

            GeoPoint gp = SgGeocoder.CallSgGeocoder(dObj);

            Assert.Equal("(33.2846566°N,-111.8873966°E)", gp.ToString());
        }

        [Fact]
        public void TestInvalidZipCode()
        {
            string zip = "abcde";
            AddressObject dObj = new AddressObject(auth_id, auth_token, zip, null, null);

            Exception ex = Assert.Throws<ApplicationException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("Blank lookup (you must provide a ZIP Code and/or City/State combination).", ex.Message);
        }

        [Fact]
        public void TestInvalidKey()
        {
            string invalid_auth_id = "abcdef12345";
            string zip = "85225";
            AddressObject dObj = new AddressObject(invalid_auth_id, auth_token, zip, null, null);

            Exception ex = Assert.Throws<BadCredentialsException>(() => SgGeocoder.CallSgGeocoder(dObj));

            Assert.Equal("Unauthorized: The credentials were provided incorrectly or did not match any existing, active credentials.", ex.Message);
        }
    }
}
