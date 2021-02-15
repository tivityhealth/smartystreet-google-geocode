# Introduction

Get Lat Lng from SmartyStreets and Google Geocoder

# Installation

Intall package using command line
```#!/bin/bash
dotnet add package SmartyStreetsGoogleGeocode
```

Or you can search for `SmartyStreetsGoogleGeocode` in the NuGet package manager

## Configurations

Add your keys accordingly to the environment variable;
`SmartyStreets_AuthToken`,
`SmartyStreets_AuthId`,
`Google_Api_Key`

Configure the application by adding the service and middleware

```csharp
services.AddTivityGeocoder();

app.UseTivityGeocoderService();
```

### Configuration Example
After adding the services in your Startup class, it should look like this,
```csharp
public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTivityGeocoder();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseTivityGeocoderService();
        }
    }
```

## Input

- _address_ - _Complete/Partial Address_
- _zipcode_ - _5 digit zip code_
- _city_ - _City name_
- _state_ - _State code_

You can pass in 2 types of objects

1. Address Object (complete or partial address)

```csharp
GeocodeInput input = new GeocodeInput(string address)
```

`null` input will throw an excetion

2. Zip object ('zipcode', or combination of 'city' and 'state', or all three of them)

```csharp
GeocodeInput input = new GeocodeInput(string zipcode, string city, string state)
```

Here you can have just `zipcode` as input,
OR just a combination of `city` & `state`
OR a combination of all three `zipcode`, `city` & `state`

## Result

- After a successful call you should receive a response object GeoPoint

```csharp
GeoPoint result = new GeoPoint(double latitude, double longitude)
```

- ToString() returns result in the format

```csharp
"<Lat>°N,<Lng>°E"
```

## Example

```csharp
static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            GeocodeInput geocodeInput = new GeocodeInput("123 W Chandler Blvd Chandler AZ");
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
```
