using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public class GeocodeInput
        //Rename to GeocodeInput
    {
        public string authId { get; set; }
        public string authToken { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public GeocodeInput() { }

        public GeocodeInput(string authId, string authToken, string address)
        {
            this.authId = authId;
            this.authToken = authToken;
            this.address = address;
        }

        public GeocodeInput(string authId, string authToken, string zipcode, string city, string state)
        {
            this.authId = authId;
            this.authToken = authToken;
            this.zipcode = zipcode;
            this.city = city;
            this.state = state;
        }

        public Boolean IsNull()
        {
            return ((this.address == null || this.address == "") &&
                (this.zipcode == null || this.zipcode == "") &&
                (this.city == null || this.city == "") &&
                (this.state == null || this.state == "")
                );
        }
    }
}
