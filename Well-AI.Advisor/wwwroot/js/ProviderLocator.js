kendo.ui.progress.messages = {
    loading: "Loading..."
};
function displayLoading(target, bool) {
    var element = $(target);
    kendo.ui.progress(element, bool);
}

var _operatorId = '';
var _userId = '';

function displayLoading(target, bool) {
    var element = $(target);
    kendo.ui.progress(element, bool);
}


$(document).ready(function () {

   // mappreview_op.on('idle', function () {
   //     mappreview_op.resize()
   //
   // });
   // debugger;
    if ($("#hdn_userId").val() != '') {
        LoadDriverLocation($("#hdn_userId").val(), $("#hdn_operatorId").val(), '');
    }
   

  //  debugger;
 
    //alert(' Driver Id :'+$("#hdnDriverId").val());

  //  LoadDriverLocation();
});

function LoadDriverLocation(userId, operatorId, userName) {
    debugger;
    displayLoading("#map", true);

    try {


        //var userId = $("#hdnCurrentUser").val();
        //debugger;
        if (userId == '') {
            userId = $("#hdn_userId").val();
        }
        if (operatorId == '') {
            operatorId = $("#hdn_operatorId").val();
        }


        console.log('LoadUserLocation button cliked');
        $.ajax({
            // url: '/DispatchSRV/GetUserCurrentLocation?userId=' + userId,
            url: '/DispatchSRV/GetUserCurrentRoutes?userId=' + userId,
            type: 'GET',
            dataType: "json",
            contentType: "application/json; chartset=uft-8",
            success: function (response) {
                //  debugger;
                $.ajax({
                    // url: '/DispatchSRV/GetUserCurrentLocation?userId=' + userId,
                    url: '/Dispatch/GetOperatorsharedetails_Providerlocator?userId=' + userId,
                    type: 'GET',
                    dataType: "json",
                    contentType: "application/json; chartset=uft-8",
                    success: function (results) {
                        debugger;
                        $(".marker_preview_op").remove();
                        $(".markerstart_preview_op").remove();
                        $(".truckcolor_preview").remove();
                        if (userName == "") {
                            userName = results[0].username;
                        }
                        if (response.message == null || response.message == undefined || response.message == "") {
                            var locationResponse = response;
                            //$(".marker_preview_op").remove();
                            //$(".markerstart_preview_op").remove();
                            //$(".truckcolor_preview").remove();

                            for (rl = 0; rl < results.length; rl++) {
                                updateRoutesproviderlocation(response.destinations, response.location, userName, results[rl]);
                            }
                          
                            displayLoading("#map", false);
                            //updateRoutes(response.destinations, response.location);

                            //if (mappreview_op.getSource("routepreviewsrc_op") == undefined) {
                            //    mappreview_op.addSource('routepreviewsrc_op', {
                            //        'type': 'geojson',
                            //        'data': {
                            //            'type': 'Feature',
                            //            'properties': {},
                            //            'geometry': {
                            //                'type': 'LineString',
                            //                'coordinates': [
                            //                ]
                            //            }
                            //        }
                            //    });
                            //    if (mappreview_op.getLayer('routepreview_op')) {
                            //        mappreview_op.addLayer({
                            //            'id': 'routepreview_op',
                            //            'type': 'line',
                            //            'source': 'routepreviewsrc_op',
                            //            'layout': {
                            //                'line-join': 'round',
                            //                'line-cap': 'round'
                            //            },
                            //            'paint': {
                            //                'line-color': '#888',
                            //                'line-width': 8
                            //            }
                            //        });
                            //    }
                            //}
                            // mappreview.getSource("routepreviewsrc").setData(responsedata);

                            //  UpdateLocation(locationResponse, userName);


                        } else {
                            displayLoading("#map", false);
                            planalert(response.message);

                        }
                    },
                });

                //debugger;
                console.log(response);

            },
            error: function (xhr, status, error) {
                displayLoading("#map", false);
                planalert(xhr.responseText, "Error");
            }
        })
    } catch
    {
        displayLoading("#map", false);
    }
}

function resizeGrid() {

    var gridElement = $("#Dispatch"),
        dataArea = gridElement.find(".k-grid-content"),
        gridHeight = gridElement.innerHeight(),
        otherElements = gridElement.children().not(".k-grid-content"),
        otherElementsHeight = 0;

    otherElements.each(function () {
        otherElementsHeight += $(this).outerHeight();
    });

    console.log('gridHeight - parseFloat(otherElementsHeight)' + gridHeight - parseFloat(otherElementsHeight));
    //dataArea.height(parseInt($(window).height())-300)

    $.when(resize()).done(function (x) {
      
        setTimeout(function () {

            $("#map").height(parseInt($("#Dispatch").height()));
        }, 4000);

    });



}
function resize() {
    var gridElement = $("#Dispatch"),
        dataArea = gridElement.find(".k-grid-content"),
        gridHeight = gridElement.innerHeight(),
        otherElements = gridElement.children().not(".k-grid-content"),
        otherElementsHeight = 0;   
    $("#Dispatch").height(627);
    dataArea.height(627);
    $("#map").height(627);

}

function onDataBound(e) {

}

function UpdateLocation(locationResponse, userName) {

    var coordinates = [];

    if (locationResponse.message == null) {
        coordinates.push(locationResponse.longitude);
        coordinates.push(locationResponse.latitude);

        const el = document.createElement('div');
        //el.className = 'fa fa-truck fa-2x truckcolor_preview';
        el.className = 'truckcolor';

        new mapboxgl.Marker(el)
            .setLngLat(coordinates)
            .setPopup(
                new mapboxgl.Popup({ offset: 25 }) // add popups
                    .setHTML(
                        `<h3>User</h3><p>Name : ${userName}</p>`
                    )
            )
            .addTo(mappreview);

        //ETA_Timelocation_Preview(destinations, location);

        //debugger
        mappreview.flyTo({
            center: [locationResponse.longitude, locationResponse.latitude],
            zoom: 8,
            speed: 5,
        });

    } else {

        //coordinates.push("-102.36896137371849");
        //coordinates.push("31.846556737999546");

        //// add markers to map

        //const el = document.createElement('div');
        //el.className = 'fa fa-truck fa-2x truckcolor_preview';
        //// make a marker for each feature and add it to the map
        //new mapboxgl.Marker(el)
        //    .setLngLat(coordinates)
        //    .addTo(map);
        //map.flyTo({
        //    center: [locationResponse.longitude, locationResponse.latitude],
        //    zoom: 16,
        //    speed: 5,
        //});
    }

}



function updateRoutesproviderlocation(destinations, location, userName, results) {

                $(".marker").remove();
                $(".markerstart").remove();
                $(".truckcolor").remove();

   // displayLoading("#mappreview_op", true);
    try {
        //debugger;
        var responsedata = {};

        var centercoordinates = [];

        if (destinations.length > 0) {
            responsedata.type = 'Feature';
            responsedata.geometry = {};
            responsedata.geometry.type = "LineString";
            responsedata.geometry.coordinates = [];

            var k = 0;


            var matchlocation = false;


            for (m = 0; m < destinations.length; m++) {
                if (destinations[m].directions != null) {
                  
                
                    if (matchlocation == false) {


                        var legs = destinations[m].directions.legs;
                        if (legs != null) {
                            for (i = 0; i < legs.length; i++) {
                                if (legs[i].path != null) {
                                    for (j = 0; j < legs[i].path.length; j++) {
                                        if (legs[i].path[j].lng != null) {
                                            responsedata.geometry.coordinates[k] = {};
                                            responsedata.geometry.coordinates[k] = [legs[i].path[j].lng, legs[i].path[j].lat];
                                            //  if (m == 0 && i == 0 && j == 0) {
                                            centercoordinates[0] = legs[0].path[0].lng;
                                            centercoordinates[1] = legs[0].path[0].lat;
                                            //  }
                                            k = k + 1;
                                        }
                                    }
                                }
                            }
                        }

                        if (destinations[m].activity_id == results.activityid) {
                            matchlocation = true;
                        }
                    }
                }
            }


        }

        //else {

        //    responsedata.type = 'Feature';
        //    responsedata.geometry = {};
        //    responsedata.geometry.type = "LineString";
        //    responsedata.geometry.coordinates = [];
        //}

        console.log('responsedata ' + responsedata);
        console.log('responsedata json ' + JSON.stringify(responsedata));
        var jsonobj = JSON.parse('{  "type": "Feature",  "geometry": {"type": "LineString","coordinates": [[-100.483696, 37.833818],[-78.483696, 40.833818]]},  "properties": {    "name": "Dinagat Islands"  }}');


        map.flyTo({
            center: centercoordinates,
            zoom: 6,
            speed: 5
        });

    


        //mappreview.flyTo({
        //    center: [locationResponse.longitude, locationResponse.latitude],
        //    zoom: 8,
        //    speed: 5,
        //});
      //  mappreview_op.getSource("routepreviewsrc_op").setData(responsedata);
        //if (mappreview_op.getSource("routepreviewsrc_op") != undefined) {
        map.getSource("routesrc").setData(responsedata);
        //}

        if (destinations.length > 0) {
            //  debugger;

            if (destinations[0].directions != null) {
                if (destinations[0].directions.legs != null) {
                    if (destinations[0].directions.legs[0].path != null) {
                        if (destinations[0].directions.legs[0].path[0].lng != null) {


                            // var etaval = ETA_Timelocation(destinations, location);
                            //  debugger;
                            var coordinates = [];
                            coordinates[0] = destinations[0].directions.legs[0].path[0].lng;
                            coordinates[1] = destinations[0].directions.legs[0].path[0].lat;

                            const el = document.createElement('div');
                            el.className = 'markerstart';


                            new mapboxgl.Marker(el)
                                .setLngLat(coordinates)
                                //.setPopup(
                                //    new mapboxgl.Popup({ offset: 25 }) // add popups
                                //        .setHTML(
                                //            `<h3>${destinations[0].destination.replace(',', ', ')}</h3><p>${destinations[0].destination.replace(',', ', ')}</p>`
                                //            /*   `<h3>${destinations[0].destination.place}</h3><p>${destinations[0].destination.place}</p>`*/
                                //        )
                                //)
                                .addTo(map);
                        }

                    }
                }
            }

            //debugger;
              //ETA_Timelocation(destinations, location);
            ETA_Timelocation_Providerlocation(destinations, location, results);
          



        }


        //Set user's current location
        var locationcoordinates = [];
        locationcoordinates[0] = location.longitude;
        locationcoordinates[1] = location.latitude;

        const el = document.createElement('i');
       // el.className = 'fa fa-truck fa-2x truckcolor_preview';
        el.className = 'truckcolor';

        //new mapboxgl.Marker(el)
        //    .setLngLat(locationcoordinates)
        //    .addTo(mappreview);

        new mapboxgl.Marker(el)
            .setLngLat(locationcoordinates)
            .setPopup(
                new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up' }) // add popups
                    .setHTML(
                       // `<h3>User</h3><p>Name : ${userName}</p>` 
//                        `<div class="pop-head" style="margin-top:5px"><label class="pop-head-font"
//style="margin-top:15px"><p>Name : ${userName}</p></label></div><div class="pop-bootom1" ">`
                        `<div class="pop-head" style="margin-top:5px"><label class="pop-head-font"
style="margin-top:15px">${userName}</label></div><div class="pop-bootom1" style="margin-top:5px;float:left">
<label>Latitude           :${location.latitude}</label>  <br>
<label>Longitude          :${location.longitude}</label>  <br>
<label>Time Remaining     :${location.time_remaining}</label>  <br>
<label>Distance Remaining :${location.distance_remaining}</label> <br>
<label>ETA    :${location.eta_timestamp_UT}</label>

</div>`

                    )
            )
            .addTo(map);

       
    }
    catch
    {
        displayLoading("#map", false);
       // displayLoading("#mappreview", false);
    }

}



function ETA_Timelocation_Providerlocation(destinations, location, results) {

    //  debugger;

    var totalkm = 0;
    var totaldistance = 0;
    var totalduration = 0;
    var totalmile = 0;

    var totalkm2 = 0;
    var totaldistance2 = 0;
    var totalduration2 = 0;
    var totalmile2 = 0;

    var truckstartplace = false;
    const today = new Date();


    const startDate = new Date();
    const endDate = new Date();
    var matchlocation2 = false;
    for (i = 0; i < destinations.length; i++) {


      

        if (matchlocation2 == false) {
            if (i == 0) {
                //  var v1test = destinations[0].destination.latitude.substr(0, 7);
                var destination_coordinateslatitude = destinations[0].destination_coordinates.latitude;
                var destination_coordinateslongitude = destinations[0].destination_coordinates.longitude;

                // destination_coordinates = Object { latitude: 32.065548, longitude: -103.534513 }

                var locationlatitude = location.latitude.toString();
                var locationlongitude = location.longitude.toString();

                var locationlatitude2 = locationlatitude.substr(0, 7);

                if (destination_coordinateslatitude + ',' + destination_coordinateslongitude ==
                    locationlatitude.substr(0, 7) + ',' + locationlongitude.substr(0, 7)) {
                    truckstartplace = true;

                }
                else {
                    truckstartplace = false;


                    if (destinations[i].directions.legs != null) {
                        for (j = 0; j < destinations[i].directions.legs.length; j++) {

                            if (destinations[i].directions.legs[j] != null) {
                                if (destinations[i].directions.legs[j].distance_miles != null) { totalmile += destinations[i].directions.legs[j].distance_miles; }
                                if (destinations[i].directions.legs[j].distance != null) { totaldistance += destinations[i].directions.legs[j].distance; }
                                if (destinations[i].directions.legs[j].duration != null) { totalduration += destinations[i].directions.legs[j].duration; }

                            }
                        }


                    }

                }
            }
            else {
                truckstartplace = false;
                if (destinations[i].directions.legs != null) {
                    for (j = 0; j < destinations[i].directions.legs.length; j++) {




                        if (destinations[i].directions.legs[j] != null) {

                            if (destinations[i].directions.legs[j].distance_miles != null) { totalmile += destinations[i].directions.legs[j].distance_miles; }
                            if (destinations[i].directions.legs[j].distance != null) { totaldistance += destinations[i].directions.legs[j].distance; }
                            if (destinations[i].directions.legs[j].duration != null) { totalduration += destinations[i].directions.legs[j].duration; }

                        }
                    }


                }



            }




            if (truckstartplace == true) {

                var markerPoints = destinations[i].destination_coordinates;//.destination_coordinates;
                // //debugger;
                var coordinates = [];
                coordinates[0] = markerPoints.longitude;
                coordinates[1] = markerPoints.latitude;


                const el = document.createElement('div');
                el.className = 'markerstart_preview_op';

                //new mapboxgl.Marker(el)
                //    .setLngLat(coordinates)
                //    .setPopup(
                //        new mapboxgl.Popup({ offset: 25 }) // add popups
                //            .setHTML(
                //                `<h3>${destinations[i].destination}</h3><p>${destinations[i].destination}</p>`

                //            )
                //    )
                //    .addTo(mappreview_op);
            }
            else {

                //debugger;

                var duration_mins = totalduration / 60;



                endDate.setMinutes(endDate.getMinutes() + duration_mins);
                //const days_ofduration = parseInt((endDate - today) / (1000 * 60 * 60 * 24));
                //const hours_ofduration = parseInt(Math.abs(endDate - today) / (1000 * 60 * 60) % 24);
                //const minutes_ofduration = parseInt(Math.abs(endDate.getTime() - today.getTime()) / (1000 * 60) % 60);
                //const seconds_ofduration = parseInt(Math.abs(endDate.getTime() - today.getTime()) / (1000) % 60);

                const days_ofduration = parseInt((endDate - startDate) / (1000 * 60 * 60 * 24));
                const hours_ofduration = parseInt(Math.abs(endDate - startDate) / (1000 * 60 * 60) % 24);
                const minutes_ofduration = parseInt(Math.abs(endDate.getTime() - startDate.getTime()) / (1000 * 60) % 60);
                const seconds_ofduration = parseInt(Math.abs(endDate.getTime() - startDate.getTime()) / (1000) % 60);

                startDate.setMinutes(startDate.getMinutes() + duration_mins);

                //-------------------

                var Total_Duration = "";
                if (days_ofduration != 0) {
                    Total_Duration = days_ofduration + ' days ,';
                }
                else if (hours_ofduration != 0) {
                    Total_Duration = Total_Duration + hours_ofduration + ' hrs ,' + minutes_ofduration + ' mins ';
                }
                else if (minutes_ofduration != 0) {
                    Total_Duration = Total_Duration + minutes_ofduration + ' mins ';
                }
                else {
                    Total_Duration = "";
                }

                var Total_Distance = Math.floor(totaldistance / 1000) + ' km';

                //--------------------------
                var ampm = endDate.getHours() >= 12 ? 'PM' : 'AM';
                // var ETADatetime = moment(endDate).format('MM/DD/YYYY HH:mm') + ' ' + ampm;
                var ETADatetime = moment(endDate).format('MM/DD/YYYY HH:mm');
                // var ETADatetime = moment(endDate, 'MM/DD/YYYY HH:mm tt').format("MM/DD/YYYY HH:mm tt");
                var Totalmiles = "";
                if (totalmile > 0) {
                    Totalmiles = Math.floor(totalmile) + ' miles';
                }
                // var  Totalmiles = Math.floor( totalmile)+' miles';
                //var Totalmiles =totalmile + ' miles';
                //---------------
                var markerPoints = destinations[i].destination_coordinates;//.destination_coordinates;
                // //debugger;
                var coordinates = [];
                coordinates[0] = markerPoints.longitude;
                coordinates[1] = markerPoints.latitude;

                //const el = document.createElement('div');
                //el.className = 'marker_preview';
                //new mapboxgl.Marker(el)
                //    .setLngLat(coordinates)
                //    .setPopup(
                //        new mapboxgl.Popup({ offset: 25 }) // add popups
                //            .setHTML(
                //                `<h4>${destinations[i].destination.place}</h4><p>${destinations[i].destination.place}</p><p>Total Duration :${Total_Duration}</p>
                //   <p>Total Km :${Total_Distance}</p>`
                //            )
                //    )
                //    .addTo(map);

                if (totalduration == 0) {
                    ETADatetime = "";
                }

              

               
            }

            //-------------
         
            if (destinations[i].activity_id == results.activityid) {
                matchlocation2 = true;
                const el = document.createElement('div');
                el.className = 'marker_preview_op';
              
                new mapboxgl.Marker(el)
                    .setLngLat(coordinates)
                    .setPopup(
                        new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up-op' }) // add popups
                            .setHTML(
                                //                            `<h3>${destinations[i].destination}</h3><p>${destinations[i].destination}</p> <p>ETA :${ETADatetime}</p> <p>Total Duration :${Total_Duration}</p>
                                //<p>Total Km :${Total_Distance}</p>`

//                                `<h3>${destinations[i].destination}</h3><p>${destinations[i].destination}</p>  
//<p>ETA :${ETADatetime}</p> <p>Total Destinations:${i + 1}</p>  <p>Total Duration :${Total_Duration}</p>
//<p>Total Miles :${Totalmiles}</p>`


                                    `<div class="pop-head" style="margin-top:5px"><label class="pop-head-font"
style="margin-top:15px">${destinations[i].destination}</label></div><div class="pop-bootom1" style="margin-top:5px;float:left">

<label>ETA            :${ETADatetime}</label>  <br>
<label>Total Duration :${Total_Duration}</label> <br>
<label>Total Miles    :${Totalmiles}</label></div>`

                            )
                    )
                    .addTo(map);

             
            }

            totaldistance = 0;
            totalmile = 0;
            totalduration = 0;
            ETADatetime = "";
            Total_Distance = "";
            Total_Duration = "";
        }
    }

}

function planalert(content, alerttitle) {
    $("<div></div>").kendoAlert({
        title: alerttitle,
        content: content
    }).data("kendoAlert").open();
}