# What is this?

Get Lat Long from SmartyStreets and Google Geocoder

# Installation

`npm install sggeocode --save`

Then to use it

```
const {sgGeocode} = require('sgGeocode');

sgGeocode({
    fullAddress: '155 E Frye Rd Chandler AZ',
    webKey: '1234567890'
    }).then((res) => {
        console.log(res)
});

//OR

sgGeocode({
    zipcode: '12345',
    webKey: '1234567890'
    }).then((res) => {
        console.log(res)
});
```

## Options

- _fullAddress_ - _Complete Address_
- _zipcode_ - _5 digit zip code_
- _city_ - _City name_
- _state_ - _State code_
