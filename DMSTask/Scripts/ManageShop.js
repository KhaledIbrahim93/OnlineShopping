var ItemArr = [];
var data = new FormData();
function AddToCart(id , price, tax, discount) {
    debugger;
    quntity = $("#qut").val();
    data.append("ItemId",id);
    data.append("Price",price);
    data.append("Tax", tax);
    data.append("Discount", discount);
    data.append("Quntity", quntity);

    $.ajax({
        type: "POST",
        url: '/Items/AddItemsToCart/',
        data:data,
        processData: false,
        contentType: false,
        success: function (Result) {
            debugger;
            alert("Added Sucsess");
            window.location.href = '/Items/Index';
        },
        error: function () {
            alert("Error .....");
        }

    });
}
