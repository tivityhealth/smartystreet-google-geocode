const {callZipApi} = require("./zipApi")
const { callUSStreetApi } = require("./usStreetApi");

//user passes prediction
//Google autocomplete as input

const sgGeocode = (options) => {
  if (options.zipcode || (options.city && options.state)) {
    let zipData = {
      city: options.city,
      state: options.state,
      zipcode: options.zipcode,
      webKey: options.webKey
    };
    return callZipApi(zipData)
        .then(({result}) => result)
  } else if (options.address) {
      //undefined
      return callUSStreetApi(options)
        .then(({result}) =>  result )
  } else if (options.prediction) {
    console.log(123);
     return usePrediction(options)
        .then(({result}) =>  result )
  }
};

const usePrediction = (options) => {

  let zipKeyWords = ["postal_code", "locality"];

  if(zipKeyWords.some(x => options.prediction.types.includes(x))){
    let zipData = {
      city: '',
      state: '',
      zipcode: '',
      webKey: options.webKey
    };
    let termsLength = options.prediction.terms.length - 1;

    if(!isNaN(options.prediction.terms[termsLength - 1].value)){
      zipData.zipcode = options.prediction.terms[termsLength - 1].value;
    }
    else if(termsLength == 2){
      zipData.city = options.prediction.terms[0].value;
      zipData.state = options.prediction.terms[1].value;
    }

    return callZipApi(zipData)
  } else {
    let streetData = {
      address: options.prediction.description,
      webKey: options.webKey
    }

    return callUSStreetApi(streetData)
  }
}

module.exports.sgGeocode = sgGeocode;





//prediction[0]

//options.prediction = prdiction[0]

/**
let options = {
         "description" : "155 East Warner Road, Gilbert, AZ, USA",         
         "place_id" : "ChIJDfaWdmepK4cRJ-IVtbEzX1o",
         "reference" : "ChIJDfaWdmepK4cRJ-IVtbEzX1o",
         "structured_formatting" :
         "terms" : [
            {
               "offset" : 0,
               "value" : "155"
            },
            {
               "offset" : 4,
               "value" : "East Warner Road"
            },
            {
               "offset" : 22,
               "value" : "Gilbert"
            },
            {
               "offset" : 31,
               "value" : "AZ"
            },
            {
               "offset" : 35,
               "value" : "USA"
            }
         ],
         "types" : [ "premise", "geocode" ]
      },
      */