using System;
using System.IO;
using SmartyStreets;
using SmartyStreets.USStreetApi;

namespace SmartyStreetsGoogleGeocode
{
    class UsStreetApi
    {
		public static GeoPoint CallUsStreet(AddressObject options)
		{
			var authId = options.authId;
			var authToken = options.authToken;

			var client = new ClientBuilder(authId, authToken)
				//.WithCustomBaseUrl("us-street.api.smartystreets.com")
				//.ViaProxy("http://localhost:8080", "username", "password") // uncomment this line to point to the specified proxy.
				.BuildUsStreetApiClient();

			// Documentation for input fields can be found at:
			// https://smartystreets.com/docs/us-street-api#input-fields

			var lookup = new Lookup(options.address)
			{
				MaxCandidates = 1,
				MatchStrategy = Lookup.INVALID // "invalid" is the most permissive match,
											   // this will always return at least one result even if the address is invalid.
											   // Refer to the documentation for additional MatchStrategy options.
			};

			try
			{
				client.Send(lookup);
			}
			catch (SmartyException ex)
			{
				throw ex;
			}
			catch (IOException ex)
			{
				throw ex;
			}

			var candidates = lookup.Result;

			if (candidates.Count == 0)
			{
				Console.WriteLine("SmartyStreets cannot lookup this address. Calling Google geocoder");
				return GoogleGeocode.callGoogleGeocoder(options.address);
			}

			var firstCandidate = candidates[0];

			GeoPoint geoPoint = new GeoPoint(firstCandidate.Metadata.Latitude, firstCandidate.Metadata.Longitude);

			return geoPoint;
		}
	}
}
