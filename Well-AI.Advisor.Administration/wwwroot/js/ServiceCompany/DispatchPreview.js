


//------------------------------------------
//debugger;
var DispatchListBox2 = $("#DispatchList").data("kendoListBox");

var DispatchListBox = $("#DispatchList").data("kendoListBox");
var dispatchlistarray = [];
var deleteddispatchlistarray = [];





function setDiscontinued(ev, flag) {
    // var DispatchList = $("#DispatchList").data("kendoListBox");
    //debugger;
    var removedItems = ev.dataItems;
    for (var i = 0; i < removedItems.length; i++) {
        DispatchListBox.remove(removedItems[i].uid);

    }

    //    var text = $("#remove-textbox").val().toLowerCase();
    //var items = DispatchList.items();
    //for (var i = 0; i < items.length; i++) {
    //    var dataItem = DispatchList.getByUid.dataItem(items[i]);
    //        if (dataItem.ProductName.toLowerCase().indexOf(text) >= 0) {
    //            DispatchList.remove(items[i]);
    //        }
    //    }


}





$(document).ready(function () {

 //   debugger;
   

    //var Vb_ShareRoute = ViewBag.ShareRoute;
    //var Vd_ShareRoute = ViewData["ShareRoute"];

    $("#divRigSearch").css("display", "none");
    $("#divApiSearch").css("display", "block");

    $("#numAPI").prop('disabled', false).removeClass("k-state-disabled");
    $("#customer").prop('disabled', true).addClass("k-state-disabled");
    $("#api").prop('disabled', true).addClass("k-state-disabled");
    $("#rig").prop('disabled', true).addClass("k-state-disabled");
    $("#wellName").prop('disabled', true).addClass("k-state-disabled");
    $("#rigName").prop('disabled', true).addClass("k-state-disabled");


    $("#locationname").prop('disabled', true).addClass("k-state-disabled");
    $("#address").prop('disabled', true).addClass("k-state-disabled");
    $("#zip").prop('disabled', true).addClass("k-state-disabled");
    $("#city").prop('disabled', true).addClass("k-state-disabled");
    $("#state").prop('disabled', true).addClass("k-state-disabled");
    $("#latitude").prop('disabled', true).addClass("k-state-disabled");
    $("#longitude").prop('disabled', true).addClass("k-state-disabled");
    $("#ScheduledArrival").closest("span.k-datetimepicker").width(200);

   // mapboxgl.accessToken = 'pk.eyJ1Ijoid2VsbC1haSIsImEiOiJjbDFxZ25yMnIwN3c3M2JzNml1dWdpMmR0In0.l-lDzOnU03tFScjiMkRObg';
  

     mappreview = new mapboxgl.Map({
        container: 'mappreview',
        style: 'mapbox://styles/mapbox/streets-v11',
        center: [-102.36896137371849, 31.846556737999546],
        zoom: 4,
        pixelRatio: window.devicePixelRatio || 1
    });




    //$.when(mappreview.on('load', () => {
    //    //   //debugger;
    //    mappreview.addSource('routepreview', {
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
    //    mappreview.addLayer({
    //        'id': 'routepreview',
    //        'type': 'line',
    //        'source': 'routepreview',
    //        'layout': {
    //            'line-join': 'round',
    //            'line-cap': 'round'
    //        },
    //        'paint': {
    //            'line-color': '#888',
    //            'line-width': 8
    //        }
    //    });

    //    mappreview.addControl(new mapboxgl.NavigationControl());

    //    mappreview.resize();

    //})
    //).then(function (data, textStatus, jqXHR) {
    //    //LoadAllUserDestinations();
    //    //$.when(loadroutes()).done(function (x) {

    //    //    setTimeout(function () {
    //    //        $(".mapboxgl-map").height(parseInt($("#Dispatch").height()));
    //    //    }, 4000);
    //    //});
    //});


    mappreview.on('idle', function () {
        mappreview.resize()

    })


    if (mappreview != undefined) {

    }


    debugger;
    //--------------------------

    var userid = $("#lbluserid").text();
    $.ajax({
        url: "/DispatchSRV/GetDispatchList_Preview?userId=" + userid,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            dispatchlistarray = [];
            debugger;

            if (data.Data.length > 0) {


                $.each(data.Data, function (key, value) {

                    var dispatchlistarrayvalues = {
                        createddate: value.createddate,
                        locationname: value.locationname,
                        customer: value.customer,
                        address: value.address,
                        city: value.city,
                        state: value.state,
                        zip: value.zip,
                        latitude: parseFloat(value.latitude),
                        longitude: parseFloat(value.longitude),
                        dispatchnotes: value.dispatchnotes,
                        dispatchid: value.dispatchid,
                        userid: value.userid,
                        api: value.api,
                        wellname: value.wellname,
                        rigname: value.rigname,
                        rigid: value.rigid,
                        wellid: value.wellid,
                        scheduledArrivalDate: value.scheduledArrivalDate

                        //  latitude_2: value.latitude,
                        //  longitude_2: value.longitude
                    };
                    dispatchlistarray.push(dispatchlistarrayvalues);

                });
                LoadAllUserPathspriview2();
            }
        }
    });


   

function LoadAllUserPathspriview() {
    //  //debugger;
    var destinations = [];
    var location = [];
    var userid = $("#lbluserid").text();
    //-------
    $.each(dispatchlistarray, function (key, value) {

        var destinationsvalue = {

            activity_id: value.dispatchid,
            destination: "Coordinates:" + value.latitude + "," + value.longitude,
            destination_coordinates: {
                latitude: parseFloat(value.latitude),
                longitude: parseFloat(value.longitude)
            },
            directions: {
                legs: [
                    {
                        path:
                            [
                                {
                                    intersection: null,
                                    lat: parseFloat(value.latitude),
                                    lng: parseFloat(value.longitude),
                                    maneuver: { type: 'depart', instruction: 'Drive northeast on I 20 Business/East 2nd Street.', bearing_after: 60, mapbox_streets_v8: null, bearing_before: 0 },
                                    point_id: 0,
                                    speed: null,
                                    summary: "TX 158, TX 128"
                                }
                            ]
                    }
                ]
            },
            origin: value.locationname + ", " + value.city + ", " + value.state,
            rig_id: value.rigid ? '' : "0",
            user_path: null,
            well_id: value.wellid ? '' : "0"



        };




        destinations.push(destinationsvalue);


        //var locationvalue = {
        //    activity: null,
        //    activity_id: value.dispatchid,
        //    details: null,
        //    latitude: value.latitude_2,
        //    logged_dt: "2022-04-12T14:10:01",
        //    longitude: value.longitude_2,
        //    message: null,
        //    user_id: value.dispatchid,
        //};
        //location.push(locationvalue);

    });

    //--------------

    console.log('LoadUserLocation button cliked');
    $.ajax({
        url: '/DispatchSRV/GetUserCurrentLocation?userId=' + $("#hdnCurrentUser").val(),
        type: 'GET',
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {



            //------
          debugger;
            console.log(response);
            if (response.message == null || response.message == undefined || response.message == "") {
                var locationResponse = response;


                //geojson = destinations;//.legs;

                var lastcoordinates = [];
                var zoomsize = 4;

                var currentMarkers = [];


                $(".marker_preview").remove();
                $(".markerstart_preview").remove();
                $(".truckcolor_preview").remove();
                updateRoutes_Preview(destinations, response);




            } else {
                planalert(response.message);

            }
        },
        error: function (xhr, status, error) {
            planalert(xhr.responseText, "Error");
        }
    })

    //------------------



}


function updateRoutes_Preview(destinations, location) {



  //  debugger;

    //------------------
    var responsedata = {};

    var centercoordinates = [];

    //make the destinations array with existing destination item and new destination


    //debugger;
    if (destinations.length > 0) {
        responsedata.type = 'Feature';
        responsedata.geometry = {};
        responsedata.geometry.type = "LineString";
        responsedata.geometry.coordinates = [];

        var k = 0;
        for (m = 0; m < destinations.length; m++) {
           // if (destinations[m] != null) {
            var legs = destinations[m].directions.legs;
                if (destinations[m].directions.legs != null) {
                    for (i = 0; i < legs.length; i++) {
                        if (legs[i] != null) {
                            if (legs[i].path != null) {
                                for (j = 0; j < legs[i].path.length; j++) {
                                    responsedata.geometry.coordinates[k] = {};
                                    responsedata.geometry.coordinates[k] = [legs[i].path[j].lng, legs[i].path[j].lat];


                                    if (m == 0 && i == 0 && j == 0) {

                                        centercoordinates[0] = legs[0].path[0].lng;
                                        centercoordinates[1] = legs[0].path[0].lat;

                                    }
                                    k = k + 1;
                                }
                            }
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


    mappreview.flyTo({
        center: centercoordinates,
        zoom: 6,
        speed: 5
    });


    if (mappreview.getSource("routepreviewsrc") != undefined) {
        mappreview.getSource("routepreviewsrc").setData(responsedata);
    }



    if (destinations.length > 0) {

        if (destinations[0].directions.legs[0] != null) {
            if (destinations[0].directions.legs[0].path[0] != null) {
                // //debugger;
                var coordinates = [];
                coordinates[0] = destinations[0].directions.legs[0].path[0].lng;
                coordinates[1] = destinations[0].directions.legs[0].path[0].lat;

                const el = document.createElement('div');
                el.className = 'markerstart_preview';


                new mapboxgl.Marker(el)
                    .setLngLat(coordinates)
                    .setPopup(
                        new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up' }) // add popups
                            .setHTML(
                            /* `<h3>${destinations[0].destination}</h3><p>${destinations[0].destination}</p>`*/
                                `<div class="pop-head2" style="margin-top:5px"><label class="pop-head-font"
style="margin-top:15px">${destinations[0].destination}</label></div><div class="pop-bootom1">`
                            )
                    )
                    .addTo(mappreview);

                ETA_Timelocation_Preview(destinations, location);
                //for (m = 0; m < destinations.length; m++) {
                //    var markerPoints = destinations[m].destination_coordinates;
                //    // //debugger;
                //    var coordinates = [];
                //    coordinates[0] = markerPoints.longitude;
                //    coordinates[1] = markerPoints.latitude;

                //    if (Math.abs(markerPoints.longitude - location.longitude) >= 0.0001 && Math.abs(markerPoints.latitude - location.latitude) >= 0.0001) {
                //        const el = document.createElement('div');
                //        el.className = 'marker_preview';

                //        new mapboxgl.Marker(el)
                //            .setLngLat(coordinates)
                //            .setPopup(
                //                new mapboxgl.Popup({ offset: 25 }) // add popups
                //                    .setHTML(
                //                        `<h3>${destinations[m].destination}</h3><p>${destinations[m].destination}</p>`
                //                    )
                //            )
                //            .addTo(mappreview);
                //    }


                //}
            }
        }


        //Set user's current location
        var locationcoordinates = [];
        locationcoordinates[0] = location.longitude;
        locationcoordinates[1] = location.latitude;

        const el = document.createElement('i');
        el.className = 'fa fa-truck fa-2x truckcolor_preview';

        new mapboxgl.Marker(el)
            .setLngLat(locationcoordinates)
            .addTo(mappreview);

        //-----------------------


    }


    }


   


    //------------------------

    $(".addpreview").click(function () {
       // debugger;

        //debugger;
        var dispatchList = "";
        var dispatchListid = "";
        dispatchList = $("#rigName").val() + ' ' + $("#wellName").val() + ' ' + $("#locationname").val();
        dispatchList = dispatchList.trim();
        dispatchListid = "-" + count++;

        if (dispatchList != "") {


            //if ($("#wellId").val() != undefined && $("#wellId").val()) {
            //    dispatchListid = $("#wellId").val();
            //}
            //if ($("#rigId").val() != undefined && $("#rigId").val()) {
            //    dispatchListid = $("#rigId").val();
            //}

            //  $("#DispatchList").append("<option value='" + dispatchListid + "'>" + dispatchList + "</option>")


            var Product = kendo.data.Model.define({
                id: "dispatchid",
                fields: {
                    "rigwellandlocation": {
                        type: "string"
                    }
                }
            });
            var DispatchList = $("#DispatchList").data("kendoListBox");

            DispatchList.add(new Product({
                rigwellandlocation: dispatchList,
                dispatchid: dispatchListid
            }));


            setDispatcharray(dispatchListid);


            ClearValuespreview();

            LoadAllUserPathspriview();
        }
    });
   
   
   
   /// $(".k-grid-update").click(function () {
    //$('.k-grid-update').one('click', function (event) {
    // // debugger;
    //        event.preventDefault();
    //        //debugger;
    //        $.ajax({
    //            url: "/DispatchSRV/AddNewDispatch_V2",
    //            type: "POST",
    //            dataType: "json",
    //            data: JSON.stringify(dispatchlistarray),
    //            contentType: "application/json; chartset=uft-8",
    //            success: function (response) {

    //                $.ajax({
    //                    url: "/DispatchSRV/DeleteDispatch_V2",
    //                    type: "POST",
    //                    dataType: "json",
    //                    data: JSON.stringify(deleteddispatchlistarray),
    //                    contentType: "application/json; chartset=uft-8",
    //                    success: function (response) {

    //                        event.stopPropagation();

    //                        var window = $("#DispatchAssignWindow").data("kendoWindow");
    //                        window.close();

    //                        var gridObj = $('#Dispatch').data('kendoGrid');
    //                        gridObj.dataSource.read();
    //                        gridObj.refresh();

    //                        LoadAllUserPaths();

    //                        //   LoadDispatchRouteCurrentUsersmaproutpath();
    //                    },
    //                    error: function (xhr, status, error) {
    //                        planalert(xhr.responseText, "Error");
    //                    }
    //                });

    //                //var gridObj = $('#Dispatch').data('kendoGrid');
    //                //gridObj.dataSource.read();
    //                //gridObj.refresh();

    //                //var window = $("#DispatchAssignWindow").data("kendoWindow");
    //                //window.close();
    //            },
    //            error: function (xhr, status, error) {
    //                planalert(xhr.responseText, "Error");
    //            }
    //        });

    //});

    $(".k-grid-cancel").click(function () {
      //  debugger;

        //delete mappreview;
        //$("#mappreview").remove();
        //delete mappreview;
        //mappreview.remove();
        //$("#mappreview").empty();
        var window = $("#DispatchAssignWindow").data("kendoWindow");
        window.close();
       
    });

    var userId = $("#hdnCurrentUser").val();

    $.ajax({
        url: '/DispatchSRV/GetActiveUserNotes?userId=' + userId,
        type: 'GET',
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response, textStatus, jqXHR) {

            $("#hdnDispatchNotes").val(response.data);
            $("#instructions").val($("#hdnDispatchNotes").val());
        },
        error: function (xhr, status, error) {
            planalert(xhr.responseText, "Error");
        }
    });

    $(document).on("keyup", "#latitude", function () {
        $("#latitude").val($("#latitude").val().replace(/[^0-9^.-\s]/gi, '').replace(/[_\s]/g, '').toLowerCase());
    });

    $(document).on("keyup", "#longitude", function () {
        $("#longitude").val($("#longitude").val().replace(/[^0-9^.-\s]/gi, '').replace(/[_\s]/g, '').toLowerCase());
    });

   

});

function ETA_Timelocation_Preview(destinations, location) {

  // debugger;

    var totalkm = 0;
    var totaldistance = 0;
    var totalduration = 0;
   // var totalmile = 0;

    var totalmile = "";
    var total_duration_times = "";

    var totalkm2 = 0;
    var totaldistance2 = 0;
    var totalduration2 = 0;
    var totalmile2 = 0;

    var truckstartplace = false;
    const today = new Date();
    const startDate = new Date();
    const endDate = new Date();
    for (i = 0; i < destinations.length; i++) {
        
        if (i == 0) {
            var v1test = destinations[0].destination.latitude.substr(0, 7);
         

            var locationlatitude = location.latitude.toString();
            var locationlongitude = location.longitude.toString();
            if (destinations[0].destination.latitude.substr(0, 7) + ',' + destinations[0].destination.longitude.substr(0, 7) ==
                locationlatitude.substr(0, 7) + ',' + locationlongitude.substr(0, 7)) {
                truckstartplace = true;

            }
            else {
                truckstartplace = false;
                //if (destinations[i].legs != null) {

                
                //    for (j = 0; j < destinations[i].legs.length; j++) {
                //        if (destinations[i].legs != null) {
                //            if (destinations[i].legs[j] != null) {

                //                if (destinations[i].legs[j].distance_miles != null) { totalmile += destinations[i].legs[j].distance_miles; }
                //                if (destinations[i].legs[j].distance != null) { totaldistance += destinations[i].legs[j].distance; }
                //                if (destinations[i].legs[j].duration != null) { totalduration += destinations[i].legs[j].duration; }

                //            }
                //        }


                //    }



                //}
                if (destinations != null) {

                    if (destinations[i].total_distance != null) { totaldistance  = destinations[i].total_distance; }
                    if (destinations[i].total_distance_display != null) { totalmile = destinations[i].total_distance_display; }
                    if (destinations[i].total_duration != null) { totalduration = destinations[i].total_duration; }
                    if (destinations[i].total_duration_display != null) { total_duration_times = destinations[i].total_duration_display; }
                    


                }
                             

            }
        }
        else {
            truckstartplace = false;
            //if (destinations[i].legs != null) {
            //    for (j = 0; j < destinations[i].legs.length; j++) {



            //        if (destinations[i].legs != null) {
            //            if (destinations[i].legs[j] != null) {

            //                if (destinations[i].legs[j].distance_miles != null) { totalmile += destinations[i].legs[j].distance_miles; }
            //                if (destinations[i].legs[j].distance != null) { totaldistance += destinations[i].legs[j].distance; }
            //                if (destinations[i].legs[j].duration != null) { totalduration += destinations[i].legs[j].duration; }

            //            }
            //        }



            //    }

            //}

            if (destinations != null) {

                if (destinations[i].total_distance != null) { totaldistance = destinations[i].total_distance; }
                if (destinations[i].total_distance_display != null) { totalmile = destinations[i].total_distance_display; }
                if (destinations[i].total_duration != null) { totalduration = destinations[i].total_duration; }
                if (destinations[i].total_duration_display != null) { total_duration_times = destinations[i].total_duration_display; }



            }

        }
        



        if (truckstartplace == true) {
            var markerPoints = destinations[i].destination;//.destination_coordinates;
            // //debugger;
            var coordinates = [];
            coordinates[0] = markerPoints.longitude;
            coordinates[1] = markerPoints.latitude;

            const el = document.createElement('div');
            el.className = '.markerstart_preview_new';

            new mapboxgl.Marker(el)
                .setLngLat(coordinates)
//                .setPopup(
//                    new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up' }) // add popups
//                        .setHTML(

//                        /* `<h3>${destinations[i].destination.place}</h3><p>${destinations[i].destination.place}</p>`*/

//                            `<div class="pop-head2" style="margin-top:5px"><label class="pop-head-font"
//style="margin-top:15px">${destinations[i].destination.place}</label></div><div class="pop-bootom1" style="margin-top:5px;float:left">`

//                        )
//                )
                .addTo(mappreview);
        }
        else {



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
            var ETADatetime = moment(endDate).format('MM/DD/YYYY HH:mm');// + ' ' + ampm;
            // var ETADatetime = moment(endDate, 'MM/DD/YYYY HH:mm tt').format("MM/DD/YYYY HH:mm tt");
            var Totalmiles ="";
            //if (totalmile > 0) {
            //     Totalmiles = Math.floor(totalmile) + ' miles';
            //}
         
           // var Totalmiles = totalmile + ' miles';
            //---------------
            var markerPoints = destinations[i].destination;//.destination_coordinates;
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

            const el = document.createElement('div');
            el.className = 'marker_preview';

            new mapboxgl.Marker(el)
                .setLngLat(coordinates)
                .setPopup(
                    new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up-op' }) // add popups
                        .setHTML(
//                            `<h4>${destinations[i].destination.place}</h4><p>${destinations[i].destination.place}</p>  <p>ETA :${ETADatetime}</p> <p>Total Duration :${Total_Duration}</p>
//<p>Total Km :${Total_Distance}</p>`

//                            `<h4>${destinations[i].destination.place}</h4><p>${destinations[i].destination.place}</p>  <p>ETA :${ETADatetime}</p> <p>Total Duration :${Total_Duration}</p>
//<p>Total Mile :${Totalmiles}</p>`


//                            `<h4>${destinations[i].destination.place}</h4><p>${destinations[i].destination.place}</p> 
                            //<p>ETA :${ETADatetime}</p> <p>Total Duration :${total_duration_times}</p>
//<p>Total Mile :${totalmile}</p>`

                            `<div class="pop-head" style="margin-top:5px"><label class="pop-head-font"
style="margin-top:15px">${destinations[i].destination.place}</label></div><div class="pop-bootom1" style="margin-top:5px;float:left">

<label>ETA            :${ETADatetime}</label>  <br>
<label>Total Duration :${total_duration_times}</label> <br>
<label>Total Miles    :${totalmile}</label></div>`

                        )
                )
                .addTo(mappreview);
        }

        //-------------
        totaldistance = 0;
        totalmile = 0;
        totalduration = 0;
        ETADatetime = "";
        Total_Distance = "";
        Total_Duration = "";


    }


    displayLoading("#DispatchAssignWindow", false);
}


//$(document).ready(function () {
//    $(window).resize(function () {
//        resizeGrid();
//    });
//    //resizeWrapper();
//    resizeGrid();
//});

window.addEventListener('resize', function (event) {
    var newWidth = window.innerWidth;
    var newHeight = window.innerHeight;
});

//------------------



function SaveDispatchUser() {
    debugger;

    if (dispatchlistarray.length == 0) {

        planalert("Dispatch Should not be empty.", "Alert!");
    }
    else {



        displayLoading("#DispatchAssignWindow", true);
        dispatchlistarray[0].dispatchnotes = $("#instructions").val();
        // debugger;
        $.ajax({
            url: "/DispatchSRV/AddNewDispatch_V2",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(dispatchlistarray),
            contentType: "application/json; chartset=uft-8",
            success: function (response) {

                //debugger;
                if (deleteddispatchlistarray.length > 0) {
                    $.ajax({
                        url: "/DispatchSRV/DeleteDispatch_V2?HistoryHeadId=" + response,
                        type: "POST",
                        dataType: "json",
                        data: JSON.stringify(deleteddispatchlistarray),
                        contentType: "application/json; chartset=uft-8",
                        success: function (response) {
                            //debugger;
                            //   debugger;
                            var gridObj = $('#Dispatch').data('kendoGrid');
                            gridObj.dataSource.read();
                            gridObj.refresh();

                            LoadDispatchRouteCurrentUsersmaproutpath2();

                            setTimeout(function () {

                                displayLoading("#DispatchAssignWindow", false);

                                var window = $("#DispatchAssignWindow").data("kendoWindow");
                                window.close();


                            }, 2000);

                            //   LoadDispatchRouteCurrentUsersmaproutpath();
                        },
                        error: function (xhr, status, error) {
                            displayLoading("#DispatchAssignWindow", false);
                            planalert(xhr.responseText, "Error");
                        }
                    });
                }
                else {
                    //  debugger;
                    var gridObj = $('#Dispatch').data('kendoGrid');
                    gridObj.dataSource.read();
                    gridObj.refresh();

                    LoadDispatchRouteCurrentUsersmaproutpath2();

                    setTimeout(function () {



                        var window = $("#DispatchAssignWindow").data("kendoWindow");
                        window.close();

                    }, 2000);
                }

                var gridObj2 = $('#Dispatch').data('kendoGrid');
                gridObj2.dataSource.read();
                gridObj2.refresh();

                displayLoading("#DispatchAssignWindow", false);
                var window2 = $("#DispatchAssignWindow").data("kendoWindow");
                window2.close();

            },
            error: function (xhr, status, error) {
                planalert(error, "Error");
                displayLoading("#DispatchAssignWindow", false);
            }
        });
    }
}

function LoadAllUserPathspriview2() {
    debugger;
     displayLoading("#DispatchAssignWindow", true);
    if (dispatchlistarray.length != 0) {



        //----------------
        var destinations = [];
        var location = [];
        var userid = $("#lbluserid").text();
        var destinations_array = [];

        //-------
        $.each(dispatchlistarray, function (key, value) {

            var destinationsvalue = {

                activity_id: value.dispatchid,
                destination: "Coordinates:" + value.latitude + "," + value.longitude,
                destination_coordinates: {
                    latitude: parseFloat(value.latitude),
                    longitude: parseFloat(value.longitude)
                },
                directions: {
                    legs: [
                        {
                            path:
                                [
                                    {
                                        intersection: null,
                                        lat: parseFloat(value.latitude),
                                        lng: parseFloat(value.longitude),
                                        maneuver: { type: 'depart', instruction: 'Drive northeast on I 20 Business/East 2nd Street.', bearing_after: 60, mapbox_streets_v8: null, bearing_before: 0 },
                                        point_id: 0,
                                        speed: null,
                                        summary: "TX 158, TX 128"
                                    }
                                ]
                        }
                    ]
                },
                origin: value.locationname + ", " + value.city + ", " + value.state,
                rig_id: value.rigid ? '' : "0",
                user_path: null,
                well_id: value.wellid ? '' : "0"
            };

            if (value.wellid == "" && value.rigid == "") {

                if (value.zip == '') {
                    var destination = {


                        id: value.latitude + "," + value.longitude,
                        type: "location"
                    };
                    destinations_array.push(destination);
                }
                else {
                    var destination = {


                        id: value.address + "," + value.city + "," + value.state + "," + value.zip,
                        type: "location"
                    };
                    destinations_array.push(destination);
                }

            }
            else {
                var destination = {


                    id: value.wellid == 0 ? value.rigid : value.wellid,
                    type: value.wellid == 0 ? "rig" : "well"
                };
                destinations_array.push(destination);
            }

            //var destination = {
            //    id: value.wellid != 0 ? value.wellid : value.rigid != 0 ? value.rigid : (value.api != '' && value.api != 0) ? value.api : value.zip != '' ? value.zip : value.,
            //    type: value.wellid != 0 ? "well" : value.wellid != 0 ? "well"
            //};



            //old
            //var destination = {


            //    id: value.wellid == 0 ? value.rigid : value.wellid,
            //    type: value.wellid == 0 ? "rig" : "well"
            //};
            //if (value.wellid != "0" || value.rigid != "0") {
            //    destinations_array.push(destination);
            //}

            destinations.push(destinationsvalue);

        });

        //--------------


        //var destinationssimulationrequest = {
        //    origin: "Odessa, TX",
        //    priority: "travel_time",
        //    destinationsarray: destinations_array
        //}

        ///  if (dispatchlistarray[0].locationname != '')
        //   debugger;
        //var destinationssimulationrequest = {
        //origin: dispatchlistarray[0].locationname != '' ? dispatchlistarray[0].locationname : dispatchlistarray[0].latitude + ',' + dispatchlistarray[0].longitude,

        var destinationssimulationrequest = {
            origin: dispatchlistarray[0].latitude + ',' + dispatchlistarray[0].longitude,


            priority: "travel_time",
            destinationsarray: destinations_array
        }

        console.log('LoadUserLocation button cliked');

        //$.when(planJSON = DrillPlanDetails(wellId, tenantId, stageUpdate)).done(function (x) {
        $.ajax({
            url: '/DispatchSRV/GetSimulationMap',
            type: 'POST',
            dataType: "json",
            data: JSON.stringify(destinationssimulationrequest),
            contentType: "application/json; chartset=uft-8",
            success: function (response) {
                //  debugger;

                // updateRoutes_Preview2(destinations, response)
                console.log(response);
                if (response.message == null || response.message == undefined || response.message == "") {
                    var locationResponse = response.parse;
                    //geojson = destinations;//.legs;
                    var lastcoordinates = [];
                    var zoomsize = 4;
                    var currentMarkers = [];

                    var location = {
                        longitude: response.directions[0].origin.longitude,
                        latitude: response.directions[0].origin.latitude
                    }

                    $(".marker_preview").remove();
                    $(".markerstart_preview").remove();
                    $(".truckcolor_preview").remove();

                    //--------------------
                    updateRoutes_Preview2(response.directions, location, response.destinations);


                } else {
                    planalert(response.message);

                }
            },
            error: function (xhr, status, error) {
                planalert(xhr.responseText, "Error");
            }
        })

    }
    else {
        $(".marker_preview").remove();
        $(".markerstart_preview").remove();
        $(".truckcolor_preview").remove();
        displayLoading("#DispatchAssignWindow", false);
    }
}


function updateRoutes_Preview2(destinations, location, destinationNames) {

    //  debugger;
    try {


        //---------------------
        var responsedata = {};

        var centercoordinates = [];

        //make the destinations array with existing destination item and new destination


        // debugger;
        if (destinations.length > 0) {
            responsedata.type = 'Feature';
            responsedata.geometry = {};
            responsedata.geometry.type = "LineString";
            responsedata.geometry.coordinates = [];

            var k = 0;
            for (m = 0; m < destinations.length; m++) {
                var legs = destinations[m].legs;
                if (legs != null) {
                    for (i = 0; i < legs.length; i++) {
                        if (legs[i] != null) {
                            if (legs[i].path != null) {
                                for (j = 0; j < legs[i].path.length; j++) {

                                    if (legs[i].path[j] != null && legs[i].path[j] != null) {
                                        if (legs[i].path[j].lng != null && legs[i].path[j].lat != null) {
                                            responsedata.geometry.coordinates[k] = {};
                                            responsedata.geometry.coordinates[k] = [legs[i].path[j].lng, legs[i].path[j].lat];
                                        }
                                    }


                                    //  if (m == 0 && i == 0 && j == 0) {
                                    if (legs[0].path[0].lng != null) {
                                        centercoordinates[0] = legs[0].path[0].lng;
                                        centercoordinates[1] = legs[0].path[0].lat;

                                    }

                                    //  }
                                    k = k + 1;
                                }
                            }
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


        mappreview.flyTo({
            center: centercoordinates,
            zoom: 6,
            speed: 5
        });


        //if (mappreview.getSource("route") != undefined) {
        //    mappreview.getSource("route").setData(responsedata);
        //}
        if (mappreview.getSource("routepreviewsrc") != undefined) {
            mappreview.getSource("routepreviewsrc").setData(responsedata);
        }


        if (destinations.length > 0) {

            if (destinations != null) {
                if (destinations[0].legs != null) {
                    if (destinations[0].legs[0] != null) {
                        if (destinations[0].legs[0].path != null) {
                            if (destinations[0].legs[0].path[0] != null) {
                                if (destinations[0].legs[0].path[0].lat != null) {
                                    // debugger;
                                    var coordinates = [];
                                    coordinates[0] = destinations[0].legs[0].path[0].lng;
                                    coordinates[1] = destinations[0].legs[0].path[0].lat;

                                    const el = document.createElement('div');
                                    el.className = 'markerstart_preview';


                                    new mapboxgl.Marker(el)
                                        .setLngLat(coordinates)
                                        .setPopup(
                                            new mapboxgl.Popup({ offset: 25 }) // add popups
                                                .setHTML(
                                                    `<h3>${destinations[0].destination.place}</h3><p>${destinations[0].destination.place}</p>`
                                                )
                                        )
                                        .addTo(mappreview);

                                }
                            }
                        }
                    }
                }
            }


            ETA_Timelocation_Preview(destinations, location);
         
            //for (m = 0; m < destinations.length; m++) {
            //    var markerPoints = destinations[m].destination;//.destination_coordinates;
            //    // //debugger;
            //    var coordinates = [];
            //    coordinates[0] = markerPoints.longitude;
            //    coordinates[1] = markerPoints.latitude;

            //    if (Math.abs(markerPoints.longitude - location.longitude) >= 0.0001 && Math.abs(markerPoints.latitude - location.latitude) >= 0.0001) {
            //        const el = document.createElement('div');
            //        el.className = 'marker_preview';

            //        new mapboxgl.Marker(el)
            //            .setLngLat(coordinates)
            //            .setPopup(
            //                new mapboxgl.Popup({ offset: 25 }) // add popups
            //                    .setHTML(
            //                        `<h4>${destinations[m].destination.place}</h4><p>${destinations[m].destination.place}</p><p>Total Duration :${destinations[m].total_duration_display}</p><p>Total Display :${destinations[m].total_distance_display}</p>`
            //                    )
            //            )
            //            .addTo(mappreview);
            //    }

            //}


        }

        //Set user's current location
        var locationcoordinates = [];
        locationcoordinates[0] = location.longitude;
        locationcoordinates[1] = location.latitude;

        const el = document.createElement('i');
        el.className = 'fa fa-truck fa-2x truckcolor_preview';

        new mapboxgl.Marker(el)
            .setLngLat(locationcoordinates)
            .addTo(mappreview);

        if (parseFloat($(window).height()) > 766) {
            //alert('Map markers rendered' + $(window).height());
            $(".k-window div.k-window-content").css("overflow", "hidden");
        } else {
            $(".k-window div.k-window-content").css("overflow", "auto");
        }

    }
    catch
    {
        displayLoading("#DispatchAssignWindow", false);
    }
    // DispatchListBox.dataSource.read();
    // DispatchListBox.refresh();

}

//-------------------
function onRemove(ev, flag) {
    debugger;
  //  displayLoading("#DispatchAssignWindow", true);
    DispatchListBox = $("#DispatchList").data("kendoListBox");
    var user_id = $("#lbluserid").text();
  

    try {

        var removedItems = ev.dataItems;
        var dispatchidarray = "";

        for (var i = 0; i < removedItems.length; i++) {
            DispatchListBox.remove(removedItems[i].uid);
            dispatchidarray = removedItems[i].dispatchid;
            //  var dispatchlistarrayIndex=  dispatchlistarray.dispatchid.indexOf(removedItems[i].dispatchid)


            // dispatchlistarray.splice(dispatchlistarrayIndex, 1);
        }

       // deleteroutrcount = deleteroutrcount + dispatchlistarray.length;
        $.each(dispatchlistarray, function (key, value) {

            var dataItem_dispatchlistarray = dispatchlistarray[key]
            if (dataItem_dispatchlistarray.dispatchid == dispatchidarray) {

                // delete dispatchlistarray[key];

                var dispatchlistdeletedarrayvalues = {

                    createddate: dataItem_dispatchlistarray.createddate,
                    locationname: dataItem_dispatchlistarray.locationname,
                    customer: dataItem_dispatchlistarray.customer,
                    address: dataItem_dispatchlistarray.address,
                    city: dataItem_dispatchlistarray.city,
                    state: dataItem_dispatchlistarray.state,
                    zip: dataItem_dispatchlistarray.zip,
                    latitude: parseFloat(dataItem_dispatchlistarray.latitude),
                    longitude: parseFloat(dataItem_dispatchlistarray.longitude),
                    dispatchnotes: $("#instructions").val(),
                    scheduledArrivalDate: dataItem_dispatchlistarray.scheduledArrivalDate,
                    api: dataItem_dispatchlistarray.api,
                    wellname: dataItem_dispatchlistarray.wellname,
                    rigname: dataItem_dispatchlistarray.rigname,
                    rigid: dataItem_dispatchlistarray.rigid,

                    dispatchid: dataItem_dispatchlistarray.dispatchid,
                    userid: user_id,
                    routeorder: dispatchlistarray.length

                };
                deleteddispatchlistarray.push(dispatchlistdeletedarrayvalues);

                dispatchlistarray.splice(key, 1);

                if (dispatchlistarray.length == 0) {
                    dispatchlistarray = [];
                }

                return false;
            }
        });


       
        //DispatchListBox.dataSource.read();
        //DispatchListBox.refresh();
        LoadAllUserPathspriview2();


        //-----------------------
        //debugger;
        //var userid = $("#lbluserid").text();

        //var dispatchlistdelete = {
        //    dispatchid: dispatchidarray,
        //    userid: userid,
        //}

        //$.ajax({
        //    url: "/DispatchSRV/DeleteDispatch_V2",
        //    type: "POST",
        //    dataType: "json",
        //    data: JSON.stringify(dispatchlistdelete),
        //    contentType: "application/json; chartset=uft-8",
        //    success: function (response) {

        //    },
        //    error: function (xhr, status, error) {
        //        planalert(xhr.responseText, "Error");
        //    }
        //});



    }
    catch
    {

    }

    // DispatchListBox.select(DispatchListBox.items().first());
    //  LoadAllUserPathspriview();
  //  displayLoading("#DispatchAssignWindow", false);

}

function onReorder(ev, flag) {
    debugger;
   // displayLoading("#DispatchAssignWindow", true);

   // debugger;
    ev.preventDefault();
    var dataSource = ev.sender.dataSource;

    var dataItem = ev.dataItems[0]
    var index = dataSource.indexOf(dataItem) + ev.offset;
    dataSource.remove(dataItem);
    dataSource.insert(index, dataItem);


    ev.sender.wrapper.find("[data-uid='" + dataItem.uid + "']").addClass("k-state-selected");


    var DispatchListBox_2 = dataSource.view();
    //-------------
    //  var DispatchListBox_Items = DispatchListBox_2.element.children();
    // var DispatchListBox_Items2 = DispatchListBox_2.dataSource.view();

    var DispatchListBox_dispatchid = "";
    var Reorderarray = [];
    for (var i = 0; i < DispatchListBox_2.length; i++) {
        DispatchListBox_dispatchid = dispatchidarray = DispatchListBox_2[i].dispatchid;

        $.each(dispatchlistarray, function (key, value) {

            //var dataItem_dispatchlistarray = dispatchlistarray[key]
            if (dispatchlistarray[key].dispatchid == DispatchListBox_dispatchid) {

                Reorderarray.push(dispatchlistarray[key]);
                //break;
                return;
            }
        });


    }
    //-----------
    dispatchlistarray = [].concat(Reorderarray);

    LoadAllUserPathspriview2();
    // ev.sender.wrapper.find("[data-uid='" + UID + "']").addClass("k-state-selected");


    //  DispatchListBox = $("#DispatchList").data("kendoListBox");
    //  DispatchListBox.dataSource.read();
    //  DispatchListBox.refresh();
    //
    //var li = DispatchListBox.items().first()
    //DispatchListBox.select(DispatchListBox.items().first());

    //  LoadAllUserPathspriview();
    //displayLoading("#DispatchAssignWindow", false);
}


function onChange(ev, flag) {

    //debugger;
    // kendoConsole.log("change : " + getWidgetName(e));
}

function onAdd(ev, flag) {
    //debugger;
    //  kendoConsole.log("add : " + getWidgetName(e) + " : " + e.dataItems.length + " item(s)");
}

function onDataBound(ev, flag) {
    //debugger;
    if ("kendoConsole" in window) {
        //  kendoConsole.log("dataBound : " + getWidgetName(e));
    }
}

function onDragStart(ev, flag) {
    //debugger;
    // kendoConsole.log("dragstart : " + getWidgetName(e));
}

function onDrag(ev, flag) {
    //debugger;
    // kendoConsole.log("drag : " + getWidgetName(e));
}

function onDrop(ev, flag) {
    //debugger;
    //  kendoConsole.log("drop : " + getWidgetName(e));
}

function onDragEnd(ev, flag) {
    //debugger;
    // kendoConsole.log("dragend : " + getWidgetName(e));
}

function getWidgetName(e) {
    var listBoxId = e.sender.element.attr("id");
    var widgetName = listBoxId === "optional" ? "left widget" : "right widget";
    return widgetName;
}



function setDispatcharray(dispatchListid) {

    var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();
    var todateDate = year + '-' + month + '-' + day;
    var SADates = $("#ScheduledArrival").data("kendoDateTimePicker").value();
    var currentuser = $("#hdnCurrentUser").val();
    var location = $("#locationname").val();
    var address = $("#address").val();
    var city = $("#city").val();
    var state = $("#state").val();
    var zip = $("#zip").val();
    var latitude = $("#latitude").val();
    var longitude = $("#longitude").val();
    var notes = $("#instructions").val();
    var customer = $("#customer").val();
    var api = $("#api").val();
    var wellName = $("#wellName").val();
    var rigName = $("#rigName").val();
    var wellId = $("#wellId").val();
    var rigId = $("#rigId").val();
    var dispatchlistarrayvalues = {
        createddate: todateDate,
        locationname: location,
        customer: customer,
        address: address,
        city: city,
        state: state,
        zip: zip,
        latitude: parseFloat(latitude),
        longitude: parseFloat(longitude),
        dispatchnotes: notes,
        dispatchid: dispatchListid,
        userid: currentuser,
        api: api,
        wellname: wellName,
        rigname: rigName,
        wellid: wellId,
        rigid: rigId,
        scheduledArrivalDate: SADates,
    };
    dispatchlistarray.push(dispatchlistarrayvalues);
}

function ClearValuespreview() {
    $("#numAPI").val("");
    $("#rigNameSearch").val("");

    $("#locationname").val("");
    $("#state").val("");
    $("#county").val("");
    $("#latitude").val("");
    $("#longitude").val("");
    $("#api").val("");
    $("#rig").val("");
    $("#rigId").val("");
    $("#rigName").val("");
    $("#wellName").val("");
    $("#customer").val("");

    $("#address").val("");
    $("#city").val("");
    $("#zip").val("");
}
var count = 1;

function Addpreview() {
    //  displayLoading("#DispatchAssignWindow", true);
    //ev.preventDefault();
    debugger;

    var radioValue = $("input[name='radio1']:checked").val();


    if (radioValue == "Name" || radioValue == "Rig" || radioValue == "API") {


        var dispatchList = "";
        var dispatchListid = "";
        dispatchList = $("#rigName").val() + ' ' + $("#wellName").val() + ' ' + $("#locationname").val();
        dispatchList = dispatchList.trim();
        dispatchListid = "-" + count++;
        if (dispatchList != "") {

            //if ($("#wellId").val() != undefined && $("#wellId").val()) {
            //    dispatchListid = $("#wellId").val();
            //}
            //if ($("#rigId").val() != undefined && $("#rigId").val()) {
            //    dispatchListid = $("#rigId").val();
            //}

            //  $("#DispatchList").append("<option value='" + dispatchListid + "'>" + dispatchList + "</option>")


            var Product = kendo.data.Model.define({
                id: "dispatchid",
                fields: {
                    "rigwellandlocation": {
                        type: "string"
                    }
                }
            });
            var DispatchList = $("#DispatchList").data("kendoListBox");

            DispatchList.add(new Product({
                rigwellandlocation: dispatchList,
                dispatchid: dispatchListid
            }));


            setDispatcharray(dispatchListid);


            ClearValuespreview();

            LoadAllUserPathspriview2();
            //   displayLoading("#DispatchAssignWindow", false);
        }
    }
    else if (radioValue == "Address") {

        var dispatchList = "";
        var dispatchListid = "";
        dispatchList = $("#locationname").val();
        dispatchList = dispatchList.trim();
        dispatchListid = "-" + count++;
        if ($("#locationname").val() != "" && $("#address").val() != "" && $("#city").val() != "" && $("#state").val() != "" && $("#zip").val() != "") {

            //if ($("#wellId").val() != undefined && $("#wellId").val()) {
            //    dispatchListid = $("#wellId").val();
            //}
            //if ($("#rigId").val() != undefined && $("#rigId").val()) {
            //    dispatchListid = $("#rigId").val();
            //}

            //  $("#DispatchList").append("<option value='" + dispatchListid + "'>" + dispatchList + "</option>")


            var Product = kendo.data.Model.define({
                id: "dispatchid",
                fields: {
                    "rigwellandlocation": {
                        type: "string"
                    }
                }
            });
            var DispatchList = $("#DispatchList").data("kendoListBox");

            DispatchList.add(new Product({
                rigwellandlocation: dispatchList,
                dispatchid: dispatchListid
            }));


            setDispatcharray(dispatchListid);


            ClearValuespreview();

            LoadAllUserPathspriview2();


        }
        else {
            planalert("Enter the required field", "Alert!");
        }

    }

    else if (radioValue == "GPS") {
        var dispatchList = "";
        var dispatchListid = "";
        dispatchList = $("#locationname").val();
        dispatchList = dispatchList.trim();
        dispatchListid = "-" + count++;
        if ($("#locationname").val() != "" && $("#latitude").val() != "" && $("#longitude").val() != "") {

            //if ($("#wellId").val() != undefined && $("#wellId").val()) {
            //    dispatchListid = $("#wellId").val();
            //}
            //if ($("#rigId").val() != undefined && $("#rigId").val()) {
            //    dispatchListid = $("#rigId").val();
            //}

            //  $("#DispatchList").append("<option value='" + dispatchListid + "'>" + dispatchList + "</option>")


            var Product = kendo.data.Model.define({
                id: "dispatchid",
                fields: {
                    "rigwellandlocation": {
                        type: "string"
                    }
                }
            });
            var DispatchList = $("#DispatchList").data("kendoListBox");

            DispatchList.add(new Product({
                rigwellandlocation: dispatchList,
                dispatchid: dispatchListid
            }));


            setDispatcharray(dispatchListid);


            ClearValuespreview();

            LoadAllUserPathspriview2();
        }
        else {
            planalert("Enter the required field", "Alert!");
        }
    }
}


function getDispatchJson() {

    var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();
    var todateDate = year + '-' + month + '-' + day;
    var SADates = $("#ScheduledArrival").data("kendoDateTimePicker").value();
    var currentuser = $("#hdnCurrentUser").val();
    var location = $("#locationname").val();
    var address = $("#address").val();
    var city = $("#city").val();
    var state = $("#state").val();
    var zip = $("#zip").val();
    var latitude = $("#latitude").val();
    var longitude = $("#longitude").val();
    var notes = $("#instructions").val();
    var customer = $("#customer").val();
    var api = $("#api").val();
    var wellName = $("#wellName").val();
    var rigName = $("#rigName").val();
    var wellId = $("#wellId").val();
    var rigId = $("#rigId").val();
    var dispatch = {
        createddate: todateDate,
        locationname: location,
        customer: customer,
        address: address,
        city: city,
        state: state,
        zip: zip,
        latitude: parseFloat(latitude),
        longitude: parseFloat(longitude),
        dispatchnotes: notes,
        dispatchid: "",
        userid: currentuser,
        api: api,
        wellname: wellName,
        rigname: rigName,
        wellid: wellId,
        rigid: rigId,
        scheduledArrivalDate: SADates
    };
    return JSON.stringify(dispatch);
}

