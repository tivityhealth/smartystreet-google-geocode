using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public static class CoreApiBuilderExtensions
    {
        public static IApplicationBuilder UseTivityGeocoderService(this IApplicationBuilder app)
        {
            Validations.Required("SmartyStreets_AuthId", Runtime.SmartyStreetsAuthId);
            Validations.Required("SmartyStreets_AuthToken", Runtime.SmartyStreetsAuthToken);
            Validations.Required("Google_Api_Key", Runtime.GoogleApiKey);

            return app;
        }
    }
}