$.ajax({
    url: "/Expenses/getall",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})

function InsertExpense() {
    var obj = new Object();
    obj.Status = 5;
    console.log(obj)

    $.ajax({
        url: "/Expenses/NewExpense",
        type: "Post",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            console.log(result)
            window.location.href = "/Reimbusments/Expense";
        },
        error: function (error) {
            console.log(error)
        }
    })
    return false;
}

$(document).ready(function () {
    table = $("#Expense-table").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpense",
            dataSrc: ""
        },
        "columns": [
            {
                "data": null,
            },
            {
                "data": "expenseId",
            },
            {
                "data": "total",
            },
            {
                "data": "description"
            },
            {
                "data": "status",
            },
        ],
        success: function (result) {
            console.log(result)
        },
        error: function (error) {
            console.log(error)
        }
    });
});
   

