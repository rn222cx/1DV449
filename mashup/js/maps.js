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

                var priority = obj.priority;
                var category = obj.category;
                var readableDate = doDateReadable(obj.createddate);

                if (category == 0) {
                    category = "Vägtrafik";
                }
                if (category == 1) {
                    category = "Kollektivtrafik";
                }
                if (category == 2) {
                    category = "Planerad störning";
                }
                if (category == 3) {
                    category = "Övrigt";
                }

                var color;

                if (priority == 1) {
                    color = 'http://maps.google.com/mapfiles/ms/icons/green-dot.png';
                }
                if (priority == 2) {
                    color = 'http://maps.google.com/mapfiles/ms/icons/yellow-dot.png';
                }
                if (priority == 3) {
                    color = 'http://maps.google.com/mapfiles/ms/icons/blue-dot.png';
                }
                if (priority == 4) {
                    color = 'http://maps.google.com/mapfiles/ms/icons/pink-dot.png';
                }
                if (priority == 5) {
                    color = 'http://maps.google.com/mapfiles/ms/icons/red-dot.png';
                }


                myLatlng = new google.maps.LatLng(obj.latitude, obj.longitude);

                allMarkers = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    icon: color,
                    date: readableDate,
                    createddate: obj.createddate,
                    animation: google.maps.Animation.DROP,
                    title: obj.title,
                    html: '<div class="markerPop">' +
                    '<h4>' + obj.title + '</h4>' +
                    '<p><b>Datum: </b>' + readableDate + '</p>' +
                    '<p><b>Beskrivning: </b>' + obj.description + '</p>' +
                    '<p><b>Kategori: </b>' + category + '</p>' +
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

            // Display number of event on the buttons
            document.getElementById("markCount1").innerHTML = " " + markerArr1.length;
            document.getElementById("markCount2").innerHTML = " " + markerArr2.length;
            document.getElementById("markCount3").innerHTML = " " + markerArr3.length;
            document.getElementById("markCount4").innerHTML = " " + markerArr4.length;


            markerArr1.sort(sortAsDate);
            markerArr2.sort(sortAsDate);
            markerArr3.sort(sortAsDate);
            markerArr4.sort(sortAsDate);


            createListOfMarkers(markerArr1, "ul1");
            createListOfMarkers(markerArr2, "ul2");
            createListOfMarkers(markerArr3, "ul3");
            createListOfMarkers(markerArr4, "ul4");
        }

    });


    function sortAsDate(a, b) {
        if (a.createddate < b.createddate)
            return 1;
        if (a.createddate > b.createddate)
            return -1;
        return 0;
    }

    function createListOfMarkers(marker, ulTag) {
        var ul = document.getElementById(ulTag);

        marker.forEach(function (entry) {
            var li = document.createElement("li");
            ul.appendChild(li);
            //var title = marker.getTitle();
            li.innerHTML = entry.date + " " + entry.title;

            //Trigger a click event to marker when the list item is clicked.
            google.maps.event.addDomListener(li, "click", function () {
                entry.setAnimation(google.maps.Animation.BOUNCE);
                setTimeout(function () {
                    entry.setAnimation(null);
                }, 750); // marker bounce one time
                google.maps.event.trigger(entry, "click");
            });
        });
    }

    function doDateReadable(date) {
        var months = [
            "Jan", "Feb", "Mar", "Apr", "Mar", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec"
        ];

        //Remove unwanted strings
        date = date.replace("/Date(", "");
        date = date.replace(")/", "");

        //convert it into an integer and format it to an date
        date = parseInt(date);
        date = new Date(date);
        hour = date.getHours();
        hour = ("0" + hour).slice(-2);
        minute = date.getHours();
        minute = ("0" + minute).slice(-2);
        date = date.getFullYear() + "-" + months[date.getMonth()] + "-" + date.getDate()
            + " " + hour + ":" + minute;

        return date;
    }

    // Toggle markers and list when click
    function toggleOnClick(index) {
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

    for (var i = 1; i < 5; i++) toggleOnClick(i);


}



