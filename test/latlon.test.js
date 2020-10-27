import {sgGeocode} from '../sgGeocode';

let obj1 = {
    address: '155 E Frye Rd Chandler AZ',
}
let obj2 = {
    zipcode: '85225',
}
let obj3 = {
    zipcode: '1234',
}
let obj4 = {
    zipcode: 'abcd',
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