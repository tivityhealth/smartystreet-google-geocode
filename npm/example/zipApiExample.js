const {SgGeocode} = require('sggeocode');

//create zip object with just zipcode
let options = {
    zipcode: '12345',
    webKey: 'your key'
}

SgGeocode.getLatLng(options).then((res) => {
    console.log(res)
});

//create zip object with city+state combination
let options = {
    city: 'Chandler',
    state: 'AZ',
    webKey: 'your key'
}

SgGeocode.getLatLng(options).then((res) => {
    console.log(res)
});

//create zip object with all three options (zipcode, city and state)

let options = {
    zipcode: '85286',
    city: 'Chandler',
    state: 'AZ',
    webKey: 'your key'
}

SgGeocode.getLatLng(options).then((res) => {
    console.log(res)
});