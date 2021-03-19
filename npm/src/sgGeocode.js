const {callZipApi} = require("./zipApi");
const { callUSStreetApi } = require("./usStreetApi");

const isOptionsNull = (options) => {
  if(!options) return true;
  if(!options.fullAddress && !options.zipcode && !options.city && !options.state && !options.prediction) return true;
  return false;
}

const usePrediction = (options, keys) => {
  let zipKeyWords = ["postal_code", "locality"];

  //Check if autocomplete object is zipcode compatible  
  if(zipKeyWords.some(x => options.prediction.types.includes(x))){
    let zipData = {
      city: '',
      state: '',
      zipcode: '',
      webKey: keys.webKey
    };
    let termsLength = options.prediction.terms.length - 1;

    //Checks if it has zipcode specifically
    if(!isNaN(options.prediction.terms[termsLength - 1].value)){
      zipData.zipcode = options.prediction.terms[termsLength - 1].value;
    }
    //Checks is autocomplete has only City-State combo
    else if(termsLength === 2){
      zipData.city = options.prediction.terms[0].value;
      zipData.state = options.prediction.terms[1].value;
    }
    return callZipApi(zipData)
  } 
  //If no zipcode, then call SmartyStreets USStreet end point
  else {
    let streetData = {
      fullAddress: options.prediction.description,
      webKey: keys.webKey,
      googleApiKey: keys.googleApiKey,
    }

    return callUSStreetApi(streetData)
  }
}

class SgGeocode{
  static getLatLng(options, keys) {
    if(isOptionsNull(options)){
      throw new Error("Object not defined correctly");
    }
    //When user passes either zipcode or city-state combo
    if (options.zipcode || (options.city && options.state)) {
      let zipData = {
        city: options.city,
        state: options.state,
        zipcode: options.zipcode,
        webKey: keys.webKey
      };
      return callZipApi(zipData)
          .then(({result}) => result)
    }
    //When the user passes complete/partial Address
    else if (options.fullAddress) {
        let streetData = {
          fullAddress : options.fullAddress,
          webKey : keys.webKey,
          googleApiKey : keys.googleApiKey
        }
        return callUSStreetApi(streetData)
          .then(({result}) =>  result )
    }
    //When the user passes Google autocomplete object as input
    else if (options.prediction) {
       return usePrediction(options, keys)
          .then(({result}) =>  result )
    }
  };
}

if (typeof window !== 'undefined') {
  window.SgGeocode = SgGeocode
}

exports.SgGeocode = SgGeocode;