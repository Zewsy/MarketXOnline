$(() => {
    $("#btnChooseCategory").click(() => {
        $("#categoriesModal").modal("hide");
    })

    $('input[name="chosenCategoryRadio"]').change(() => {
        var CategoryName = event.target.id;
        $("#properties").load('/Home/ChooseCategory', { CategoryName: CategoryName });
        $("#chosenCategory").val(CategoryName);
    })

    $("#countySelect").change(() => {
        var CountyName = $("#countySelect").val();

        $("#citySelect").load('/Home/ChooseCounty', { CountyName: CountyName });
    })

    $("#orderSelect").change(() => {
        var order = $("#orderSelect").val();
        document.forms["OrderForm"].submit();
    })

    $('input[name="chosenCategoryRadio"]:checked').parents('ul').addClass("show");
});