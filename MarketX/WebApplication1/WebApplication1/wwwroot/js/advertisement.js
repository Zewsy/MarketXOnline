$(() => {
    $("#phone-number").hide();

    $("#btn-phone").click(() => {
        $("#btn-phone").hide();
        $("#phone-number").show();

        var id = $("#phone-number").data('id');
        $.ajax({
            type: "GET",
            url: '/Advertisement/UserPhoneNumber/' + id,
            success: (number) => {
                $("#phone-number").append(number);
            }
        })
    });

    $()
});
