   $(document).ready(function () {

        //$(".defaulttemplate").kendoSwitch();
});

        function OnDataBound(e) {
            var grid = this;

            grid.tbody.find("tr[role='row']").each(function () {
                $(this).find("[id^='Default_']").kendoSwitch({
                    change: function (e) {
                        var dataItem = grid.dataItem(this.element.closest("tr"));
                        $.ajax({
                            url: "/ChecklistTemplate/ChangeChecklistDefault?TemplateId=" + dataItem.TemplateId + "&wellTypeId=" + dataItem.WellTypeId + "&IsDefault=" + e.checked,
                            type: "POST",
                            async: false,
                            dataType: "Json",
                            success: function (Response) {
                                $('#templates').data('kendoGrid').dataSource.read().then(function () {
                                    $('#templates').data('kendoGrid').refresh();
                                });
                            }
                        });
                    }
                });
            });
        }

        //kendo.syncReady(
        //    function () {
        //        $("[id^='Default_']").kendoSwitch({
        //            change: function (e) {

        //            }
        //        });
        //    }
        //);

        function AddNewTemplate() {
             window.location.href = "/ChecklistTemplate/Details";
        }

                    
function ChecklistTemplateDelete(e) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    kendo.confirm("Are you sure you want to delete this item?")
        .done(function () {
            $.ajax({
                url: "/ChecklistTemplate/ChecklistTemplateDelete?templateId=" + dataItem.TemplateId,
                type: "POST",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: false,
                success: function (Response) {
                    if (Response.result === 1) {
                        $('#templates').data('kendoGrid').dataSource.read().then(function () {
                            $('#templates').data('kendoGrid').refresh();
                        });
                    }
                    else if (Response.status === "failed") {
                        var window = $("#TemplateWindow").data("kendoWindow");
                        window.title("Information");
                        $('#WinContant').html("<span>Cannot Delete !  Template  " + "<b>" + Response.templateName + "</b>" + " is being used in Wells.</span>");
                        window.center();
                        window.open();
                    }
                },
                error: function (e) {
                }
            });
        })
        .fail(function () {
            return false;
        });
}