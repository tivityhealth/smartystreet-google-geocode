const {SgGeocode} = require('sggeocode');

//declare your keys
let keys = {
    webKey: 'your key here',
    googleApiKey: 'your google api key here'
}

//create zip object with just zipcode
let options1 = {
    zipcode: '12345'
}

SgGeocode.getLatLng(options1, keys).then((res) => {
    console.log(res)
});

//create zip object with city+state combination
let options2 = {
    city: 'Chandler',
    state: 'AZ'
}

SgGeocode.getLatLng(options2, keys).then((res) => {
    console.log(res)
});

//create zip object with all three options (zipcode, city and state)

let options3 = {
    zipcode: '85286',
    city: 'Chandler',
    state: 'AZ'
}

SgGeocode.getLatLng(options3, keys).then((res) => {
    console.log(res)
});