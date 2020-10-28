import { callZipApi } from "./zipApi";
import { callUSStreetApi } from "./usStreetAPI";

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
  }
};

module.exports.sgGeocode = sgGeocode;