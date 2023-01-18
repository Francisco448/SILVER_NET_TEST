var Id = $(".Id");
var Name = $(".Name");
var BuyCost = $(".BuyCost");
var SalePrice = $(".SalePrice");

///Validations---------------------------------------------------------------------------------------------------------------------
function validateInputs() {
    isValid = false;
    if (Id.val() != "" && Name.val() != "" && BuyCost.val() != "" && SalePrice != "") {
        isValid = true;
    } else {
        isValid = false;
    }
    return isValid;
}

///DataTable-----------------------------------------------------------------------------------------------------------------------
$(document).ready(function () {
    $('#table').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/Profile/DataTable",
            "type": "POST",
            "datatype": "json"
        },
        "pageLength": 10,
        "filter": true,
        "responsivePriority": 1,
        "data": null,
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "Name", "name": "Name", "autoWidth": true },
            { "data": "BuyCost", "name": "Buy Cost", "autoWidth": true },
            { "data": "SalePrice", "name": "Sale Price", "autoWidth": true }
        ]
    });
});

///Add Product-----------------------------------------------------------------------------------------------------------------------
$(".addButton").on('click', function () {
    $.ajax({
        url: '/Profile/addProduct',
        method: 'POST',
        data: {
            product: {
                Name: Name.val(),
                BuyCost: BuyCost.val(),
                SalePrice: SalePrice.val()
            }
        },
        success: function (result) {
            if (result == 'True') {
                $('#table').DataTable().ajax.reload();
                Id.val("");
                Name.val("");
                BuyCost.val("");
                SalePrice.val("");
            }
        }
    })
})

///Delete Product---------------------------------------------------------------------------------------------------------------------
$(".deleteButton").on('click', function () {
    $.ajax({
        url: '/Profile/deleteProduct',
        method: 'GET',
        data: {
            Id: Id.val()
        },
        success: function (result) {
            if (result == 'True') {
                $('#table').DataTable().ajax.reload();
                Id.val("");
                Name.val("");
                BuyCost.val("");
                SalePrice.val("");
            }
        }
    })
})

///Update Product-----------------------------------------------------------------------------------------------------------------------
$(".editButton").on('click', function () {
    if (validateInputs()) {
        $.ajax({
            url: '/Profile/updateProduct',
            method: 'POST',
            data: {
                products: {
                    Id: Id.val(),
                    Name: Name.val(),
                    BuyCost: BuyCost.val(),
                    SalePrice: SalePrice.val()
                }
            },
            success: function (result) {
                if (result == 'True') {
                    $('#table').DataTable().ajax.reload();
                    Id.val("");
                    Name.val("");
                    BuyCost.val("");
                    SalePrice.val("");
                    $(".inputValidate").removeClass('show');
                }
            }
        })
    } else {
        $(".inputValidate").addClass('show');
    }
})

///LogOut--------------------------------------------------------------------------------------------------------------------------------
$(".logOut").on('click', function () {
    $.ajax({
        url: '/Profile/LogOut',
        success: function (result) {
            if (result == 'True') {
                location.href = window.location.protocol + "//" + window.location.host + "/account/Index";
            }
        }
    })
})