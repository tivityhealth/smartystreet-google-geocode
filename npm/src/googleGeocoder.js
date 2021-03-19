const axios = require("axios");

const handleResult = (res) => {
	if(res.data.status === 'ZERO_RESULTS' || res.data.results.length === 0){
        //when Google returns zero results         
        return {status: res.data.status};
    }
	let result = {
		lat: res.data.results[0].geometry.location.lat,
		lng: res.data.results[0].geometry.location.lng
	}

	return result;
}

const handleError = (e) => {
	throw new Error(e.response.data.error_message);
}

const callGeocoder = (fullAddress, apiKey) => {
	const geocoderQuery = encodeURIComponent(`${fullAddress}`.replace(/ /g, '+'))

	return axios.get(`https://maps.googleapis.com/maps/api/geocode/json?address=${geocoderQuery}&key=${apiKey}`)
		.then((res) => {
			return handleResult(res)
		})
		.catch((e) => {
			return handleError(e)
		});
}

exports.callGeocoder = callGeocoder;