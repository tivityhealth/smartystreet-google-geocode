# What is this?

Get Lat Long from SmartyStreets and Google Geocoder

# Installation

`npm install sggeocode --save`

Then to use it

```
const {sgGeocode} = require('sgGeocode');

sgGeocode(options).then((res) => {
    console.log(res)
});
```

## Options

- _fullAddress_ - _Complete Address_
- _zipcode_ - _5 digit zip code_
- _city_ - _City name_
- _state_ - _State code_

You can pass in 3 types of objects
1. Address Object (fullAddress)
```
{
    fullAddress: '123 W Chandler Blvd Chandler AZ',
    webKey: '1234567890'
}
```

2. Zip object ('zipcode', or combination of 'city' and 'state', or all three of them)
```
{
    zipcode: '12345',
    webKey: '1234567890'
}

//OR a combination of City-State

{
    city: 'Chandler',
    state: 'AZ'
    webKey: '1234567890'
}

//OR all three of them
{
    zipcode: '12345'
    city: 'Chandler',
    state: 'AZ'
    webKey: '1234567890'
}
```

3. Google autocomplete object ('prediction'), which looks like this
```
{
    //name the autocomplete object as 'prediction'
    prediction: {
        "description" : "Chandler, AZ 85225, USA",
        "matched_substrings" : [
           {
              "length" : 5,
              "offset" : 13
           }
        ],
        "terms" : [
           {
              "offset" : 0,
              "value" : "Chandler"
           },
           {
              "offset" : 10,
              "value" : "AZ"
           },
           {
              "offset" : 13,
              "value" : "85225"
           },
           {
              "offset" : 20,
              "value" : "USA"
           }
        ],
        "types" : [ "postal_code", "geocode" ]
     },
     webKey: SmartyStreetsKey 
}
```

## Result

- After a successful call you should receive a response oject like
```
{
    lat: 32.12345,
    lng: -111.98765
}
```

- Unsuccessful calls should receive a response object as,
```
{
    error: "error message"
}
```
