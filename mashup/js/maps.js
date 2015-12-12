function initMap() {

    var markerArr1 = [];
    var markerArr2 = [];
    var markerArr3 = [];
    var markerArr4 = [];


    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 6,
        center: {lat: 61.39, lng: 15.35}
    });

    var infoWindow = new google.maps.InfoWindow();

    $.ajax({
        url: 'getData.php',
        dataType: 'json',
        type: 'get',
        success: function (data) {

            for (var key in data.messages) {

                var obj = data.messages[key];

                var date = obj.createddate;
                var readableDate = doDateReadable(obj.createddate);
                var title = obj.title;
                var description = obj.description;
                var latitude = obj.latitude;
                var longitude = obj.longitude;


                myLatlng = new google.maps.LatLng(latitude, longitude);

                allMarkers = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    date: readableDate,
                    createddate: date,
                    animation: google.maps.Animation.DROP,
                    title: title,
                    html: '<div class="markerPop">' +
                    '<h1>' + title + '</h1>' + //substring removes distance from title
                    '<p>' + description + '</p>' +
                    '<p>' + readableDate + '</p>' +
                    '</div>'
                });

                if (obj.category == 0) {
                    markerArr1.push(allMarkers); // Markers for Vägtrafik categories
                }
                if (obj.category == 1) {
                    markerArr2.push(allMarkers); // Markers for Kollektivtrafik categories
                }
                if (obj.category == 2) {
                    markerArr3.push(allMarkers); // Markers for Planerad störning categories
                }
                if (obj.category == 3) {
                    markerArr4.push(allMarkers); // Markers for Övrigt categories
                }

                // Make markers clickable
                google.maps.event.addListener(allMarkers, 'click', function () {
                    infoWindow.setContent(this.html);
                    infoWindow.open(map, this);
                });


            }

            document.getElementById("markCount1").innerHTML = " " + markerArr1.length;
            document.getElementById("markCount2").innerHTML = " " + markerArr2.length;
            document.getElementById("markCount3").innerHTML = " " + markerArr3.length;
            document.getElementById("markCount4").innerHTML = " " + markerArr4.length;


            function sortAsDate(a, b) {
                if (a.createddate < b.createddate)
                    return 1;
                if (a.createddate > b.createddate)
                    return -1;
                return 0;
            }

            markerArr1.sort(sortAsDate);
            markerArr2.sort(sortAsDate);
            markerArr3.sort(sortAsDate);
            markerArr4.sort(sortAsDate);


            createListOfMarkers(markerArr1, "ul1");
            createListOfMarkers(markerArr2, "ul2");
            createListOfMarkers(markerArr3, "ul3");
            createListOfMarkers(markerArr4, "ul4");

            function createListOfMarkers(marker, ulTag) {
                var ul = document.getElementById(ulTag);

                marker.forEach(function (entry) {
                    var li = document.createElement("li");
                    ul.appendChild(li);
                    //var title = marker.getTitle();
                    li.innerHTML = entry.date + entry.title;

                    //Trigger a click event to marker when the list item is clicked.
                    google.maps.event.addDomListener(li, "click", function () {
                        entry.setAnimation(google.maps.Animation.BOUNCE);
                        setTimeout(function () { entry.setAnimation(null); }, 750); // marker bounce one time
                        google.maps.event.trigger(entry, "click");
                    });
                });
            }

            function doDateReadable(date) {
                var months = [
                    "Januari", "Februari", "Mars", "April", "Mars", "Juni", "Juli", "Augusti", "September", "Oktober", "November", "December"
                ];

                //Remove unwanted strings
                date = date.replace("/Date(", "");
                date = date.replace(")/", "");

                //convert it into an integer and format it to an date
                date = parseInt(date);
                date = new Date(date);
                date = date.getDate() + " " + months[date.getMonth()] + " " + date.getFullYear();

                return date;
            }

            // Toggle markers when click
            function addOnclick(index) {
                document.getElementById("btn" + index).addEventListener("click", function () {
                    $("#ul" + index).toggle();

                    if (index == 1) var arr = markerArr1;
                    if (index == 2) var arr = markerArr2;
                    if (index == 3) var arr = markerArr3;
                    if (index == 4) var arr = markerArr4;

                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i].getMap() === null) {
                            arr[i].setMap(map);
                        } else {
                            arr[i].setMap(null);
                        }
                    }
                });
            }

            for (var i = 1; i < 5; i++) addOnclick(i);

        }

    });

}



