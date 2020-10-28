import {sgGeocode} from '../src/sgGeocode';

//passing address to get lat long
    //creating address object
    let addrObject = {
        address: '1050 S Terrace Rd Tempe AZ'
    }

    //calling the library with zip code
    sgGeocode(addrObject).then((res) => {
        //response returns lat long object
        console.log('Result', res)
    })

//When smartystreet fails to return lat long, the call is redirected to google geocoder
    //eg: 1448 S Spectrum Blvd Chandler AZ
    let businessObject = {
        address: '1448 S Spectrum Blvd Chandler AZ'
    }

    sgGeocode(businessObject).then((res) => {
        //response returns lat long object
        console.log('Result', res)
    })