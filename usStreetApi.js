import { callGeocoder } from './geocoder'
const callUSStreetApi = (fullAddress) => {
    /* Initialize the SDK. */
    const SmartyStreetsSDK = require("smartystreets-javascript-sdk");
    const SmartyStreetsCore = SmartyStreetsSDK.core;
    const Lookup = SmartyStreetsSDK.usStreet.Lookup;

    //Client side key
    const websiteKey = "8945708976961762"; // Update this with your website key.
    const credentials = new SmartyStreetsCore.SharedCredentials(websiteKey);

    //build new client
    const clientBuilder = new SmartyStreetsCore.ClientBuilder(credentials);
    const client = clientBuilder.buildUsStreetApiClient();

    // Create a new lookup with the input value.
    const lookup = new Lookup(fullAddress);
    lookup.maxCandidates = 1;
    lookup.match = "strict";

    // Send the lookup with the SDK. The SDK returns a promise.
    return client.send(lookup).then((res) => {
        return handleResponse(res)
    }).catch((res) => {
        return handleError(res);
    });
};

const handleResponse = (res) => {
    console.log('In Street Handle response');
    let result;
    if(res.lookups[0].result == undefined || res.lookups[0].result.length == 0){
        //returning undefined
        result = callGeocoder(res.lookups[0].street)
            .then(geo => geo);        
    }
    else{
        let lat = res.lookups[0].result[0].metadata.latitude;
        let lng = res.lookups[0].result[0].metadata.longitude;

        result = {
            lat: lat,
            lng: lng
        }
    }

    //return lat lng object
    return { result };
};
  
const handleError = (res) => {
    let result = {error: res.error.message};
    return {result};
  }

module.exports.callUSStreetApi = callUSStreetApi;