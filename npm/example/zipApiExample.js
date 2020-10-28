import {sgGeocode} from '../src/sgGeocode';

//passing zipcode to get lat long
    //creating zipcode object
    let zipObject = {
        zipcode: '85225'
    }

    //calling the library with zip code
    sgGeocode(zipObject).then((res) => {
        //response returns lat long object
        console.log('Result', res)
    })

//passing City/State to get lat long
    //creating city/state object
    let cityStateObject = {
        city: 'Chandler',
        state: 'AZ'
    }

    //calling the library with zip code
    sgGeocode(cityStateObject).then((res) => {
        //response returns lat long object
        console.log('Result', res)
    })

