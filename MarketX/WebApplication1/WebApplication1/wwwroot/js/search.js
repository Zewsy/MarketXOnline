$(() => {
    $("#btnChooseCategory").click(() => {
        $("#categoriesModal").modal("hide");
    })

    $('input[name="chosenCategoryRadio"]').change(() => {
        var CategoryId = event.target.value;
        var CategoryName = event.target.id;
        $("#properties").load('/Home/ChooseCategory', { categoryId: CategoryId });
        $("#chosenCategoryLabel").text(CategoryName);
        $("#chosenCategory").val(CategoryId);
    })

    $("#countySelect").change(() => {
        var CountyId = $("#countySelect").val();

        $("#citySelect").load('/Home/ChooseCounty', { countyId: CountyId });
    })

    $("#orderSelect").change(() => {
        document.forms["OrderForm"].submit();
    })

    $('input[name="chosenCategoryRadio"]:checked').parents('ul').addClass("show");
});