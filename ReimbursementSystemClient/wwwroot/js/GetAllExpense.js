$(document).ready(function () {
   
    table = $("#tabelExpense").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Expense/GetAll/",
            "type": "GET",
            "datatype": "json",
            dataSrc: ""
        },
        "columns": [
            {
                "data": "EmployeeId"
            },
            {
                "data": "Status"
            },
            {
                "data": "Approver"
            },
            { "data": "Description" },
            {
                "data": "Comment"
            },
            {
                "data": "Total"
            }
           
        ]
    });
});