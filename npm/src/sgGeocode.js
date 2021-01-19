const {callZipApi} = require("./zipApi");
const { callUSStreetApi } = require("./usStreetApi");

const sgGeocode = (options) => {
  if(isOptionsNUll(options)){
    throw "Object not defined correctly"
  }
  //When user passes either zipcode or city-state combo
  if (options.zipcode || (options.city && options.state)) {
    let zipData = {
      city: options.city,
      state: options.state,
      zipcode: options.zipcode,
      webKey: options.webKey
    };
    return callZipApi(zipData)
        .then(({result}) => result)
  }
  //When the user passes complete/partial fullAddress
  else if (options.fullAddress) {
      return callUSStreetApi(options)
        .then(({result}) =>  result )
  }
  //When the user passes Google autocomplete object as input
  else if (options.prediction) {
     return usePrediction(options)
        .then(({result}) =>  result )
  }
};

const isOptionsNUll = (options) => {
  if(!options) return true;
  if(!options.fullAddress && !options.zipcode && !options.city && !options.state && !options.prediction) return true;
  return false;
}

const usePrediction = (options) => {
  let zipKeyWords = ["postal_code", "locality"];

  //Check if autocomplete object is zipcode compatible  
  if(zipKeyWords.some(x => options.prediction.types.includes(x))){
    let zipData = {
      city: '',
      state: '',
      zipcode: '',
      webKey: options.webKey
    };
    let termsLength = options.prediction.terms.length - 1;

    //Checks if it has zipcode specifically
    if(!isNaN(options.prediction.terms[termsLength - 1].value)){
      zipData.zipcode = options.prediction.terms[termsLength - 1].value;
    }
    //Checks is autocomplete has only City-State combo
    else if(termsLength == 2){
      zipData.city = options.prediction.terms[0].value;
      zipData.state = options.prediction.terms[1].value;
    }
    return callZipApi(zipData)
  } 
  //If no zipcode, then call SmartyStreets USStreet end point
  else {
    let streetData = {
      fullAddress: options.prediction.description,
      webKey: options.webKey,
      gApiKey: options.gApiKey,
    }

    return callUSStreetApi(streetData)
  }
}

module.exports.sgGeocode = sgGeocode;