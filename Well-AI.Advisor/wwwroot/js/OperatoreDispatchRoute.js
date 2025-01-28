
kendo.ui.progress.messages = {
    loading: "Loading..."
};

function displayLoading(target, bool) {
    var element = $(target);
    debugger;
    kendo.ui.progress(element, bool);
}

var dispatchlistarraymain = [];
var deleteddispatchlistarraymain = [];

function LoadDispatchRouteCurrentUsersmaproutpath2() {
  //  debugger;
    
    var userid = $("#hdnCurrentUser").val();
    $.ajax({
        url: "/Dispatch/GetDispatchList_Preview?userId=" + userid,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
          //  if (currentchanges == true) {
                dispatchlistarraymain = [];
                //  debugger;

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
                        wellid: value.wellid

                        //  latitude_2: value.latitude,
                        //  longitude_2: value.longitude
                    };
                    dispatchlistarraymain.push(dispatchlistarrayvalues);

                });
                 LoadAllUserPaths();
              //  Loadcurrentuserrout2();
           // }
        }
    });
}



function LoadDispatchRouteCurrentUsersmaproutpath() {
    //  debugger;
    var userid = $("#hdnCurrentUser").val();
    $.ajax({
        url: "/Dispatch/GetDispatchList_Preview?userId=" + userid,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (currentchanges == true) {
                dispatchlistarraymain = [];
                //  debugger;

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
                        wellid: value.wellid

                        //  latitude_2: value.latitude,
                        //  longitude_2: value.longitude
                    };
                    dispatchlistarraymain.push(dispatchlistarrayvalues);

                });
                Loadcurrentuserrout2();
            }
        }
    });
}
function Loadcurrentuserrout2(currentlocation) {
   // debugger;
    //----------------
    var destinations = [];
    var location = [];
    var userid = $("#hdnCurrentUser").val();
    var destinations_array = [];

    //-------
    $.each(dispatchlistarraymain, function (key, value) {

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

        //var destination = {
        //    id: value.wellid == 0 ? value.rigid : value.wellid,
        //    type: value.wellid == 0 ? "rig" : "well"
        //};

        //if (value.wellid != "0" || value.rigid != "0") {
        //    destinations_array.push(destination);
        //}


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

        destinations.push(destinationsvalue);

    });

    //--------------


   var destinationssimulationrequest = {
           origin: dispatchlistarraymain[0].locationname != '' ? dispatchlistarraymain[0].locationname : dispatchlistarraymain[0].latitude + ',' + dispatchlistarraymain[0].longitude,
      
        priority: "travel_time",
        destinationsarray: destinations_array
    }
      
    //var destinationssimulationrequest = {
    //    //   origin: dispatchlistarraymain[0].locationname != '' ? dispatchlistarraymain[0].locationname : dispatchlistarraymain[0].latitude + ',' + dispatchlistarraymain[0].longitude,
    //    origin= {
    //        latitude: currentlocation.latitude,
    //        longitude: currentlocation.longitude
    //    },
    //    priority: "travel_time",
    //    destinationsarray: destinations_array
    //}
    console.log('LoadUserLocation button cliked');

    //$.when(planJSON = DrillPlanDetails(wellId, tenantId, stageUpdate)).done(function (x) {
    $.ajax({
        url: '/Dispatch/GetSimulationMap',
        type: 'POST',
        dataType: "json",  
        data: JSON.stringify(destinationssimulationrequest),
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
        //   debugger;

            // updateRoutes_Preview2(destinations, response)
            console.log(response);
            if (response.message == null || response.message == undefined || response.message == "") {
                var locationResponse = response.parse;
                //geojson = destinations;//.legs;
                var lastcoordinates = [];
                var zoomsize = 4;
                var currentMarkers = [];

                //var location = {
                //    longitude: response.directions[0].origin.longitude,
                //    latitude: response.directions[0].origin.latitude
                //}

              
               // $(".marker").remove();
               // $(".markerstart").remove();
               // $(".truckcolor").remove();
                //--------------------
        //        updateRoutes_Currentuser(response.directions, location, response.destinations);
                updateRoutes_Currentuser(response.directions, currentlocation, response.destinations);

            } else {
                planalert(response.message);

            }
        },
        error: function (xhr, status, error) {
            planalert(xhr.responseText, "Error");
        }
    })

}


function updateRoutes_Currentuser(destinations, location, destinationNames) {

 //   debugger;//

    //---------------------
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
       // if (destinations[m] != null) {
        //    for (m = 0; m < destinations.length; m++) {
        //        var legs = destinations[m].legs;
        //        if (destinations[m].legs != null) {
        //            for (i = 0; i < legs.length; i++) {
        //                if (legs[i] != null) {
        //                    if (legs[i].path != null) {
        //                        for (j = 0; j < legs[i].path.length; j++) {
        //                            responsedata.geometry.coordinates[k] = {};
        //                            responsedata.geometry.coordinates[k] = [legs[i].path[j].lng, legs[i].path[j].lat];

        //                          //  if (m == 0 && i == 0 && j == 0) {
        //                                centercoordinates[0] = legs[0].path[0].lng;
        //                                centercoordinates[1] = legs[0].path[0].lat;

        //                         //   }
        //                            k = k + 1;
        //                        }
        //                    }
        //                }
        //            }
                
        //    }
        //}


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
                                centercoordinates[0] = legs[0].path[0].lng;
                                centercoordinates[1] = legs[0].path[0].lat;

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


    map.flyTo({
        center: centercoordinates,
        zoom: 6,
        speed: 5
    });


    //if (mappreview.getSource("route") != undefined) {
    //    mappreview.getSource("route").setData(responsedata);
    //}
    if (map.getSource("routecurrentsrc") != undefined) {
        map.getSource("routecurrentsrc").setData(responsedata);
    }

   //debugger;
    if (destinations.length > 0) {
        if (destinations[0].legs != null) {
            if (destinations[0].legs[0] != null) {
                if (destinations[0].legs[0].path != null) {
                    if (destinations[0].legs[0].path[0] != null) {
                        if (destinations[0].legs[0].path[0].lng != null) {
                            // debugger;
                            var coordinates = [];
                            coordinates[0] = destinations[0].legs[0].path[0].lng;
                            coordinates[1] = destinations[0].legs[0].path[0].lat;

                            const el = document.createElement('div');
                            //   el.className = 'markerstart';
                            el.className = 'markernew';


                            //   new mapboxgl.Marker(el)
                            new mapboxgl.Marker(el)
                                .setLngLat(coordinates)
                                .setPopup(
                                    new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up-op2' }) // add popups
                                        .setHTML(
                                          //  `<h3>${destinations[0].destination.place}</h3><p>${destinations[0].destination.place}</p>`

                                            `<div class="pop-head3" style="margin-top:px"><label class="pop-head-font"
style="margin-top:15px">${destinations[0].destination.place}</label></div>`
                                        )
                                )
                                .addTo(map);

                        }

                    }

                }
            }
        }
            ETA_Timelocation(destinations, location);
        
    }
            //for (m = 0; m < destinations.length; m++) {
            //    var markerPoints = destinations[m].destination;//.destination_coordinates;
            //    // //debugger;
            //    var coordinates = [];
            //    coordinates[0] = markerPoints.longitude;
            //    coordinates[1] = markerPoints.latitude;

            //    if (Math.abs(markerPoints.longitude - location.longitude) >= 0.0001 && Math.abs(markerPoints.latitude - location.latitude) >= 0.0001) {
            //        const el = document.createElement('div');
            //        el.className = 'marker';

            //      //  new mapboxgl.Marker(el)
            //        new mapboxgl.Marker()
            //            .setLngLat(coordinates)
            //            .setPopup(
            //                new mapboxgl.Popup({ offset: 25 }) // add popups
            //                    .setHTML(
            //                        `<h4>${destinations[m].destination.place}</h4><p>${destinations[m].destination.place}</p><p>Total Duration :${destinations[m].total_duration_display}</p><p>Total Display :${destinations[m].total_distance_display}</p>`
            //                    )
            //            )
            //            .addTo(map);
            //    }

            //}
            ////Set user's current location
            //var locationcoordinates = [];
            //locationcoordinates[0] = location.longitude;
            //locationcoordinates[1] = location.latitude;

            //  const el = document.createElement('i');
            //  el.className = 'fa fa-truck fa-2x truckcolor';

            //new mapboxgl.Marker(el)
            //    .setLngLat(locationcoordinates)
            //    .addTo(map);
            //new mapboxgl.Marker()
            //    .setLngLat(locationcoordinates)
            //    .addTo(map);
            // DispatchListBox.dataSource.read();
            // DispatchListBox.refresh();

        
    
}

//--------------------------------------------------


function LoadDispatchRouteUsers() {
    var window = $("#DispatchAssignUserWindow").data("kendoWindow");
    window.refresh({
        url: "/Dispatch/LoadAdvisorUsers"
    });

    $("#DispatchAssignUserWindow").closest(".k-window").css({
        //top: 150,
        //left: 650
    });

    //window.refresh().center().open();
   // window.center().refresh().open();
    window.center().open();
}

function RefreshRoutes() {
    debugger;
    displayLoading("#Dispatchgrid", true);
    $.ajax({
        url: '/Dispatch/RefreshRoutes',
        type: 'POST',
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
          //  location.reload();
            currentchanges = false;
            console.log(response);
            var gridObj = $('#Dispatch').data('kendoGrid');
            gridObj.dataSource.read();
            gridObj.refresh();
            resize();
            displayLoading("#Dispatchgrid", true);
            currentchanges = false;
        },
        error: function (xhr, status, error) {
            planalert(xhr.responseText, "Error");
            displayLoading("#Dispatchgrid", true);
        }
    })
   
}

function LoadAssignDispatch(userId) {
 // debugger;
    $("#hdnCurrentUser").val(userId);
   
    var window1 = $("#DispatchAssignWindow").data("kendoWindow");
    window1.refresh({
        url: "../../Dispatch/LoadAssignDispatch?userId=" + userId
    });

    //var percent = Math.round(100 * $(window).innerHeight() / ($(window).innerHeight()));
   

  //  var percent = Math.round(100 * $(window).height() / ($(window).height()));
    //  var percent = $(window).height(); Math.round($(window).height() / $(window).height() * $(window).height());
    //var percent =  Math.round($(window).innerHeight() / $(window).innerHeight() * $(window).innerHeight());
   // var percent = $(window).height();

   // var percent2 = percent - 30;

  //  var widthpercent = Math.round(100 * $(window).innerWidth() / ($(window).innerWidth()));
    

    // var widthpercent = Math.round($(window).width() / $(window).width() * $(window).width());
   // var widthpercent = Math.round($(window).innerWidth() / $(window).innerWidth() * $(window).innerWidth());
   // var widthpercent = $(window).innerWidth();

    //-----------
    //var widthpercent2 = widthpercent - 27;
  
    //var heightpopup = (percent - 27) + "%";
    //var widthpopup = (widthpercent - 33) + "%";
    //window1.height = percent2;
    //window1.height = "" + percent2 + "%";

  ////  if ($(window).innerHeight() < 1370) {
  ////      var percent = Math.round(100 * $(window).innerHeight() / ($(window).innerHeight()));
  ////      var widthpercent = Math.round(100 * $(window).innerWidth() / ($(window).innerWidth()));
  ////      var percent2 = percent - 16;
  ////      var widthpercent2 = widthpercent - 28;

  ////      var heightpopup = (percent - 18) + "%";
  ////      var widthpopup = (widthpercent - 28) + "%";
  ////      // window1.height = percent2;
  ////      window1.height = "70%";
  ////      $("#DispatchAssignWindow").closest(".k-window").css({
  ////          top: 100,
  ////          left: 650,
  ////          ///  height: heightpopup,
  ////          // width: widthpopup,
  ////          height: "62%",
  ////          width: "82%",
  ////          scrollable: true
  ////      });
  ////  }
  ////  else {
  //      var widthpercent = Math.round(100 * $(window).innerWidth() / ($(window).innerWidth()));
  //      var percent = Math.round(100 * $(window).innerHeight() / ($(window).innerHeight()));
  //      var widthpercent2 = widthpercent - 27;
  //      var percent2 = percent - 30;

  //  var heightpopup = (percent - 27) + "%";
  //  var widthpopup = (widthpercent - 33) + "%";
  ////  window1.height = percent2;
  //  window1.height = "70%";
  //      $("#DispatchAssignWindow").closest(".k-window").css({
  //          top: 150,
  //          left: 650,
  //          ///  height: heightpopup,
  //          // width: widthpopup,
  //          height: "73%",
  //          width: "67%",

  //      });
  //  }
 
    //-----------
    //var percent = Math.round($(window).innerHeight() / 100);
    //var widthpercent = Math.round($(window).innerWidth() / 100);
    //var heightpopup = percent+ "%";
    //var widthpopup = widthpercent + "%";

   // window1.height = "71%";
  

   

    // wnd.center().open();
    window1.refresh().center().open();

   


   // debugger;

   // //public - pk.eyJ1Ijoia2FydGhpa3RoYW5nYXZlbCIsImEiOiJjbDFkZHk5MjYwZ21lM2NwMm9tcTVueGMyIn0.jufh1E3v_JpXizuN7Rfudg
   // //Well AI - pk.eyJ1Ijoid2VsbC1haSIsImEiOiJjbDFxZ25yMnIwN3c3M2JzNml1dWdpMmR0In0.l-lDzOnU03tFScjiMkRObg
   // mapboxgl.accessToken = 'pk.eyJ1Ijoid2VsbC1haSIsImEiOiJjbDFxZ25yMnIwN3c3M2JzNml1dWdpMmR0In0.l-lDzOnU03tFScjiMkRObg';

   // $("#mappreview").empty();
   // const mappreview = new mapboxgl.Map({
   //     container: 'mappreview',
   //     style: 'mapbox://styles/mapbox/streets-v11',
   //     center: [-102.36896137371849, 31.846556737999546],
   //     zoom: 4
   // });

   //// $.when(mappreview.on('load', () => {
   //     //   debugger;
   //     mappreview.addSource('route', {
   //         'type': 'geojson',
   //         'data': {
   //             'type': 'Feature',
   //             'properties': {},
   //             'geometry': {
   //                 'type': 'LineString',
   //                 'coordinates': [
   //                 ]
   //             }
   //         }
   //     });
   //     mappreview.addLayer({
   //         'id': 'route',
   //         'type': 'line',
   //         'source': 'route',
   //         'layout': {
   //             'line-join': 'round',
   //             'line-cap': 'round'
   //         },
   //         'paint': {
   //             'line-color': '#888',
   //             'line-width': 8
   //         }
   //     });

   //     mappreview.addControl(new mapboxgl.NavigationControl());
   //     mappreview.resize();
   //// })
   // //).then(function (data, textStatus, jqXHR) {
   // //    //LoadAllUserDestinations();
   // //    //$.when(loadroutes()).done(function (x) {

   // //    //    setTimeout(function () {
   // //    //        $(".mapboxgl-map").height(parseInt($("#Dispatch").height()));
   // //    //    }, 4000);
   // //    //});
   // //});
   // mappreview.on('idle', function () {
   //     mappreview.resize()
   // })
}

function LoadUserReRoutes(userId) {
    /*debugger;*/
    $("#hdnCurrentUser").val(userId);

    var window = $("#DispatchUserRoutesWindow").data("kendoWindow");
    window.refresh({
        url: "/Dispatch/LoadUserRoutes?userId=" + userId
    });

    $("#DispatchUserRoutesWindow").closest(".k-window").css({
        top: 150,
        left: 650
    });
    window.center().open();

}


function LoadDispatchNotes() {
    var window = $("#DispatchNotesWindow").data("kendoWindow");
    window.refresh({
        url: "/Dispatch/LoadDispatchNotes"
    });

    $("#DispatchNotesWindow").closest(".k-window").css({
        top: 150,
        left: 650
    });

    window.center().open();
}

function onFilter() {

    var radioValue = $("input[name='radio1']:checked").val();

    if (radioValue == "") {
        planalert("Please select any one of the search type");
    }
    return {
        text: $("#numAPI").val(),
        filterType: radioValue
    };
}

function onRigFilter() {

    var radioValue = $("input[name='radio1']:checked").val();

    if (radioValue == "") {
        planalert("Please select any one of the search type");
    }
    return {
        text: $("#rigNameSearch").val(),
        filterType: radioValue
    };
}


function onApiNumChange() {
    if ('@TempData["Error"]') {
        var autocomplete = $("#numAPI").data("kendoAutoComplete");
        autocomplete.setOptions({ noDataTemplate: "No Location found" });
    }
}

function onApiNumSelect(e) {
    //debugger
    var dataItem = this.dataItem(e.item.index());
    console.log(' dataItem api_number' + dataItem.api_number);
    console.log(' dataItem name ' + dataItem.name);
    if (dataItem.api_number != "") {
        $("#locationname").val(dataItem.lease_name).trigger("change");
        $("#state").val(dataItem.state).trigger("change");
        $("#county").val(dataItem.county).trigger("change");
        $("#latitude").val(dataItem.latitude).trigger("change");
        $("#longitude").val(dataItem.longitude).trigger("change");
        $("#api").val(dataItem.api_number).trigger("change");
        $("#wellName").val(dataItem.name).trigger("change");
        $("#wellId").val(dataItem.id)

        if (dataItem.operator != undefined && dataItem.operator != 'undefined') {
            $("#customer").val(dataItem.operator).trigger("change");
        }
      
      
  

    }
}

function gridEdit(e) {
    if (e.model.isNew()) {
        e.container.data("kendoWindow").title("Assign Dispatch");

    }
}


var userId = $("#hdnCurrentUser").val();
var mapDataSource = new kendo.data.DataSource({
    transport: {
        read: {
            url: 'Dispatch/DispatchRoutes_Read?user=' + userId,
            dataType: "jsonp"
        }
    }
});

function onDataBound(e) {
   /* //debugger*/
    var grid = this;

    grid.element.on('click', '.customEdit', function (e) {
        //  debugger;
        //delete mappreview;
      //  mappreview.empty();
        //$("#mappreview").empty();
        //debugger;
        var dataItem = grid.dataItem($(e.target).closest("tr"));

        $("#DispatchAssignWindow_wnd_title").text("Dispatch Assignment  for " + dataItem.username+"");
        $("#hdnCurrentUser").val(dataItem.userid);
        LoadAssignDispatch(dataItem.userid);

        //-----------------






    });

    grid.element.on('click', '.customMap', function (e) {
        debugger;
        var dataItem = grid.dataItem($(e.target).closest("tr"));

        $("#hdnCurrentUser").val(dataItem.userid);

        LoadAllUserPaths();

    });
    $("#Dispatch tbody").on("click", "tr", function (e) {
       // debugger;
        var dataItem = grid.dataItem($(e.target).closest("tr"));

        $("#hdnCurrentUser").val(dataItem.userid);
    });

    grid.element.on('dblclick', '.customMap', function (e) {

        /*debugger;*/
        var dataItem = grid.dataItem($(e.target).closest("tr"));

        $("#hdnCurrentUser").val(dataItem.userid);

        LoadUserReRoutes(dataItem.userid);

    });
    var dataItems = this.dataSource.view();
    for (var i = 0; i < dataItems.length; i++) {
        if (i == dataItems.length - 1) {
            $.when(loadroutes()).done(function (x) {
                setTimeout(function () {
                  //  $(".mapboxgl-map").height(parseInt($("#Dispatch").height()));
                    $("#map").height(parseInt($("#Dispatch").height()));
                }, 3000);
            });
        }
    }
}


function planalert(content, alerttitle) {
    $("<div></div>").kendoAlert({
        title: alerttitle,
        content: content
    }).data("kendoAlert").open();
}


function LoadUserLocation() {
    var userId = $("#hdnCurrentUser").val();
    console.log('LoadUserLocation button cliked');
    $.ajax({
        url: '/Dispatch/GetUserCurrentLocation?userId=' + $("#hdnCurrentUser").val(),
        type: 'GET',
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            console.log(response);
            if (response.message == null || response.message == undefined || response.message == "") {
                var locationResponse = response;
                $(".marker").remove();
                $(".markerstart").remove();
                $(".truckcolor").remove();
                $(".markeruser").remove();
                $(".markernew").remove();
                var responsedata = {};
                responsedata.type = 'Feature';
                responsedata.geometry = {};
                responsedata.geometry.type = "LineString";
                responsedata.geometry.coordinates = [];
                //updateRoutes(response.destinations, response.location);
                map.getSource("routesrc").setData(responsedata);

                UpdateLocation(locationResponse);
            } else {
                planalert(response.message);

            }
        },
        error: function (xhr, status, error) {
            planalert(xhr.responseText, "Error");
        }
    })

}

function UpdateLocation(locationResponse) {

    var coordinates = [];

    if (locationResponse.message == null) {
        coordinates.push(locationResponse.longitude);
        coordinates.push(locationResponse.latitude);

        const el = document.createElement('div');
        el.className = 'markeruser';

        new mapboxgl.Marker(el)
            .setLngLat(coordinates)
            .setPopup(
                new mapboxgl.Popup({ offset: 25 }) // add popups
                    .setHTML(
                        `<h3>Destination</h3><p>${locationResponse.latitude}</p><p>${locationResponse.longitude}</p>`
                    )
            )
            .addTo(map);
        //debugger
        map.flyTo({
            center: [locationResponse.longitude, locationResponse.latitude],
            zoom: 16,
            speed: 5,
        });

    } else {

        coordinates.push("-102.36896137371849");
        coordinates.push("31.846556737999546");

        // add markers to map

        const el = document.createElement('div');
        el.className = 'markeruser';
        // make a marker for each feature and add it to the map
        new mapboxgl.Marker(el)
            .setLngLat(coordinates)
            .addTo(map);
        map.flyTo({
            center: [locationResponse.longitude, locationResponse.latitude],
            zoom: 16,
            speed: 5,
        });
    }

}



function UpdateDispatch() {
   /* debugger;*/
    var dispatchNotes = $("#instructionsNotes").val();
    var dispatchId = $("#hdnDispatchId").val();
    var dispatch = {
        dispatchid: dispatchId,
        dispatchnotes: dispatchNotes,
        userid: $("#hdnCurrentUser").val()
    }
    var dispatchJson = JSON.stringify(dispatch);
    $.ajax({
        url: "/Dispatch/UpdateDispatchNotes",
        type: "POST",
        dataType: "json",
        data: dispatchJson,
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            var gridObj = $('#Dispatch').data('kendoGrid');
            gridObj.dataSource.read();
            gridObj.refresh();

            var window = $("#DispatchNotesWindow").data("kendoWindow");
            window.close();

        },
        error: function (xhr, status, error) {
            planalert(xhr.responseText, "Error");
        }
    });
}

function onRigNameChange() {
    if ('@TempData["Error"]') {
        var autocomplete = $("#rigNameSearch").data("kendoAutoComplete");
        autocomplete.setOptions({ noDataTemplate: "No Rigs found" });
    }
}

function onRigNameSelect(e) {
    //debugger
    var dataItem = this.dataItem(e.item.index());
    console.log(' dataItem name ' + dataItem.rig);
    if (dataItem.rig != "") {

        $("#latitude").val(dataItem.latitude).trigger("change");
        $("#longitude").val(dataItem.longitude).trigger("change");

        $("#rigId").val(dataItem.rig_id);
        $("#rigName").val(dataItem.rig).trigger("change");
    }
}

function onSearchOptionClick() {
    //debugger;
    var radioValue = $("input[name='radio1']:checked").val();

    $("#numAPI").val("");
    $("#rigNameSearch").val("");

    //if (radioValue == "Rig") {
    //    $("#divRigSearch").css("display", "block");
    //    $("#divApiSearch").css("display", "none");
    //} else {
    //    $("#divRigSearch").css("display", "none");
    //    $("#divApiSearch").css("display", "block");
    //}
    //--------------------------
    if (radioValue == "Name") {
        $("#DestinationLookup").css("display", "block");
        $("#DestinationAddress").css("display", "none");

        $("#divRigSearch").css("display", "none").addClass("k-state-disabled");
        $("#divApiSearch").css("display", "block").removeClass("k-state-disabled");

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
    }
    else if (radioValue == "Rig") {

        $("#DestinationLookup").css("display", "block");
        $("#DestinationAddress").css("display", "none");
        $("#divRigSearch").css("display", "block").removeClass("k-state-disabled");
        $("#divApiSearch").css("display", "none").addClass("k-state-disabled");

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
    }
    else if (radioValue == "API") {

        $("#DestinationLookup").css("display", "block");
        $("#DestinationAddress").css("display", "none");
        $("#divRigSearch").css("display", "none").addClass("k-state-disabled");
        $("#divApiSearch").css("display", "block").removeClass("k-state-disabled");

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
    }
    else if (radioValue == "Address") {
        $("#DestinationLookup").css("display", "none");
        $("#DestinationAddress").css("display", "block");
        ////   $("#divRigSearch").css("display", "none").addClass("k-state-disabled");
        //   $("#divApiSearch").css("display", "none").addClass("k-state-disabled");
        $("#AddressBlock").css("display", "block");
        $("#CityBlock").css("display", "block");
        $("#StateBlock").css("display", "block");
        $("#ZipBlock").css("display", "block");
        $("#LongitudeBlock").css("display", "none");
        $("#LatitudeBlock").css("display", "none")
        //$("#numAPI").prop('disabled', true).addClass("k-state-disabled");
        //$("#customer").prop('disabled', true).addClass("k-state-disabled");
        //$("#api").prop('disabled', true).addClass("k-state-disabled");
        //$("#rig").prop('disabled', true).addClass("k-state-disabled");
        //$("#wellName").prop('disabled', true).addClass("k-state-disabled");
        //$("#rigName").prop('disabled', true).addClass("k-state-disabled");

        $("#locationname").prop('disabled', false).removeClass("k-state-disabled");
        $("#address").prop('disabled', false).removeClass("k-state-disabled");
        $("#zip").prop('disabled', false).removeClass("k-state-disabled");
        $("#city").prop('disabled', false).removeClass("k-state-disabled");
        $("#state").prop('disabled', false).removeClass("k-state-disabled");
        $("#latitude").prop('disabled', true).addClass("k-state-disabled");
        $("#longitude").prop('disabled', true).addClass("k-state-disabled");
    }
    else if (radioValue == "GPS") {
        $("#DestinationLookup").css("display", "none");
        $("#DestinationAddress").css("display", "block");
        $("#AddressBlock").css("display", "none");
        $("#CityBlock").css("display", "none");
        $("#StateBlock").css("display", "none");
        $("#ZipBlock").css("display", "none");
        $("#LongitudeBlock").css("display", "block");
        $("#LatitudeBlock").css("display", "block")
        // $("#divRigSearch").css("display", "none").addClass("k-state-disabled");
        //  $("#divApiSearch").css("display", "none").addClass("k-state-disabled");

        //$("#numAPI").prop('disabled', true).addClass("k-state-disabled");

        //$("#customer").prop('disabled', true).addClass("k-state-disabled");
        //$("#api").prop('disabled', true).addClass("k-state-disabled");
        //$("#rig").prop('disabled', true).addClass("k-state-disabled");
        //$("#wellName").prop('disabled', true).addClass("k-state-disabled");
        //$("#rigName").prop('disabled', true).addClass("k-state-disabled");

        $("#locationname").prop('disabled', false).removeClass("k-state-disabled");
        //$("#address").prop('disabled', true).addClass("k-state-disabled");
        //$("#zip").prop('disabled', true).addClass("k-state-disabled");
        //$("#city").prop('disabled', true).addClass("k-state-disabled");
        //$("#state").prop('disabled', true).addClass("k-state-disabled");
        $("#latitude").prop('disabled', false).removeClass("k-state-disabled");
        $("#longitude").prop('disabled', false).removeClass("k-state-disabled");
    }

    //--------------------------


    ClearValues();
}


function ClearValues() {
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
    $("#wellId").val("");
    $("#zip").val("");

    
    
}

function LoadAllUserDestinations() {
    /*//debugger;*/
    var geojson = "";
    $.ajax({
        url: "/Dispatch/GetUserDestinations?userId=" + $("#hdnCurrentUser").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            /*//debugger;*/
            geojson = response;

            var lastcoordinates = [];
            var zoomsize = 4;

            // markers saved here
            var currentMarkers = [];
            if (geojson.features.length > 0) {

                $(".marker").remove();
                $(".markerstart").remove();
                $(".truckcolor").remove();
                $(".markeruser").remove();
                $(".markernew").remove();
                if (geojson.features.length > 0 && geojson.features.length < 5) {
                    zoomsize = 4;
                } else if (geojson.features.length > 5 && geojson.features.length < 10) {
                    zoomsize = 4;
                }


                for (const feature of geojson.features) {
                    // create a HTML element for each feature
                    const el = document.createElement('div');
                    el.className = 'marker';

                    // make a marker for each feature and add it to the map
                    new mapboxgl.Marker(el)
                        .setLngLat(feature.geometry.coordinates)
                        .setPopup(
                            new mapboxgl.Popup({ offset: 25 }) // add popups
                                .setHTML(
                                    `<h3>${feature.properties.title}</h3><p>${feature.properties.description}</p>`
                                )
                        )
                        .addTo(map);
                    lastcoordinates = feature.geometry.coordinates;
                }

                map.flyTo({
                    center: lastcoordinates,
                    zoom: zoomsize,
                    speed: 5,
                });
                resizeWrapper();
                resizeGrid();

            }
        },
        error: function (xhr, status, error) {

        }
    });

}

function LoadAllUserPaths() {
    displayLoading("#map", true);
   // debugger;;
    $.ajax({
        url: "/Dispatch/GetUserCurrentRoutes?userId=" + $("#hdnCurrentUser").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
         //debugger
            geojson = response.destinations;//.legs;

            var lastcoordinates = [];
            var zoomsize = 4;

            var currentMarkers = [];
            if (geojson != null) {

                $(".marker").remove();
                $(".markerstart").remove();
                $(".truckcolor").remove();
                $(".markeruser").remove();
                $(".markernew").remove();
                updateRoutes(response.destinations, response.location);

            } else {
                $(".marker").remove();
                $(".markerstart").remove();
                $(".truckcolor").remove();
                $(".markeruser").remove();
                $(".markernew").remove();
                var responsedata = {};
                responsedata.type = 'Feature';
                responsedata.geometry = {};
                responsedata.geometry.type = "LineString";
                responsedata.geometry.coordinates = [];
                //updateRoutes(response.destinations, response.location);
                map.getSource("routesrc").setData(responsedata);

                // add markers to map

                //const el = document.createElement('div');
                //el.className = 'marker';
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
        },
        error: function (xhr, status, error) {

        }
    });

}

function updateRoutes(destinations, location) {
    var uname = "";

    $.ajax({
        url: "/Dispatch/GetUserName?userId=" + $("#hdnCurrentUser").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            uname = response;            
            updateRoutesDetails(destinations, location, uname)
        }
    });   
  }

function updateRoutesDetails(destinations, location, uname) {
     
    debugger;
    try {
        //var uname = "";

        //$.ajax({
        //    url: "/Dispatch/GetUserName?userId=" + $("#hdnCurrentUser").val(),
        //    type: "GET",
        //    dataType: "json",
        //    contentType: "application/json; chartset=uft-8",
        //    success: function (response) {
                
        //        uname = response;
        //        alert(uname);
        //    }
        //});
                
        var responsedata = {};

        var centercoordinates = [];

        if (destinations.length > 0) {
            responsedata.type = 'Feature';
            responsedata.geometry = {};
            responsedata.geometry.type = "LineString";
            responsedata.geometry.coordinates = [];

            var k = 0;



            for (m = 0; m < destinations.length; m++) {
                if (destinations[m].directions != null) {
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
           /* center: centercoordinates,*/
            zoom: 7,
            speed: 5
        });

        map.getSource("routesrc").setData(responsedata);


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
//                                .setPopup(
//                                    new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up' }) // add popups
//                                        .setHTML(

//                                            `<div class="pop-head2" style="margin-top:5px"><label class="pop-head-font"
//style="margin-top:15px">${destinations[0].destination}</label></div><div class="pop-bootom1">`
//                                            //`<h3>${destinations[0].destination.replace(',', ', ')}</h3><p>${destinations[0].destination.replace(',', ', ')}</p>`

//                                        /*  old `<h3>${destinations[0].destination.place}</h3><p>${destinations[0].destination.place}</p>`*/
//                                        )
//                                )
                                .addTo(map);
                        }

                    }
                }
            }

            //debugger;
            //   ETA_Timelocation(destinations, location);
            ETA_Timelocation_Existrout(destinations, location);
            //for (m = 0; m < destinations.length; m++) {
            //    var markerPoints = destinations[m].destination_coordinates;
            //    // //debugger;
            //    var coordinates = [];
            //    coordinates[0] = markerPoints.longitude;
            //    coordinates[1] = markerPoints.latitude;

            //    if (Math.abs(markerPoints.longitude - location.longitude) >= 0.0001 && Math.abs(markerPoints.latitude - location.latitude) >= 0.0001) {
            //        const el = document.createElement('div');
            //        el.className = 'marker';

            //        new mapboxgl.Marker(el)
            //            .setLngLat(coordinates)
            //            .setPopup(
            //                new mapboxgl.Popup({ offset: 25 }) // add popups
            //                    .setHTML(
            //                        `<h3>${destinations[m].destination}</h3><p>${destinations[m].destination}</p>`

            //                          /*  `<h4>${destinations[m].destination.place}</h4><p>${destinations[m].destination.place}</p><p>Total Duration :${destinations[m].total_duration_display}</p><p>Total Display :${destinations[m].total_distance_display}</p>`*/


            //                    )
            //            )
            //            .addTo(map);
            //    }


            //}



        }


        //Set user's current location
        var locationcoordinates = [];
        locationcoordinates[0] = location.longitude;
        locationcoordinates[1] = location.latitude;
       
        const el = document.createElement('i');
        //el.className = 'fa fa-truck fa-2x truckcolor';
        el.className = ' truckcolor ';

        new mapboxgl.Marker(el)
            .setLngLat(locationcoordinates)
            .setPopup(
                new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up-op' }) // add popups
                    .setHTML(
                        //                            `<h3>${destinations[i].destination}</h3><p>${destinations[i].destination}</p> <p>ETA :${ETADatetime}</p> <p>Total Duration :${Total_Duration}</p>
                        //<p>Total Km :${Total_Distance}</p>`
                        /*<div class="pop-head"><label>${destinations[i].destination}</label></div> <br>*/
//                        `<div class="pop-head" style="margin-top:5px"><label class="pop-head-font"
//style="margin-top:15px">${'Coordinates '} <br> ${location.latitude + ', ' + location.longitude}</label></div><div class="pop-bootom1" style="margin-top:5px;float:left">`
                 

                        `<div class="pop-head" style="margin-top:5px"><label class="pop-head-font"
style="margin-top:15px">${uname}</label></div><div class="pop-bootom1" style="margin-top:5px;float:left">
<label>Latitude           :${location.latitude}</label>  <br>
<label>Longitude          :${location.longitude}</label>  <br>
<label>Time Remaining     :${location.time_remaining}</label>  <br>
<label>Distance Remaining :${location.distance_remaining}</label> <br>
<label>ETA    :${location.eta_timestamp_UT}</label>

</div>`


                    )
            )

            .addTo(map);

        //debugger;
        if (currentchanges == true) {

            //var location = {
            //    longitude: response.directions[0].origin.longitude,
            //    latitude: response.directions[0].origin.latitude
            //}

            // LoadDispatchRouteCurrentUsersmaproutpath();
            Loadcurrentuserrout2(location);


        }
        else {
            deleteddispatchlistarraymain = [];
            displayLoading("#map", false);
        }
    }
    catch
    {

        displayLoading("#map", false);
    }
    
}
//ETA_Timelocation


function ETA_Timelocation_Existrout(destinations, location) {

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
    for (i = 0; i < destinations.length; i++) {

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
            el.className = 'markerstart';

            new mapboxgl.Marker(el)
                .setLngLat(coordinates)
                .setPopup(
                    new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up'  }) // add popups
                        .setHTML(
                        /*  `<h3>${destinations[i].destination}</h3><p>${destinations[i].destination}</p>`*/

                            `<div class="pop-head2" style="margin-top:5px"><label class="pop-head-font"
style="margin-top:15px">${destinations[i].destination}</label></div><div class="pop-bootom1" ">`

                        )
                )
                .addTo(map);
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

            const el = document.createElement('div');
            el.className = 'marker';

            new mapboxgl.Marker(el)
                .setLngLat(coordinates)
                .setPopup(
                    new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up-op' }) // add popups
                        .setHTML(
                            //                            `<h3>${destinations[i].destination}</h3><p>${destinations[i].destination}</p> <p>ETA :${ETADatetime}</p> <p>Total Duration :${Total_Duration}</p>
                            //<p>Total Km :${Total_Distance}</p>`

//                            `<h3>${destinations[i].destination}</h3><p>${destinations[i].destination}</p> <p>ETA :${ETADatetime}</p> <p>Total Duration :${Total_Duration}</p>
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

        //-------------
        totaldistance = 0;
        totalmile = 0;
        totalduration = 0;
        ETADatetime = "";
        Total_Distance = "";
        Total_Duration = "";


    }

}

   

function ETA_Timelocation(destinations, location) {

  //debugger;

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
                //for (j = 0; j < destinations[i].legs.length; j++) {
                  
                //        if (destinations[i].legs[j] != null) {
                //            if (destinations[i].legs[j].distance_miles != null) { totalmile += destinations[i].legs[j].distance_miles; }
                //            if (destinations[i].legs[j].distance != null) { totaldistance += destinations[i].legs[j].distance; }
                //            if (destinations[i].legs[j].duration != null) { totalduration += destinations[i].legs[j].duration; }

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
        }
        else {
            truckstartplace = false;
            //if (destinations[i].legs != null) {
            //for (j = 0; j < destinations[i].legs.length; j++) {



              
            //        if (destinations[i].legs[j] != null) {

            //            if (destinations[i].legs[j].distance_miles != null) { totalmile += destinations[i].legs[j].distance_miles; }
            //            if (destinations[i].legs[j].distance != null) { totaldistance += destinations[i].legs[j].distance; }
            //            if (destinations[i].legs[j].duration != null) { totalduration += destinations[i].legs[j].duration; }

            //        }
            //    }

            //}

            truckstartplace = false;

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
            el.className = 'markernew';

            new mapboxgl.Marker(el)
                .setLngLat(coordinates)
                .setPopup(
                    new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up-op2' }) // add popups
                        .setHTML(

                          //  `<h3>${destinations[0].destination.place}</h3><p>${destinations[0].destination.place}</p>`
                            `<div class="pop-head3" style="margin-top:5px"><label class="pop-head-font"
style="margin-top:15px">${destinations[0].destination.place}</label></div>`
                        )
                )
                .addTo(map);
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
            var ETADatetime = moment(endDate).format('MM/DD/YYYY HH:mm');//+ ' ' + ampm;
            // var ETADatetime = moment(endDate, 'MM/DD/YYYY HH:mm tt').format("MM/DD/YYYY HH:mm tt");
            var Totalmiles = "";
         //   if (totalmile > 0) {
            //     Totalmiles = Math.floor(totalmile) + ' miles';
          //  }
           
            //var Totalmiles = totalmile + ' miles';
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
            el.className = 'markernew';

            new mapboxgl.Marker(el)
                .setLngLat(coordinates)
                .setPopup(
                    new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up-op2' }) // add popups
                        .setHTML(
//                            `<h4>${destinations[i].destination.place}</h4><p>${destinations[i].destination.place}</p>  <p>ETA :${ETADatetime}</p> <p>Total Duration :${Total_Duration}</p>
//<p>Total Km :${Total_Distance}</p>`

//                            `<h4>${destinations[i].destination.place}</h4><p>${destinations[i].destination.place}</p>  <p>ETA :${ETADatetime}</p> <p>Total Duration :${Total_Duration}</p>
//<p>Total Miles :${Totalmiles}</p>`

//                            `<h4>${destinations[i].destination.place}</h4><p>${destinations[i].destination.place}</p>  <p>ETA :${ETADatetime}</p> <p>Total Duration :${total_duration_times}</p>
//<p>Total Miles :${totalmile}</p>`

                            `<div class="pop-head3" style="margin-top:5px"><label class="pop-head-font"
style="margin-top:15px">${destinations[i].destination.place}</label></div><div class="pop-bootom1" style="margin-top:5px;float:left">

<label>ETA            :${ETADatetime}</label>  <br>
<label>Total Duration :${total_duration_times}</label> <br>
<label>Total Miles    :${totalmile}</label></div>`
                        )
                )
                .addTo(map);
        }

        //-------------
      

        totaldistance = 0;
        totalmile = 0;
        totalduration = 0;
        ETADatetime = "";
        Total_Distance = "";
        Total_Duration = "";


    }
    displayLoading("#map", false);
}


function moveUp(e) {

    e.preventDefault();

    var dataItem = this.dataItem(this.select());
    moveRow(this, dataItem, -1);
}


function moveDown(e) {

    e.preventDefault();

    var dataItem = this.dataItem(this.select());
    moveRow(this, dataItem, 1);
}

function swap(a, b, propertyName) {
    var temp = a[propertyName];
    a[propertyName] = b[propertyName];
    b[propertyName] = temp;
}

function moveRow(grid, dataItem, direction) {
    var record = dataItem;
    if (!record) {
        return;
    }

    // //debugger;
    var dataItem = this.dataItem(this.select());
    var newIndex = this.dataSource.indexOf(dataItem);

    direction < 0 ? newIndex-- : newIndex++;
    swap(grid.dataSource._data[newIndex], grid.dataSource._data[index], 'position');

    grid.dataSource.remove(record);
    grid.dataSource.insert(newIndex, record);
}

function UpdateOrders() {
    //debugger;
    var gridObj = $('#Dispatch').data('kendoGrid');

    var dispatch = $('#Dispatch').data('kendoGrid')._data;
    var dispatchList = [];
    var userid = "";
    routeOrder = 1;
    var dispatchRoutes = "";
    var dispatchNew = "";
    // //debugger;
    dispatch.forEach(function (value) {

        if (userid != value.userid) {
            routeOrder = 1
            userid = value.userid;
        } else {
            routeOrder = parseInt(routeOrder) + 1;
        }
        var dispatchNew = {
            routeorder: routeOrder,
            userid: value.userid,
            dispatchid: value.dispatchid
        }

        dispatchList.push(dispatchNew);
    });

    var Details = {
        DispatchRoutesModel: dispatchList
    }

    dispatchRoutes.DispatchRoutesModel = dispatchList;

    $.when(dispatchJson = JSON.stringify(Details)).done(function (x) {
        planalert(dispatchJson);
        // //debugger;
        $.ajax({
            url: "/Dispatch/UpdateDispatchRouteOrder",
            type: "POST",
            dataType: "json",
            data: dispatchJson,
            contentType: "application/json; chartset=uft-8",
            success: function (response) {
                var gridObj = $('#Dispatch').data('kendoGrid');
                gridObj.dataSource.read();
                gridObj.refresh();

            },
            error: function (xhr, status, error) {
                planalert(xhr.responseText, "Error");
            }
        });
    });
}


function LoadUserRoutes() {
    //debugger;;
    var geojson = "";
    $.ajax({
        url: "/Dispatch/GetUserRoutes",
        type: "GET",
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            ////debugger;
            geojson = response;

            var lastcoordinates = [];
            var zoomsize = 8;

            // markers saved here
            if (geojson.length > 0) {

                $(".marker").remove();
                $(".markerstart").remove();
                $(".truckcolor").remove();
                $(".markeruser").remove();

                $(".markernew").remove();
                     
                //if (geojson.features.length > 0 && geojson.features.length < 5) {
                //    zoomsize = 4;
                //} else if (geojson.features.length > 5 && geojson.features.length < 10) {
                //    zoomsize = 4;
                //}
                      var responsedata = {};
                        responsedata.type = 'Feature';
                        responsedata.geometry = {};
                        responsedata.geometry.type = "LineString";
                        responsedata.geometry.coordinates = [];

                    if (map.getSource("routesrc") != undefined) {
                        map.getSource("routesrc").setData(responsedata);
                    }

                if (map.getSource("routecurrentsrc") != undefined) {
                    map.getSource("routecurrentsrc").setData(responsedata);
                }
                
              


                for (i = 0; i < geojson.length; i++) {
                    // create a HTML element for each feature
                    const el = document.createElement('div');
                    el.className = 'markeruser';



                    if (geojson[i].location != null) {

                        var arr = [];
                        arr[0] = geojson[i].location.longitude;
                        arr[1] = geojson[i].location.latitude;


                        new mapboxgl.Marker(el)
                            /*.setLngLat(loc.geometry.coordinates)*/
                            .setLngLat(arr)
                            .setPopup(
                                new mapboxgl.Popup({ offset: 25, closeButton: true, closeOnMove: true, className: 'pop-up-op' }) // add popups
                                    .setHTML(
                                        /*`<h3>Current Location</h3><p>${arr.longitude}</p>`*/
                                    /* `<h3>Current Location</h3><p>${arr[0]}</p><p>${arr[1]}</p>`*/
                                        `<div class="pop-head" style="margin-top:5px"><label class="pop-head-font"
style="margin-top:15px">${geojson[i].username}</label></div><div class="pop-bootom1" style="margin-top:5px;float:left">

<label>Longitude:${arr[0]}</label>  <br>
<label>Latitude : ${arr[1]}</label>  <br></div> <br>`
//  <lbel>Address :${geojson[i].area}</label> <br>
//<label>City     :${geojson[i].city}</label> <br>
//   <label>State :${geojson[i].state}</label>

                                    )
                            )
                            .addTo(map);
                        if (i == 0) {
                            lastcoordinates = arr;
                        }                        
                    }

                    //else {
                       
                    //    var responsedata = {};
                    //    responsedata.type = 'Feature';
                    //    responsedata.geometry = {};
                    //    responsedata.geometry.type = "LineString";
                    //    responsedata.geometry.coordinates = [];
                       
                    //    map.getSource("route").setData(responsedata);

                    //    // add markers to map

                    //    //const el = document.createElement('div');
                    //    //el.className = 'marker';
                    //    //// make a marker for each feature and add it to the map
                    //    //new mapboxgl.Marker(el)
                    //    //    .setLngLat(coordinates)
                    //    //    .addTo(map);
                    //    //map.flyTo({
                    //    //    center: [locationResponse.longitude, locationResponse.latitude],
                    //    //    zoom: 16,
                    //    //    speed: 5,
                    //    //});
                    //}
                    // make a marker for each feature and add it to the map

                }
                map.flyTo({
                    center: lastcoordinates,
                    zoom: zoomsize,
                    speed: 5,
                });
                resizeWrapper();
                resizeGrid();

            }
        },
        error: function (xhr, status, error) {

        }
    });
   
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
        //$(".mapboxgl-canvas").height(parseInt($("#Dispatch").height()) - 200);
        //$("#map").height(parseInt($(".mapboxgl-canvas").height()) - 200);
        //map.resize()
        
        //$("#map").height(parseInt($("#Dispatch").height()));
        //$("#map").height(parseInt(dataArea.height()));
        //$("#map").height(parseInt($("#Dispatch").height()));
        setTimeout(function () {

            $("#map").height(parseInt($("#Dispatch").height()));
            //   $(".mapboxgl-map").height(parseInt($("#Dispatch").height()));
            //$(".mapboxgl-canvas").height(parseInt($("#Dispatch").height()));
            //$("#map").height(parseInt($("#Dispatch").height()));
        }, 4000);
       
    });
    


}
function resize() {
    var gridElement = $("#Dispatch"),
        dataArea = gridElement.find(".k-grid-content"),
        gridHeight = gridElement.innerHeight(),
        otherElements = gridElement.children().not(".k-grid-content"),
        otherElementsHeight = 0;
    //$("#Dispatch").height(parseInt($(window).height()) - 200);
    //dataArea.height(parseInt($(window).height()) - 300)
    //$("#Dispatch").height(parseInt($(window).height()) - 185);
    //dataArea.height(parseInt($(window).height()) - 280);

    $("#Dispatch").height(627);

    dataArea.height(parseInt(627));
}



        //function loadroutes() {
    //        LoadUserRoutes();
    //    $("#Dispatch").height(parseInt($(window).height()) - 185);
    //    var dataArea = gridElement.find(".k-grid-content")
    //    dataArea.height(parseInt($(window).height()) - 280);
    //}

var previousGrouping = {};
var currentchanges = false;

function onDataBoundSRV1(e) {
    //this.expandRow(false.tbody.find("tr.k-master-row").first());
    //debugger;
    var grid = this;
    var firstGroup = grid.dataSource.group()[0].field;

    // var grid = $(this).data("kendoGrid");
    var gridData = grid.dataSource.view();
    for (var i = 0; i < gridData.length; i++) {

        // var uid = gridData[i].uid;
        // var groupRow = grid.table.find("tr[data-uid='" + uid + "']");
        //var currentUid = gridData[i].uid;
        //     if (gridData[i].value =="Alexa L") {
        //  var currentRow = grid.table.find("tr[data-uid='" + currentUid + "']");
        //var createUserButton = $(currentRow).find("butCreateUser");
        //createUserButton.hide();

        var routeorderchanges = false;
        currentchanges = false;
        for (var j = 0; j < gridData[i].items.length; j++) {

            var currentUid = gridData[i].items[j].uid;

            var currentRow = grid.table.find("tr[data-uid='" + currentUid + "']");
            var currentcommandbutton = currentRow.children().eq(12);
            var currentcommandbutton2 = currentRow.find(".fa-share-alt");
          //  currentcommandbutton.css('color', '#7332a8');

            if (gridData[i].items[j].islocationshared == true) {
                currentcommandbutton2.css('color', '#e60b1a');
            }
       
         //   var currentSharebutton = currentcommandbutton.table.find("td[a]");

            //   currentRow.hide();
            if (gridData[i].items[j].recordstatus == '0') {
                //currentRow.css('color', 'red');
                currentchanges = true;
                var cell = currentRow.children().eq(2);
                cell.css('color', 'red');
                cell = currentRow.children().eq(3);
                cell.css('color', 'red');
            }
            else if (gridData[i].items[j].recordstatus == '1') {
                //currentRow.css('color', '#2bcf44');
                var cell = currentRow.children().eq(2);
                cell.css('color', '#2bcf44');
                cell = currentRow.children().eq(3);
                cell.css('color', '#2bcf44');
                currentchanges = true;
            }
            else if (gridData[i].items[j].currentrouterorder != '' && gridData[i].items[j].currentrouterorder != null) {

                if (gridData[i].items[j].routeorder != gridData[i].items[j].currentrouterorder) {
                    //currentRow.css('color', '#4d02ed');
                    currentchanges = true;

                    var cell = currentRow.children().eq(2);
                    cell.css('color', '#4d02ed');
                    cell = currentRow.children().eq(3);
                    cell.css('color', '#4d02ed');
                }
                else {
                    //currentRow.css('color', '#212529');

                    var cell = currentRow.children().eq(2);
                    cell.css('color', '#212529');
                    cell = currentRow.children().eq(3);
                    cell.css('color', '#212529');

                    //         grid.dataSource.options.fields[i].groupHeaderTemplate.replace("<label class='customMap' for='user'>User : #= recordstatus # </label> <button class='btn btn-primary customEdit'><i class='fa fa-plus'></i></button>");

                }
            }
            else {

                var cell = currentRow.children().eq(2);
                cell.css('color', '#212529');
                cell = currentRow.children().eq(3);
                cell.css('color', '#212529');

                //currentRow.css('color', '#212529');
                //          grid.dataSource.options.fields[i].groupHeaderTemplate.replace("<label class='customMap' for='user'>User : #= recordstatus # </label> <button class='btn btn-primary customEdit'><i class='fa fa-plus'></i></button>");

            }

            if (gridData[i].items[j].routeorder != '0') {

                if (gridData[i].items[j].currentrouterorder != '' && gridData[i].items[j].currentrouterorder != null) {


                    if ((gridData[i].items[j].recordstatus == '0' || gridData[i].items[j].recordstatus == '1') || (gridData[i].items[j].routeorder != gridData[i].items[j].currentrouterorder)) {

                        routeorderchanges = true;

                        currentchanges = true;

                    }
                }

                else {
                    //  currentRow.css('color', '#212529');

                    //         grid.dataSource.options.fields[i].groupHeaderTemplate.replace("<label class='customMap' for='user'>User : #= recordstatus # </label> <button class='btn btn-primary customEdit'><i class='fa fa-plus'></i></button>");

                }
            }


        }

        for (var j2 = 0; j2 < gridData[i].items.length; j2++) {

            var currentUid = gridData[i].items[j2].uid;
            var currentRow = grid.table.find("tr[data-uid='" + currentUid + "']");
            if (routeorderchanges == true) {
                if (gridData[i].items[j2].routeorder != '0') {
                    //currentRow.css('background-color', '#d9dea6');
                    var cell = currentRow.children().eq(2);
                    cell.css('color', '#015EE9');
                    cell = currentRow.children().eq(3);
                    cell.css('color', '#015EE9');
                }

                if (gridData[i].items[j2].islocationshared == true) {
                    e.sender.tbody.find(".k-grid-EditCustom").removeClass("locationnotshared");
                    e.sender.tbody.find(".k-grid-EditCustom").addClass("locationshared");
                } else {
                    e.sender.tbody.find(".k-grid-EditCustom").removeClass("locationshared");
                    e.sender.tbody.find(".k-grid-EditCustom").addClass("locationnotshared");
                }
            }


        }
        //debugger
        //var rows = e.sender.tbody.children();
        //for (var j = 0; j < rows.length; j++) {
        //    var row = $(rows[j]);

        //    var cell = row.children().eq(2);
        //    cell.css('color', 'black');
        //    //cell.addClass("actioncolumn");
        //}     

    }


    grid.collapseGroup(grid.tbody.find(">tr.k-grouping-row"));

  
    grid.element.on('click', '.customEdit', function (e) {
        //  debugger;

        //debugger;
        var dataItem = grid.dataItem($(e.target).closest("tr"));
        $("#DispatchAssignWindow_wnd_title").text("Dispatch Assignment  for " + dataItem.username + "");
        $("#hdnCurrentUser").val(dataItem.userid);
        LoadAssignDispatch(dataItem.userid);
      
     //   LoadDispatchRouteCurrentUsersmaproutpath2();



    });


    grid.element.on('click', '.SendDistination', function (e) {
        debugger;

        //debugger;
        var dataItem = grid.dataItem($(e.target).closest("tr"));

        $("#hdnCurrentUser").val(dataItem.userid);
        if ($("#gridclick").val() == "0") {
            $("#gridclick").val("1");
            $.ajax({
                url: "/Dispatch/Senddispatchrouts_V2",
                type: "POST",
                dataType: "json",
                data: JSON.stringify(dataItem.userid),
                contentType: "application/json; chartset=uft-8",
                success: function (response) {
                    $("#gridclick").val("0");
                    debugger;
                    if (response == true) {
                        var gridObj = $('#Dispatch').data('kendoGrid');
                        gridObj.dataSource.read();
                        gridObj.refresh();
                    }
                    else {
                        planalert(response, "Error");
                    }


                },
                error: function (xhr, status, error) {
                    $("#gridclick").val("0");
                    planalert(xhr.responseText, "Error");
                }
            });
        }


    });

    //Loadcurrentuserrout2
    grid.element.on('click', '.customtrash', function (e) {
       
        e.preventDefault();
        //debugger;
       
        var dataItem = grid.dataItem($(e.target).closest("tr"));
       
        if (dataItem != null) {
            if ($("#gridclick").val() == "0") {
                $("#gridclick").val("1");
                $("#hdnCurrentUser").val(dataItem.userid);
                var a = dataItem.userid;
                kendo.confirm("Do you wish to archive this dispatch?")
                .done(function () {                

                        $.ajax({
                            url: "/Dispatch/deletedispatchrouts_V2",
                            type: "POST",
                            dataType: "json",
                            data: JSON.stringify(dataItem.userid),
                            contentType: "application/json; chartset=uft-8",
                            success: function (response) {
                                $("#gridclick").val("0")
                                //  debugger;
                                if (response == true) {
                                    var gridObj = $('#Dispatch').data('kendoGrid');
                                    gridObj.dataSource.read();
                                    gridObj.refresh();
                                }
                                else {
                                    planalert(response, "Error");
                                }

                            },
                            error: function (xhr, status, error) {
                                $("#gridclick").val("0")
                                planalert(xhr.responseText, "Error");
                            }
                        });

                  
                })
                    .fail(function () {
                     $("#gridclick").val("0");
                     return false;
                });
       
        }

        }
    });


    grid.element.on('click', '.customMap', function (e) {
      //   debugger;
        var dataItem = grid.dataItem($(e.target).closest("tr"));

        $("#hdnCurrentUser").val(dataItem.userid);

       

        var userid = $("#hdnCurrentUser").val();
        $.ajax({
            url: "/Dispatch/GetDispatchList_Preview?userId=" + userid,
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
    //            debugger;
                if (currentchanges == true) {
                    dispatchlistarraymain = [];
                    //  debugger;

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
                            wellid: value.wellid

                            //  latitude_2: value.latitude,
                            //  longitude_2: value.longitude
                        };
                        dispatchlistarraymain.push(dispatchlistarrayvalues);

                    });
                    //  Loadcurrentuserrout2();
                }
                LoadAllUserPaths();
            }
        });


    //    LoadDispatchRouteCurrentUsersmaproutpath();

    });
    $("#Dispatch tbody").on("click", "tr", function (e) {
        /* debugger;*/
        var dataItem = grid.dataItem($(e.target).closest("tr"));

        $("#hdnCurrentUser").val(dataItem.userid);
    });

    grid.element.on('dblclick', '.customMap', function (e) {

        //debugger;
        var dataItem = grid.dataItem($(e.target).closest("tr"));

        $("#hdnCurrentUser").val(dataItem.userid);

        LoadUserReRoutes(dataItem.userid);
       // LoadDispatchRouteCurrentUsersmaproutpath();

    });
    var dataItems = this.dataSource.view();
    for (var i = 0; i < dataItems.length; i++) {
        if (i == dataItems.length - 1) {
            $.when(loadroutes()).done(function (x) {
                setTimeout(function () {
                    //  $(".mapboxgl-map").height(parseInt($("#Dispatch").height()));
                    $("#map").height(parseInt($("#Dispatch").height()));
                }, 3000);
            });
        }
    }
}

var UserDetails = { userId: null, operatorId: null, activity_id: null }
var dishbatchgrdobj;
function customShare(e) {
    debugger;
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    dishbatchgrdobj = this.dataItem($(e.currentTarget).closest("tr"));
    var subscriptionstatus = dataItem.subscriptionstatus;
    var _userid = dataItem.userid;
    var _operatorid = null;
    var _activityid = dataItem.activityid;
    var result = kendo.confirm("Do you want to share this location to Operator ?")
        .done(function () {

          
            //var subscriptionstatus = dataItem.subscriptionstatus;
            //kendo.alert(subscriptionstatus);

            if (subscriptionstatus === "Active") {
                if (dataItem.customer !== "") {

                    openProviderDirectory();

                    UserDetails = { userId: _userid, operatorId: _operatorid, activityId: _activityid  }


                    //$.ajax({
                    //   // url: "/Dispatch/SendMailforOprator?userId='" + userid + "' & operatorId=" + operatorid  ,
                    //    url: "/Dispatch/ShareDriverLocationToOperator",
                    //    type: "POST",
                    //    dataType: "json",
                    //   // data: JSON.stringify(data),
                    //    data: JSON.stringify(UserDetails),
                    //    contentType: "application/json; chartset=uft-8",
                    //    success: function (response) {
                    //        kendo.alert("This Location will be sent to Operator :" + dataItem.customer);
                    //    },
                    //    error: function (xhr, status, error) {
                    //        displayLoading("#DispatchAssignWindow", false);
                    //        planalert(xhr.responseText, "Error");
                    //    }
                    //});

                } else {


                    openProviderDirectory();

                    UserDetails = { userId: _userid, operatorId: _operatorid, activityId: _activityid}

                    //$.ajax({
                    //    // url: "/Dispatch/SendMailforOprator?userId='" + userid + "' & operatorId=" + operatorid  ,
                    //    url: "/Dispatch/ShareDriverLocationToOperator",
                    //    type: "POST",
                    //    dataType: "json",
                    //    // data: JSON.stringify(data),
                    //    data: JSON.stringify(UserDetails),
                    //    contentType: "application/json; chartset=uft-8",
                    //    success: function (response) {
                    //        kendo.alert("This Location will be sent to Operator");
                    //    },
                    //    error: function (xhr, status, error) {
                    //        displayLoading("#DispatchAssignWindow", false);
                    //        planalert(xhr.responseText, "Error");
                    //    }
                    //});
                }

                return false;
            } else {
                window.customPrompt("You need to Subscribe to the Advisor !").then(function (data) {
                    window.location.href = "/registration";
                }, function () {
                    //window.myalert("Cancel entering value.");
                });
            }



        })
        .fail(function () {
            return false;
        });
    
    if (result == true) {
        kendo.alert("You need to Subscribe to the Advisor");
        setTimeout(
            300
        );
    }
}


function closeProvider() {
    var window = $("#SelectOperator").data("kendoWindow");
    window.close();
}



function openProviderDirectory() {
    var wnd = $("#SelectOperator").data("kendoWindow");
    wnd.center().open();
}



function onGridSave(e) {
    setTimeout(function () {
        $('#providers').data('kendoGrid').dataSource.read().then(function () {
            $('#providers').data('kendoGrid').refresh();
        });
    }, 1800);
}


function sharebutncolor() {
    debugger;
    var dspatchgrid = $("#Dispatch").data("kendoGrid");
    
    var dataItems = dishbatchgrdobj;
  //  var dataItems = dspatchgrid.dataSource.view();

   

    var currentRow = dataItems;
    var currentcommandbutton = currentRow.children().eq(12);
    var currentcommandbutton2 = currentRow.find(".fa-share-alt");
    //  currentcommandbutton.css('color', '#7332a8');

 
        currentcommandbutton2.css('color', '#e60b1a');
    
    dishbatchgrdobj = null;

    //for (var i = 0; i < dataItems.length; i++) {
        
    //}
}

$(document).ready(function () {

    $(".k-grid-update").click(function (e) {

      //  var sharbutton = $(this).val();
      ///  sharbutton.css('color', '#e60b1a');
            
      ///  debugger;
        var sourcegrid = $('#vendors').data('kendoGrid');

        const SelectedItems = sourcegrid.select();

        if (SelectedItems.length > 0) {
            sourcegrid.select().each(function () {
                var dataItem = sourcegrid.dataItem($(this));
                var vendor =
                {
                    CompanyId: dataItem.ID,
                    Secondary: false,
                };


                debugger;
                UserDetails.operatorId = vendor.CompanyId;

                $.ajax({
                    // url: "/Dispatch/SendMailforOprator?userId='" + userid + "' & operatorId=" + operatorid  ,
                    url: "/Dispatch/ShareDriverLocationToOperator",
                    type: "POST",
                    dataType: "json",
                    // data: JSON.stringify(data),
                    data: JSON.stringify(UserDetails),
                    contentType: "application/json; chartset=uft-8",
                    success: function (response) {
                       // sharebutncolor();
                        //   kendo.alert("This Location will be sent to Operator :" + dataItem.customer);
                        var gridObj = $('#Dispatch').data('kendoGrid');
                        gridObj.dataSource.read();
                        gridObj.refresh();

                        var window = $("#SelectOperator").data("kendoWindow");
                        window.close();
                        return false;
                    },
                    error: function (xhr, status, error) {
                        displayLoading("#DispatchAssignWindow", false);
                        planalert(xhr.responseText, "Error");
                    }
                });

               




              //  alert(vendor.CompanyId)
             
            });
        }
        else {
            planalert("Please select a operator", "Information");
        }
    });

});
