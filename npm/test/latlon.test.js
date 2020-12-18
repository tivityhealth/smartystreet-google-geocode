import {sgGeocode} from '../src/sgGeocode';

let obj1 = {
    fullAddress: '155 E Frye Rd Chandler AZ',
    webKey: '${{secrets.NUGET_API_KEY}}'
}
let obj2 = {
    zipcode: '85225',
    webKey: '${{secrets.NUGET_API_KEY}}'
}
let obj3 = {
    zipcode: '1234',
    webKey: '${{secrets.NUGET_API_KEY}}'
}
let obj4 = {
    zipcode: 'abcd',
    webKey: '${{secrets.NUGET_API_KEY}}'
}

let obj5 = {
    fullAddress: '155 E Frye Rd Chandler AZ',
    webKey: '894570897696176'
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
     },
     webKey: '${{secrets.NUGET_API_KEY}}'
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
     },
     webKey: '${{secrets.NUGET_API_KEY}}'
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
     },
     webKey: '${{secrets.NUGET_API_KEY}}'
}

test('test usStreetAPI', () => {
    return sgGeocode(obj1).then(res => {
        expect(res.lat).toBe(33.32371)
        expect(res.lng).toBe(-111.83018)
    })
});

test('test zipAPI', () => {
    return sgGeocode(obj2).then(res => {
        expect(res.lat).toBe(33.31666)
        expect(res.lng).toBe(-111.83182)
    })
});

test('test invalid zip code', () => {
    return sgGeocode(obj3).then(res => {
        expect(res.error).toBe("Invalid ZIP Code.")
    })
});

test('test non numeric zip code', () => {
    return sgGeocode(obj4).then(res => {
        expect(res.error).toBe("Blank lookup (you must provide a ZIP Code and/or City/State combination).")
    })
});

test('test invalid key', () => {
    return sgGeocode(obj5).then(res => {
        expect(res.error).toBe("Unauthorized: The credentials were provided incorrectly or did not match any existing active credentials.")
    })
});

test('test Prediction Zipcode', () => {
    return sgGeocode(obj6).then(res => {
        expect(res.lat).toBe(33.31666)
        expect(res.lng).toBe(-111.83182)
    })
});

test('test Prediction CityState', () => {
    return sgGeocode(obj7).then(res => {
        expect(res.lat).toBe(33.43681)
        expect(res.lng).toBe(-111.943)
    })
});

test('test Prediction usStreetAPI', () => {
    return sgGeocode(obj8).then(res => {
        expect(res.lat).toBe(33.32371)
        expect(res.lng).toBe(-111.83018)
    })
});