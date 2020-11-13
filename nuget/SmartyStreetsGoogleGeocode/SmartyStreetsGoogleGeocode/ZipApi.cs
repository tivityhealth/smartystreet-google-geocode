using System;
using System.IO;
using SmartyStreets;
using SmartyStreets.USZipCodeApi;

namespace SmartyStreetsGoogleGeocode
{
	class ZipApi
	{
		public static GeoPoint CallZip(AddressObject options)
		{
			// You don't have to store your keys in environment variables, but we recommend it.
			var authId = options.authId;
			var authToken = options.authToken;
			var client = new ClientBuilder(authId, authToken).BuildUsZipCodeApiClient();

			// Documentation for input fields can be found at:
			// https://smartystreets.com/docs/us-zipcode-api#input-fields

			var lookup = new Lookup
			{
				City = options.city,
				State = options.state,
				ZipCode = options.zipcode,
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

			var result = lookup.Result;
			if (result.Status == "invalid_zipcode")
			{
				throw new ApplicationException("Invalid Zipcode");
			}
			else if (result.Status == "blank")
			{
				throw new ApplicationException("Blank lookup (you must provide a ZIP Code and/or City/State combination).");
			}

			var zipCodes = result.ZipCodes[0];

			GeoPoint geoPoint = new GeoPoint();
			geoPoint.Latitude = zipCodes.Latitude;
			geoPoint.Longitude = zipCodes.Longitude;

			return geoPoint;
		}
	}
}
