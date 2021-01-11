using System;
using System.Collections.Generic;
using System.Text;

namespace SmartyStreetsGoogleGeocode
{
    public class GeocodeInput
        //Rename to GeocodeInput
    {        
        public string address { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public GeocodeInput() { }

        public GeocodeInput(string address)
        {
            this.address = address;
        }

        public GeocodeInput(string zipcode, string city, string state)
        {
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
