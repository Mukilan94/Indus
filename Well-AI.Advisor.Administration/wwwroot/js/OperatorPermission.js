function AddPermission(value) {
    //alert(DrillingPlanId);
    var window = $("#AddPermissionWindow").data("kendoWindow");
    window.refresh({
        url: "/Customer/AddPermission?PermisionName=" + value //?wellId=" + wellId + "&DrillPlanId=" + DrillingPlanId
    });
    //$("#AddPermissionWindow").kendoWindow({
    //    content: {
    //        url: "/Customer/AddPermission",
    //    },
    //    visible:true
    //});
    window.center();
    window.open();
    window.wrapper.find('.k-window').css({
        'top': '210px !important',
        'width':'450px'
    });
}


