import { callZipApi } from "./zipAPI";
import { callUSStreetApi } from "./usStreetAPI";

const latlon = (options) => {
  if (options.zipcode || (options.city && options.state)) {
    let zipData = {
      city: options.city,
      state: options.state,
      zipcode: options.zipcode
    };
    return callZipApi(zipData)
        .then(({result}) => result)
  } else if (options.address) {
      //undefined
      return callUSStreetApi(options.address)
        .then(({result}) =>  result )
  }
};

module.exports.latlon = latlon;