using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public static class CommonApiServiceCollectionExtensions
    {
        public static void AddTivityGeocoder(this IServiceCollection services)
        {
            Validations.Required("SmartyStreets_AuthId", Runtime.SmartyStreetsAuthId);
            Validations.Required("SmartyStreets_AuthToken", Runtime.SmartyStreetsAuthToken);
            Validations.Required("Google_Api_Key", Runtime.GoogleApiKey);

            services.AddSingleton<ISgGeocoder, SgGeocoder>();
        }
    }
}
