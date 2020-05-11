$(() => {
    $(".btnApproveAd").click(() => {
        var adId = $(event.target).attr("value");
        $.ajax({
            url: `/Admin/ApproveAdvertisement/${adId}`,
            type: 'PUT',
            success: () => {
                var adRow = $(`#ad-${adId}`);
                adRow.remove();
            }
        })
    })
})