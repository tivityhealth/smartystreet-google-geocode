const {SgGeocode} = require('../src/sgGeocode')

let SmartyStreetsKey = '8945712748934021'
let GoogleApiKey = 'AIzaSyBiwguft-a0AmS7kCh0nJ9RMh_rrc7e-oY'

let keys = {
    webKey: SmartyStreetsKey,
    googleApiKey: GoogleApiKey,
}

let invalidKeys = {
    webKey: '123456789',
    googleApiKey: '123456789',
}

let obj1 = {
    fullAddress: '155 E Frye Rd Chandler AZ'
}
let obj2 = {
    zipcode: '85225'
}
let obj3 = {
    zipcode: '1234'
}
let obj4 = {
    zipcode: 'abcd'
}

let obj5 = {
    zipcode: '85225'
}

let obj6 = {
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

let obj7 = {
    prediction: {
        "description" : "Tempe, AZ, USA",
        "terms" : [
           {
              "offset" : 0,
              "value" : "Tempe"
           },
           {
              "offset" : 7,
              "value" : "AZ"
           },
           {
              "offset" : 11,
              "value" : "USA"
           }
        ],
        "types" : [ "locality", "political", "geocode" ]
     }
}

let obj8 = {
    prediction: {
        "description" : "155 East Frye Road, Chandler, AZ, USA",
        "terms" : [
           {
              "offset" : 0,
              "value" : "155"
           },
           {
              "offset" : 4,
              "value" : "East Frye Road"
           },
           {
              "offset" : 20,
              "value" : "Chandler"
           },
           {
              "offset" : 30,
              "value" : "AZ"
           },
           {
              "offset" : 34,
              "value" : "USA"
           }
        ],
        "types" : [ "premise", "geocode" ]
     }
}

let obj9 = {
    fullAddress: 'LA Fitness, S Arizona Avenue, Chandler AZ'
}

let obj10 = {
    address: '123',
    key: '123'
}

let obj11 = {
    fullAddress: 'sdflkjsdfablsdkfjsdfdsfl'
}

test('test usStreetAPI', () => {
    return SgGeocode.getLatLng(obj1, keys).then(res => {
        expect(res.lat).toBe(33.32371)
        expect(res.lng).toBe(-111.83018)
    })
});

test('test zipAPI', () => {
    return SgGeocode.getLatLng(obj2, keys).then(res => {
        expect(res.lat).toBe(33.31666)
        expect(res.lng).toBe(-111.83182)
    })
});

it('test invalid zipcode', async () => {
    try {
        await SgGeocode.getLatLng(obj3, keys);
    } catch (e) {
        expect(e.message).toBe('Invalid ZIP Code.');
    }
});

it('Blank lookup', async () => {
    try {
        await SgGeocode.getLatLng(obj4, keys);
    } catch (e) {
        expect(e.message).toBe('Blank lookup (you must provide a ZIP Code and/or City/State combination).');
    }
});

it('test invalid key', async () => {
    try {
        await SgGeocode.getLatLng(obj5, invalidKeys);
    } catch (e) {
        expect(e.message).toBe('Unauthorized: The credentials were provided incorrectly or did not match any existing active credentials.');
    }
});

test('test Prediction Zipcode', () => {
    return SgGeocode.getLatLng(obj6, keys).then(res => {
        expect(res.lat).toBe(33.31666)
        expect(res.lng).toBe(-111.83182)
    })
});

test('test Prediction CityState', () => {
    return SgGeocode.getLatLng(obj7, keys).then(res => {
        expect(res.lat).toBe(33.43681)
        expect(res.lng).toBe(-111.943)
    })
});

test('test Prediction usStreetAPI', () => {
    return SgGeocode.getLatLng(obj8, keys).then(res => {
        expect(res.lat).toBe(33.32371)
        expect(res.lng).toBe(-111.83018)
    })
});

test('test googleapi', () => {
    return SgGeocode.getLatLng(obj9, keys).then(res => {
        expect(res.lat).toBe(33.248528)
        expect(res.lng).toBe(-111.8381307)
    })
});

test('test incorrect object', () => {
    expect(() => {
        SgGeocode.getLatLng(obj10, keys);
    }).toThrow('Object not defined correctly');
})

test('test google zero result', () => {
    return SgGeocode.getLatLng(obj11, keys).then(res => {
        expect(res.status).toBe('ZERO_RESULTS');
    })
});