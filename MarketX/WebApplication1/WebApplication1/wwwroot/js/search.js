$(() => {
    $("#btnChooseCategory").click(() => {
        $("#categoriesModal").modal("hide");
        var radios = document.getElementsByName('chosenCategoryRadio');

        for (var i = 0; i < radios.length; i++) {
            if (radios[i].checked) {
                var CategoryName = radios[i].id;
                $("#chosenCategory").text(CategoryName);
                $("#properties").load('/Home/ChooseCategory', { CategoryName: CategoryName });
                break;
            }
        }
    })

    $("#countySelect").change(() => {
        var CountyName = $("#countySelect").val();

        $("#citySelect").load('/Home/ChooseCounty', { CountyName: CountyName });
    })
});