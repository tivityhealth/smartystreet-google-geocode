const {SgGeocode} = require('sgGeocode');

//create address object with complete address (fullAddress)
let options = {
    fullAddress: '123 W Chandler Blvd Chandler AZ',
    webKey: 'your key'
}

SgGeocode.getLatLng(options).then((res) => {
    console.log(res)
});


/**
 * When smartystreet fails to return lat long, the call is redirected to google geocoder
 * eg: 1448 S Spectrum Blvd Chandler AZ
*/
let options = {
    fullAddress: '1448 S Spectrum Blvd Chandler AZ',
    webKey: 'your key'
}

SgGeocode.getLatLng(options).then((res) => {
    console.log(res)
})