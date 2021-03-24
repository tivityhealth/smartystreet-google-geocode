const handleResponse = (res) => {
  let result;
  if(res.lookups[0].result[0].status){
    throw new Error(res.lookups[0].result[0].reason);
  }
  else{      
    let lat = res.lookups[0].result[0].zipcodes[0].latitude;
    let lng = res.lookups[0].result[0].zipcodes[0].longitude;
  
    result = { lat, lng }
  }  
  return { result };
};

const handleError = (res) => {  
  if(res.message){
    throw new Error(res.message)
  }
  else{
    throw new Error(res.error.message)
  }
}

const callZipApi = ({city, state, zipcode, webKey}) => {
  /* Initialize the SDK. */
  const SmartyStreetsSDK = require("smartystreets-javascript-sdk");
  const SmartyStreetsCore = SmartyStreetsSDK.core;
  const Lookup = SmartyStreetsSDK.usZipcode.Lookup;

  //Client side key
  const websiteKey = webKey;
  const credentials = new SmartyStreetsCore.SharedCredentials(websiteKey);

  //build new client
  let client = SmartyStreetsCore.buildClient.usZipcode(credentials);

  // Create a new lookup with the input value.
  let lookup = new Lookup();
  lookup.city = city;
  lookup.state = state;
  lookup.zipCode = zipcode;

  // Send the lookup with the SDK. The SDK returns a promise.
  return client.send(lookup).then((res) => {
    return handleResponse(res)
  }).catch((res) => {
    return handleError(res);
  });
};

exports.callZipApi = callZipApi;