$(() => {
    function readURL(input) {
        if (input.files) {
            $("img").attr('src', "/images/image-placeholder.jpg");
            if (input.files.length > 1) {
                for (var i = 0; i < input.files.length; i++) {
                    (function (file) {
                        var reader = new FileReader();
                        var id = "img-" + i;
                        reader.onload = (e) => {
                            $('#' + id).attr('src', e.target.result);
                        }
                        reader.readAsDataURL(input.files[i]);
                    })(input.files[i]);
                }
            }
            else if (input.files[0]) {
                var reader = new FileReader();
                var id = "img-0";

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