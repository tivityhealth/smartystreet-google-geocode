# What is this?

Get Lat Long from SmartyStreets and Google Geocoder

# Installation

`npm i sggeocode --save`

Then...

```
import { sgGeocode } from 'sggeocode';

sgGeocode({
    address: 'someAddress',
});

//OR

sgGeocode({
    zipcode: 'XXXXX',
    city: 'Lorem',
    state: 'XY'
});
```

## Options

- _address_ - _Complete Address_
- _zipcode_ - _5 digit zip code_
- _city_ - _City name_
- _state_ - _State code_
