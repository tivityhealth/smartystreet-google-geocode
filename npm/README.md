# Introduction
Get Lat Lng from SmartyStreets and Google Geocoder

## Installation
```#!/bin/bash
npm install sggeocode
```

## Example
```jsx
const {SgGeocode} = require('sgGeocode');

let keys = {
    webKey: 'your key here',
    googleApiKey: 'your google api key here'
}

let options = {
    fullAddress: '123 W Chandler Blvd Chandler AZ',
}

SgGeocode.getLatLng(options, keys).then((res) => {
    console.log(res)
});
```

## Parameter
You pass in the following parameters in the keys object
- _webKey_ - _SmartyStreets web key_
- _googleApiKey_ - _Google Places API key_

You can pass in the following parameters in the options object
- _fullAddress_ - _Complete Address_
- _zipcode_ - _5 digit zip code_
- _city_ - _City name_
- _state_ - _State code_

The Lookup object can be constructed in 3 different ways
1. Lookup by Address (fullAddress)
```jsx
let options = {
    fullAddress: '123 W Chandler Blvd Chandler AZ'
}
```

2. Lookup by Zip ('zipcode', or combination of 'city' and 'state', or all three of them)
(Google API key is optional when constructing zip object) 
```jsx
let options = {
    zipcode: '12345'
}

//OR a combination of City-State

let options = {
    city: 'Chandler',
    state: 'AZ'
}

//OR all three of them
let options = {
    zipcode: '12345'
    city: 'Chandler',
    state: 'AZ'
}
```

3. Lookup by Google autocomplete object ('prediction'),
```jsx
let options = {
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
     }
}
```

## Result
- After a successful call you should receive a response oject like
```jsx
{
    lat: 32.12345,
    lng: -111.98765
}
```

- When no results are found, response object should look like this
```jsx
{
    status: 'ZERO_RESULTS'
}
```

- Unsuccessful calls should receive a response object as,
```jsx
{
    error: "error message"
}
```
