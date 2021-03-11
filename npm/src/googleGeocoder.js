const axios = require("axios");

const handleResult = (res) => {
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