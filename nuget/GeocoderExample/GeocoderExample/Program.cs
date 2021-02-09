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
            GeocodeInput geocodeInput = new GeocodeInput("85225", null, null);
            GeoPoint gp = new GeoPoint();

            CreateHostBuilder(args).Build().Run();

            gp = SgGeocoder.CallSgGeocoder(geocodeInput);

            Console.WriteLine(gp.ToString());

        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
