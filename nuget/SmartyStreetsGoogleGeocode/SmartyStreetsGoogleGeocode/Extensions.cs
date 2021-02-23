using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public static class CommonApiServiceCollectionExtensions
    {
        public static void AddTivityGeocoder(this IServiceCollection services, AuthOptions options)
        {
            Validations.Required("SmartyStreets_AuthId", options.SmartyStreetsAuthId);
            Validations.Required("SmartyStreets_AuthToken", options.SmartyStreetsAuthToken);
            Validations.Required("Google_Api_Key", options.GoogleApiKey);

            services.AddSingleton<ISgGeocoder>(f => new SgGeocoder(options));
        }
    }
}
