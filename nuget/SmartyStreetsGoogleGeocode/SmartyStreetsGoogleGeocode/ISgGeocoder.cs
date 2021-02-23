using System;

namespace SmartyStreetsGoogleGeocode
{
    public interface ISgGeocoder
    {
        GeoPoint GetLatLng(GeocodeInput geoInput);
    }
}
