const { Client } = require("@googlemaps/google-maps-services-js");

const handleResult = (res) => {
	let result = {
		lat: res.data.results[0].geometry.location.lat,
		lng: res.data.results[0].geometry.location.lng
	}

	return { result };
}

const handleError = (e) => {
	throw new Error(e.response.data.error_message);
}

const callGeocoder = (fullAddress, apiKey) => {
	const client = new Client({});

	return client
		.geocode({
			params: {
				address: fullAddress,
				key: apiKey,
			},
			timeout: 1000, // milliseconds
		})
		.then((res) => {
			return handleResult(res)
		})
		.catch((e) => {
			return handleError(e)
		});
}

exports.callGeocoder = callGeocoder;