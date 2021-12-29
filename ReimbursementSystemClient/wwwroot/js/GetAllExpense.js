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
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['formId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Delete('${row['formId']}')" data-placement="top" title="Delete">
                            <i class="far fa-times-circle"></i>
                            </button>
                            <button type="button" class="btn btn-info" data-toggle="modal" 
                            onclick="getDataUpdate('${row['formId']}')" title="Edit" data-target="#UpdateModals">
                            <i class="far fa-check-circle"></i>
                            </button>`;
                }
            }
          
           
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