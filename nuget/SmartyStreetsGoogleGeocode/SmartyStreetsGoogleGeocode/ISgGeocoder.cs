using System;

namespace SmartyStreetsGoogleGeocode
{
    public interface ISgGeocoder
    {
        GeoPoint CallSgGeocoder(GeocodeInput geoInput);
    }
}
