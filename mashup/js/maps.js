function initMap() {

    //list = data-messages
    //list.sort(sortIncidentsByDate);
    //list.reverse();
    
    // sort=createddate+desc

    var tempMarkerHolder = [];

    var m1 = [];
    var m2 = [];
    var m3 = [];
    var m4 = [];
    // var allMarkers = []; //returned from the API



    function addOnclick(index)
    {
        document.getElementById("btn" + index).addEventListener("click", function(){

            $("#ul" + index).toggle();

            if(index == 1)
                var arr = m1;
            if(index == 2)
                var arr = m2;
            if(index == 3)
                var arr = m3;
            if(index == 4)
                var arr = m4;
            //console.log(arr);

            for (var i = 0; i < arr.length; i++) {
                if (arr[i].getMap() === null) {
                    arr[i].setMap(map);
                } else {
                    arr[i].setMap(null);
                }
            }
        });
    }

    for ( var i = 1; i < 5; i++ ) addOnclick( i );


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

               // if(obj.category == 2){

              //  if(obj.category == 3){

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

                if(obj.category == 0){
                    m1.push(allMarkers);
                    createMarkerButton(allMarkers, "ul1");
                }
                if(obj.category == 1){
                    m2.push(allMarkers);
                    createMarkerButton(allMarkers, "ul2");
                }
                if(obj.category == 2){
                    m3.push(allMarkers);
                    createMarkerButton(allMarkers, "ul3");
                }
                if(obj.category == 3){
                    m4.push(allMarkers);
                    createMarkerButton(allMarkers, "ul4");
                }


                //console.log(allMarkers);
                //console.log(mm);

                //createMarkerButton(allMarkers);


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
           // }
            function createMarkerButton(marker, ulla) {
                //Creates a sidebar button
               // var div = document.getElementById("marker_list");
                var ul = document.getElementById(ulla);
               // var ul = document.createElement("ul");
                var li = document.createElement("li");
              //  ul.className = "ulla";
                ul.appendChild(li);
                //var title = marker.getTitle();
                li.innerHTML = title;
                //div.appendChild(ul);

                //Trigger a click event to marker when the button is clicked.
                google.maps.event.addDomListener(li, "click", function(){
                    google.maps.event.trigger(marker, "click");
                });
            }
            //google.maps.event.addDomListener(window, "load", initialize);
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
      //  }
    });

}



