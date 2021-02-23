using Microsoft.AspNetCore.Mvc;
using SmartyStreetsGoogleGeocode;

namespace GeocoderExampleCore.Controllers
{
    public class GeocodeController : Controller
    {
        private readonly ISgGeocoder _sgGeocoder;

        public GeocodeController(ISgGeocoder sgGeocoder)
        {
            _sgGeocoder = sgGeocoder;
        }

        public IActionResult Index()
        {
            GeocodeInput geoInput = new GeocodeInput("85225", null, null);
            ViewBag.result = _sgGeocoder.GetLatLng(geoInput);
            return View();
        }
    }
}
