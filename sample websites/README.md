## Installation

Install `node` from `https://nodejs.org/en/download/` \
Clone this repo and navigate to 'sample websites' folder and then run `npm install`

## Usage

To run this `npm run serve`

## Workings

1. Avoids loading google map as well as autocomplete suggestions until you type in 5 characters (5 because, you should be able to see the location when you enter a zip code).
2. We use Google Autocomplete to show the list of mathcing locations. Google Autocomplete, because it is more user friendly and has a wider range of predictions.
3. When you enter City/State/Zipcode, we make a call to SmartyStreets Zipcode API.
4. When you enter a complete address, we make a call to the freeform SmartyStreets USstreet API.
5. Finally when SmartyStreets fails to retrieve a location, we make a call to Google's Geocoder service.
