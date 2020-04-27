$(() => {
    function readURL(input) {
        if (input.files) {
            var reader = new FileReader();

            reader.onload = (e) => {
                $('#img').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $('input[type="file"]').change(() => {
        readURL(event.target);
    })
})