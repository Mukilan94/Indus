function LoadDispatchRouteUsers() {
    var window = $("#DispatchAssignUserWindow").data("kendoWindow");
    window.refresh({
        url: "/DispatchSRV/LoadAdvisorUsers"
    });
  
    $("#DispatchAssignUserWindow").closest(".k-window").css({
        top: 150,
        left: 650
    });


    window.open();
}

function RefreshRoutes() {
   
    $.ajax({
        url: '/DispatchSRV/RefreshRoutes',
        type: 'POST',
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            $.ajax({
                url: '/DispatchSRV/GetUserRoutes',
                type: 'GET',
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
        },
        error: function (xhr, status, error) {
            planalert(xhr.responseText, "Error");
        }
    })
}

function LoadAssignDispatch(userId) {

    $("#hdnCurrentUser").val(userId);

    var window = $("#DispatchAssignWindow").data("kendoWindow");
    window.refresh({
        url: "/DispatchSRV/LoadAssignDispatch?userId=" + userId
    });

    $("#DispatchAssignWindow").closest(".k-window").css({
        top: 150,
        left: 650
    });
    window.open();
}
function LoadDispatchNotes() {
  
    var window = $("#DispatchNotesWindow").data("kendoWindow");
    window.refresh({
        url: "/DispatchSRV/LoadDispatchNotes"
    });

    $("#DispatchNotesWindow").closest(".k-window").css({
        top: 150,
        left: 650
    });

    window.open();
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


//DWOP
function onApiNumSelect(e) {
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
            url: 'DispatchSRV/DispatchRoutes_Read?user=' + userId,
            dataType: "jsonp"
        }
    }
});

function onDataBound(e) {
   
    var grid = this;
   

    grid.element.on('click', '.customEdit', function (e) {

        var dataItem = grid.dataItem($(e.target).closest("tr"));
       
        $("#hdnCurrentUser").val(dataItem.userid);
        LoadAssignDispatch(dataItem.userid);

    });

    grid.element.on('click', '.customMap', function (e) {

        var dataItem = grid.dataItem($(e.target).closest("tr"));
       
        $("#hdnCurrentUser").val(dataItem.userid);
      
        LoadAllUserPaths();

    });
    $("#Dispatch tbody").on("click", "tr", function (e) {
        /* debugger;*/
        var dataItem = grid.dataItem($(e.target).closest("tr"));

        $("#hdnCurrentUser").val(dataItem.userid);
    });
    grid.element.on('dblclick', '.customMap', function (e) {

        var dataItem = grid.dataItem($(e.target).closest("tr"));
        
        $("#hdnCurrentUser").val(dataItem.userid);
       
        LoadUserReRoutes(dataItem.userid);

    });

    var dataItems = this.dataSource.view();
    for (var i = 0; i < dataItems.length; i++) {
        if (i == dataItems.length - 1) {
            $.when(LoadUserRoutes()).done(function (x) {
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
    
        new mapboxgl.Marker(el)
            .setLngLat(coordinates)
            .setPopup(
                new mapboxgl.Popup({ offset: 25 }) // add popups
                    .setHTML(
                        `<h3>Destination</h3><p>${locationResponse.latitude}</p><p>${locationResponse.longitude}</p>`
                    )
            )
            .addTo(map);

        map.flyTo({
            center: [locationResponse.longitude, locationResponse.latitude],
            zoom: 16,
            speed: 5,
        });

        //map.setZoom(6);

    } else {

        coordinates.push("-102.36896137371849");
        coordinates.push("31.846556737999546");
     
        // add markers to map
      
        const el = document.createElement('div');
        el.className = 'marker';  
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

function DispatchDelete(e) {
  
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
  
    kendo.confirm("Are you sure you want to delete this?")
        .done(function () {
            $.ajax({
                url: "/DispatchSRV/DispatchDataDestroy?dispatchId=" + dataItem.dispatchid + "&userId=" + $("#hdnCurrentUser").val(),
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    var grid = $("#Dispatch").data("kendoGrid");
                    grid.dataSource.read();
                   
                }
            });
        })
        .fail(function () {
            return false;
        });
}


function UpdateDispatch() {
  
    var dispatchNotes = $("#instructionsNotes").val();
    var dispatchId = $("#hdnDispatchId").val();
    var dispatch = {
        dispatchid: dispatchId,
        dispatchnotes: dispatchNotes,
        userid: $("#hdnCurrentUser").val()
    }
    var dispatchJson = JSON.stringify(dispatch);
    $.ajax({
        url: "/DispatchSRV/UpdateDispatchNotes",
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
   
    var dataItem = this.dataItem(e.item.index());
    console.log(' dataItem name ' + dataItem.name);
    if (dataItem.name != "") {
        $("#latitude").val(dataItem.latitude).trigger("change");
        $("#longitude").val(dataItem.longitude).trigger("change");
      
        $("#rigId").val(dataItem.rig_id);
        $("#rigName").val(dataItem.rig).trigger("change");
    }
}

function onSearchOptionClick() {
 
    var radioValue = $("input[name='radio1']:checked").val();
   
    $("#numAPI").val("");
    $("#rigNameSearch").val("");

    if (radioValue == "Rig") {
        $("#divRigSearch").css("display", "block");
        $("#divApiSearch").css("display", "none");
    } else {
        $("#divRigSearch").css("display", "none");
        $("#divApiSearch").css("display", "block");
    }
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
}

function LoadAllUserDestinations() {

    var geojson = "";
    $.ajax({
        url: "/DispatchSRV/GetUserDestinations?userId=" + $("#hdnCurrentUser").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            geojson = response;
           
            var lastcoordinates = [];
            var zoomsize = 4;
         
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
                 
                    const el = document.createElement('div');
                    el.className = 'marker';

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

    //debugger;
    //var Dispatch = $("#Dispatch").data("kendoGrid");
    //var data = Dispatch.dataSource.data();
   

    //for (var i = 0; i < data; i++) {
    //    var currentDataItem = data[i].dispatchId;

    //}



    $.ajax({
        url: "/DispatchSRV/GetUserCurrentRoutes?userId=" + $("#hdnCurrentUser").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
           
            geojson = response.destinations;//.legs;
            if (geojson != null) {

                $(".marker").remove();
                $(".markerstart").remove();
                $(".truckcolor").remove();
                updateRoutes(response.destinations, response.location);

            } else {
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

grid.element.on('click', '.customtrash', function (e) {

    e.preventDefault();
    debugger;

    var dataItem = grid.dataItem($(e.target).closest("tr"));

    if (dataItem != null) {
        if ($("#gridclick").val() == "0") {
            $("#gridclick").val("1");
            $("#hdnCurrentUser").val(dataItem.userid);
            var a = dataItem.userid;
            kendo.confirm("Are you sure you want to delete this?")
                .done(function () {

                    $.ajax({
                        url: "/DispatchSRV/deletedispatchrouts_V2",
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
                    return false;
                });

        }

    }
});

function updateRoutes(destinations, location) {

    var responsedata = {};
   
    var centercoordinates = [];
   
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
       
       // debugger;
        var coordinates = [];
        coordinates[0] = destinations[0].directions.legs[0].path[0].lng;
        coordinates[1] = destinations[0].directions.legs[0].path[0].lat;
       
        const el = document.createElement('div');
        el.className = 'markerstart';

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
           // debugger;
            var coordinates = [];
            coordinates[0] = markerPoints.longitude;
            coordinates[1] = markerPoints.latitude;
            
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
}


function LoadUserReRoutes(userId) {

    $("#hdnCurrentUser").val(userId);

    var window = $("#DispatchUserRoutesWindow").data("kendoWindow");
    window.refresh({
        url: "/DispatchSRV/LoadUserRoutes?userId=" + userId
    });

    $("#DispatchUserRoutesWindow").closest(".k-window").css({
        top: 150,
        left: 650
    });
    window.open();

}


function LoadUserRoutes() {
   // debugger;
    var geojson = "";
    $.ajax({
        url: "/DispatchSRV/GetUserRoutes",
        type: "GET",
        dataType: "json",
        contentType: "application/json; chartset=uft-8",
        success: function (response) {
            debugger;
            geojson = response;

            var lastcoordinates = [];
            var zoomsize = 4;

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