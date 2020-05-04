$(() => {
    $(".btnDeleteAd").click(() => {
        var adId = $("#adId").val();
        $.ajax({
            url: `/Account/DeleteAdvertisement/${adId}`,
            type: 'DELETE',
            success: () => {
                var adRow = $(`#ad-${adId}`);
                adRow.remove();
            }
        })
    })
})