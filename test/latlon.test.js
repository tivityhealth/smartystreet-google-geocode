import {sgGeocode} from '../js/sgGeocode';

let obj1 = {
    address: '155 E Frye Rd Chandler AZ',
    webKey: '8945708976961762'
}
let obj2 = {
    zipcode: '85225',
    webKey: '8945708976961762'
}
let obj3 = {
    zipcode: '1234',
    webKey: '8945708976961762'
}
let obj4 = {
    zipcode: 'abcd',
    webKey: '8945708976961762'
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