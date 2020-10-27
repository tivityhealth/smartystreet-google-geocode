const callZipApi = (zipData) => {
  /* Initialize the SDK. */
  const SmartyStreetsSDK = require("smartystreets-javascript-sdk");
  const SmartyStreetsCore = SmartyStreetsSDK.core;
  const Lookup = SmartyStreetsSDK.usZipcode.Lookup;

  //Client side key
  const websiteKey = zipData.webKey; // Update this with your website key.
  const credentials = new SmartyStreetsCore.SharedCredentials(websiteKey);

  //build new client
  let client = SmartyStreetsCore.buildClient.usZipcode(credentials);

  // Create a new lookup with the input value.
  let lookup = new Lookup();
  lookup.city = zipData.city;
  lookup.state = zipData.state;
  lookup.zipCode = zipData.zipcode;

  // Send the lookup with the SDK. The SDK returns a promise.
  return client.send(lookup).then((res) => {
      return handleResponse(res)
  }).catch((res) => {
    return handleError(res)
  })
};

const handleResponse = (res) => {
  console.log('In Zip Handle response');
  let result;
  if(res.lookups[0].result[0].status){
    result = {error: res.lookups[0].result[0].reason};
  }
  else{      
    let lat = res.lookups[0].result[0].zipcodes[0].latitude;
    let lng = res.lookups[0].result[0].zipcodes[0].longitude;
  
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

module.exports.callZipApi = callZipApi;