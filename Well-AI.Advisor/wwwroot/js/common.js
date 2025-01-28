function changeValueAccountType1() {
    $("#Input_AccountType").val($('#ddlAccountType1').val());
    console.log($("#Input_AccountType").val());
}

function changeValueSubscription(val, price) {
    $("#Input_Subscription").val(val);
    console.log($("#Input_Subscription").val());

    var totalcost = price;
    var noOfdRigs = $('#txtTotalRigs').val();

    totalcost = parseFloat(price * noOfdRigs);
    $('#spnTotalCost').text(totalcost.toFixed(2));
    $('#hdnSelctedPackPrice').val(price.toFixed(2));
    
    $('span.selectedpack').removeClass('selectedpack');
    $('#mySpan' + val).addClass('selectedpack');
}

function CalculateTotalCost() {
    var price = parseFloat($('#hdnSelctedPackPrice').val());
    var noOfdRigs = $('#txtTotalRigs').val();

    var totalcost = parseFloat(price * noOfdRigs);
    $('#spnTotalCost').text(totalcost.toFixed(2));
}