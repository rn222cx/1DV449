function initMap() {

    var m1 = [];
    var m2 = [];
    var m3 = [];
    var m4 = [];

    function addOnclick(index) {
        document.getElementById("btn" + index).addEventListener("click", function () {

            $("#ul" + index).toggle();

            if (index == 1)
                var arr = m1;
            if (index == 2)
                var arr = m2;
            if (index == 3)
                var arr = m3;
            if (index == 4)
                var arr = m4;

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

                var date = obj.createddate;
                var parsedDate = formatDate(obj.createddate);
                var title = obj.title;
                var description = obj.description;
                var latitude = obj.latitude;
                var longitude = obj.longitude;


                myLatlng = new google.maps.LatLng(latitude, longitude);

                allMarkers = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    date: parsedDate,
                    createddate: date,
                    animation: google.maps.Animation.DROP,
                    title: title,
                    html: '<div class="markerPop">' +
                    '<h1>' + title + '</h1>' + //substring removes distance from title
                    '<p>' + description + '</p>' +
                    '<p>' + parsedDate + '</p>' +
                    '</div>'
                });

                if (obj.category == 0) {
                    m1.push(allMarkers);
                }
                if (obj.category == 1) {
                    m2.push(allMarkers);
                }
                if (obj.category == 2) {
                    m3.push(allMarkers);
                }
                if (obj.category == 3) {
                    m4.push(allMarkers);
                }


                google.maps.event.addListener(allMarkers, 'click', function () {
                    infoWindow.setContent(this.html);
                    infoWindow.open(map, this);
                });


            }


            function compare(a, b) {
                if (a.createddate < b.createddate)
                    return 1;
                if (a.createddate > b.createddate)
                    return -1;
                return 0;
            }

            m1.sort(compare);
            m2.sort(compare);
            m3.sort(compare);
            m4.sort(compare);


            createMarkerButton(m1, "ul1");
            createMarkerButton(m2, "ul2");
            createMarkerButton(m3, "ul3");
            createMarkerButton(m4, "ul4");

            function createMarkerButton(marker, ulla) {
                marker.forEach(function (entry) {

                    var ul = document.getElementById(ulla);
                    var li = document.createElement("li");
                    ul.appendChild(li);
                    //var title = marker.getTitle();
                    li.innerHTML = entry.date + entry.title;
                    //div.appendChild(ul);

                    //Trigger a click event to marker when the button is clicked.
                    google.maps.event.addDomListener(li, "click", function () {
                        entry.setAnimation(google.maps.Animation.BOUNCE);
                        setTimeout(function () {
                            entry.setAnimation(null);
                        }, 750);
                        google.maps.event.trigger(entry, "click");
                    });
                });
            }

            function formatDate(date) {
                var months = [
                    "Januari", "Februari", "Mars", "April", "Mars", "Juni", "Juli", "Augusti", "September", "Oktober", "November", "December"
                ];

                //Remove /Date
                date = date.replace("/Date(", "");
                date = date.replace(")/", "");

                //Make it into an integer and format it nicely
                date = parseInt(date, 10);
                date = new Date(date);
                date = date.getDate() + " " + months[date.getMonth()] + " " + date.getFullYear();

                return date;
            }

        }

    });

}



