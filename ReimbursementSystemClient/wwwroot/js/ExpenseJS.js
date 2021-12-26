////$.ajax({
////    "url": "/Forms/getall",
////    success: function (result) {
////        console.log(result)
////    },
////    error: function (error) {
////        console.log(error)
////    }
////})




$.ajax({
    url: "/Expenses/GetID",
    success: function (result) {
        $(".expense-title span").html(result.expenseID);
    },
    error: function (error) {
        console.log(error)
    }
})


function InsertForm() {
    var obj = new Object();
    obj.expenseID = $(".expense-title span").text(); 
    $.ajax({
        url: "/Forms/Post",
        type: "Post",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            window.location.href = "/Reimbusments/Form";
        },
        error: function (error) {
            console.log(error)
        }
    })
    return false;
}

//var a = $(".expense-title span").text();
//console.log("/Forms/GetForm/" + a);
//$.ajax({
//    "ajax": {
//        "url": "/Forms/GetForm/" + $(".expense-title span").text(),
//    },
//    success: function (result) {
//        //console.log(result)
//    },
//    error: function (error) {
//        //console.log(error)
//    }
//})


$(document).ready(function () {
    var expenseID = $(".expense-title span").text();
    table = $("#Formtable").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Forms/GetForm/" + expenseID,
            dataSrc: ""
        },
        "columns": [
            {
                "data": "receipt_Date",
            },
            {
                "data": "category",
            },
            {
                "data": "receipt_Date",
            },
            { "data": "payee" },
            {
                "data": "payee",
            },
            {
                "data": "total",
            }
            //{ "data": "phone" },
            //{
            //    "data": null,
            //    "render": function (data, type, row) {
            //        return `<button type="button" class="btn btn-primary" data-toggle="modal" 
            //        onclick="getData('${row['nik']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
            //        <i class="fas fa-info-circle"></i> 
            //        </button>
            //        <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Delete('${row['nik']}')" data-placement="top" title="Delete">
            //        <i class="fas fa-trash-alt"></i> 
            //        </button>
            //        <button type="button" class="btn btn-info" data-toggle="modal" 
            //        onclick="getDataUpdate('${row['nik']}')" title="Edit" data-target="#UpdateModals">
            //        <i class="fas fa-edit"></i>
            //        </button>`;
            //    }
            //}
        ],
    });
});