var chosenCategoryId;
var chosenPropertyId;

function registerChosenPropertyRadioHandler() {
    $('input[name="chosenPropertyRadio"]').change(() => {
        chosenPropertyId = event.target.value;
        $("#edit-property").load('/Admin/EditProperty', { propertyId: chosenPropertyId }, () => {
            registerEditPropertyHandlers()
        });
    });
}

function registerHandlers() {
    $('input[name="chosenCategoryRadio"]').change(() => {
        chosenCategoryId = event.target.value;
        $("#edit-category").load('/Admin/EditCategory', { categoryId: chosenCategoryId }, () => {
            registerChosenPropertyRadioHandler();
            registerEditCategoryHandlers();
        });
    });
    $('button[name=deleteCategory').click(() => {
        var parentId = $(event.target).parent("ul").attr("id");
        if (!parentId)
            parentId = $(event.target).parents("li")[1]?.id;
        var data = $(event.target).attr("value");
        var CategoryId = data.split("-")[1];
        $.ajax({
            type: "DELETE",
            url: '/Admin/DeleteCategory',
            data: {
                id: CategoryId
            },
            success: (partialView) => {
                $("#edit-list").html(partialView);
                var parent = $("#" + parentId);
                if (parent) {
                    parent.parents("ul").addClass("show");
                    parent.addClass("show");
                    parent.children("ul").addClass("show");
                }
                registerHandlers();
            }
        })
    })
    $('button[name="addCategory"]').click(() => {
        var data = $(event.target).attr("value");
        var CategoryName;
        var CategoryId;
        if (data) {
            CategoryName = data.split("-")[0];
            CategoryId = data.split("-")[1];
        }
        $("#chosenCategory").text("Kiválasztott szülőkategória: " + (CategoryName ?? "Nincs"));
        $("#btnChooseCategory").click(() => {
            var newCategoryName = $("#newCategoryName").val();
            $("#addCategoryModal").modal("hide");
            $.ajax({
                type: "POST",
                url: '/Admin/AddCategory',
                data: {
                    newCategoryName: newCategoryName,
                    parentId: CategoryId
                },
                success: (partialView) => {
                    $("#edit-list").html(partialView);
                    $('#' + newCategoryName).parents('ul').addClass("show");
                    registerHandlers();
                }
            });
        })
    })
}

function registerEditCategoryHandlers() {
    $("#btn-save-category").click(() => {
        var newName = $("#categoryNewNameInput").val();
        $.ajax({
            type: "PUT",
            url: '/Admin/EditCategory',
            data: {
                categoryId: chosenCategoryId,
                newName: newName
            },
            success: (partialView) => {
                $("#edit-list").html(partialView);
                $('#' + newName).parents('ul').addClass("show");
                registerHandlers();
                registerChosenPropertyRadioHandler();
                registerEditCategoryHandlers();
            }
        })
    });
    $('button[name="delete-category-property"]').click(() => {
        $.ajax({
            type: "DELETE",
            url: '/Admin/DeleteCategoryProperty',
            data: {
                categoryId: chosenCategoryId,
                propertyId: $(event.target).attr("value")
            },
            success: (partialView) => {
                $("#edit-category").html(partialView);
                registerChosenPropertyRadioHandler();
                registerEditCategoryHandlers();
            }
        })
    });
    $("#btn-save-new-property").click(() => {
        var newPropertyName = $("#newPropertyInput").val();
        if (newPropertyName) {
            $.ajax({
                type: "POST",
                url: '/Admin/AddCategoryProperty',
                data: {
                    categoryId: chosenCategoryId,
                    propertyName: newPropertyName
                },
                success: (partialView) => {
                    $("#edit-category").html(partialView);
                    registerChosenPropertyRadioHandler();
                    registerEditCategoryHandlers();
                }
            })
        }
    })
}

function registerEditPropertyHandlers() {
    checkPropValueTypeForList();
    $("#add-property-value").click(() => {
        event.preventDefault();
        var newPropValueName = $("#new-property-value-name").val();
        if (newPropValueName) {
            $.ajax({
                type: "POST",
                url: '/Admin/AddPropertyValue',
                data: {
                    propertyId: chosenPropertyId,
                    valueName: newPropValueName
                },
                success: (partialView) => {
                    $("#property-values").append(partialView);
                }
            })
        }
    });
    $("#PropertyValueType").change(() => {
        checkPropValueTypeForList();
    });
    $("#prop-form").submit(() => {
        event.preventDefault();
        var formData = $("#prop-form").serializeArray();
        $.ajax({
            type: "PUT",
            url: '/Admin/UpdateProperty',
            data: formData,
            success: () => {
                $("#successful-save").modal("show");
            }
        })
    })
}

function checkPropValueTypeForList() {
    if ($("#PropertyValueType").val() == "SelectableFromList") {
        $("#prop-values-list-div").show();
    }
    else {
        $("#prop-values-list-div").hide();
    }
}

registerHandlers();
registerEditCategoryHandlers();
registerEditPropertyHandlers();
