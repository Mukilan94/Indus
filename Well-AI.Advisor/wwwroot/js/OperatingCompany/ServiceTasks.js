
$(document).ready(function () {
    var value = $("#ServiceDurationDays").val();
    if (value == "") {
        $("#ServiceDurationDays").val("00");
    }
});

function DataBound(e) {
    var grid = this;
    grid.tbody.find("tr[role='row']").each(function () {
        var model = grid.dataItem(this);

    });
    grid.element.off('dblclick');
    grid.element.on('dblclick', 'tbody tr[data-uid]', function (e) {
        var model = grid.dataItem(this);
        console.log('active category ' + model.IsActiveCategory);
        grid.editRow($(e.target).closest('tr'));
    })
}

function OnServiceCategoryChange(e) {
    
    var ServiceCategoryId = $("#ServiceCategoryName").val();
    var ServiceCategoryId = $("#ServiceCategoryName").val();
    $("#ServiceCategoryId").data("kendoMaskedTextBox").value(ServiceCategoryId);
    $("#ServiceCategoryId").data("kendoMaskedTextBox").trigger("change");
   
    var scname = $("#ServiceCategoryName");
    $("#CategoryName").data("kendoMaskedTextBox").value(scname.data("kendoMultiColumnComboBox").text());
    $("#CategoryName").data("kendoMaskedTextBox").trigger("change");
    //textbox.value(value);
    //model.CategoryName = 'Service Category';
}

function OnServiceCategoryDataBound(e) {
    var ServiceCategoryId = $("#ServiceCategoryId").data("kendoMaskedTextBox").value();
    //alert('ServiceCategoryId on bound ' + ServiceCategoryId);
    $("#ServiceCategoryName").data("kendoMultiColumnComboBox").value(ServiceCategoryId);
    $("#ServiceCategoryName").data("kendoMultiColumnComboBox").trigger("change");

    
    //alert($("#ServiceCategoryName").data("kendoMultiColumnComboBox").text());
}

function OnEmployeeDataBound(e) {
    //var dataitem = this.dataItem();
    //var employeeId = $("#EmployeeId").data("kendoMaskedTextBox").value();
    //$("#EmployeeName").data("kendoDropDownList").value(dataitem.EmployeeName);
    //$("#EmployeeName").data("kendoDropDownList").trigger("change");
}

function OnEmployeeChange(e) {
    debugger;        
    var dataitem = this.dataItem();
    $("#EmployeeId").data("kendoDropDownList").value(dataitem.EmployeeId);
    //$("#EmployeeId").data("kendoDropDownList").trigger("change");
    $("#EmployeeNameField").data("kendoMaskedTextBox").value(dataitem.EmployeeName);
    $("#EmployeeNameField").data("kendoMaskedTextBox").trigger("change");

    $("#EmployeeIdField").data("kendoMaskedTextBox").value(dataitem.EmployeeId);
    $("#EmployeeIdField").data("kendoMaskedTextBox").trigger("change");
}
function OnVendorDataBound() {
    //var ServiceCategoryId = $("#Vendor").data("kendoMaskedTextBox").value();
    //$("#EmployeeName").data("kendoDropDownList").value(ServiceCategoryId);
    //$("#EmployeeName").data("kendoDropDownList").trigger("change");
}

function OnVendorChange(e) {
    var DataItem = this.dataItem();
    //Vendor
    $("#Vendor").data("kendoDropDownList").value(DataItem.Vendor);
    //$("#Vendor").data("kendoDropDownList").trigger("change");

    //kendoMaskedTextBox
    $("#VendorName").data("kendoMaskedTextBox").value(DataItem.VendorName);
    $("#VendorName").data("kendoMaskedTextBox").trigger("change");
}




function DrillplanGridEdit(e) {
    //Karthik
    var ScheduleTimeValue = $("#ScheduleTime").val();
    $("#ScheduleTimePicker").data("kendoTimePicker").value(ScheduleTimeValue);

    if (e.model.isNew()) {
        e.container.data("kendoWindow").title("Add Task");
        $('.k-grid-update').text("Save");
        //$("#IsSpecialServices").value(1);
        $("#IsActive").prop('checked', true).trigger("change");
    } else {
        //if (e.model.ServiceCategoryId === e.model.ParentId) {
        //    $('.k-dropdown').addClass("k-state-disabled").css("pointer-events", "none");
        //}
    }
    if (e.model.SeletedDependency !== null && e.model.SeletedDependency !== undefined) {
        let arr = e.model.SeletedDependency.split(',')
        if (arr != null && arr != undefined) {
            BindDependency(e.model.TaskId, arr);
        }
        else {
            BindDependency(e.model.TaskId, []);
        }
        //BindDependency(e.model.TaskId, arr);
    }
    else {
        let arr = e.model.SeletedDependency
        //BindDependency(e.model.TaskId, arr);
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
        filter: "startswith",
        minLength: 3,
        autoBind: true,
        dataSource: {
            type: "json",
            //serverFiltering: true,
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
function ServicedaysDataBound(e) {
    //var selectedFooType = $("#ServiceDurationDays").data("kendoMultiColumnComboBox").value();
    //if (selectedFooType == "") {
    //    this.select(0);
    //    this.trigger('change');
    //}
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
function multiChangeTasks(e) {
    var multiselect = $("#Dependency").data("kendoMultiSelect");
    var selectedData = "";
    var items = multiselect.value();
    for (var i = 0; i < items.length; i++) {
        selectedData += items[i] + ";";
    }
    $("#SeletedDependency").val(selectedData);
    $("#SeletedDependency").trigger("change");
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
        //document.getElementById("ScheduleTimePicker").disabled = "disabled";
        //document.getElementById("Duration").disabled = true;
        document.getElementById("depth").disabled = false;
        document.getElementById("LeadTime").disabled = false;
        document.getElementById("Dependancy").disabled = true;
    }
    else if (service == 2) {
        document.getElementById("name").disabled = false;
        document.getElementById("Day").disabled = false;
        //document.getElementById("ScheduleTimePicker").disabled = false;
        //document.getElementById("Duration").disabled = false;
        document.getElementById("depth").disabled = false;
        document.getElementById("LeadTime").disabled = false;
        document.getElementById("Dependancy").disabled = false;
    }
    else if (service == 3) {
        document.getElementById("name").disabled = false;
        document.getElementById("Day").disabled = false;
        //document.getElementById("ScheduleTimePicker").disabled = false;
        //document.getElementById("Duration").disabled = false;
        document.getElementById("depth").disabled = false;
        document.getElementById("LeadTime").disabled = false;
        document.getElementById("Dependancy").disabled = false;
    }
    else if (service == 4) {
        document.getElementById("name").disabled = false;
        document.getElementById("Day").disabled = false;
        //document.getElementById("ScheduleTimePicker").disabled = "disabled";
        //document.getElementById("Duration").disabled = true;
        document.getElementById("depth").disabled = false;
        document.getElementById("LeadTime").disabled = false;
        document.getElementById("Dependancy").disabled = true;
    }
}

function SchedulerTime(e) {
    var ScheduleTimeValue = $("#ScheduleTimePicker").val();
    $("#ScheduleTime").data("kendoMaskedTextBox").value(ScheduleTimeValue);
    $("#ScheduleTime").data("kendoMaskedTextBox").trigger("change");
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
    var PlanGrid = $("#DrillPlan_" + wellId).data("kendoGrid");
    var PlanGrid1 = $("#DrillPlan1_" + wellId).data("kendoGrid");
    PlanGrid1.dataSource.data([]);
    PlanGridData = PlanGrid.dataSource._data;
    PlanGridData.forEach(function (value) {
        PlanGrid1.dataSource.add(value);
    });


    var gridElement = $('#DrillPlan1_' + wellId);
    printableContent = '',
        //var window = $("#AddTaskListWindow").data("kendoWindow");
        win = window.open('', 'DrillPlan', 'width=1600, height=900, resizable=1, scrollbars=1,top=' + top + ', left=' + left),
        console.log(win);
    doc = win.document.open();

    var WellDetails = DrillPlanDetails(wellId);
    var wellData = JSON.parse(WellDetails);

    var RigRealese = wellData.RigRealese == null ? "" : formatDate(new Date(wellData.RigRealese));
    var SpudWell = wellData.SpudWell == null ? "" : formatDate(new Date(wellData.SpudWell));
    var NextBopTest = wellData.NextBopTest == null ? "" : formatDate(new Date(wellData.NextBopTest));
    var LastBopTest = wellData.LastBopTest == null ? "" : formatDate(new Date(wellData.LastBopTest));

    var htmlStart =
        '<!DOCTYPE html>' +
        '<html>' +
        '<head>' +
        '<meta charset="utf-8" />' +
        '<title>' + DrillPlanName + '</title>' +
        '<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">' +
        '<link href="https://kendo.cdn.telerik.com/' + kendo.version + '/styles/kendo.common.min.css" rel="stylesheet" /> ' +
        //'<script src="~/js/OperatingCompany/PlanPrint.js"></script>' +
        '<style>' +
        'html { font: 11pt sans-serif; }' +
        '.k-grid { border-top-width: 0; }' +
        '.k-filter-row, .k-command-cell {display: none;}' +
        '.k-grid, .k-grid-content { height: auto !important; }' +
        '.k-grid-content { overflow: visible !important; }' +
        'h5 {text-align:left !important;}' +
        '.k-grid-header th:nth-child(10)  { border-left-style: hidden !important; }' +
        'div.k-grid table { table-layout: auto; width: 100% !important; }' +
        '.k-grid .k-grid-header th { border-top: 1px solid; }' +
        '.k-grouping-header, .k-grid-toolbar, .k-grid-pager > .k-link { display: none; }' +
        '#blockstyle {margin-left: 10px !important;margin-right: 10px !important; border - radius: 5px;min - height: 60px!important;height: auto;width:99% !important ;min - width: 130px;display: inline - block;margin: 10px 0px 0 0px;background - color: #fff;border: 2px solid black;position: relative;justify - content: space - between;' +
        '.col-md-4 {max-width: 33.333333 %;}' +
        '#DrillPlan_' + wellId + '{margin:15px !important;width:99% !important}' +
        '@page {size: A4;margin: 0;font: 12pt "Tahoma";}' +
        '@media print {html, body {width: 210mm height: 297mm; }' +
        '.k-grid {width: 99% !important;}' +
        'body { font-family: initial;font - size: 1.4rem;font - weight: bolder;line - height: 1.5;  }' +
        '</style>' +
        '<script>' +
        '</script>' +
        '</head>' +
        '<body>' +
        '<div id="WellLogo" class="col-12 pull-left">' +
        '<img id="WellLogo" class="sidebar-toggle" style="height:56px !important;vertical-align: top;" src="/img/logohumb.png"/>' +
        '<img id="WellLogo" style="height:81px !important;" src="/img/logoname.png"/>' +
        '</div>' +
        '<div id="Div" class="col-12" style="margin-top:10px;margin-left:10px;">' +
        '<div class=row>' +
        '<div class=col-md-4>' +
        '<h5 >' + "Drill Plan Name : " + DrillPlanName + '</h5>' +
        '</div>' +
        '<div class=col-md-4 >' +
        '<h5 >' + "Well Name : " + wellName + '</h5>' +
        '</div>' +
        '<div class=col-md-4 >' +
        '<h5 >' + "Rig Name : " + Rigname + '</h5>' +
        '</div>' +
        '</div>' +
        '</div>' +
       // '<div class="row style="margin-right:15px; margin-left:15px;margin-bottom: 10px;">' +
        '<div id="blockstyle" class="col-md-12 blockstyle">' +
        '<div class=row >' +
        '<div class=col-md-4>' +
        '<h5>' + "Rig Release : " + RigRealese + '</h5>' +
        '</div>' +
        '<div class=col-md-4>' +
        '<h5>' + "Spud Well : " + SpudWell + '</h5>' +
        '</div>' +
        '<div class=col-md-4>' +
        '<h5>' + "PlannedTD : " + wellData.PlannedTD + '</h5>' +
        '</div>' +
        '<div class=col-md-4>' +
        '<h5>' + "Next Bop Test : " + NextBopTest + '</h5>' +
        '</div>' +
        '<div class=col-md-4>' +
        '<h5>' + "Last Bop Test : " + LastBopTest + '</h5>' +
        //'</div>' +
        '</div>' +
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