const {SgGeocode} = require('sgGeocode');

//declare your keys
let keys = {
    webKey: 'your key here',
    googleApiKey: 'your google api key here'
}

//create address object with complete address (fullAddress)
let options1 = {
    fullAddress: '123 W Chandler Blvd Chandler AZ',
}

SgGeocode.getLatLng(options1, keys).then((res) => {
    console.log(res)
});


/**
 * When smartystreet fails to return lat long, the call is redirected to google geocoder
 * eg: 1448 S Spectrum Blvd Chandler AZ
*/
let options2 = {
    fullAddress: '1448 S Spectrum Blvd Chandler AZ',
    webKey: 'your key'
}

SgGeocode.getLatLng(options2, keys).then((res) => {
    console.log(res)
})