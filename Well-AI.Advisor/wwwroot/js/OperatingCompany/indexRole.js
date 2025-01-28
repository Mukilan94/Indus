$(function () {
    $("#roleList").find(".k-grid-toolbar").on("click", ".k-grid-addNewRecord", function (e) {
        e.preventDefault();
        $("#Creates").kendoWindow({
            content: {
                url: "/manageRoles/loadCreateRoleWindow",
                data: {}
            }
        });
        var wnd = $("#Creates").data("kendoWindow");
        wnd.open();
        $("#Creates").kendoWindow({
            position: {
                top: 100
            }
        }).center();
    });
});

function validateAddRole() {
    var flag = true;
    var errorMessage = "";
    $("#errorMessage").html("");
    if ($("#Name").val().trim() === "") {
        flag = false;
        errorMessage += "Role name is required field.<br>";
    }
    if (flag === false) {
        $("#errorMessage").html(errorMessage);
        return false;
    }
}
