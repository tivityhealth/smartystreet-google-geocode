const debounce = (func, wait, immediate) => {
    let timeout;
    return function() {
      let context = this,
        args = arguments;
      let later = function() {
        timeout = null;
        if (!immediate) func.apply(context, args);
      };
      let callNow = immediate && !timeout;
      clearTimeout(timeout);
      timeout = setTimeout(later, wait);
      if (callNow) func.apply(context, args);
    };
  }
  const showMap = () => {
    let map = new google.maps.Map(document.getElementById('map'), {
      disableDefaultUI: true
    });
    navigator.geolocation.getCurrentPosition(function(position) {
      // Center on user's current location if geolocation prompt allowed
      var initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
      map.setCenter(initialLocation);
      map.setZoom(13);
    }, function(positionError) {
      // User denied geolocation prompt - default to Phoenix
      centerMap(33, -111);
    });
  }
  const centerMap = (map_lat, map_lon) => {
    let map = new google.maps.Map(document.getElementById('map'), {
      disableDefaultUI: true
    });
    map.setCenter(new google.maps.LatLng(map_lat, map_lon));
    map.setZoom(13);
  }
  let autocomplete_input = document.getElementById('my-input-autocomplete');
  let autocomplete_results = document.querySelector('.autocomplete-results');
    
  const getLocation = (selectedText, placeId, resultsPredictions) => {
    const selected_place = resultsPredictions.find((x) => x.place_id === placeId);
    console.log('Terms: ', selected_place.terms);
    const isZip = selected_place.types.includes("postal_code");
    //Length of terms
    let len = selected_place.terms.length - 1
    let freeformAddress = selected_place.description;
    let zData = {
      city: '',
      state: '',
      zipcode: '',
    };
    //Zip if present is always present at the n-1st position
    if (len !== 2 && isZip && !isNaN(selected_place.terms[len - 1].value)) {
      zData.zipcode = selected_place.terms[len - 1].value;
      getZip(zData);
    }
    else if (len == 2) {
      //When you enter 'City State' city is at position 1 and state at position 2
      zData.city = selected_place.terms[0].value;
      zData.state = selected_place.terms[1].value;
      getZip(zData);
    } else {
      getFullAddress(freeformAddress);
    }
    autocomplete_input.value = selectedText;
    autocomplete_results.style.display = 'none';
    //Create Location DOM Element
    var loc = document.createElement("p");
    loc.textContent = "Location: " + selectedText;
    var outputDiv = document.getElementById("output");
    outputDiv.appendChild(loc);
  }
  function initAutocomplete() {
    // Create the search box and link it to the UI element.
  
    let service = new google.maps.places.AutocompleteService();
    //let serviceDetails = new google.maps.places.PlacesService(map);
    let displaySuggestions = function(predictions, status) {
      if (status != google.maps.places.PlacesServiceStatus.OK) {
        alert(status);
        return;
      }
      showMap();
      let results_html = [];
      let resultsPredictions = []
      predictions.forEach(function(prediction) {
        results_html.push(`<li class="autocomplete-item" data-type="place" data-place-id=${prediction.place_id} data-place-types=${prediction.types}><span class="autocomplete-icon icon-localities"></span>                      <span class="autocomplete-text">${prediction.description}</span></li>`);
        resultsPredictions.push(prediction);
      });
      autocomplete_results.innerHTML = results_html.join("");
      autocomplete_results.style.display = 'block';
      let autocomplete_items = autocomplete_results.querySelectorAll('.autocomplete-item');
      //On ENTER Key
      autocomplete_input.addEventListener('keydown', function(e) {
        if (e.which === 13 || e.keyCode === 13) {
          e.preventDefault();
            console.log('On Keydwn');
          var first_item = document.querySelectorAll(".autocomplete-results > .autocomplete-item")[0];
          var selectedText = first_item.textContent.replace('"', '\\"').replace(/^\s+|\s+$/g, '');
          const placeId = first_item.getAttribute('data-place-id');
             getLocation(selectedText, placeId, resultsPredictions)
        }
      });
      for (let autocomplete_item of autocomplete_items) {
        autocomplete_item.addEventListener('click', function() {
            console.log('On Click');
          const selectedText = this.querySelector('.autocomplete-text').textContent;
          const placeId = this.getAttribute('data-place-id');
                  getLocation(selectedText, placeId, resultsPredictions)    
        })
      }
    };
  
    autocomplete_input.addEventListener('input', debounce(function() {
      $('#output > p').html("");
      let value = this.value;
      value.replace('"', '\\"').replace(/^\s+|\s+$/g, '');
      if (value !== "" && value.length > 4) {
        service.getPlacePredictions({
          input: value,
          componentRestrictions: {
            country: 'us'
          },
        }, displaySuggestions);
      } else {
        autocomplete_results.innerHTML = '';
        autocomplete_results.style.display = 'none';
      }
    }, 150));
  }
  const getZip = (zData) => {
    // create the data
    const SmartyStreetsCore = SmartyStreetsSDK.core;
      const Lookup = SmartyStreetsSDK.usZipcode.Lookup;
    const websiteKey = "8945708976961762"; // Update this with your website key.
    const credentials = new SmartyStreetsCore.SharedCredentials(websiteKey);
  
      let client = SmartyStreetsCore.buildClient.usZipcode(credentials);
    
    let lookup = new Lookup();
    lookup.city = zData.city;
    lookup.state = zData.state;
    lookup.zipCode = zData.zipcode;
      
    //Make the call
    client.send(lookup)
      .then(handleZipResponse)
      .catch(callGeoCode);
  }
  
  const handleZipResponse = (res) => {
    
    var lat = res.lookups[0].result[0].zipcodes[0].latitude;
    var lon = res.lookups[0].result[0].zipcodes[0].longitude;
    //create DOM elements
    const latLon = document.createElement("p");
    latLon.textContent = "Lat/Lon: " + lat + ", " + lon;
    //append to output
    var outputDiv = document.getElementById("output");
    outputDiv.appendChild(latLon);
    centerMap(lat, lon);
  }
  
  const getFullAddress = (fullAddress) => {
    /* Initialize the SDK. */
    const SmartyStreetsCore = SmartyStreetsSDK.core;
    const Lookup = SmartyStreetsSDK.usStreet.Lookup;
    const websiteKey = "8945708976961762"; // Update this with your website key.
    const credentials = new SmartyStreetsCore.SharedCredentials(websiteKey);
    const clientBuilder = new SmartyStreetsCore.ClientBuilder(credentials);
    const client = clientBuilder.buildUsStreetApiClient();
    // Create a new lookup with the input value.
    //SmartyStreets sometimes cannot lookup address when entered with USA at the end
    //So we remove 'USA' from the end of the address string
    const lookup = new Lookup(fullAddress.substring(0, fullAddress.length - 5));
    lookup.maxCandidates = 1;
    lookup.match = "strict";
    // Send the lookup with the SDK. The SDK returns a promise.
    client.send(lookup)
      .then(handleResponse)
      .catch(callGeoCode);
  }
  /* Do something with the response */
  const handleResponse = (res) => {
    var lat = res.lookups[0].result[0].metadata.latitude;
    var lon = res.lookups[0].result[0].metadata.longitude;
    //create DOM elements
    const latLon = document.createElement("p");
    latLon.textContent = "Lat/Lon: " + lat + ", " + lon;
    //append to output
    var outputDiv = document.getElementById("output");
    outputDiv.appendChild(latLon);
    centerMap(lat, lon);
  }
  /* Handle any errors. */
  const handleError = (res) => {
    callGeoCode();
  }
  const callGeoCode = () => {
    console.log('In Geocoder');
    var address = document.getElementById('my-input-autocomplete').value;
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({
      'address': address
    }, function(results, status) {
      if (status == google.maps.GeocoderStatus.OK) {
        var lat = results[0].geometry.location.lat();
        var lon = results[0].geometry.location.lng();
        const latLon = document.createElement("p");
        latLon.textContent = "Lat/Lon: " + lat + ", " + lon;
        //append to output
        var outputDiv = document.getElementById("output");
        outputDiv.appendChild(latLon);
        centerMap(lat, lon);
      }
    })
  }
  document.addEventListener("DOMContentLoaded", function(event) {
    initAutocomplete();
  });