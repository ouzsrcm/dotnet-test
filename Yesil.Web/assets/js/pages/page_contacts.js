var ContactPage = function () {

    return {
        
    	//Basic Map
        initMap: function () {
			var map;
			$(document).ready(function(){
			  map = new GMaps({
				div: '#map',
				scrollwheel: false,				
				lat: 40.748866,
				lng: -73.988366
			  });
			  var marker = map.addMarker({
			      lat: 38.361059,
			      lng: 35.087406,
			      title: 'YSL TARIM DIÞ TÝCARET A.Þ.'
		       });
			});
        },

        //Panorama Map
        initPanorama: function () {
		    var panorama;
		    $(document).ready(function(){
		      panorama = GMaps.createPanorama({
		          el: '#panorama',
		          lat: 38.361059,
		          lng: 35.087406,
		      });
		    });
		}        

    };
}();