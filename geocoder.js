const callGeocoder = (fullAddress) => {
	console.log('In Geocoder')
	var geocoder = new google.maps.Geocoder();

	return new Promise((resolve, reject) => {
		geocoder.geocode({
			'address': fullAddress
		}, function (results, status) {
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