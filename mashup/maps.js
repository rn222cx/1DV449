function initMap() {

    //list = data-messages
    //list.sort(sortIncidentsByDate);
    //list.reverse();
    
    // sort=createddate+desc

    var markers = [];
    var long = [];
    var lat = [];
    var allLatlng = []; //returned from the API
    var tempMarkerHolder = [];
    var heh = [];
    // var allMarkers = []; //returned from the API


    document.getElementById("submit").addEventListener("click", function(){
        for (var i = 0; i < heh.length; i++) {
            if (heh[i].getMap() === null) {
                heh[i].setMap(map);
            } else {
                heh[i].setMap(null);
            }
        }
    });

    var myLatLng = {lat: 61.39, lng: 15.35};

    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 6,
        center: myLatLng
    });

    var infoWindow = new google.maps.InfoWindow();


    $.ajax({
        url: 'getData.php',
        dataType: 'json',
        type: 'get',
        success: function (data) {

            for (var key in data.messages) {

                var obj = data.messages[key];



               // console.log(obj);
                var title = obj.title;
                var description = obj.description;
                var latitude = obj.latitude;
                var longitude = obj.longitude;
                // console.log(longitude);

                myLatlng = new google.maps.LatLng(latitude, longitude);

                allMarkers = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    title: title,
                    html: '<div class="markerPop">' +
                    '<h1>' + title + '</h1>' + //substring removes distance from title
                    '<p>' + description + '</p>' +
                    '</div>'
                });
                if(obj.category == 2){
                    console.log('hj');
                    heh.push(allMarkers);
                }
                ////put all lat long in array
                //allLatlng.push(myLatlng);
                //
                ////Put the marketrs in an array
                tempMarkerHolder.push(allMarkers);
                google.maps.event.addListener(allMarkers, 'click', function () {
                    infoWindow.setContent(this.html);
                    infoWindow.open(map, this);
                });

            }


            //var bounds = new google.maps.LatLngBounds ();
            //
            //for (var t = 0, LtLgLen = allLatlng.length; t < LtLgLen; t++) {
            //    //  And increase the bounds to take this point
            //    bounds.extend (allLatlng[t]);
            //}
            //
            //map.fitBounds (bounds);

            //console.log(data);
        }

    });

}



