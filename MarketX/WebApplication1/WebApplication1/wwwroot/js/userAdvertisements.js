﻿$(() => {
    $(".btnDeleteAd").click(() => {
        var adId = $(event.target).attr("value");
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