# Introduction

Get Lat Lng from SmartyStreets and Google Geocoder

## Installation

Intall package using command line
```#!/bin/bash
dotnet add package SmartyStreetsGoogleGeocode
```

Or you can search for `SmartyStreetsGoogleGeocode` in the NuGet package manager

# Usage for .net core

## Configuration

1. Add your keys with the following name to the environment variable;
`SmartyStreets_AuthToken`,
`SmartyStreets_AuthId`,
`Google_Api_Key`
> **NOTE:** You can find Environment variables in your project properties, under the `Debug` section

2. Configure the application by adding the geocoder service. Your `ConfigureServices` method should look like,

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTivityGeocoder(new AuthOptions
    {
        SmartyStreetsAuthId = Environment.GetEnvironmentVariable("SmartyStreets_AuthId"),
        SmartyStreetsAuthToken = Environment.GetEnvironmentVariable("SmartyStreets_AuthToken"),
        GoogleApiKey = Environment.GetEnvironmentVariable("Google_Api_Key")
    });
}
```
> **NOTE:** include `using SmartyStreetsGoogleGeocode;` at the top

## Example

We have our services set up to easily inject instances of ISgGeocoder by adding `AddTivityGeocoder` service. Now we have to setup our class for proper injection

```csharp
class GeocoderExample
    {
        private readonly ISgGeocoder _sgGeocoder;
        public ConsoleApplication(ISgGeocoder sgGeocoder)
        {
            _sgGeocoder = sgGeocoder;
        }
        //You can define this method as per your needs
        public void Run()
        {
            GeocodeInput geoInput = new GeocodeInput("85225", null, null);
            var result = _sgGeocoder.CallSgGeocoder(geoInput);
            Console.WriteLine(result);
        }
    }
```
> **NOTE:** include `using SmartyStreetsGoogleGeocode;` at the top

# Usage for .net framework

## Configuration

Add the keys and values to your `App.config`. It should be looking like this;

```xml
<appSettings>  
  <add key="SmartyStreets_AuthId" value="your auth id"/>
  <add key="SmartyStreets_AuthToken" value="your auth token"/>
  <add key="Google_Api_Key" value="your google api key"/>
</appSettings>
```

## Example

You can directly create instance of SgGeocode and pass in the auth keys and GeocodeInput wihtout any dependency injection

```csharp
class GeocoderFrameworkExample{
    static void Main()
    {
        GeocodeInput geocodeInput = new GeocodeInput("85225", null, null);

        SgGeocoder sgGeocoder = new SgGeocoder(new AuthOptions
        {
            SmartyStreetsAuthId = ConfigurationManager.AppSettings.Get("SmartyStreets_AuthId"),
            SmartyStreetsAuthToken = ConfigurationManager.AppSettings.Get("SmartyStreets_AuthToken"),
            GoogleApiKey = ConfigurationManager.AppSettings.Get("Google_Api_Key"),
        });
        GeoPoint result = sgGeocoder.GetLatLng(geocodeInput);

        Console.WriteLine(result);
    }
}
```

# Input

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
