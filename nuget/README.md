# What is this?

Get Lat Lng from SmartyStreets and Google Geocoder

# Installation

Intall package using command line
`dotnet add package SmartyStreetsGoogleGeocode`

Or you can search for `SmartyStreetsGoogleGeocode` in the NuGet package manager

## Configurations

Add your keys accordingly to the environment variable;
`SmartyStreets_AuthToken`,
`SmartyStreets_AuthId`,
`Google_Api_Key`

Configure the application by adding the service (in ConfigureServices())

```
services.AddTivityGeocoder();
```

Configure the reuqest pipeline by adding the middleware service (in Configure())

```
services.UseTivityGeocoderService();
```

## Input

- _address_ - _Complete/Partial Address_
- _zipcode_ - _5 digit zip code_
- _city_ - _City name_
- _state_ - _State code_

You can pass in 2 types of objects

1. Address Object (complete or partial address)

```
GeocodeInput input = new GeocodeInput(string address)
```

`null` input will throw an excetion

2. Zip object ('zipcode', or combination of 'city' and 'state', or all three of them)

```
GeocodeInput input = new GeocodeInput(string zipcode, string city, string state)
```

Here you can have just `zipcode` as input,
OR just a combination of `city` & `state`
OR a combination of all three `zipcode`, `city` & `state`

## Result

- After a successful call you should receive a response object GeoPoint

```
GeoPoint result = new GeoPoint(double latitude, double longitude)
```

- ToString() returns result in the format

```
"<Lat>°N,<Lng>°E"
```

## Example

```
GeocodeInput geocodeInput = new GeocodeInput("123 E Chandler Blvd, Chandler AZ");
GeoPoint result = new GeoPoint();
result = SgGeocoder.CallSgGeocoder(geocodeInput);
```
