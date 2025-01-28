

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

function onDone(e) {
    var registration = {
        SubscriptionDetails: GetSubscriptionDetails(),
        CompanyDetails: GetCompanyDetails(),
        PaymentDetails: GetPaymentDetails()
    }
    function GetSubscriptionDetails() {

        //var subscriptiondetails = [];
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
                ProviderTotal: parseFloat($("#txtProviderCount").val() * parseFloat($("#lblProviderUnitPrice").text())),
                TotalValue: parseFloat(parseFloat($("#lblDispatchTotalValue").text().replace('$', '')) + parseFloat($("#lblProviderTotalValue").text().replace('$', '')))
            }
            return subscription;
            //subscriptiondetails.push(dispatchsubscription);
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
                OperatorTotal: parseFloat($("#txtOperatorCount").val() * parseFloat($("#lblOperatorUnitPrice").text())),
                TotalValue: parseFloat(parseFloat($("#lblDispatchTotalValue").text().replace('$', '')) + parseFloat($("#lblOperatorTotalValue").text().replace('$', '')))

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
                DispatchTotal: parseFloat($("#txtDispatchCount").val() * parseFloat($("#lblDispatchUnitPrice").text())),
                TotalValue: parseFloat($("#txtDispatchCount").val() * parseFloat($("#lblDispatchUnitPrice").text()))

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
                ProviderTotal: parseFloat($("#txtProviderCount").val() * parseFloat($("#lblProviderUnitPrice").text())),
                TotalValue: parseFloat($("#txtProviderCount").val() * parseFloat($("#lblProviderUnitPrice").text()))
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
                OperatorTotal: parseFloat($("#txtOperatorCount").val() * parseFloat($("#lblOperatorUnitPrice").text())),
                TotalValue: parseFloat($("#txtOperatorCount").val() * parseFloat($("#lblOperatorUnitPrice").text()))
            }
            return subscription;
            //subscriptiondetails.push(Operatorsubscription);
        }

        return subscriptiondetails;
    }
    function GetCompanyDetails() {
        var CompanyDetails = {
            Name: $("#CompanyDetails_Name").val(),
            CName: $("#CompanyDetails_CName").val(),
            Title: $("#CompanyDetails_Title").val(),
            CompanyPhone: $("#CompanyDetails_CompanyPhone").val(),
            CompanyEmail: $("#CompanyDetails_CompanyEmail").val(),
            CompanyAddress1: $("#CompanyDetails_CompanyAddress1").val(),
            CompanyAddress2: $("#CompanyDetails_CompanyAddress2").val(),
            CompanyCity: $("#CompanyDetails_CompanyCity").val(),
            CompanyState: $("#CompanyDetails_CompanyState").data("kendoDropDownList").text(),
            CompanyZip: $("#CompanyDetails_CompanyZip").val(),
            BillingName: $("#CompanyDetails_BillingName").val(),
            BillingPhone: $("#CompanyDetails_BillingPhone").val(),
            BillingEmail: $("#CompanyDetails_BillingEmail").val(),
            BillingAddress1: $("#CompanyDetails_BillingAddress1").val(),
            BillingAddress2: $("#CompanyDetails_BillingAddress2").val(),
            BillingCity: $("#CompanyDetails_BillingCity").val(),
            BillingState: $("#CompanyDetails_BillingState").data("kendoDropDownList").text(),
            BillingZip: $("#CompanyDetails_BillingZip").val()

        }
        return CompanyDetails;
    }
    function GetPaymentDetails() {

        displayLoading("#wizard", true);
        var paymenttype = $("#PaymentType").val();
        if (paymenttype == "Credit Card" || paymenttype == "Debit Card") {
            var PaymentDetails = {
                PaymentType: $("#PaymentType").val(),
                CardName: $("#CardName").val(),
                CardNumber: $("#CardNumber").val(),
                Month: $("#biMonth").val(),
                Year: $("#biYear").val(),
                CardVerificationNumber: $("#CardVerificationNumber").val()
            }
            var PaymentDetailsJson = PaymentDetails;
            return PaymentDetailsJson;
        }
        else {
            var PaymentDetails = {
                PaymentType: $("#PaymentType").val(),
                CustomerName: $("#CustomerName").val(),
                BankName: $("#BankName").val(),
                AccountNumber: $("#AccountNumber").val(),
                TypeofAccount: $("#TypeofAccount").val(),
                RoutingNumber: $("#RoutingNumber").val()
            }
            var PaymentDetailsJson = PaymentDetails;
            return PaymentDetailsJson;
        }
    }

    $.ajax({
        type: "POST",
        data: JSON.stringify(registration),
        dataType: "json",
        url: "Registration/RegistrationDetails",
        contentType: "application/json; charset=utf-8",
        success: function (responce) {

            displayLoading("#wizard", false);
            //debugger;
         //   alert("success");
          ///  planalert("Thank you! Your Registration Completed Successully. Please check your email.(" + responce.email + ")", "RegisterConfirmation-(" + responce.type +")");

            if (responce.type == "Error") {
                planalert("(" + responce.email + ")", "RegisterConfirmation -" + responce.type + "");

            }
            else {
                var result = kendo.confirm("Thank you! Your Registration Completed Successully. Please check your email. " + responce.email + "")
                    .done(function () {



                       // location.reload();
                        window.location.href = '/Identity/Account/Login';


                    })
                    .fail(function () {
                        location.reload();
                        return false;
                    });

            }

            

           
        }
    });
    //debugger;
    console.log(JSON.stringify(registration))
}

function loaddatasummary() {
    $("#lblUnitTypes").text($("#lblUnitTypeForCount").text());
    $("#lblName").text($("#CompanyDetails_Name").val());
    $("#lblPhone").text($("#CompanyDetails_BillingPhone").val());
    $("#lblEmail").text($("#CompanyDetails_BillingEmail").val());
    $("#lblAddress").text($("#CompanyDetails_BillingAddress1").val());
    $("#lblCity").text($("#CompanyDetails_BillingCity").val());
    $("#lblZip").text($("#CompanyDetails_BillingZip").val());
    $("#lblTotal").text($("#lblTotalValue").text());

    var Type = $("#PaymentType").val();
    if (Type == "Credit Card" || Type == "Debit Card") {
        $("#orderachdetails").css("display", "none");
        $("#ordercarddetails").css("display", "block");
        $("#lblCardHolder").text($("#CardName").val());
        $("#lblCardNumber").text($("#CardNumber").val());
        $("#lblPaymentType").text($("#PaymentType").val());
        $("#lblCardMonth").text($("#biMonth").val());
        $("#lblCardYear").text($("#biYear").val());
    }
    else if (Type == "ACH Payment") {
        $("#orderachdetails").css("display", "block");
        $("#ordercarddetails").css("display", "none");
        $("#lblCustomerName").text($("#CustomerName").val());
        $("#lblaccountNumber").text($("#AccountNumber").val());
        $("#lblBankName").text($("#BankName").val());
        $("#lblAccountType").text($("#TypeofAccount").val());
        $("#lblRoutingNumber").text($("#RoutingNumber").val());
        $("#lblPT").text($("#PaymentType").val());

    }



    //date
    var today = new Date();
    var date = (today.getMonth() + 1) + '/' + today.getDate() + '/' + today.getFullYear();
    $("#lblStartDate").text(date);
    var myFutureDate = new Date(today);
    myFutureDate.setDate(myFutureDate.getDate() + 30);
    var edate = (myFutureDate.getMonth() + 1) + '/' + myFutureDate.getDate() + '/' + myFutureDate.getFullYear();
    $("#lblEndDate").text(edate);
}

function SetUnitPriceAndLabelCaptions() {
  // debugger;
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
        $("#lblDispatchTotalValue").text('$' +parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text()));
        $("#lblProviderTotalValue").text('$' +parseInt($("#txtProviderCount").val()) * parseFloat($("#lblProviderUnitPrice").text()));

        var dispatch_total = parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text());
        var provider_total = parseInt($("#txtProviderCount").val()) * parseFloat($("#lblProviderUnitPrice").text());
        $("#lblTotalValue").text('$' + (dispatch_total+provider_total));
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
        $("#lblDispatchTotalValue").text('$' +parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text()));
        //
        $("#lblOperatorTotalValue").text('$' +parseInt($("#txtOperatorCount").val()) * parseFloat($("#lblOperatorUnitPrice").text()));


        var dispatch_total = parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text());
        var operatore_total = parseInt($("#txtOperatorCount").val()) * parseFloat($("#lblOperatorUnitPrice").text());
        $("#lblTotalValue").text('$' + (dispatch_total + operatore_total));
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
        $("#lblDispatchTotalValue").text('$' +parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text()));

        var dispatch_total = parseInt($("#txtDispatchCount").val()) * parseFloat($("#lblDispatchUnitPrice").text());
       
        $("#lblTotalValue").text('$' + (dispatch_total));
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
        $("#lblProviderTotalValue").text('$' +parseInt($("#txtProviderCount").val()) * parseFloat($("#lblProviderUnitPrice").text()));
        var provider_total = parseInt($("#txtProviderCount").val()) * parseFloat($("#lblProviderUnitPrice").text());

        $("#lblTotalValue").text('$' + (provider_total));
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
        $("#lblOperatorTotalValue").text('$' +parseInt($("#txtOperatorCount").val()) * parseFloat($("#lblOperatorUnitPrice").text()));

        var operatore_total = parseInt($("#txtOperatorCount").val()) * parseFloat($("#lblOperatorUnitPrice").text());
        $("#lblTotalValue").text('$' + (operatore_total));
    }
    types = PackTypes;
    //CalculateTotalValue();
}

function LoadTerms() {

   // debugger;
    var window2 = $("#TermsandCondition").data("kendoWindow");
    window2.refresh({
        url: "/Registration/_Termsandcondition"
    });

    $("#TermsandCondition").closest(".k-window").css({
        top: 250,
        left: 650
        
    });


    window2.refresh().center().open();
}

 
