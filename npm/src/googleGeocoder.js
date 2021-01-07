import { Client } from "@googlemaps/google-maps-services-js"

const callGeocoder = (fullAddress) => {
	console.log('In Geocoder')
	const client = new Client({});
	let result;

	return client
		.geocode({
			params: {
				address: fullAddress,
				key: process.env.GOOGLE_API_KEY,
			},
			timeout: 1000, // milliseconds
		})
		.then((res) => {
			console.log(123);
			return handleResult(res)
		})
		.catch((e) => {
			return handleError(e)
		});
}

const handleResult = (res) => {
	let result;
	result = {
		lat: res.data.results[0].geometry.location.lat,
		lng: res.data.results[0].geometry.location.lng
	}

	return { result };
}

const handleError = (e) => {
	let result = { error: e.response.data.error_message };
	return { result };
}

module.exports.callGeocoder = callGeocoder;