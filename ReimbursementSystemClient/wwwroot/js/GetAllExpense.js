$(document).ready(function () {
   
    table = $("#tabelExpense").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseModified",
            "type": "GET",
            "datatype": "json",
            dataSrc: ""
        },
        "columns": [
            {
                "data": "employeeId"
            },
            {
                "data": "firstName"
            },
            {
                "data": "dateTime"
            },
            {
                "data": "total"
            },
            {
                "data": "status"
            },
          
           
        ]
    });
});

$.ajax({
    "url": "/Expenses/GetAll",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})