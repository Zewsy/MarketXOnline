$(() => {
    function readURL(input) {
        if (input.files) {
            $("img").attr('src', "/images/image-placeholder.jpg");
            if (input.files[0]) {
                var reader = new FileReader();
                var id = "img-profile";

                reader.onload = (e) => {
                    $('#' + id).attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
    }

    $('input[type="file"]').change(() => {
        readURL(event.target);
    })
})