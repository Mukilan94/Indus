var WellDesignValue = "";

$(document).ready(function () {
    setTimeout(function () {
        if ($("#WellTypeName").data("kendoDropDownList").value() == "") {
            $("#EditWellDesign").attr('disabled', 'disabled');
        } else {
            $("#EditWellDesign").removeAttr('disabled');
        }
        //$("#btnImportTasks").attr("disabled", "disabled");
        $("#BtnWellDesignDelete").attr("disabled", "disabled");
        var window = $("#AddTaskListWindow").data("kendoWindow");
        window.wrapper.find('.k-window').css({
            'top': '210px !important'
        });
    }, 1000);

    //$(".k-grid-filter").click(function (e) {
    //    alert('filter call');
    //    var fmc = $(e.target).closest("th").data("kendoFilterMultiCheck");
    //    fmc.checkSource.read();
    //    fmc.container.empty();
    //    fmc.refresh();
    //});

});

$(document).ready(function () {
    SelectedSpecialService(0);

    $("#ServiceDurationDays").val("00");
});

function ImportTasksFromServiceMaster() {

    var WellTypeId = $("#WellTypeName").data("kendoDropDownList").value();
    console.log('WellTypeId' + WellTypeId);
    if ($("#WellTypeName").data("kendoDropDownList").text() == "Select Well Type" && $("#WellDesignTypeName").val() == "") {
        return false;
    }

    if (WellTypeId == "" || WellTypeId == undefined) {
        var GridObject = $("#CheckList").data("kendoGrid");
        var Url = "/ChecklistTemplate/ReadCheckListTemplate?welltype=";
        GridObject.dataSource.transport.options.read.url = Url;
        GridObject.dataSource.read();
        GridObject.dataSource.filter({});
    }
    else {
        $.ajax({
            url: '/ChecklistTemplate/WellTypeChecklistExists?WellDesignId=' + WellTypeId,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.status == "Exists") {
                    kendo.confirm("Are you sure you want to download Tasks and overwrite current list ?")
                        .done(function () {
                            var GridObject = $("#CheckList").data("kendoGrid");
                            var Url = "/ChecklistTemplate/ReadCheckListTemplate?welltype=";
                            GridObject.dataSource.transport.options.read.url = Url;
                            GridObject.dataSource.read();
                            GridObject.dataSource.filter({});
                        })
                        .fail(function () {
                            return false;
                        });
                }
                else {
                    GridObject = $("#CheckList").data("kendoGrid");
                    var Url = "/ChecklistTemplate/ReadCheckListTemplate?welltype=";
                    GridObject.dataSource.transport.options.read.url = Url;
                    GridObject.dataSource.read();
                    GridObject.dataSource.filter({});
                }
            }
        });
    }

}
function checklistalert(content, alerttitle) {
    $("<div></div>").kendoAlert({
        title: alerttitle,
        content: content
    }).data("kendoAlert").open();
}

function LoadTasksPopup() {
    var window = $("#AddTaskListWindow").data("kendoWindow");
    var WellTypeId = $("#WellTypeName").data("kendoDropDownList").value();
    window.refresh({
        url: "/ChecklistTemplate/AddTasks?WellType=" + WellTypeId
    });
    window.wrapper.find('.k-window').css({
        'top': '210px !important'
    });
    window.center();
    window.open();
}

function stageFilter(element) {
    var url = "FilterMenuCustomization_Stages";

    element.kendoDropDownList({
        dataSource: {
            transport: {
                read: url
            }
        },
        optionLabel: "--Select Stage--"
    });
}

function OnWellTypeChange(e) {

    if ($("#WellTypeName").data("kendoDropDownList").value() == "") {
        $("#EditWellDesign").attr('disabled', 'disabled');
    } else {
        $("#EditWellDesign").removeAttr('disabled');
        $("#BtnWellDesignDelete").removeAttr("disabled");

    }

    var dataItem = e.sender.dataItem();
    var GridObject = $("#CheckList").data("kendoGrid");

    //alert(dataItem.WellTypeId);

    if (dataItem.WellTypeId === "") {
        GridObject.dataSource.data([]);
    }
    else {
        //GridObject.dataSource.filter({});
        GridObject.dataSource.data([]);
        //GridObject.refresh();
        //return false;
        e.preventDefault();
        var Url = "/ChecklistTemplate/ReadCheckListTemplate?welltype=" + dataItem.WellTypeId;
        GridObject.dataSource.transport.options.read.url = Url;
        GridObject.dataSource.read();

        // dataSource.filter({ field: "OrderID", operator: "gt", value: 5 });

    }
}


$(".k-icon k-i-filter").click(function (e) {
    //alert('grid filter call');
});



var SelectedTasks = [];

function OnChange(e) {
    //SelectedTasks = e.sender.selectedKeyNames();

    var rows = e.sender.select();
    rows.each(function (e) {
        var grid = $("#TasksGrid").data("kendoGrid");
        var dataItem = grid.dataItem(this);
        var DataValue = {
            Name: dataItem.Name,
            CategoryName: dataItem.CategoryName,
            ServiceCategoryId: dataItem.ServiceCategoryId,
            Depth: dataItem.Depth,
            Description: dataItem.Description,
            IsSpecialServices: dataItem.IsSpecialServices,
            SeletedDependency: dataItem.SeletedDependency,
            ServiceDuration: dataItem.ServiceDuration,
            StageTypeName: dataItem.StageTypeName,
            StageType: dataItem.StageType,
            LeadTime: dataItem.LeadTime,
            IsBiddable: dataItem.IsBiddable,
            Day: dataItem.Day,
            ScheduleTime: dataItem.ScheduleTime,
        }

        SelectedTasks.push(DataValue);
    })
}

function Close() {
    var window = $("#AddTaskListWindow").data("kendoWindow");
    window.Close();
}


function moveUp(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    moveRow(this, dataItem, -1);
}

function moveDown(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
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
    var newIndex = index = grid.dataSource.indexOf(record);
    direction < 0 ? newIndex-- : newIndex++;
    swap(grid.dataSource._data[newIndex], grid.dataSource._data[index], 'position');
    grid.dataSource.remove(record);
    grid.dataSource.insert(newIndex, record);
}



function SaveTasks() {
    if (document.getElementById("WellDesignTypeName").style.display == "block") {
        if ($("#WellDesignTypeName").data("kendoMaskedTextBox").value() == "") {
            var window = $("#WellTypeWindow").data("kendoWindow");
            window.center();
            window.open();
            return false;
        }
    } else if ($("#WellTypeName").data("kendoDropDownList").value() == "") {
        var window = $("#WellTypeWindow").data("kendoWindow");
        window.center();
        window.open();
        return false;
    }

    if ($("#WellTypeName").data("kendoDropDownList").value() == "") {
        $.ajax({
            url: '/ChecklistTemplate/WellTypeNameExists?TypeName=' + $("#WellDesignTypeName").val(),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.status == "Exists") {
                    checklistalert("Well Design Name already exists !", "Information");
                    return false;
                }
                else {
                    saveTasksCall();
                }
            }
        });
    }
    else {
        saveTasksCall();
    }
}
function saveTasksCall() {
    $.ajax({
        url: "/ChecklistTemplate/SaveTasks",
        type: 'POST',
        data: TasksList(),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response.status == 1) {
                $("#WellDesignTypeName").attr("style", "display:none");
                $(".k-maskedtextbox").attr("style", "display:none");
                $("#WellDesignTypeName").data("kendoMaskedTextBox").value("");
                $(".k-dropdown").attr("style", "display:block");
                $("#WellTypeName").data("kendoDropDownList").dataSource.read();
                setTimeout(function () {
                    $("#WellTypeName").data("kendoDropDownList").value(response.id);
                    $("#WellTypeName").data("kendoDropDownList").trigger("change");
                }, 3000)
            }
        }
    });
}


function TasksList() {
    var WellTypeName = "";
    var Tasks = $('#CheckList').data('kendoGrid');
    var items = Tasks.dataSource.data();
    var WellType = $("#WellTypeName").data("kendoDropDownList");

    if ($("#WellDesignTypeName").data("kendoMaskedTextBox").value() == "") {
        WellTypeName = $("#WellTypeName").data("kendoDropDownList").text();
    } else {
        WellTypeName = $("#WellDesignTypeName").data("kendoMaskedTextBox").value();
    }

    var TaskData = [];
    var TaskIndex = 1;
    for (var i = 0; i < items.length; i++) {
        TaskIndex += i;
        var Taskvalue = {
            TaskId: items[i].TaskId,
            CategoryName: items[i].CategoryName,
            ServiceCategoryId: items[i].ServiceCategoryId,
            Depth: items[i].Depth,
            Description: items[i].Description,
            IsSpecialServices: items[i].IsSpecialServices,
            Name: items[i].Name,
            SeletedDependency: items[i].SeletedDependency,
            ServiceDuration: items[i].ServiceDuration,
            StageTypeName: items[i].StageTypeName,
            StageType: items[i].StageType,
            WellDesignId: WellType.value(),
            WellDesignName: WellTypeName,
            TaskOrder: TaskIndex,
            LeadTime: items[i].LeadTime,
            IsBiddable: items[i].IsBiddable,
            Day: items[i].Day,
            ScheduleTime: items[i].ScheduleTime,
            IsActive: items[i].IsActive,
            ExportToMaster: items[i].ExportToMaster,
            IsPreSpud: items[i].IsPreSpud,
            IsBenchMark: items[i].IsBenchMark
        };

        TaskData.push(Taskvalue);
    }

    var Data = JSON.stringify(TaskData);

    return Data;
}


function AddWellType() {

    //$("#btnImportTasks").removeAttr("disabled");
    $("#WellDesignTypeName").attr("style", "display:block");
    $(".k-maskedtextbox").attr("style", "");
    $(".k-dropdown").attr("style", "display:none");
    $("#EditWellDesign").attr('disabled', 'disabled');
    $("#BtnWellDesignDelete").attr("disabled", "disabled");
    $('#CheckList').data('kendoGrid').dataSource.data([]);
    $("#WellDesignTypeName").data("kendoMaskedTextBox").value(null)
    $("#WellTypeName").data("kendoDropDownList").value(null);
}

function EditWellType() {

    // $("#btnImportTasks").removeAttr("disabled");
    $("#WellDesignTypeName").attr("style", "display:block");
    $(".k-maskedtextbox").attr("style", "");
    $(".k-dropdown").attr("style", "display:none");
    $("#BtnWellDesignDelete").attr("disabled", "disabled");
    //$('#CheckList').data('kendoGrid').dataSource.data([]);     
    $("#WellDesignTypeName").data("kendoMaskedTextBox").value($("#WellTypeName").data("kendoDropDownList").text());
    $("#WellDesignTypeName").data("kendoMaskedTextBox").trigger("change");
}


function CancelWellDesign() {
    $("#WellDesignTypeName").attr("style", "display:none");
    $("#WellDesignTypeName").data("kendoMaskedTextBox").value("");
    $(".k-maskedtextbox").attr("style", "display:none");
    $(".k-dropdown").attr("style", "display:block");
    $("#WellTypeName").data("kendoDropDownList").dataSource.read();
    $("#WellTypeName").data("kendoDropDownList").select(0);
    $('#CheckList').data('kendoGrid').dataSource.data([]);
    if ($("#WellTypeName").data("kendoDropDownList").value() == "") {
        $("#EditWellDesign").attr('disabled', 'disabled');
        $("#BtnWellDesignDelete").attr("disabled", "disabled");

    } else {
        $("#EditWellDesign").removeAttr('disabled');
        $("#BtnWellDesignDelete").removeAttr("disabled");

    }
    // $("#btnImportTasks").attr("disabled", "disabled");
    //$("#BtnWellDesignDelete").attr("disabled", "disabled");

}

function customDelete(e) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

    console.log('dataItem.TaskId' + dataItem.Name);

    //var WellTypeId = $("#WellTypeName").data("kendoDropDownList").value();
    //var dataSource = $("#CheckList").data("kendoGrid").dataSource;
    //dataSource.remove(dataItem);
    //dataSource.sync();
    kendo.confirm("Are you sure you want to delete this item?")
        .done(function () {
            var dataSource = $("#CheckList").data("kendoGrid").dataSource;
            dataSource.remove(dataItem);
            dataSource.sync();
        })
    //    .fail(function () {
    //        return false;
    //    });
}

function WellDesignDelete(e) {
    WellDesignValue = $("#WellTypeName").data("kendoDropDownList").value();
    kendo.confirm("Are you sure you want to delete this WellDesign ?")
        .done(function () {
            $.ajax({
                url: "/ChecklistTemplate/DeleteWellDesign?WellDesignId=" + WellDesignValue,
                type: "POST",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                async: false,
                success: function (data) {
                    console.log(data);
                    if (data.Status == "Success") {
                        $("#WellTypeName").data("kendoDropDownList").dataSource.read();
                        $('#CheckList').data('kendoGrid').dataSource.data([]);

                    }
                    else if (data.Status == "Failed") {
                        var window = $("#WellTypeWindow").data("kendoWindow");
                        window.title("Information");
                        $('#WinContant').html("<span>Cannot Delete ! " + "<b>" + data.WellDesignName + "</b>" + " is being used in Templates / Wells.</span>");
                        window.center();
                        window.open();
                    }
                }
            });
        }).fail(function () {
            return false;
        });
}

function OnSelect(e) {
    var FileLenth = e.files.length;
    if (FileLenth > 1) {
        alertbox("Please Import one file at a time", "File Empty");
        return false;
    }
}
//DWOP
function FileUploadSuccess(e) {
    $('#TasksGrid').data('kendoGrid').dataSource.read().then(function () {
        $('#TasksGrid').data('kendoGrid').refresh();
    });

    console.log('u-pload success' + e);
    services = e.response.Value;
    if (services) {

        $('#TasksDuplicateContant').html("Some tasks is duplicate. ")
        // $('#WindowContent').css('color', 'Red')
        var wnd = $("#TasksDuplicate").data("kendoWindow");
        wnd.center().open();
        //alertbox("Some tasks is duplicate " + "<a href='javascript:void(0)'  onclick='showTasks()'>Show duplicated tasks</a>", "Service Tasks");
        return false;
    }
}


function DataBound(e) {
    console.log('Checklist Template Databound event');
    var grid = this;

    grid.tbody.find("tr[role='row']").each(function () {
        var model = grid.dataItem(this);

    });
    grid.element.off('dblclick');
    grid.element.on('dblclick', 'tbody tr[data-uid]', function (e) {
        //alert('dbl click event')
        var model = grid.dataItem(this);
        console.log('active category ' + model.IsActiveCategory);
        //if (model.IsActiveCategory === true)
        grid.editRow($(e.target).closest('tr'));
    })
}


function gridEdit(e) {

    //document.getElementById("ExportMaster").style.display = "none";

    var ScheduleTimeValue = $("#ScheduleTime").val();
    $("#ScheduleTimePicker").data("kendoTimePicker").value(ScheduleTimeValue);

    if (e.model.isNew()) {
        e.container.data("kendoWindow").title("Add Task");
        $('.k-grid-update').text("Save");
        $("#IsActive").prop('checked', true).trigger("change");
        document.getElementById("ExportMaster").style.display = "block";
    } else {
        if (e.model.ServiceCategoryId === e.model.ParentId) {
            $('.k-dropdown').addClass("k-state-disabled").css("pointer-events", "none");
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
                    url: "/welltasks/GetTaskDependencyList?id=" + id,
                }
            }
        },
        value: selectedDependency,
        change: multiChangeTasks
    });
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
    //return
    if (service == 1 || service == 0) {
        //document.getElementById("name").disabled = false;
        document.getElementById("Day").disabled = false;
        document.getElementById("ScheduleTimePicker").disabled = "diabled";
        //document.getElementById("Duration").disabled = true;
        document.getElementById("depth").disabled = false;
        document.getElementById("LeadTime").disabled = false;
        // document.getElementById("Dependancy").disabled = true;
    }
    else if (service == 2) {
        document.getElementById("name").disabled = false;
        document.getElementById("Day").disabled = false;
        document.getElementById("ScheduleTimePicker").disabled = false;
        //document.getElementById("Duration").disabled = false;
        document.getElementById("depth").disabled = false;
        document.getElementById("LeadTime").disabled = false;
        //document.getElementById("Dependancy").disabled = false;
    }
    else if (service == 3) {
        document.getElementById("name").disabled = false;
        document.getElementById("Day").disabled = false;
        document.getElementById("ScheduleTimePicker").disabled = false;
        //document.getElementById("Duration").disabled = false;
        document.getElementById("depth").disabled = false;
        document.getElementById("LeadTime").disabled = false;
        document.getElementById("Dependancy").disabled = false;
    }
    else if (service == 4) {
        document.getElementById("name").disabled = false;
        document.getElementById("Day").disabled = false;
        document.getElementById("ScheduleTimePicker").disabled = "diabled";
        //document.getElementById("Duration").disabled = true;
        document.getElementById("depth").disabled = false;
        document.getElementById("LeadTime").disabled = false;
        //document.getElementById("Dependancy").disabled = true;
    }
}
function SchedulerTime(e) {
    var ScheduleTimeValue = $("#ScheduleTimePicker").val();
    $("#ScheduleTime").data("kendoMaskedTextBox").value(ScheduleTimeValue);
    $("#ScheduleTime").data("kendoMaskedTextBox").trigger("change");
}

function error(args) {
    if (args.errors) {
        var grid = $("#CheckList").data("kendoGrid");
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

