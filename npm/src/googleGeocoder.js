import {Client} from "@googlemaps/google-maps-services-js";

const callGeocoder = (fullAddress) => {
	console.log('In Geocoder')
	const client = new Client({});

	const params = {
		address: fullAddress,
		key: process.env.GOOGLE_API_KEY
	  };

	return new Promise((resolve, reject) => {
		client.geocode({ params: params }, function (results, status) {
			if (status == google.maps.GeocoderStatus.OK) {
				var lat = results[0].geometry.location.lat();
				var lng = results[0].geometry.location.lng();

				let result = {
					lat: lat,
					lng: lng
				}
				resolve(result)
			}
		})

	})
}

module.exports.callGeocoder = callGeocoder;