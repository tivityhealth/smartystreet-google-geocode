const { callGeocoder } = require("./googleGeocoder");

const handleResponse = async (res, googleApiKey) => {
    let result;
    if(res.lookups[0].result === undefined || res.lookups[0].result.length === 0){
        //when smartystreets returns undefined         
        result = await callGeocoder(res.lookups[0].street, googleApiKey)
            .then(geo => geo);           
    }
    else{
        let lat = res.lookups[0].result[0].metadata.latitude;
        let lng = res.lookups[0].result[0].metadata.longitude;

        result = { lat, lng }
    }

    //return lat lng object
    return { result };
};

const handleError = (res) => {
    console.log(123)
    throw new Error(res.error.message)
  }

const callUSStreetApi = ({fullAddress, webKey, googleApiKey}) => {
    /* Initialize the SDK. */
    const SmartyStreetsSDK = require("smartystreets-javascript-sdk");
    const SmartyStreetsCore = SmartyStreetsSDK.core;
    const Lookup = SmartyStreetsSDK.usStreet.Lookup;

    //Client side key
    const websiteKey = webKey; // Update this with your website key.
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
        return handleResponse(res, googleApiKey)
    }).catch((res) => {
        return handleError(res);
    });
};

exports.callUSStreetApi = callUSStreetApi;