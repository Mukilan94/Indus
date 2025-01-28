
var detailsTemplate = kendo.template($("#template").html());
function showDetails(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var wnd = $("#Details").data("kendoWindow");
    wnd.content(detailsTemplate(dataItem));
    wnd.open();
}

function showEditModel(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    $("#Edits").kendoWindow({
        content: {
            url: "/addUpdateUsers/loadEditUserWindow",
            data: { id: dataItem.UserId}
        }
    });
    var wnd = $("#Edits").data("kendoWindow");
    wnd.open();
    $("#Edits").kendoWindow({
        position: {
            top: 100
        }
    }).center();
}

$(function () {
    $("#users").find(".k-grid-toolbar").on("click", ".k-grid-addNewRecord", function (e) {
        var wnd = $("#Creates").data("kendoWindow");
        wnd.center().open();
        $("#Creates").kendoWindow({
            content: {
                url: "/addUpdateUsers/loadCreateUserWindow",
                data: {}
            }
        });
    });
});

function validateAddUser() {
    
    var flag = true;
    var errorMessage = "";
    $("#errorMessage").html("");
    if ($("#Password").val() !== $("#ConfirmPassword").val()) {
        flag = false;
        errorMessage += "Confirm password is not matching.<br>";
    }
    if ($("#ConfirmPassword").val().trim() === "") {
        flag = false;
        errorMessage += "Confirm password is required field.<br>";
    }
    if ($("#Password").val().trim() === "") {
        flag = false;
        errorMessage += "Password is required field.<br>";
    }
    if ($("#Email").val().trim() === "") {
        flag = false;
        errorMessage += "Email is required field.<br>";
    }
    if ($('#Name').val() === null) {
        flag = false;
        errorMessage += "Role is required field.<br>";
    }
    if (flag === false) {
        $("#errorMessage").html(errorMessage);
        return false;
    }
}


function validateEditUser() {
    var flag = true;
    var errorMessage = "";
    $("#errorMessage").html("");
    if ($("#Email").val().trim() === "") {
        flag = false;
        errorMessage += "Email is required field.<br>";
    }
    if (flag === false) {
        $("#errorMessage").html(errorMessage);
        return false;
    }
}