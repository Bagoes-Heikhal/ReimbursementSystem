////$.ajax({
////    url: "/Expenses/GetID/" ,
////    success: function (result) {
////        $(".expense-title span").html(result.expenseID)
////    },
////    error: function (error) {
////        console.log(error)
////    }
////})

function InsertForm() {
    window.location.href = "/Reimbusments/Form"
}

$(document).ready(function () {

    $.ajax({
        url: "/Expenses/GetID",
        success: function (result) {
            $(".expense-title span").html(result.expenseID);
            table = $("#Formtable").DataTable({
                responsive: true,
                "ajax": {
                    "url": "/forms/getform/" + result.expenseID,
                    type: "Get",
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
                    {
                        "data": "payee"
                    },
                    {
                        "data": "payee",
                    },
                    {
                        "data": "total",
                    }
                ]
            });

            $.ajax({
                url: "/forms/TotalExpenseForm/" + result.expenseID,
                type: "Get",
                success: function (result) {
                    $("#Total").val(result.total)
                },
                error: function (error) {
                    console.log(error)
                }
            })
        },
        error: function (error) {
            console.log(error)
        }
    })


});

function Submit() {
    var obj = new Object();
    obj.expenseId = parseInt($(".expense-title span").text());
    obj.purpose = $("#Purpose").val();
    obj.description = $("#Description").val();
    obj.status = 3;

    //console.log(obj)
    $.ajax({
        url: "/Expenses/Submit",
        type: "Put",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            Swal.fire(
                'Good job!',
                'Your data has been Submitted!',
                'success',    
            ).then((result2) => {
                if (result2) {
                    //need to close expense session first
                    window.location.href = "/Reimbusments/Reimbusment"
                }
            })
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Fail!'
            })
        }
    })
}

function SaveExit() {

    var obj = new Object();
    obj.expenseId = parseInt($(".expense-title span").text());
    obj.purpose = $("#Purpose").val();
    obj.description = $("#Description").val();
    obj.status = 4;
    /*     console.log(obj)*/
    $.ajax({
        url: "/Expenses/Submit",
        type: "Put",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            Swal.fire(
                'Good job!',
                'Your data has been saved!',
                'success',
            ).then((result2) => {
                if (result2) {
                    //need to close expense session first
                    window.location.href = "/Reimbusments/Reimbusment"
                }
            })
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Fail!'
            })
        }
    })
}