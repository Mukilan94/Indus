
$(function () {
    $("#btnProposalCancel").click(function () {
        var wnd = $("#newRequestsWindow").data("kendoWindow");
        wnd.close();
    });
});

$(document).ready(function () {
    RealTimeReportChart();
    document.getElementById("TaskChart").style.display = "none";
});


function moveUp(e) {
    IsPlanDetailsChanged = true;
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    moveRow(this, dataItem, -1);
}

function moveDown(e) {
    IsPlanDetailsChanged = true;
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    moveRow(this, dataItem, 1);
}

function swap(a, b, propertyName) {
    var temp = a[propertyName];
    a[propertyName] = b[propertyName];
    b[propertyName] = temp;
}

function moveByRows(grid, dataItem, moveRows) {

    var record = dataItem;
    if (!record) {
        return;
    }
    //var newIndex = index = grid.dataSource.indexOf(record);
    ////direction < 0 ? newIndex--;
    //newIndex = -moveRows;

    var newIndex = index = parseInt(grid.dataSource.indexOf(record));
    var newTaskOrder = moveRows - (newIndex + 1);
    newIndex = -(moveRows - (newIndex + 1)); //- moveRows);
    swap(grid.dataSource._data[newIndex], grid.dataSource._data[index], 'position');

    var newdataItem = grid.dataSource.at(index - 1);
    console.log('Set Flag complete at Move up logic' + newTaskOrder);
    //var planStartDate = newdataItem.PlanStart;
    //record.set("PlanStart", planStartDate);
    //record.set("TaskOrder", newTaskOrder);

    grid.dataSource.remove(record);
    grid.dataSource.insert(newIndex, record);

    var movedRecord = grid.dataSource.at(newTaskOrder);
    //var movedRecord = grid.dataItem(e.target.closest("tr"));
    //movedRecord.set("TaskOrder", newTaskOrder+1);


    grid.select("tr:eq(" + parseInt(newIndex) + ")");

    var scrollContentOffset = grid.element.find("tbody").offset().top;
    var selectContentOffset = grid.select().offset().top;
    var distance = selectContentOffset - scrollContentOffset;

    grid.element.find(".k-grid-content").animate({
        scrollTop: distance
    }, 400);
    //$("#CurrentRowIndex").val("");
}

function moveRow(grid, dataItem, direction) {
    var record = dataItem;
    if (!record) {
        return;
    }
    var newIndex = index = grid.dataSource.indexOf(record);
    direction < 0 ? newIndex-- : newIndex++;
    swap(grid.dataSource._data[newIndex], grid.dataSource._data[index], 'position');

    if (direction == 1) { //Move Down logic to set PlanModified flag for recalculation starts  

        var planToBeModifyTaskIndex = parseInt(index) + 1;
        if (parseInt(planToBeModifyTaskIndex) <= parseInt(grid.dataSource.total())) {
            var newDataItem = grid.dataSource.at(parseInt(planToBeModifyTaskIndex));
            newDataItem.set("IsPlanModified", true);

            //Set Plan Start Date
            var planStartDate = newDataItem.PlanStart;
            record.set("PlanStart", planStartDate);
        }
    } else if (direction == -1) { //Move up logic to set PlanModified flag for recalculation starts  
        var newdataItem = grid.dataSource.at(index - 1);
        console.log('Set Flag complete at Move up logic');
        var planStartDate = newdataItem.PlanStart;
        record.set("PlanStart", planStartDate);
    }

    grid.dataSource.remove(record);
    grid.dataSource.insert(newIndex, record);
}

function Addtasks(wellId) {
    var destinationgrid = $("#PlanList_" + wellId).data("kendoGrid");
    //var dataRows = destinationgrid.items();
    //var rowIndex = dataRows.index(destinationgrid.select());
    if (destinationgrid != undefined) {
        var sel = destinationgrid.select();
        if (sel != undefined) {
            var sel_idx = sel.index();
        }
        //var idx = destinationgrid.dataSource.indexOf(taskData);
        if (sel_idx != undefined) {
            $("#CurrentRowIndex").val(parseInt(sel_idx));
            console.log('Plan Details rowIndex ' + sel_idx);
        }
    }
    
    var DrillingPlanId = $("#DrillingPlanId").data("kendoDropDownList").value();
    //alert(DrillingPlanId);
    var window = $("#AddTaskListWindow").data("kendoWindow");
    window.refresh({
        url: "/ActiveDrillPlan/Tasks?wellId=" + wellId + "&DrillPlanId=" + DrillingPlanId
    });
    window.center();
    window.open();
    window.wrapper.find('.k-window').css({
        'top': '210px !important'
    });
}


    function Cancel() {
        var wnd = $("#TasksDuplicate").data("kendoWindow");
        wnd.close();
    }


    function ServicedaysDataBound(e) {
        var selectedFooType = $("#ServiceDurationDays").data("kendoMultiColumnComboBox").value();
        if (selectedFooType == "") {
        this.select(0);
            this.trigger('change');
        }
    }
    function ServiceHoursDataBound(e) {
        var selectedFooType = $("#ServiceDurationHours").data("kendoMultiColumnComboBox").value();
        if (selectedFooType == "") {
        this.select(0);
            this.trigger('change');
        }
    }

    function ServiceMinutesDataBound(e) {
        var selectedFooType = $("#ServiceDurationMinutes").data("kendoMultiColumnComboBox").value();
        if (selectedFooType == "") {
        this.select(0);
            this.trigger('change');
        }
    }

    function SchedulerTime(e) {
        var ScheduleTimeValue = $("#ScheduleTimePicker").val();
        $("#ScheduleTime").data("kendoMaskedTextBox").value(ScheduleTimeValue);
        $("#ScheduleTime").data("kendoMaskedTextBox").trigger("change");
    }


    function OnGrigSave(e) {
        if (e.model.error) { }
        //$(".k-grid-update").click(function () {
        //setTimeout(function () {
        //    $('#TasksGrid').data('kendoGrid').dataSource.read().then(function () {
        //        $('#TasksGrid').data('kendoGrid').refresh();
        //    });
        //}, 1000);

        //});

}
function PlanBeforeEdit(e) {
    var wellId = sessionStorage.getItem("wellId");
    var destinationgrid = $("#PlanList_" + wellId).data("kendoGrid");
    //var dataRows = destinationgrid.items();
    //var rowIndex = dataRows.index(destinationgrid.select());
    if (destinationgrid != undefined) {
        var sel = destinationgrid.select();
        if (sel != undefined) {
            var sel_idx = sel.index();
        }
        //var idx = destinationgrid.dataSource.indexOf(taskData);
        if (sel_idx != undefined) {
            $("#CurrentRowIndex").val(parseInt(sel_idx));
            console.log('Plan Details rowIndex at New Task ' + sel_idx);
        }
    }
}

function PlangridEdit(e) {
    var wellId = sessionStorage.getItem("wellId");
        var destinationgrid = $("#PlanList_" + wellId).data("kendoGrid");
        //var dataRows = destinationgrid.items();
        //var rowIndex = dataRows.index(destinationgrid.select());
        if (destinationgrid != undefined) {
            var sel = destinationgrid.select();
            if (sel != undefined) {
                var sel_idx = sel.index();
            }
            //var idx = destinationgrid.dataSource.indexOf(taskData);
            if (sel_idx != undefined) {
                $("#CurrentRowIndex").val(parseInt(sel_idx));
                console.log('Plan Details rowIndex at New Task ' + sel_idx);
            }
        }

        var ScheduleTimeValue = $("#ScheduleTime").val();
        $("#ScheduleTimePicker").data("kendoTimePicker").value(ScheduleTimeValue);
        
        if (e.model.isNew()) {
            e.container.data("kendoWindow").title("Add Task");
            $('.k-grid-update').text("Save");
            $("#IsActive").prop('checked', true).trigger("change");
            $("#ActualPlanFinishedDate").data("kendoDateTimePicker").enable(false);
        } else {
            //alert(e.model.ActualPlanStart);
            if (e.model.ActualPlanStart === null || e.model.ActualPlanStart === undefined) {
                $("#ActualPlanFinishedDate").data("kendoDateTimePicker").enable(false);
            }
            else if (e.model.ActualPlanStart !== null && e.model.ActualPlanStart !== undefined ) {
                    ActualPlanStartChange(null,e.model.ActualPlanStart);
            } 	
            

        }
        if (e.model.SeletedDependency !== null && e.model.SeletedDependency !== undefined) {
            let arr = e.model.SeletedDependency.split(',')
            if (arr != null && arr != undefined) {              
                BindDependency(e.model.TaskId, arr);                  
            }
            else {
                BindDependency(e.model.TaskId, []);
            }
        }
        else {
            let arr = e.model.SeletedDependency
            if (arr != null && arr != undefined) {
                if (arr.length > 0) {
                    if (arr[0] = !"") {
                        BindDependency(e.model.TaskId, arr);
                    }
                }
            }
            else {
                BindDependency(e.model.TaskId, []);
            }
    }

    $.ajax({
        url: "/ChecklistTemplate/tasksExits?taskId=" + e.model.TaskId,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (response) {
            if (response.status === true) {
                document.getElementById("ExportMaster").style.display = "none";
            }
        }
    });

}



function BindDependency(id, selectedDependency) {
        $("#Dependency").kendoMultiSelect({
            placeholder: "Services Dependency...",
            dataTextField: "Name",
            dataValueField: "TaskId",
            autoBind: true,
            filter: "startswith",
            minLength: 3,
            //MinLength:3,
            dataSource: {
                type: "json",
                serverFiltering: true,
                transport: {
                    read: {
                        url: "/ChecklistTemplate/GetTaskDependencyList?id=" + id,
                    }
                }
            },
            value: selectedDependency,
            change: multiChangeTasks
        });
 }

function multiChangeTasks(e) {
    var multiselect = $("#Dependency").data("kendoMultiSelect");
    var selectedData = "";
    var items = multiselect.value();
    for (var i = 0; i < items.length;) {
        selectedData += items[i] + ";";
    }
    $("#SeletedDependency").val(selectedData);
    $("#SeletedDependency").trigger("change");
}

   
    function error(args) {
        if (args.errors) {
            var grid = $("#TasksGrid").data("kendoGrid");
            grid.one("dataBinding", function (e) {
        e.preventDefault();

                $.each(args.errors, function (propertyName) {
                    var messages = this.errors[0];
                    $('.errors').text(messages);
                    $('.errors').addClass("alert alert-danger");
                });
            });
        }
}
    function setSelectedTasks(e) {
        var row = $(event.srcElement).closest("tr");
        var grid = $(event.srcElement).closest("[data-role=grid]").data("kendoGrid");
        var dataItem = grid.dataItem(row);
    }
    function SelectedSpecialService(service) {

        if (service == 1 || service == 0) {
        document.getElementById("name").disabled = false;
            document.getElementById("Day").disabled = false;
            document.getElementById("ScheduleTimePicker").disabled = "diabled";
            document.getElementById("Duration").disabled = true;
            document.getElementById("depth").disabled = false;
            document.getElementById("LeadTime").disabled = false;
            document.getElementById("Dependancy").disabled = true;
        }
        else if (service == 2) {
        document.getElementById("name").disabled = false;
            document.getElementById("Day").disabled = false;
            document.getElementById("ScheduleTimePicker").disabled = false;
            document.getElementById("Duration").disabled = false;
            document.getElementById("depth").disabled = false;
            document.getElementById("LeadTime").disabled = false;
            document.getElementById("Dependancy").disabled = false;
        }
        else if (service == 3) {
        document.getElementById("name").disabled = false;
            document.getElementById("Day").disabled = false;
            document.getElementById("ScheduleTimePicker").disabled = false;
            document.getElementById("Duration").disabled = false;
            document.getElementById("depth").disabled = false;
            document.getElementById("LeadTime").disabled = false;
            document.getElementById("Dependancy").disabled = false;
        }
        else if (service == 4) {
        document.getElementById("name").disabled = false;
            document.getElementById("Day").disabled = false;
            document.getElementById("ScheduleTimePicker").disabled = "diabled";
            document.getElementById("Duration").disabled = true;
            document.getElementById("depth").disabled = false;
            document.getElementById("LeadTime").disabled = false;
            document.getElementById("Dependancy").disabled = true;
        }
    }


function OnServiceCategoryChange(e) {
    var ServiceCategoryId = $("#ServiceCategoryName").val();
    $("#ServiceCategoryId").data("kendoMaskedTextBox").value(ServiceCategoryId);
    $("#ServiceCategoryId").data("kendoMaskedTextBox").trigger("change");
}

function OnServiceCategoryDataBound(e) {
    var ServiceCategoryId = $("#ServiceCategoryId").val();
    $("#ServiceCategoryName").data("kendoMultiColumnComboBox").value(ServiceCategoryId);
    $("#ServiceCategoryName").data("kendoMultiColumnComboBox").trigger("change");
}

function OnEmployeeDataBound() {
    var ServiceCategoryId = $("#EmployeeId").val();
    $("#EmployeeName").data("kendoDropDownList").value(ServiceCategoryId);
    $("#EmployeeName").data("kendoDropDownList").trigger("change");
}

function OnEmployeeChange() {
    var ServiceCategoryId = $("#EmployeeName").val();
    $("#EmployeeId").data("kendoDropDownList").value(ServiceCategoryId);
    $("#EmployeeId").data("kendoDropDownList").trigger("change");
}

function formatDate(dateVal) {
    var newDate = new Date(dateVal);

    var sMonth = padValue(newDate.getMonth() + 1);
    var sDay = padValue(newDate.getDate());
    var sYear = newDate.getFullYear();
    var sHour = newDate.getHours();
    var sMinute = padValue(newDate.getMinutes());
    var sAMPM = "AM";

    var iHourCheck = parseInt(sHour);

    if (iHourCheck > 12) {
        sAMPM = "PM";
        sHour = iHourCheck - 12;
    }
    else if (iHourCheck === 0) {
        sHour = "12";
    }

    sHour = padValue(sHour);

    return sMonth + "-" + sDay + "-" + sYear + " " + sHour + ":" + sMinute + " " + sAMPM;
}

function padValue(value) {
    return (value < 10) ? "0" + value : value;
}
function printPlan(wellId, wellName, DrillPlanName, Rigname) {
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;
    width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;
    var left = ((width / 2) - (1500 / 2)) + dualScreenLeft;
    var top = ((height / 2) - (900 / 2)) + dualScreenTop;

    var PlanGridData = [];
    //$("#PlanList1_").data('kendoGrid').dataSource.data([]);
    var PlanGrid = $("#PlanList_" + wellId).data("kendoGrid");
    var PlanGrid1 = $("#PlanList1_" + wellId).data("kendoGrid");
    PlanGrid1.dataSource.data([]);
    PlanGridData = PlanGrid.dataSource._data;
    PlanGridData.forEach(function (value) {
        PlanGrid1.dataSource.add(value);
    });
    
    var gridElement = $('#PlanList1_' + wellId);

    printableContent = '',
        //var window = $("#AddTaskListWindow").data("kendoWindow");
         win = window.open('', 'DrillPlan', 'width=1500, height=900, resizable=1, scrollbars=1,top=' + top + ', left=' + left),
        doc = win.document.open();

    var WellDetails = DrillPlanDetails(wellId);
    //console.log(WellDetails);
    var wellData = JSON.parse(WellDetails);

    var RigRealese = wellData.RigRealese == null ? "" : formatDate(new Date(wellData.RigRealese));
    var SpudWell = wellData.SpudWell == null ? "" : formatDate(new Date(wellData.SpudWell));
    var NextBopTest = wellData.NextBopTest == null ? "" : formatDate(new Date(wellData.NextBopTest));
    var LastBopTest = wellData.LastBopTest == null ? "" : formatDate(new Date(wellData.LastBopTest));


    
    //grid.unlockColumn("TaskName");

    //alert(wellData);
    var htmlStart =
        '<!DOCTYPE html>' +
        '<html>' +
        '<head>' +
        '<meta charset="utf-8" />' +
        '<title>' + DrillPlanName + '</title>' +
        '<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">' +
        '<link href="https://kendo.cdn.telerik.com/' + kendo.version + '/styles/kendo.common.min.css" rel="stylesheet" /> ' +
        '<script src="~/js/OperatingCompany/PlanPrint.js"></script>' +        
        '<script src="https://code.jquery.com/jquery-1.12.3.min.js"></script>' +
        '<style>' +
        'html { font: 11pt sans-serif; }' +
        '.k-grid { border-top-width: 0; }' +
        '.k-filter-row, .k-command-cell {display: none;}' +
        '.k-grid, .k-grid-content { height: auto !important; }' +
        '.k-grid-content { overflow: visible !important; }' +
        'h5 {text-align:left !important;}' +
        '.k-filter-row th, .k-grid-header th.k-header {white-space: none !important;}' +
        //'.k-grid tbody td:nth-child(19)  { border-left-style: hidden !important;}' +
        //'.k-grid tbody td:nth-child(20)  { border-left-style: hidden !important;}' +  
        //'.k-grid-header th:nth-child(14) { border-left-style: hidden !important;display: none !important; }' +
        //'.k-grid-header th:nth-child(15) { border-left-style: hidden !important;}' +
        //'.k-grid-header th:nth-child(1) { border-right-style: hidden !important; }' +
        '.k-grid-header th.k-header .k-checkbox { display: none !important; }' +
        //'.k-grid tbody td:nth-child(1)  { border-right-style: hidden !important;}' +
        '.k-grid tbody .k-checkbox { display: none !important; }' +      
        'div.k-grid table { table-layout: auto; width: 100% !important; }' +
        '.k-grid .k-grid-header th { border-top: 1px solid; }' +
        '.k-grouping-header, .k-grid-toolbar, .k-grid-pager > .k-link { display: none; }' +
        '#blockstyle {margin-left: 10px !important;margin-right: 10px !important; border - radius: 5px;min - height: 60px!important;height: auto;width: 99%;min - width: 130px;display: inline - block;margin: 10px 0px 0 0px;background - color: #fff;border: 2px solid black;position: relative;justify - content: space - between;' +
        '.col-md-4 {max-width: 33.333333 %;}' +       
        '.k-checkbox {display: none;}' +
        '@page {size: A4;margin: 0;font: 12pt "Tahoma";}' +
        '@media print {html, body {width: 210mm height: 297mm; }' +
        '.k-grid {width: 99% !important;}' +
        'body { font-family: initial;font - size: 1.4rem;font - weight: bolder;line - height: 1.5;  }' +// optional: hide the whole pager
        '</style>' +
        '<script>' +
        '$(document).ready(function () {' +
        'var Stagebtn = document.getElementsByClassName("k-grid-content-locked");' +
        'for (var i = 0; Stagebtn.length > 0; i++) {' +
        'Stagebtn[i].className = Stagebtn[i].className.replace(" k-grid-content-locked", "k-grid-content");' +
         '}' +        
        '});' +
        '</script>' +
        '</head>' +
        '<body>' +
        '<div id="WellLogo" class="col-12 pull-left">' +
        '<img id="WellLogo" class="sidebar-toggle" style="height:56px !important;vertical-align: top;" src="/img/logohumb.png"/>' +
        '<img id="WellLogo" style="height:81px !important;" src="/img/logoname.png"/>' +
        '</div>' +
        '<div id="Div" class="col-sm-12 col-md-12" style="margin-top:10px;margin-left:10px;">' +
        '<div class=row>' +
        '<div class="col-sm-2 col-md-4">' +
        '<h5 >' + "Drill Plan Name : " + DrillPlanName + '</h5>' +
        '</div>' +
        '<div class="col-sm-2 col-md-4" >' +
        '<h5 >' + "Well Name : " + wellName + '</h5>' +
        '</div>' +
        '<div class="col-sm-2 col-md-4" >' +
        '<h5 >' + "Rig Name : " + Rigname + '</h5>' +
        '</div>' + 
        '</div>' +
        '</div>' +
        //'<div class="row float-right" style="margin-right:15px; margin-left:15px;margin-bottom: 10px;">' +
        '<div id="blockstyle" class="col-md-12 blockstyle">' +
        '<div class=row >' +
        '<div class="col-md-4 col-sm-4">' +
        '<h5>' + "Rig Release : " + RigRealese + '</h5>' +
        '</div>' +
        '<div class=col-md-4 col-sm-4>' +
        '<h5>' + "Spud Well : " + SpudWell + '</h5>' +
        '</div>' +
        '<div class=col-md-4 col-sm-4>' +
        '<h5>' + "Planned TD : " + wellData.PlannedTD + '</h5>' +
        '</div>' +
        '<div class="col-md-4 col-sm-4">' +
        '<h5>' + "Next Bop Test : " + NextBopTest  + '</h5>' +
        '</div>' +
        '<div class="col-md-4 col-sm-4">' +
        '<h5>' + "Last Bop Test : " + LastBopTest + '</h5>' +
        '</div>' +
        //'</div>' +
        '</div>' +
        '</div>' +
        '<div class="col-md-12" style="padding-left:0px !important">';

    var htmlEnd =
        '</div>' +
        '</body>' +
        '</html>';

    var gridHeader = gridElement.children('.k-grid-header');
    if (gridHeader[0]) {
        var thead = gridHeader.find('thead').clone().addClass('k-grid-header');


        printableContent = gridElement
            .clone()
            .children('.k-grid-header').remove()
            .end()
            .children('.k-grid-content')
            .end()
            .find('table')
            .first()
            .children('tbody').before(thead)
            .end()
            .end()
            .end()
            .end()[0].outerHTML;
    } else {
        printableContent = gridElement.clone()[0].outerHTML;
    }

    doc.write(htmlStart + printableContent + htmlEnd);
    doc.close();
    setTimeout(function () { win.print(); }, 1000);

    PlanGrid1.dataSource.data([]);
}


function RealTimeReport(WellId, WellName) {
    document.getElementById("TaskChart").style.display = "block";
    document.getElementById("WellName").textContent = WellName;

    var Grid = $('#PlanList_' + WellId).data('kendoGrid');
    var GridData = Grid.dataSource._data;
    var ChartData = [];
    var Data = [];
    GridData.forEach(function (value) {
       
        console.log(value);

        var data = {
            PlanStartDate: value.PlanStart,
            PlanFinishedDate: value.PlanFinishedDate,
            ActualPlanStart: value.ActualPlanStart,
            ActualFinished: value.ActualPlanFinishedDate,
            PlanFinishedOperationHours: value.OperationHours,
            ActualOperationFinishedHours: OperationFinishedHourscalculation(value.ActualPlanStart, value.ActualPlanFinishedDate),
            TaskName: value.TaskName,
            CategoryName: value.CategoryName,
            StageTypeName: value.StageTypeName
        };

        Data.push(data);
    });

    Data.forEach(function (value) {
        if (value.ActualOperationFinishedHours !== 0) {
            ChartData.push(value);
        }
    });

    RealTimeReportChart(ChartData);
}


function OperationFinishedHourscalculation(ActualPlanStart, ActualPlanFinishedDate) {

    if (ActualPlanStart !== null && ActualPlanFinishedDate !== null) {

        var DateDiff = ActualPlanFinishedDate.valueOf() - ActualPlanStart.valueOf();
        var ActualHours = DateDiff / 1000 / 60 / 60;

        return ActualHours;
    }

    return 0;
}
//Drill plan Real TimeReport Chart

function RealTimeReportChart(ChartData) {
    $("#TaskComparisonChart").kendoChart({
        title: {
            text: "Real Time Report - Task"
        },
        chartArea: {
            width: 800,
            height: 800
        },
        dataSource: ChartData,
        legend: {
            visible: false
        },     
        legend: {
            position: "top"
        },
        seriesDefaults: {
            type: "line",           
        },
        series:
            [{
                field: "PlanFinishedOperationHours",
                categoryField: "TaskName",
                name: "Planned Hours "
            }, {
                field: "ActualOperationFinishedHours",
                categoryField: "TaskName",
                name: "Actual Hours"
            }],
        categoryAxis: {
            labels: {
                rotation: -90
            },
            majorGridLines: {
                visible: false
            }
        },
        valueAxis: {
            //max: 300,
            labels: {
                format: "N0"
            },
            majorUnit: 10,
            line: {
                visible: false
            }
        },
        tooltip: {
            visible: true,
            format: "N0"
        }
    });

    var StageChartData = JSON.stringify(ChartData);
    $.ajax({
        url: "/ActiveDrillPlan/GetSatageData",
        type: "POST",
        data: StageChartData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#StageComparisonChart").kendoChart({
                title: {
                    text: "Real Time Report - Stage"
                },
                chartArea: {
                    width: 750,
                    height: 800
                },
                dataSource: response,
                legend: {
                    visible: false
                },
                legend: {
                    position: "top"
                },
                seriesDefaults: {
                    type: "line"
                    //width: 100,
                    //gap: 1
                },
                series:
                    [{
                        field: "PlanFinishedHours",
                        categoryField: "StageName",
                        name: "Planned Hours "
                    }, {
                        field: "ActualFinishedHours",
                        categoryField: "StageName",
                        name: "Actual Hours"
                    }],
                categoryAxis: {
                    labels: {
                        rotation: -90
                    },
                    majorGridLines: {
                        visible: false
                    }
                },
                valueAxis: {
                   min : 0,
                    labels: {
                        format: "N0"
                    },
                    majorUnit: 20,
                    line: {
                        visible: false
                    }
                },
                tooltip: {
                    visible: true,
                    format: "N0"
                }
            });

        }
    });
}

//$(document).ready(RealTimeReportChart);
//$(document).bind("kendo:skinChange", RealTimeReportChart);

function CloseChart() {
    document.getElementById("TaskChart").style.display = "none";
}


function ActualPlanStartChange(e,startDate) {
    var end = $("#ActualPlanFinishedDate").data("kendoDateTimePicker");
    var projectStartDate = $("#ActualPlanFinishedDate").data("kendoDateTimePicker");
    if (startDate === undefined) {
        startDate = this.value();
    }
    var endDate = end;
    if (startDate) {
        startDate = new Date(startDate);
        startDate.setDate(startDate.getDate());
        //end.value(new Date(startDate));
        end.min(startDate);
        //projectStartDate.value(new Date(startDate));
        projectStartDate.min(startDate);
    } else if (endDate) {
        start.max(new Date(endDate));
    } else {
        endDate = new Date();
        start.max(endDate);
        end.min(endDate);
    }
    $("#ActualPlanFinishedDate").data("kendoDateTimePicker").enable(true);
}


function onChengeckb() {
    if ($("#IsPrivate").is(':checked')) {
        $('#srvTenantDivId').show();
        $("#SRVTenantId").prop('required', 'true');
    } else {
        $('#srvTenantDivId').hide();
        $("#SRVTenantId").removeAttr('required');
    }
}

function GetServiceTenantNames() {
    return {
        ServiceCategoryId: $("#ServiceCategoryId").val()
    };
}

function startChange() {
    var end = $("#AuctionEnd").data("kendoDatePicker");
    var projectStartDate = $("#ProjectStartDate").data("kendoDatePicker");
    var startDate = this.value();
    var endDate = end;
    if (startDate) {
        startDate = new Date(startDate);
        startDate.setDate(startDate.getDate());
        end.value(new Date(startDate));
        end.min(startDate);
        projectStartDate.value(new Date(startDate));
        projectStartDate.min(startDate);
    } else if (endDate) {
        start.max(new Date(endDate));
    } else {
        endDate = new Date();
        start.max(endDate);
        end.min(endDate);
    }
}


function onBenchMarkChange(e) {
    var ChkValue = $('#BenchMark').is(':checked');
    if (ChkValue) {
        $("#PreSpud").attr("disabled", true);
    } else {
        $("#PreSpud").attr("disabled", false);
    }
}

function onPreSpudChange() {
    var ChkValue = $('#PreSpud').is(':checked');
    if (ChkValue) {
        $("#BenchMark").attr("disabled", true);
    } else {
        $("#BenchMark").attr("disabled", false);
    }
}

