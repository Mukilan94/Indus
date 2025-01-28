function RefreshRoutes() {
    //alert('Refresh routes');
    $.ajax({
        url: '/Dispatch/RefreshRoutes',
        type: 'POST',
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            console.log(response);
            var gridObj = $('#Dispatch').data('kendoGrid');
            gridObj.dataSource.read();
            gridObj.refresh();
        },
        error: function (xhr, status, error) {
            planalert(xhr.responseText, "Error");
        }
    })
}

var userId = $("#hdnCurrentUser").val();
var mapDataSource = new kendo.data.DataSource({
    transport: {
        read: {
            url: 'DispatchSRV/DispatchRoutes_Read?user=' + userId,
            dataType: "jsonp"
        }
    }
});

function onDataBound(e) {
    //////debugger;
    //alert('Data bound event click');
    var grid = this;
    //grid.element.off('click');

    grid.element.on('click', function (e) {
        //////debugger;
        //planalert('Grid row click');
        var dataItem = grid.dataItem($(e.target).closest("tr"));
        $("#hdnCurrentUser").val(dataItem.userid);
        //$("#hdnDispatchNotes").val(dataItem.dispatchnotes);
        $("#hdnDispatchId").val(dataItem.dispatchid);
        //loadMap();       
    
        $.ajax({
            url: '/DispatchSRV/DispatchRoutes_Read?user=' + dataItem.userid,
            type: 'POST',
            dataType: "json",
            contentType: "application/json; chartset=uft-8",
            success: function (response) {
                console.log(response);

                //type: 'geojson',            data: {                type: 'Feature',                properties: {},                geometry: {                    type: 'LineString',                    coordinates: [                        [-100.483696, 37.833818],                        [-78.483482, 40.833174],					[-75.483482, 35.833174]                    ]                }            }                                               
                //updateRoutes(response);

            },
            error: function (xhr, status, error) {
                planalert(xhr.responseText, "Error");
            }
        })

    });

    grid.element.on('click', '.customEdit', function (e) {
        debugger;
        //grid.editRow($(e.target).closest('tr'));
        ////debugger;
        var dataItem = grid.dataItem($(e.target).closest("tr"));
        //var dataItem = grid.dataItem($(e.target).closest("tr"));
        //alert('dataItem.userId' + dataItem.userId);
        //call assign Dispatch window
        $("#hdnCurrentUser").val(dataItem.userid);
        LoadAssignDispatch(dataItem.userid);

    });

    grid.element.on('click', '.customMap', function (e) {
        debugger;
        //grid.editRow($(e.target).closest('tr'));
        //debugger;
        var dataItem = grid.dataItem($(e.target).closest("tr"));
        //var dataItem = grid.dataItem($(e.target).closest("tr"));
        //alert('dataItem.userId' + dataItem.userId);
        $("#hdnCurrentUser").val(dataItem.userid);
        //call assign Dispatch window
        //LoadAssignDispatch(dataItem.userid);
        LoadAllUserPaths();

    });

    var dataItems = this.dataSource.view();
    for (var i = 0; i < dataItems.length; i++) {
        if (i == dataItems.length - 1) {
            $.when(loadroutes()).done(function (x) {
                setTimeout(function () {
                    $(".mapboxgl-map").height(parseInt($("#Dispatch").height()));
                }, 4000);
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
    //return;
    //////debugger;
    $.ajax({
        url: '/DispatchSRV/GetUserCurrentLocation?userId=' + $("#hdnCurrentUser").val(),
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
                var responsedata = {};
                responsedata.type = 'Feature';
                responsedata.geometry = {};
                responsedata.geometry.type = "LineString";
                responsedata.geometry.coordinates = [];
                //updateRoutes(response.destinations, response.location);
                map.getSource("route").setData(responsedata);


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
        el.className = 'marker';
        // make a marker for each feature and add it to the map
        new mapboxgl.Marker(el)
            .setLngLat(coordinates)
            .setPopup(
                new mapboxgl.Popup({ offset: 25 }) // add popups
                    .setHTML(
                        `<h3>Destination</h3><p>${locationResponse.latitude}</p><p>${locationResponse.longitude}</p>`
                    )
            )
            .addTo(map);


        //map.flyTo({ center: [locationResponse.longitude, locationResponse.latitude] });

        map.flyTo({
            center: [locationResponse.longitude, locationResponse.latitude],
            zoom: 16,
            speed: 5,
        });

        //map.setZoom(6);

    } else {

        coordinates.push("-102.36896137371849");
        coordinates.push("31.846556737999546");
        //map.setCenter(10);
        // add markers to map
        //for (const feature of geojson.features) {
        // create a HTML element for each feature
        const el = document.createElement('div');
        el.className = 'marker';
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


function LoadAllUserDestinations() {
    var geojson = "";
    $.ajax({
        url: "/Dispatch/GetUserDestinations?userId=" + $("#hdnCurrentUser").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            geojson = response;
            //////debugger;
            var lastcoordinates = [];
            var zoomsize = 4;
            //var marker = new mapboxgl.Marker().addTo(map);
            //marker.remove();
            // markers saved here
            $(".marker").remove();
            $(".markerstart").remove();
            $(".truckcolor").remove();
            var currentMarkers = [];
            if (geojson.features.length > 0) {

                $(".marker").remove();
                $(".markerstart").remove();
                $(".truckcolor").remove();
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


                //var window = $("#DispatchNotesWindow").data("kendoWindow");
                //window.close();
            }
        },
        error: function (xhr, status, error) {
            //planalert(xhr.responseText, "Error");
        }
    });

}

function LoadAllUserPaths() {
    //debugger;
    $.ajax({
        url: "/DispatchSRV/GetUserCurrentRoutes?userId=" + $("#hdnCurrentUser").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {

            geojson = response.destinations;
            ////debugger;
            var lastcoordinates = [];
            var zoomsize = 4;
            //var marker = new mapboxgl.Marker().addTo(map);
            //marker.remove();
            // markers saved here
            var currentMarkers = [];
            if (geojson != null) {

                $(".marker").remove();
                $(".markerstart").remove();
                $(".truckcolor").remove();
                updateRoutes(response.destinations, response.location);

                //var window = $("#DispatchNotesWindow").data("kendoWindow");
                //window.close();
            }
        },
        error: function (xhr, status, error) {
            //planalert(xhr.responseText, "Error");
        }
    });

}

function updateRoutes(destinations, location) {

    var responsedata = {};
    ////debugger;
    var centercoordinates = [];
    //centercoordinates[0] = location.longitude;
    //centercoordinates[1] = location.latitude;
    if (destinations.length > 0) {
        responsedata.type = 'Feature';
        responsedata.geometry = {};
        responsedata.geometry.type = "LineString";
        responsedata.geometry.coordinates = [];

        var k = 0;
        for (m = 0; m < destinations.length; m++) {
            var legs = destinations[m].directions.legs;
            for (i = 0; i < legs.length; i++) {
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

    ////debugger;
    console.log('responsedata ' + responsedata);
    console.log('responsedata json ' + JSON.stringify(responsedata));
    var jsonobj = JSON.parse('{  "type": "Feature",  "geometry": {"type": "LineString","coordinates": [[-100.483696, 37.833818],[-78.483696, 40.833818]]},  "properties": {    "name": "Dinagat Islands"  }}');


    map.flyTo({
        center: centercoordinates,
        zoom: 6,
        speed: 5
    });

    map.getSource("route").setData(responsedata);
    if (destinations.length > 0) {
        ////legs[i].path[j].lng, legs[i].path[j].lat
        ////var destination = destinations[0].directions.legs;
        //var markerPoints = destinations[0].directions.legs[0].path[j].longitude;//.destination_coordinates;
        //debugger;
        var coordinates = [];
        coordinates[0] = destinations[0].directions.legs[0].path[0].lng;
        coordinates[1] = destinations[0].directions.legs[0].path[0].lat;
        //for (const feature of geojson.features) {
        // create a HTML element for each feature
        const el = document.createElement('div');
        el.className = 'markerstart';

        // make a marker for each feature and add it to the map
        new mapboxgl.Marker(el)
            .setLngLat(coordinates)
            .setPopup(
                new mapboxgl.Popup({ offset: 25 }) // add popups
                    .setHTML(
                        `<h3>${destinations[0].destination}</h3><p>${destinations[0].destination}</p>`
                    )
            )
            .addTo(map);


        for (m = 0; m < destinations.length; m++) {
            var markerPoints = destinations[m].destination_coordinates;
            //debugger;
            var coordinates = [];
            coordinates[0] = markerPoints.longitude;
            coordinates[1] = markerPoints.latitude;
            //for (const feature of geojson.features) {
            // create a HTML element for each feature
            const el = document.createElement('div');
            el.className = 'marker';
            if (Math.abs(markerPoints.longitude - location.longitude) >= 0.0001 && Math.abs(markerPoints.latitude - location.latitude) >= 0.0001) {
                // make a marker for each feature and add it to the map
                new mapboxgl.Marker(el)
                    .setLngLat(coordinates)
                    .setPopup(
                        new mapboxgl.Popup({ offset: 25 }) // add popups
                            .setHTML(
                                `<h3>${destinations[m].destination}</h3><p>${destinations[m].destination}</p>`
                            )
                    )
                    .addTo(map);
            }            
        }
    }

    //Set user's current location
    var locationcoordinates = [];
    locationcoordinates[0] = location.longitude;
    locationcoordinates[1] = location.latitude;

    const el = document.createElement('i');
    el.className = 'fa fa-truck fa-2x truckcolor';

    new mapboxgl.Marker(el)
        .setLngLat(locationcoordinates)
        .addTo(map);
    //
    //lastcoordinates = feature.geometry.coordinates;
    //}
    //msap.getSource("route").setData(jsonobj);
}

function LoadUserRoutes() {
    //debugger;
    var geojson = "";
    $.ajax({
        url: "/DispatchSRV/GetUserRoutes",
        type: "GET",
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            ////debugger;
            geojson = response;

            var lastcoordinates = [];
            var zoomsize = 16;

            // markers saved here
            if (geojson.length > 0) {

                $(".marker").remove();
                $(".markerstart").remove();
                $(".truckcolor").remove();
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

                if (map.getSource("route") != undefined) {
                    map.getSource("route").setData(responsedata);
                }


                for (i = 0; i < geojson.length; i++) {
                    // create a HTML element for each feature
                    const el = document.createElement('div');
                    el.className = 'marker';



                    if (geojson[i].location != null) {

                        var arr = [];
                        arr[0] = geojson[i].location.longitude;
                        arr[1] = geojson[i].location.latitude;


                        new mapboxgl.Marker(el)
                            /*.setLngLat(loc.geometry.coordinates)*/
                            .setLngLat(arr)
                            .setPopup(
                                new mapboxgl.Popup({ offset: 25 }) // add popups
                                    .setHTML(
                                        /*`<h3>Current Location</h3><p>${arr.longitude}</p>`*/
                                        `<h3>Current Location</h3><p>${arr[0]}</p><p>${arr[1]}</p>`
                                    )
                            )
                            .addTo(map);

                        lastcoordinates = arr;
                    }

                    else {
                        

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
    ////debugger;

    var gridElement = $("#Dispatch"),
        dataArea = gridElement.find(".k-grid-content"),
        gridHeight = gridElement.innerHeight(),
        otherElements = gridElement.children().not(".k-grid-content"),
        otherElementsHeight = 0;

    $("#Dispatch").height(parseInt($(window).height()) - 185);
    dataArea.height(parseInt($(window).height()) - 280);

    otherElements.each(function () {
        otherElementsHeight += $(this).outerHeight();
    });

    //alert('resize call grid height' + gridHeight);

    setTimeout(function () {
        $(".mapboxgl-map").height(parseInt($("#Dispatch").height()));
        //$(".mapboxgl-canvas").height(parseInt($("#Dispatch").height()));
        //$("#map").height(parseInt($("#Dispatch").height()));
    }, 4000);
}

function resize() {
    var gridElement = $("#Dispatch"),
        dataArea = gridElement.find(".k-grid-content"),
        gridHeight = gridElement.innerHeight(),
        otherElements = gridElement.children().not(".k-grid-content"),
        otherElementsHeight = 0;
    $("#Dispatch").height(parseInt($(window).height()) - 185);
    dataArea.height(parseInt($(window).height()) - 280);

}