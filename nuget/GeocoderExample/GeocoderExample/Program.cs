using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartyStreetsGoogleGeocode;
using System;
using System.Configuration;

namespace GeocoderExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection and configure our services
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<ConsoleApplication>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTivityGeocoder(new AuthOptions { 
                SmartyStreetsAuthId = "your auth id",           //ConfigurationManager.AppSettings.Get("SmartyStreets_AuthId");
                SmartyStreetsAuthToken = "your auth token",     //OR
                GoogleApiKey = "your google api key"            //Environment.GetEnvironmentVariables("Google_Api_Key");
            });
            services.AddTransient<ConsoleApplication>();
            return services;
        }
    }
}
