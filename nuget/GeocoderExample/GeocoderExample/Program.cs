using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SmartyStreetsGoogleGeocode;
using System;

namespace GeocoderExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Configure service defaults
            CreateHostBuilder(args).Build().Run();

            //Call SmartyStreets-Google service
            GeocodeInput geocodeInput = new GeocodeInput("85225", null, null);
            GeoPoint result = new GeoPoint();            

            result = SgGeocoder.CallSgGeocoder(geocodeInput);

            Console.WriteLine(result.ToString());

        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
