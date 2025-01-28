 //var isDefault;    
var DeletedTasks = [];

$(function () {
    var templateId = $("#TemplateId").val();
    if (templateId === "") {
        $("#btntemplateDelete").attr("disabled", "disabled");
    } else {
        $("#btntemplateDelete").removeAttr("disabled");
    }

});

function LoadTasksPopup() {

    var window = $("#AddTaskListWindow").data("kendoWindow");
    window.wrapper.find('.k-window').css({
        'top': '210px !important'
    });
    var templateId = $("#TemplateId").val();
    if (templateId === undefined) {
        templateId = "";
    }
    window.refresh({
        url: "/ChecklistTemplate/AddTasks?templateId=" + templateId
    });
   
    window.center();
    window.open();
}

   function ChangeDefaultTemplate(e) {
       return false;
       //isDefault = e.checked;
       var wellTypeId = $("#WellTypeId").data("kendoDropDownList").value();
       var Defaultswitch = $("#IsDefault").kendoSwitch().data("kendoSwitch").value();

            if ($("#TemplateId").val()) {
            //alert("Template id " + $("#TemplateId").val());
            $.ajax({
                url: "/ChecklistTemplate/ChangeChecklistDefault?TemplateId=" + $("#TemplateId").val() + "&wellTypeId=" + wellTypeId + "&IsDefault=" + Defaultswitch,
                type: "POST",
                async: false,
                dataType: "Json",
                success: function (Response) {
                }
            });
            }
   }

        function OnWellTypeChange(e) {
            var dataItem = e.sender.dataItem();
            console.log('OnWellTypeChange ' + dataItem);

            console.log('OnWellTypeChange 1 ' + dataItem[0]);

            var GridObject = $("#CheckList").data("kendoGrid");
            var Url = "/ChecklistTemplate/ReadChecklistTemplateForDesign?welltype=" + dataItem.wellTypeId;
            GridObject.dataSource.transport.options.read.url = Url;
            GridObject.dataSource.read();
            GridObject.dataSource.filter({ });
        }

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

        function ChangeStus() {
            var destinationgrid = $('#CheckList').data('kendoGrid');
            destinationgrid.read();
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



        function TaskList() {
            var Tasks = $('#CheckList').data('kendoGrid');
            var items = Tasks.dataSource.data();
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
                    TaskOrder: TaskIndex,
                    LeadTime: items[i].LeadTime,
                    IsBiddable: items[i].IsBiddable,
                    Day: items[i].Day,
                    ScheduleTime: items[i].ScheduleTime,
                    IsActive: items[i].IsActive,
                    ExportToMaster: items[i].ExportToMaster,
                    IsPreSpud:  items[i].IsPreSpud,
                    IsBenchMark:  items[i].IsBenchMark
                };
                TaskData.push(Taskvalue);
            }

            return TaskData;
        }

function SaveTasks() {
            var TemplateId = $("#TemplateId").val();
            var TemplateName = $("#TemplateName").val();
            var WellType = $("#WellTypeId").data("kendoDropDownList");

            if (WellType.value() !== "" && TemplateName !== "") {

                //var Default = $("#IsDefault").data("kendoSwitch").toggle();
                var Defaultswitch = $("#IsDefault").kendoSwitch().data("kendoSwitch");
                const BopFrequency = $("#BopFrequency").data("kendoDropDownList").value();

                //alert('BopFrequency' + BopFrequency);
                var bopFrequencyDays = 0;
                if (BopFrequency != null || BopFrequency != undefined) {
                    bopFrequencyDays = parseInt(BopFrequency);
                }
                //alert('bopFrequencyDays'+bopFrequencyDays);

                var ChecklistTemplate = {
                    ChecklistTemplateId: TemplateId,
                    ChecklistTemplateName: TemplateName,
                    WellTypeId: WellType.value(),
                    IsDefault: Defaultswitch.value(),
                    DeletedTasks: DeletedTasks,
                    Checklist: TaskList(),
                    BopFrequency: parseInt(bopFrequencyDays)
                };

                var Data = JSON.stringify(ChecklistTemplate);

                console.log(Data);

                //kendo.alert('data' + Data);

                $.ajax({
                    url: "/ChecklistTemplate/SaveAndUpdateChecklistTemplate",
                    type: 'POST',
                    data: Data,
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response.TemplateId) {
                            $('#CheckList').data('kendoGrid').dataSource.read().then(function () {
                                $('#CheckList').data('kendoGrid').refresh();
                            });
                            //$("#TemplateId").val(response.TemplateId);
                            window.location.href = "/ChecklistTemplate/Details?CheckListId=" + response.TemplateId;
                            $(".IsDefault").kendoSwitch();                            
                            //$("#IsDefault").kendoSwitch({
                            //    checked: true
                            //});

                        }
                    }
                });
            } else {
                kendo.alert("Template name and well type is required");
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

    var ScheduleTimeValue = $("#ScheduleTime").val();
    $("#ScheduleTimePicker").data("kendoTimePicker").value(ScheduleTimeValue);

    if (e.model.isNew()) {
        e.container.data("kendoWindow").title("Add Task");
        $('.k-grid-update').text("Save");
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

    if (service == 1 || service == 0) {
        document.getElementById("name").disabled = false;
        document.getElementById("Day").disabled = false;
        document.getElementById("ScheduleTimePicker").disabled = "disabled";
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
        document.getElementById("ScheduleTimePicker").disabled = "disabled";
        document.getElementById("Duration").disabled = true;
        document.getElementById("depth").disabled = false;
        document.getElementById("LeadTime").disabled = false;
        document.getElementById("Dependancy").disabled = true;
    }
}
function SchedulerTime(e) {
    const ScheduleTimeValue = $("#ScheduleTimePicker").val();
    $("#ScheduleTime").data("kendoMaskedTextBox").value(ScheduleTimeValue);
    $("#ScheduleTime").data("kendoMaskedTextBox").trigger("change");
}


function customDelete(e) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    DeletedTasks.push(dataItem.Name);
    kendo.confirm("Are you sure you want to delete this item?")
        .done(function () {            
            var dataSource = $("#CheckList").data("kendoGrid").dataSource;
            dataSource.remove(dataItem);
            dataSource.sync(); 
        })
        .fail(function () {
            return false;
        });
}


function DisplayError(args) {
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


function templateDelete() {

   var templateId = $("#TemplateId").val();

    kendo.confirm("Are you sure you want to delete this item?")
        .done(function () {
            $.ajax({
                url: "/ChecklistTemplate/ChecklistTemplateDelete?templateId=" + templateId,
                type: "POST",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: false,
                success: function (Response) {
                    
                    if (Response.status === "failed") {
                        var wnd = $("#TemplateWindow").data("kendoWindow");
                        wnd.title("Information");
                        $('#WinContant').html("<span>Cannot Delete !  Template  " + "<b>" + Response.templateName + "</b>" + " is being used in Wells.</span>");
                        wnd.center();
                        wnd.open();
                    }
                    else {
                        window.location.href = "/ChecklistTemplate/Details";
                    }
                }
            });
        })
        .fail(function () {
            return false;
        });
}

function BulkLoadTasks(TemplateId) {

    var WellTypeId = $("#WellTypeId").data("kendoDropDownList").value();
    console.log('WellTypeId' + WellTypeId);
    //if ($("#WellTypeId").data("kendoDropDownList").text() == "Select Well Type" && $("#WellTypeId").data("kendoDropDownList").val() == "") {
    //    return false;
    //}

    if (WellTypeId == "" || WellTypeId == undefined) {
        var GridObject = $("#CheckList").data("kendoGrid");
        var Url = "/ChecklistTemplate/ReadServiceTasks";
        GridObject.dataSource.transport.options.read.url = Url;
        GridObject.dataSource.read();
        GridObject.dataSource.filter({});
    }
    else {
        $.ajax({
            url: '/ChecklistTemplate/WellTypeChecklistExists?WellDesignId=' + WellTypeId + "&TemplateId=" + TemplateId,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.status == "Exists") {
                    kendo.confirm("Are you sure you want to download Tasks and overwrite current list ?")
                        .done(function () {
                            var GridObject = $("#CheckList").data("kendoGrid");
                            var Url = "/ChecklistTemplate/ReadServiceTasks";
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
                    var Url = "/ChecklistTemplate/ReadServiceTasks";
                    GridObject.dataSource.transport.options.read.url = Url;
                    GridObject.dataSource.read();
                    GridObject.dataSource.filter({});
                }
            }
        });
    }
}


function OnSaveChanges(e) {
    //$("#ScheduleTime").data("kendoMaskedTextBox").value(ScheduleTimeValue);
    //$("#ScheduleTime").data("kendoMaskedTextBox").trigger("change");

    $("#ScheduleTimePicker").data("kendoTimePicker").value(null)
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