$(() => {
    $("#customer-phone").hide();
    $("#seller-phone").hide();

    $("#btn-phone-seller").click(() => {
        $("#btn-phone-seller").hide();
        $("#seller-phone").show();
    });

    $("#btn-phone-customer").click(() => {
        $("#btn-phone-customer").hide();
        $("#customer-phone").show();
    });
});
