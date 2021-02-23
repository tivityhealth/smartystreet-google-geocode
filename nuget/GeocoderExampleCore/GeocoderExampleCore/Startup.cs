using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using SmartyStreetsGoogleGeocode;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeocoderExampleCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Add Geocoder services
            services.AddTivityGeocoder(new AuthOptions
            {
                SmartyStreetsAuthId = "your auth id", //ConfigurationManager.AppSettings.Get("SmartyStreets_AuthId");
                SmartyStreetsAuthToken = "your auth token", //OR
                GoogleApiKey = "your google key" //Environment.GetEnvironmentVariables("Google_Api_Key");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Geocode}/{action=Index}/{id?}");
            });
        }
    }
}
