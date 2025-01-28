
var DispatchVal = 0, ProviderVal = 0, OperatorVal = 0;


$(document).ready(function () {
    $("#txtDispatchCount").on('change paste keyup', function () {

        document.querySelector('#updatepack').disabled = false;
        // debugger;
        ProviderVal = parseFloat($("#txtProviderCount").val() * parseFloat($("#lblProviderUnitPrice").text())).toFixed(2);
        OperatorVal = parseFloat($("#txtOperatorCount").val() * parseFloat($("#lblOperatorUnitPrice").text())).toFixed(2);
        DispatchVal = parseFloat($(this).val() * parseFloat($("#lblDispatchUnitPrice").text())).toFixed(2);

        if ($("#chkDispatch").prop("checked") === true && $("#chkProvider").prop("checked") === true) {
            $("#lblDispatchTotal").text('$' + DispatchVal);
            $("#lblProviderTotal").text('$' + ProviderVal);
            var Total = +parseFloat(DispatchVal) + +parseFloat(ProviderVal);
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }

        else if ($("#chkDispatch").prop("checked") === true && $("#chkProvider").prop("checked") === false && $("#chkOperator").prop("checked") === false) {
            $("#lblDispatchTotal").text('$' + DispatchVal);
            var Total = $("#lblDispatchTotal").text();
            $("#lblTotal").text($("#lblDispatchTotal").text());
            $("#lblTotal1").text($("#lblTotal").text());
        }
        else if ($("#chkDispatch").prop("checked") === true && $("#chkOperator").prop("checked") === true) {
            $("#lblDispatchTotal").text('$' + DispatchVal);
            $("#lblOperatorTotal").text('$' + OperatorVal);
            var Total = +parseFloat(DispatchVal) + +parseFloat(OperatorVal);
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }

    });
    $("#txtProviderCount").on('change paste keyup', function () {
        document.querySelector('#updatepack').disabled = false;
        // debugger;
        ProviderVal = parseFloat($(this).val() * parseFloat($("#lblProviderUnitPrice").text())).toFixed(2);
        DispatchVal = parseFloat($("#txtDispatchCount").val() * parseFloat($("#lblDispatchUnitPrice").text())).toFixed(2);

        if ($("#chkDispatch").prop("checked") === true && $("#chkProvider").prop("checked") === true) {
            $("#lblProviderTotal").text('$' + ProviderVal);
            var Total = +parseFloat(DispatchVal) + +parseFloat(ProviderVal);
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }
        else if ($("#chkDispatch").prop("checked") === false && $("#chkProvider").prop("checked") === true) {
            $("#lblProviderTotal").text('$' + ProviderVal);
            var Total = $("#lblProviderTotal").text();
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }
    });
    $("#txtOperatorCount").on('change paste keyup', function () {


        document.querySelector('#updatepack').disabled = false;

        DispatchVal = parseFloat($("#txtDispatchCount").val() * parseFloat($("#lblDispatchUnitPrice").text())).toFixed(2);
        OperatorVal = parseFloat($(this).val() * parseFloat($("#lblOperatorUnitPrice").text())).toFixed(2);
        if ($("#chkDispatch").prop("checked") === true && $("#chkOperator").prop("checked") === true) {
            $("#lblOperatorTotal").text('$' + OperatorVal);
            var Total = +parseFloat(DispatchVal) + +parseFloat(OperatorVal);
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }
        else if ($("#chkDispatch").prop("checked") === false && $("#chkOperator").prop("checked") === true) {
            $("#lblOperatorTotal").text('$' + OperatorVal);
            var Total = $("#lblOperatorTotal").text();
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }
    });



});

//$(document).ready(function () {

//    $("#txtDispatchCount").on('change paste keyup', function () {
//        document.querySelector('#updatepack').disabled = false;
//        debugger;
//        ProviderVal = parseFloat($("#txtProviderCount").val() * parseFloat($("#lblProviderUnitPrice").text())).toFixed(2);
//        OperatorVal = parseFloat($("#txtOperatorCount").val() * parseFloat($("#lblOperatorUnitPrice").text())).toFixed(2);
//        DispatchVal = parseFloat($(this).val() * parseFloat($("#lblDispatchUnitPrice").text())).toFixed(2);
//        if ($("#chkDispatch").prop("checked") === true && $("#chkProvider").prop("checked") === true) {
//            $("#lblDispatchTotal").text('$' + DispatchVal);
//            $("#lblProviderTotal").text('$' + ProviderVal);
//            var Total = +parseFloat(DispatchVal) + +parseFloat(ProviderVal);
//            $("#lblTotalValue").text('$' + parseFloat(Total).toFixed(2));
//        }

//        else if ($("#chkDispatch").prop("checked") === true && $("#chkProvider").prop("checked") === false && $("#chkOperator").prop("checked") === false) {
//            $("#lblDispatchTotal").text('$' + DispatchVal);
//            var Total = $("#lblDispatchTotal").text();
//            $("#lblTotalValue").text($("#lblDispatchTotal").text());
//        }
//        else if ($("#chkDispatch").prop("checked") === true && $("#chkOperator").prop("checked") === true) {
//            $("#lblDispatchTotal").text('$' + DispatchVal);
//            $("#lblOperatorTotal").text('$' + OperatorVal);
//            var Total = +parseFloat(DispatchVal) + +parseFloat(OperatorVal);
//            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
//        }

//    });
//    $("#txtProviderCount").on('change paste keyup', function () {
//        document.querySelector('#updatepack').disabled = false;
//        debugger;
//        ProviderVal = parseFloat($(this).val() * parseFloat($("#lblProviderUnitPrice").text())).toFixed(2);
//        DispatchVal = parseFloat($("#txtDispatchCount").val() * parseFloat($("#lblDispatchUnitPrice").text())).toFixed(2);
//        if ($("#chkDispatch").prop("checked") === true && $("#chkProvider").prop("checked") === true) {
//            $("#lblProviderTotal").text('$' + ProviderVal);
//            var Total = +parseFloat(DispatchVal) + +parseFloat(ProviderVal);
//            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
//        }
//        else if ($("#chkDispatch").prop("checked") === false && $("#chkProvider").prop("checked") === true) {
//            $("#lblProviderTotal").text('$' + ProviderVal);
//            var Total = $("#lblProviderTotal").text();
//            $("#lblTotalValue").text($("#lblProviderTotal").text());
//        }
//    });
//    $("#txtOperatorCount").on('change paste keyup', function () {
//        document.querySelector('#updatepack').disabled = false;
//        DispatchVal = parseFloat($("#txtDispatchCount").val() * parseFloat($("#lblDispatchUnitPrice").text())).toFixed(2);
//        OperatorVal = parseFloat($(this).val() * parseFloat($("#lblOperatorUnitPrice").text())).toFixed(2);
//        if ($("#chkDispatch").prop("checked") === true && $("#chkOperator").prop("checked") === true) {
//            $("#lblOperatorTotal").text('$' + OperatorVal);
//            var Total = +parseFloat(DispatchVal) + +parseFloat(OperatorVal);
//            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
//        }
//        else if ($("#chkDispatch").prop("checked") === false && $("#chkOperator").prop("checked") === true) {
//            $("#lblOperatorTotalValue").text('$' + OperatorVal);
//            var Total = $("#lblOperatorTotalValue").text();
//            $("#lblTotal").text($("#lblOperatorTotalValue").text());

//        }
//    });
//});

function displayLoading(target, bool) {
    var element = $(target);
    kendo.ui.progress(element, bool);
}

function planalert(content, alerttitle) {
    $("<div></div>").kendoAlert({
        title: alerttitle,
        content: content
    }).data("kendoAlert").open();
}

function updatepackage(e) {
    if ($("#chkDispatch").prop("checked") == true && $("#chkProvider").prop("checked") == true) {
        if ($("#txtDispatchCount").val() == 0 || $("#txtDispatchCount").val() == "" || $("#txtProviderCount").val() == 0 || $("#txtProviderCount").val() == "") {
            planalert("Please the count value more than 0. ", "Alert!");
            return false;
            // e.preventDefault();
        }
    }
    if ($("#chkDispatch").prop("checked") == true && $("#chkOperator").prop("checked") == true) {
        if ($("#txtDispatchCount").val() == 0 || $("#txtDispatchCount").val() == "" || $("#txtOperatorCount").val() == 0 || $("#txtOperatorCount").val() == "") {
            planalert("Please the count value more than 0. ", "Alert!");
            return false;
            //e.preventDefault();
        }
    }
    if ($("#chkDispatch").prop("checked") == true && $("#chkProvider").prop("checked") == false && $("#chkOperator").prop("checked") == false) {
        if ($("#txtDispatchCount").val() == 0 || $("#txtDispatchCount").val() == "") {
            planalert("Please the count value more than 0. ", "Alert!");
            return false;
            //e.preventDefault();
        }
    }
    if ($("#chkDispatch").prop("checked") == false && $("#chkProvider").prop("checked") == true && $("#chkOperator").prop("checked") == false) {
        if ($("#txtProviderCount").val() == 0 || $("#txtProviderCount").val() == "") {
            planalert("Please the count value more than 0. ", "Alert!");
            return false;
            //e.preventDefault();
        }
    }
    if ($("#chkDispatch").prop("checked") == false && $("#chkProvider").prop("checked") == false && $("#chkOperator").prop("checked") == true) {
        if ($("#txtOperatorCount").val() == 0 || $("#txtOperatorCount").val() == "") {
            planalert("Please the count value more than 0. ", "Alert!");
            return false;
            //e.preventDefault();
        }
    }

    displayLoading("#content", true);

    var UpdateSubscriptionViewModel = {
        UpdateSubscriptionDetails: GetSubscriptionDetails(),
        UpdateCompanyDetails: GetCompanyDetails(),
        UpdatePaymentDetails: GetPaymentDetails()
    }
    //var updatesubscription = {
    //    SubscriptionDetails: GetSubscriptionDetails(),
    //    CompanyDetails: GetCompanyDetails(),
    //    PaymentDetails: GetPaymentDetails()

    //}
    function GetSubscriptionDetails() {

        debugger;

        if ($("#chkDispatch").prop("checked") && $("#chkProvider").prop("checked")) {
            var subscription = {
                DispatchName: $("#DispatchName").text(),
                DispatchUnitPrice: parseFloat($("#hdnDispatchPrice").val()),
                //DispatchType:$("#lblUnitTypes").text(),
                DispatchQuantity: $("#txtDispatchCount").val(),
                DispatchTotal: parseFloat($("#txtDispatchCount").val() * parseFloat($("#lblDispatchUnitPrice").text())),
                ProviderName: $("#ProviderName").text(),
                ProviderUnitPrice: parseFloat($("#hdnProviderPrice").val()),
                // ProviderType: $("#lblUnitTypes").text(),
                ProviderQuantity: $("#txtProviderCount").val(),
                ProviderTotal: parseFloat($("#txtProviderCount").val()),//* parseFloat($("#lblProviderUnitPrice").text())),
                TotalValue: parseFloat(parseFloat($("#lblDispatchTotal").text().replace('$', '')) + parseFloat($("#lblProviderTotal").text().replace('$', '')))
            }


            return subscription;

        }

        if ($("#chkDispatch").prop("checked") && $("#chkOperator").prop("checked")) {
            var subscription = {
                DispatchName: $("#DispatchName").text(),
                DispatchUnitPrice: parseFloat($("#hdnDispatchPrice").val()),
                //DispatchType: $("#lblUnitTypes").text(),
                DispatchQuantity: $("#txtDispatchCount").val(),
                DispatchTotal: parseFloat($("#txtDispatchCount").val() * parseFloat($("#lblDispatchUnitPrice").text())),
                OPeratorName: $("#OperatorName").text(),
                OPeratorUnitPrice: parseFloat($("#hdnOperatorPrice").val()),
                //OPeratorType: $("#lblUnitTypes").text(),
                OperatorQuantity: $("#txtOperatorCount").val(),
                OperatorTotal: parseFloat($("#txtOperatorCount").val()),//* parseFloat($("#lblOperatorUnitPrice").text())),
                TotalValue: parseFloat(parseFloat($("#lblDispatchTotal").text().replace('$', '')) + parseFloat($("#lblOperatorTotal").text().replace('$', '')))

            }
            return subscription;
            //subscriptiondetails.push(dispatchsubscription);
        }

        if ($("#chkDispatch").prop("checked")) {
            var subscription = {
                DispatchName: $("#DispatchName").text(),
                DispatchUnitPrice: parseFloat($("#hdnDispatchPrice").val()),
                //DispatchType: $("#lblUnitTypes").text(),
                DispatchQuantity: $("#txtDispatchCount").val(),
                DispatchTotal: parseFloat($("#txtDispatchCount").val()),// * parseFloat($("#lblDispatchUnitPrice").text())),
                TotalValue: parseFloat($("#txtDispatchCount").val())// * parseFloat($("#lblDispatchUnitPrice").text()))

            }
            return subscription;
            //subscriptiondetails.push(providersubscription);
        }
        if ($("#chkProvider").prop("checked")) {
            var subscription = {
                ProviderName: $("#ProviderName").text(),
                ProviderUnitPrice: parseFloat($("#hdnProviderPrice").val()),
                //ProviderType: $("#lblUnitTypes").text(),
                ProviderQuantity: $("#txtProviderCount").val(),
                ProviderTotal: parseFloat($("#txtProviderCount").val()), // * parseFloat($("#lblProviderUnitPrice").text())),
                TotalValue: parseFloat($("#txtProviderCount").val())// * parseFloat($("#lblProviderUnitPrice").text()))
            }

            return subscription;
            //subscriptiondetails.push(providersubscription);
        }
        if ($("#chkOperator").prop("checked")) {
            var subscription = {
                OPeraorName: $("#OperatorName").text(),
                OPeratorUnitPrice: parseFloat($("#hdnOperatorPrice").val()),
                // OPeratorType: $("#lblUnitTypes").text(),
                OperatorQuantity: $("#txtOperatorCount").val(),
                OperatorTotal: parseFloat($("#txtOperatorCount").val()),// *  parseFloat($("#lblOperatorUnitPrice").text())),
                TotalValue: parseFloat($("#txtOperatorCount").val()) //* parseFloat($("#lblOperatorUnitPrice").text()))
            }
            return subscription;
            //subscriptiondetails.push(Operatorsubscription);
        }

        return subscriptiondetails;
    }




    function GetCompanyDetails() {
        var CompanyDetails = {
            Name: $("#lblName").text(),
            CName: $("#lblName").text(),
            Title: $("#lblName").text(),
            CompanyPhone: $("#lblPhone").text(),
            CompanyEmail: $("#lblEmail").text(),
            CompanyAddress1: $("#lblAddress").text(),
            CompanyAddress2: $("#lblAddress").text(),
            CompanyCity: $("#lblCity").text(),
            CompanyState: $("#lblCity").text(),
            CompanyZip: $("#lblZip").text(),
            BillingName: $("#lblName").text(),
            BillingPhone: $("#lblPhone").text(),
            BillingEmail: $("#lblEmail").text(),
            BillingAddress1: $("#lblAddress").text(),
            BillingAddress2: $("#lblAddress").text(),
            BillingCity: $("#lblCity").text(),
            BillingState: $("#lblCity").text(),
            BillingZip: $("#lblZip").text()

        }
        return CompanyDetails;
    }

    function GetPaymentDetails2() {

        // displayLoading("#wizard", true);
        var paymenttype = $("#PaymentType").val();
        var ptype = $("#lblPaymentType").text();

        if (ptype == "Credit Card" || ptype == "Debit Card" || ptype == "1" || ptype == "2") {
            var PaymentDetails = {
                PaymentType: $("#lblPaymentType").text(),
                CardName: $("#lblCardHolder").text(),
                CardNumber: $("#lblCardNumber").text(),
                Month: $("#lblCardMonth").text(),
                Year: $("#lblCardYear").text(),
                PaymentMethodId: $("#hdnpaymentmethodid").val()
            }
            var PaymentDetailsJson = PaymentDetails;
            return PaymentDetailsJson;
        }
        else {
            var PaymentDetails = {
                PaymentType: $("#lblPT").text(),
                CustomerName: $("#lblCustomerName").text(),
                BankName: $("#lblBankName").text(),
                AccountNumber: $("#lblaccountNumber").text(),
                TypeofAccount: $("#lblAccountType").text(),
                RoutingNumber: $("#lblRoutingNumber").text(),
                PaymentMethodId: $("#hdnpaymentmethodid").val()
            }
            var PaymentDetailsJson = PaymentDetails;
            return PaymentDetailsJson;
        }
    }


    function GetPaymentDetails() {

        var PaymentDetails = {
            PaymentMethodId: $("#hdnpaymentmethodid").val()
        }
        var PaymentDetailsJson = PaymentDetails;
        return PaymentDetailsJson;

    }

    $.ajax({
        type: "POST",
        data: JSON.stringify(UpdateSubscriptionViewModel),
        dataType: "json",
        url: "ProductSubscriptionSRVNew/UpdateDetails",
        contentType: "application/json; charset=utf-8",
        success: function (responce) {
            //debugger;
            displayLoading("#content", false);
            if (responce == "1") {
                planalert("Plan Updated Successfully", "Alert!");
                ordersummary();
            }
            else if (responce == "0") {
                planalert("Payment Not Completed!", "Alert!");
            }
            //  ordersummary();
            // window.console.log('Successful');
        }, error: function (response) {
            // debugger;
            // alret(response.text())
        }

    }

    );
    //debugger;
    //  console.log(JSON.stringify(Updatesubscription))
}
//function updatepackage(e) {
//    debugger;
//    displayLoading("#content", true);
  
//    var UpdateSubscriptionViewModel = {
//        UpdateSubscriptionDetails: GetSubscriptionDetails(),
//        UpdateCompanyDetails: GetCompanyDetails(),
//        UpdatePaymentDetails: GetPaymentDetails()
//    }
//    //var updatesubscription = {
//    //    SubscriptionDetails: GetSubscriptionDetails(),
//    //    CompanyDetails: GetCompanyDetails(),
//    //    PaymentDetails: GetPaymentDetails()

//    //}
//    //function GetSubscriptionDetails() {

//    //    debugger;
       
//    //    if ($("#chkDispatch").prop("checked") && $("#chkProvider").prop("checked")) {
//    //        var subscription = {
//    //            DispatchName: $("#DispatchName").text(),
//    //            DispatchUnitPrice: parseFloat($("#hdnDispatchPrice").val()),
//    //            //DispatchType:$("#lblUnitTypes").text(),
//    //            DispatchQuantity: $("#txtDispatchCount").val(),
//    //            DispatchTotal: parseFloat($("#txtDispatchCount").val() * parseFloat($("#lblDispatchUnitPrice").text())),
//    //            ProviderName: $("#ProviderName").text(),
//    //            ProviderUnitPrice: parseFloat($("#hdnProviderPrice").val()),
//    //            // ProviderType: $("#lblUnitTypes").text(),
//    //            ProviderQuantity: $("#txtProviderCount").val(),
//    //            ProviderTotal: parseFloat($("#txtProviderCount").val() ),//* parseFloat($("#lblProviderUnitPrice").text())),
//    //            TotalValue: parseFloat(parseFloat($("#lblDispatchTotal").text().replace('$', '')) + parseFloat($("#lblProviderTotal").text().replace('$', '')))
//    //        }

            
//    //        return subscription;
           
//    //    }

//    //    if ($("#chkDispatch").prop("checked") && $("#chkOperator").prop("checked")) {
//    //        var subscription = {
//    //            DispatchName: $("#DispatchName").text(),
//    //            DispatchUnitPrice: parseFloat($("#hdnDispatchPrice").val()),
//    //            //DispatchType: $("#lblUnitTypes").text(),
//    //            DispatchQuantity: $("#txtDispatchCount").val(),
//    //            DispatchTotal: parseFloat($("#txtDispatchCount").val() * parseFloat($("#lblDispatchUnitPrice").text())),
//    //            OPeratorName: $("#OperatorName").text(),
//    //            OPeratorUnitPrice: parseFloat($("#hdnOperatorPrice").val()),
//    //            //OPeratorType: $("#lblUnitTypes").text(),
//    //            OperatorQuantity: $("#txtOperatorCount").val(),
//    //            OperatorTotal: parseFloat($("#txtOperatorCount").val() ),//* parseFloat($("#lblOperatorUnitPrice").text())),
//    //            TotalValue: parseFloat(parseFloat($("#lblDispatchTotal").text().replace('$', '')) + parseFloat($("#lblOperatorTotal").text().replace('$', '')))

//    //        }
//    //        return subscription;
//    //        //subscriptiondetails.push(dispatchsubscription);
//    //    }

//    //    if ($("#chkDispatch").prop("checked")) {
//    //        var subscription = {
//    //            DispatchName: $("#DispatchName").text(),
//    //            DispatchUnitPrice: parseFloat($("#hdnDispatchPrice").val()),
//    //            //DispatchType: $("#lblUnitTypes").text(),
//    //            DispatchQuantity: $("#txtDispatchCount").val(),
//    //            DispatchTotal: parseFloat($("#txtDispatchCount").val()),// * parseFloat($("#lblDispatchUnitPrice").text())),
//    //            TotalValue: parseFloat($("#txtDispatchCount").val())// * parseFloat($("#lblDispatchUnitPrice").text()))

//    //        }
//    //        return subscription;
//    //        //subscriptiondetails.push(providersubscription);
//    //    }
//    //    if ($("#chkProvider").prop("checked")) {
//    //        var subscription = {
//    //            ProviderName: $("#ProviderName").text(),
//    //            ProviderUnitPrice: parseFloat($("#hdnProviderPrice").val()),
//    //            //ProviderType: $("#lblUnitTypes").text(),
//    //            ProviderQuantity: $("#txtProviderCount").val(),
//    //            ProviderTotal: parseFloat($("#txtProviderCount").val()), // * parseFloat($("#lblProviderUnitPrice").text())),
//    //            TotalValue: parseFloat($("#txtProviderCount").val())// * parseFloat($("#lblProviderUnitPrice").text()))
//    //        }
        
//    //        return subscription;
//    //        //subscriptiondetails.push(providersubscription);
//    //    }
//    //    if ($("#chkOperator").prop("checked")) {
//    //        var subscription = {
//    //            OPeraorName: $("#OperatorName").text(),
//    //            OPeratorUnitPrice: parseFloat($("#hdnOperatorPrice").val()),
//    //            // OPeratorType: $("#lblUnitTypes").text(),
//    //            OperatorQuantity: $("#txtOperatorCount").val(),
//    //            OperatorTotal: parseFloat($("#txtOperatorCount").val()),// *  parseFloat($("#lblOperatorUnitPrice").text())),
//    //            TotalValue: parseFloat($("#txtOperatorCount").val() ) //* parseFloat($("#lblOperatorUnitPrice").text()))
//    //        }
//    //        return subscription;
//    //        //subscriptiondetails.push(Operatorsubscription);
//    //    }

//    //    return subscriptiondetails;
//    //}

    

    
//    function GetCompanyDetails() {
//        var CompanyDetails = {
//            Name: $("#lblName").text(),
//            CName: $("#lblName").text(),
//            Title: $("#lblName").text(),
//            CompanyPhone: $("#lblPhone").text(),
//            CompanyEmail: $("#lblEmail").text(),
//            CompanyAddress1: $("#lblAddress").text(),
//            CompanyAddress2: $("#lblAddress").text(),
//            CompanyCity: $("#lblCity").text(),
//            CompanyState: $("#lblCity").text(),
//            CompanyZip: $("#lblZip").text(),
//            BillingName: $("#lblName").text(),
//            BillingPhone: $("#lblPhone").text(),
//            BillingEmail: $("#lblEmail").text(),
//            BillingAddress1: $("#lblAddress").text(),
//            BillingAddress2: $("#lblAddress").text(),
//            BillingCity: $("#lblCity").text(),
//            BillingState: $("#lblCity").text(),
//            BillingZip: $("#lblZip").text()

//        }
//        return CompanyDetails;
//    }

//    function GetPaymentDetails2() {

//        // displayLoading("#wizard", true);
//        var paymenttype = $("#PaymentType").val();
//        var ptype = $("#lblPaymentType").text();

//        if (ptype == "Credit Card" || ptype == "Debit Card" || ptype == "1" || ptype == "2") {
//            var PaymentDetails = {
//                PaymentType: $("#lblPaymentType").text(),
//                CardName: $("#lblCardHolder").text(),
//                CardNumber: $("#lblCardNumber").text(),
//                Month: $("#lblCardMonth").text(),
//                Year: $("#lblCardYear").text(),
//                PaymentMethodId: $("#hdnpaymentmethodid").val()
//            }
//            var PaymentDetailsJson = PaymentDetails;
//            return PaymentDetailsJson;
//        }
//        else {
//            var PaymentDetails = {
//                PaymentType: $("#lblPT").text(),
//                CustomerName: $("#lblCustomerName").text(),
//                BankName: $("#lblBankName").text(),
//                AccountNumber: $("#lblaccountNumber").text(),
//                TypeofAccount: $("#lblAccountType").text(),
//                RoutingNumber: $("#lblRoutingNumber").text(),
//                PaymentMethodId: $("#hdnpaymentmethodid").val()
//            }
//            var PaymentDetailsJson = PaymentDetails;
//            return PaymentDetailsJson;
//        }
//    }


//    function GetPaymentDetails() {

//        var PaymentDetails = {
//            PaymentMethodId: $("#hdnpaymentmethodid").val()
//        }
//        var PaymentDetailsJson = PaymentDetails;
//        return PaymentDetailsJson;

//    }
    
//    $.ajax({
//        type: "POST",
//        data: JSON.stringify(UpdateSubscriptionViewModel),
//        dataType: "json",
//        url: "ProductSubscriptionSRVNew/UpdateDetails",
//        contentType: "application/json; charset=utf-8",
//        success: function (responce) {
//            //debugger;
//            displayLoading("#content", false);
//            if (responce == "1") {
//                planalert("Plan Updated Successfully", "Alert!");
//                ordersummary();
//            }
//            else if (responce == "0") {
//                planalert("Payment Not Completed!", "Alert!");
//            }
//          //  ordersummary();
//            // window.console.log('Successful');
//        }, error: function (response) {
//           // debugger;
//           // alret(response.text())
//        }
           
//    }
       
//    );
//    //debugger;
//  //  console.log(JSON.stringify(Updatesubscription))
//}

function ordersummary() {
    //temp------

    var window1 = $("#ordersummary").data("kendoWindow");
    window1.refresh({
        url: "/ProductSubscription/OrderSummary"
    });
    $("#ordersummary").closest(".k-window").css({
        top: 150,
        left: 650
    });
    window1.refresh().center().open();
}

function Cancelsubscription() {

    var result = kendo.confirm("Do you want to Cancel Subscription ?")
        .done(function () {
            debugger;
            var _SubscriptionId = "";
                        $.ajax({
                            url: "/ProductSubscriptionSRVNew/CancelSubscription",
                        type: "POST",
                       // dataType: "json",
                       // data: JSON.stringify(data),
                       // data: JSON.stringify(UserDetails),
                        contentType: "application/json; chartset=uft-8",
                            success: function (response) {
                                planalert(response, "Alert!");
                          //  kendo.alert("This Location will be sent to Operator :" + dataItem.customer);
                        },
                        error: function (xhr, status, error) {
                            displayLoading("#DispatchAssignWindow", false);
                            planalert(xhr.responseText, "Error");
                        }
                    });



        })
        .fail(function () {
            return false;
        });

}


//----------------------murugesh changes
//1
function newpayment() {
    //   debugger;


    var window2 = $("#addNewPaymentgird").data("kendoWindow");
    window2.refresh({
        //  url: "/ProductSubscriptionSRVNew/NewPayment"
        url: "/ProductSubscriptionSRVNew/Changenwepayment"
    });

    $("#addNewPaymentgird").closest(".k-window").css({
        top: 150,
        left: 650
    });


    window2.refresh().center().open();

    //  newpayment2();

}

function newaddress() {

    var window2 = $("#addNewbillingaddress").data("kendoWindow");
    window2.refresh({
        //  url: "/ProductSubscriptionSRVNew/NewPayment"
        url: "/ProductSubscriptionSRVNew/Billinginfo"
    });

    $("#addNewbillingaddress").closest(".k-window").css({
        top: 150,
        left: 650
    });


    window2.refresh().center().open();


}

function addnewpayment() {
    debugger;


    var window3 = $("#addNewPayment").data("kendoWindow");
    window3.refresh({
        // url: "/ProductSubscriptionSRVNew/addPayment"
        url: "../../DispatchSRV/addPayment"
    });

    $("#addNewPayment").closest(".k-window").css({
        top: 150,
        left: 650
    });


    window3.refresh().center().open();


}


function SetUnitPriceAndLabel() {
    //debugger;
    if ($("#chkDispatch").prop("checked") && $("#chkProvider").prop("checked")) {
        $("#trDispatch").css("display", "block");
        $("#trProvider").css("display", "block");
        $("#trOperator").css("display", "none");

        $("#lblUnitType").text("Per Unit");
        $("#lblUnitTypeForCount").text("Count");
        $("txtOperatorCount").val("");
        $("#lblDispatchUnitPrice").text($("#hdnDispatchPrice").val());
        $("#lblProviderUnitPrice").text($("#hdnProviderPrice").val());
        var PackTypes = [$("#DispatchName").text(), $("#ProviderName").text()];

        // $("#lblDispatchTotalValue").val('$' +parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text()));
        //  $("#lblProviderTotalValue").val('$' +parseInt($("#txtProviderCount").val()) * parseFloat($("#lblProviderUnitPrice").text()));
        $("#lblDispatchTotal").text('$' + parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text()));
        $("#lblProviderTotal").text('$' + parseInt($("#txtProviderCount").val()) * parseFloat($("#lblProviderUnitPrice").text()));

        var dispatch_total = parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text());
        var provider_total = parseInt($("#txtProviderCount").val()) * parseFloat($("#lblProviderUnitPrice").text());
        $("#lblTotal").text('$' + (dispatch_total + provider_total));
        $("#lblTotal1").text('$' + (dispatch_total + provider_total));

    }
    else if ($("#chkDispatch").prop("checked") && $("#chkOperator").prop("checked")) {
        $("#trDispatch").css("display", "block");
        $("#trProvider").css("display", "none");
        $("#trOperator").css("display", "block");

        $("#lblUnitType").text("Per Unit");
        $("#lblUnitTypeForCount").text("Count");
        $("txtProviderCount").val("");
        $("#lblDispatchUnitPrice").text($("#hdnDispatchPrice").val());
        $("#lblOperatorUnitPrice").text($("#hdnOperatorPrice").val());
        var PackTypes = [$("#DispatchName").text(), $("#OperatorName").text()];

        // $("#lblDispatchTotalValue").val('$' +parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text()));
        //
        //  $("#lblOperatorTotalValue").val('$' +parseInt($("#txtOperatorCount").val()) * parseFloat($("#lblOperatorUnitPrice").text()));
        $("#lblDispatchTotal").text('$' + parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text()));
        //
        $("#lblOperatorTotal").text('$' + parseInt($("#txtOperatorCount").val()) * parseFloat($("#lblOperatorUnitPrice").text()));


        var dispatch_total = parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text());
        var operatore_total = parseInt($("#txtOperatorCount").val()) * parseFloat($("#lblOperatorUnitPrice").text());
        $("#lblTotal").text('$' + (dispatch_total + operatore_total));
        $("#lblTotal1").text('$' + (dispatch_total + operatore_total));
    }
    else if ($("#chkDispatch").prop("checked")) {
        $("#trDispatch").css("display", "block");
        $("#trProvider").css("display", "none");
        $("#trOperator").css("display", "none");


        $("#lblUnitType").text("Per Unit");
        $("txtProviderCount").val("");
        $("txtOperatorCount").val("");
        $("#lblUnitTypeForCount").text("Count");
        $("#lblDispatchUnitPrice").text($("#hdnDispatchPrice").val());
        var PackTypes = [$("#DispatchName").text()];
        //  $("#lblDispatchTotalValue").val(parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text()));
        $("#lblDispatchTotal").text('$' + parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text()));

        var dispatch_total = parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text());

        $("#lblTotal").text('$' + (dispatch_total));
        $("#lblTotal1").text('$' + (dispatch_total));
    }
    else if ($("#chkProvider").prop("checked")) {
        $("#trDispatch").css("display", "none");
        $("#trProvider").css("display", "block");
        $("#trOperator").css("display", "none");


        $("#lblUnitType").text("Per Unit");
        $("#lblUnitTypeForCount").text("Count");
        $("txtDispatchCount").val("");
        $("txtOperatorCount").val("");
        $("#lblProviderUnitPrice").text($("#hdnProviderPrice").val());
        var PackTypes = [$("#ProviderName").text()];

        //  $("#lblProviderTotalValue").val(parseInt($("#txtProviderCount").val()) * parseFloat($("#lblProviderUnitPrice").text()));
        $("#lblProviderTotal").text('$' + parseInt($("#txtProviderCount").val()) * parseFloat($("#lblProviderUnitPrice").text()));
        var provider_total = parseInt($("#txtProviderCount").val()) * parseFloat($("#lblProviderUnitPrice").text());

        $("#lblTotal").text('$' + (provider_total));
        $("#lblTotal1").text('$' + (provider_total));
    }
    else if ($("#chkOperator").prop("checked")) {
        $("#trDispatch").css("display", "none");
        $("#trProvider").css("display", "none");
        $("#trOperator").css("display", "block");

        $("#lblUnitType").text("Per Unit");
        $("#lblUnitTypeForCount").text("Count");
        $("txtProviderCount").val("");
        $("txtDispatchCount").val("");
        $("#lblOperatorUnitPrice").text($("#hdnOperatorPrice").val());
        var PackTypes = [$("#OperatorName").text()];
        //  $("#lblOperatorTotalValue").val(parseInt($("#txtOperatorCount").val()) * parseFloat($("#lblOperatorUnitPrice").text()));
        $("#lblOperatorTotal").text('$' + parseInt($("#txtOperatorCount").val()) * parseFloat($("#lblOperatorUnitPrice").text()));
        var operatore_total = parseInt($("#txtOperatorCount").val()) * parseFloat($("#lblOperatorUnitPrice").text());
        $("#lblTotal").text('$' + (operatore_total));
        $("#lblTotal1").text('$' + (operatore_total));
    }

}


$(document).ready(function () {
    $("#txtDispatchCount").on('change paste keyup', function () {

        DispatchVal = parseFloat($(this).val() * parseFloat($("#lblDispatchUnitPrice").text())).toFixed(2);
        if ($("#chkDispatch").prop("checked") === true && $("#chkProvider").prop("checked") === true) {
            $("#lblDispatchTotal").text('$' + DispatchVal);
            $("#lblProviderTotal").text('$' + ProviderVal);
            var Total = +parseFloat(DispatchVal) + +parseFloat(ProviderVal);
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }

        else if ($("#chkDispatch").prop("checked") === true && $("#chkProvider").prop("checked") === false && $("#chkOperator").prop("checked") === false) {
            $("#lblDispatchTotal").text('$' + DispatchVal);
            var Total = $("#lblDispatchTotal").text();
            $("#lblTotal").text($("#lblDispatchTotal").text());
            $("#lblTotal1").text($("#lblTotal").text());
        }
        else if ($("#chkDispatch").prop("checked") === true && $("#chkOperator").prop("checked") === true) {
            $("#lblDispatchTotal").text('$' + DispatchVal);
            $("#lblOperatorTotal").text('$' + OperatorVal);
            var Total = +parseFloat(DispatchVal) + +parseFloat(OperatorVal);
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }

    });
    $("#txtProviderCount").on('change paste keyup', function () {
        ProviderVal = parseFloat($(this).val() * parseFloat($("#lblProviderUnitPrice").text())).toFixed(2);
        if ($("#chkDispatch").prop("checked") === true && $("#chkProvider").prop("checked") === true) {
            $("#lblProviderTotal").text('$' + ProviderVal);
            var Total = +parseFloat(DispatchVal) + +parseFloat(ProviderVal);
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }
        else if ($("#chkDispatch").prop("checked") === false && $("#chkProvider").prop("checked") === true) {
            $("#lblProviderTotal").text('$' + ProviderVal);
            var Total = $("#lblProviderTotal").text();
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }
    });
    $("#txtOperatorCount").on('change paste keyup', function () {


        OperatorVal = parseFloat($(this).val() * parseFloat($("#lblOperatorUnitPrice").text())).toFixed(2);
        if ($("#chkDispatch").prop("checked") === true && $("#chkOperator").prop("checked") === true) {
            $("#lblOperatorTotal").text('$' + OperatorVal);
            var Total = +parseFloat(DispatchVal) + +parseFloat(OperatorVal);
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }
        else if ($("#chkDispatch").prop("checked") === false && $("#chkOperator").prop("checked") === true) {
            $("#lblOperatorTotal").text('$' + OperatorVal);
            var Total = $("#lblOperatorTotal").text();
            $("#lblTotal").text('$' + parseFloat(Total).toFixed(2));
            $("#lblTotal1").text($("#lblTotal").text());
        }
    });
    
    RefreshGrid();

});


//2
function OnGridSave() {
    //setTimeout(function () {
    //    $('#paymenthods').data('kendoGrid').dataSource.read().then(function () {
    //       $('#paymenthods').data('kendoGrid').refresh();
    //    });
    //}, 1500);
}

function RefreshGrid() {
    var IntTime = 600000;
    setInterval(function () {
        $("#paymenthods").data("kendoGrid").dataSource.read();
    }, IntTime);
}

function onDataBound() {
    var grid = this;
    grid.element.off('dblclick');
    grid.element.on('dblclick', 'tbody tr[data-uid]', function (e) {
        grid.editRow($(e.target).closest('tr'));
    })
}


$(document).ready(function () {
    var url = window.location.href;
    if (url.indexOf('modal=true') > 1) {
        window.setTimeout(function () {
            $('.k-grid-add').trigger("click");
        }, 1000);

    }
});
function monthChange(e) {
    var ddlmonthValue = $("#ddlExpireMonth").val();
    $("#ExpireMonth").data("kendoMaskedTextBox").value(ddlmonthValue);
    $("#ExpireMonth").data("kendoMaskedTextBox").trigger("change");
}
function yearChange(e) {
    var ddlyearValue = $("#ddlExpireYear").val();
    $("#ExpireYear").data("kendoMaskedTextBox").value(ddlyearValue);
    $("#ExpireYear").data("kendoMaskedTextBox").trigger("change");
}
function stateChange(e) {
    var ddlstateValue = $("#ddlState").val();
    $("#State").data("kendoMaskedTextBox").value(ddlstateValue);
    $("#State").data("kendoMaskedTextBox").trigger("change");
}
function countryChange(e) {
    var ddlCountryValue = $("#ddlCountry").val();
    $("#Country").data("kendoMaskedTextBox").value(ddlCountryValue);
    $("#Country").data("kendoMaskedTextBox").trigger("change");
}

function CardChange(e) {
   // debugger;
    var ddlCardTypevalue = $("#ddlCardType").val();
    $("#CardType").data("kendoMaskedTextBox").value(ddlCardTypevalue);
    $("#CardType").data("kendoMaskedTextBox").trigger("change");
}

function paytypeChange(e) {
    // debugger;
    var ddlPayTypeValue = $("#ddlPayType").val();
    if (ddlPayTypeValue != null) {
        $("#PayType").data("kendoMaskedTextBox").value(ddlPayTypeValue);
        $("#PayType").data("kendoMaskedTextBox").trigger("change");
        if (ddlPayTypeValue == "1" || ddlPayTypeValue == "2") {
            $("input").removeAttr("required");
            $("#NameOnCard").attr("required", "true"); $("#CardNum").attr("required", "true");
            $("#NickName").attr("required", "true"); $("#ddlExpireMonth").attr("required", "true");
            $("#ddlExpireYear").attr("required", "true"); $("#FirstName").attr("required", "true");
            $("#LastName").attr("required", "true"); $("#Address1").attr("required", "true");
            $("#ddlState").attr("required", "true"); $("#ZipCode").attr("required", "true");
            $("#ddlCountry").attr("required", "true");
            $('.creditcard').removeClass('div-invisible').addClass('div-visible');
            $('.bankAccount').removeClass('div-visible').addClass('div-invisible');
            $('.check').removeClass('div-visible').addClass('div-invisible');
            if (ddlPayTypeValue == "1") {
                $('.creditcardType').removeClass('div-invisible').addClass('div-visible');
            }
            else {
                $('.creditcardType').removeClass('div-visible').addClass('div-invisible');
            }

        }
        if (ddlPayTypeValue == "4") {
            $("input").removeAttr("required");
            $("#AcHolderName").attr("required", "true");
            $("#AcNum").attr("required", "true");
            $("#RoutingNum").attr("required", "true");
            $('.bankAccount').removeClass('div-invisible').addClass('div-visible');
            $('.creditcard').removeClass('div-visible').addClass('div-invisible');
            $('.check').removeClass('div-visible').addClass('div-invisible');
            $('.creditcardType').removeClass('div-visible').addClass('div-invisible');
        }

        if (ddlPayTypeValue == "3") {

            $("input").removeAttr("required");
            $('.bankAccount').removeClass('div-visible').addClass('div-invisible');
            $('.creditcard').removeClass('div-visible').addClass('div-invisible');
            $('.check').removeClass('div-invisible').addClass('div-visible');
            $('.creditcardType').removeClass('div-visible').addClass('div-invisible');
            $("#RoutingNum1").attr("required", "true");
            $("#checkNum").attr("required", "true");
        }
    }
}
function agreeChange(e) {
    if (e.checked) {
        $(".k-grid-update").removeClass("disablelink");
        $("#Agreement").data("kendoMaskedTextBox").value("true");
        $("#Agreement").data("kendoMaskedTextBox").trigger("change");
        $(".switchwrap .k-switch-label-on").show();
        $(".switchwrap .k-switch-label-off").hide();
    }
    else {
        $(".k-grid-update").addClass("disablelink");
        $("#Agreement").data("kendoMaskedTextBox").value("false");
        $("#Agreement").data("kendoMaskedTextBox").trigger("change");
        $(".switchwrap .k-switch-label-on").hide();
        $(".switchwrap .k-switch-label-off").show();
    }
}
function customDelete(e) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

    kendo.confirm("Are you sure delete this item?")
        .done(function () {
            $.ajax({
                url: "PaymentMethods/PaymentMethods_Destroy?methodId=" + dataItem.ID,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {

                    var grid = $("#paymenthods").data("kendoGrid");
                    grid.dataSource.read();
                }
            });
        })
        .fail(function () {
            return false;
        });
}

//3
$(document).ready(function () {

    var string = $("#lblCardNumber").text();
    var new_string = string.replace(/\b[\dX][-. \dX]+(\d{4})\b/g, function (match, capture) {
        return Array(match.length - 4).join("X") + capture;
    });

    $("#lblCardNumbermask").text(new_string);
    document.querySelector('#updatepack').disabled = true;
    SetUnitPriceAndLabel();
    if ($(chkProvider.checked == true)) {
        $("#chkOperator").prop("disabled", disabled);
        $("#chkOperator").prop(disabled, true);
        $(chkOperator.disabled = true);
    }

    if ($("#lblCardMonth").text().length == 1) {
        $("#lblCardMonth").text("0" + $("#lblCardMonth").text());
    }


});
function updcheck() {
    document.querySelector('#updatepack').disabled = false;
    SetUnitPriceAndLabel();
    if ($(chkDispatch.checked == true) && $(chkProvider.checked == true)) {
        if ($("#chkDispatch").prop("checked") && $("#chkProvider").prop("checked")) {
            $("#chkOperator").prop("checked", false);
        }
    }
    if ($(chkDispatch.checked == true) && $(chkOperator.checked == true)) {
        if ($("#chkDispatch").prop("checked") && $("#chkOperator").prop("checked")) {
            $("#chkProvider").prop("checked", false);
        }
    }
}
function upduncheck() {
    document.querySelector('#updatepack').disabled = false;
    SetUnitPriceAndLabel();
    if ($(chkDispatch.checked == true) && $(chkProvider.checked == true)) {
        if ($("#chkDispatch").prop("checked") && $("#chkProvider").prop("checked")) {
            $("#chkOperator").prop("checked", false);
        }
    }
    if ($(chkProvider.checked == true)) {
        if ($("#chkProvider").prop("checked")) {
            $("#chkOperator").prop("checked", false);
        }
    }
}

function upduncheck1() {
    document.querySelector('#updatepack').disabled = false;
    SetUnitPriceAndLabel();
    if ($(chkDispatch.checked == true) && $(chkOperator.checked == true)) {
        if ($("#chkDispatch").prop("checked") && $("#chkOperator").prop("checked")) {
            $("#chkProvider").prop("checked", false);
        }
    }
    if ($(chkOperator.checked == true)) {
        if ($("#chkOperator").prop("checked")) {
            $("#chkProvider").prop("checked", false);
        }
    }
}
//5
function RenameWindow(e) {
    if (e.model.isNew()) {
        $.ajax({
            url: "/CorporateProfileSRV/GetCorporateProfile",
            type: 'GET',
            dataType: 'json', // added data type
            success: function (data) {
                console.log(data);
                $("#Address1").val(data.Address1);
                $("#Address2").val(data.Address2);
                $("#City").val(data.City);
                $("#ZipCode").val(data.Zip);
                var dropdownStatelist = $("#ddlState").data("kendoDropDownList");
                dropdownStatelist.text(data.State);
                dropdownStatelist.trigger("change");
                var dropdownCountrylist = $("#ddlCountry").data("kendoDropDownList");
                dropdownCountrylist.text(data.Country);
                dropdownCountrylist.trigger("change");

            }
        });
        e.container.data("kendoWindow").title("New payment");
    }
}
function showdelete(e) {
    e.preventDefault();
    alert('Are you sure to delete this payment method?')
}

//6

function submitpayment() {
    debugger;
    var sourcegrid = $('#paymenthods').data('kendoGrid');

    const SelectedItems = sourcegrid.select();
    if (SelectedItems.length > 0) {

        sourcegrid.select().each(function () {
            var dataItem = sourcegrid.dataItem($(this));
            var vendor =
            {
                ID: dataItem.ID,
                Holder: dataItem.Holder,
                Number: dataItem.Number,
                ExpireMonth: dataItem.ExpireMonth,
                ExpireYear: dataItem.ExpireYear,
                PayType: dataItem.PayType,
                Secondary: false,
            };


            if (vendor.PayType == "1") { $("#lblPaymentType").text("Credit Card"); }
            else if (vendor.PayType == "2") { $("#lblPaymentType").text("Debit Card"); }
            else if (vendor.PayType == "3") { $("#lblPaymentType").text("ACH Payment"); }
            $("#lblCardHolder").text(vendor.Holder);

            $("#lblCardNumber").text(vendor.Number);
            $("#lblCardMonth").text(vendor.ExpireMonth);
            $("#lblCardYear").text(vendor.ExpireYear);
            $("#hdnpaymentmethodid").val(vendor.ID);
            document.querySelector('#updatepack').disabled = false;

            var string = $("#lblCardNumber").text();
            var new_string = string.replace(/\b[\dX][-. \dX]+(\d{4})\b/g, function (match, capture) {
                return Array(match.length - 4).join("X") + capture;
            });
            debugger;
            var lm = $("#lblCardMonth").text();
            if ($("#lblCardMonth").text().length == 1) {
                $("#lblCardMonth").text("0" + $("#lblCardMonth").text());
            }

            $("#lblCardNumbermask").text(new_string);

            var window = $("#addNewPaymentgird").data("kendoWindow");
            window.close();
            //  UserDetails.operatorId = vendor.CompanyId;








            //  alert(vendor.CompanyId)

        });


    } else {
        planalert("Please select a Payment", "Information");
    }
}
function closepayment() {
    var window = $("#addNewPaymentgird").data("kendoWindow");
    window.close();
}


function planalert(content, alerttitle) {
    $("<div></div>").kendoAlert({
        title: alerttitle,
        content: content
    }).data("kendoAlert").open();
}
//4
$(document).ready(function () {
    var valmonth = $("#ExpireMonth").val();
    $("#ddlExpireMonth").data("kendoDropDownList").value(valmonth);
    var valyear = $("#ExpireYear").val();
    $("#ddlExpireYear").data("kendoDropDownList").value(valyear);
    var valstate = $("#State").val();
    $("#ddlState").data("kendoDropDownList").value(valstate);
    var valpaytype = $("#PayType").val();
    $("#ddlPayType").data("kendoDropDownList").value(valpaytype);
    if (valpaytype !== "") {
        paytypeChange();
    }
    var valcountry = $("#Country").val();
    $("#ddlCountry").data("kendoDropDownList").value(valcountry);

    var valCardType = $("#CardType").val();
    $("#ddlCardType").data("kendoDropDownList").value(valCardType);

    var agrreval = $("#Agreement").val();
    if (agrreval == "true") {
        $(".k-grid-update").removeClass("disablelink");
        $("#switchgree").data("kendoSwitch").toggle();
        $(".switchwrap .k-switch-label-on").show();
        $(".switchwrap .k-switch-label-off").hide();
    }
    else {
        $(".k-grid-update").addClass("disablelink");
        $(".switchwrap .k-switch-label-on").hide();
        $(".switchwrap .k-switch-label-off").show();
    }
});
