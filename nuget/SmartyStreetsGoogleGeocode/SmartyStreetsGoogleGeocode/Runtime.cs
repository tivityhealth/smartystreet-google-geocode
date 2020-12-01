using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public static class Runtime
    {
        public static String SmartyStreetsAuthId { get; } = Environment.GetEnvironmentVariable("SmartyStreets_AuthId");
        public static String SmartyStreetsAuthToken { get; } = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken");
        public static String GoogleApiKey { get; } = Environment.GetEnvironmentVariable("Google_Api_Key");
    }
}
