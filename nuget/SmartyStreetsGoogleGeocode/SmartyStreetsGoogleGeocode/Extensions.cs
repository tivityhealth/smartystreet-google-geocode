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
            services.AddTransient<SgGeocoder>();
        }
    }
}
