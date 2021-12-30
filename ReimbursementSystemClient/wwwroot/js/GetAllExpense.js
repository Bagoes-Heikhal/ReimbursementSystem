$(document).ready(function () {
   
    table = $("#tabelExpense").DataTable({
        "processing": true,
        "responsive": true,
        "ajax": {
            "url": "/Expenses/GetExpenseModified",
            "type": "GET",
            "datatype": "json",
            dataSrc: ""
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        "columns": [
            {
                "data": null,
                 "render": function (data, type, row) {
                     return row["employeeId"];
                }
            },
            {
                "data": null,
                 "render": function (data, type, row) {
                     return row["firstName"];
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["dateTime"]);
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row["total"] == null) {
                        return "Rp. " + 0
                    }
                    return "Rp." + row["total"];
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    switch (row["status"]) {
                        case 0:
                            return "Approved";
                        case 1:
                            return "Rejected";
                        case 2:
                            return "Canceled";
                        case 3:
                            return "Posted";
                        case 4:
                            return "Draft";
                        case 5:
                            return "Approved By Manager";
                        case 6:
                            return "Approved By Finance";
                        case 7:
                            return "Rejected By Manager";
                        case 8:
                            return "Rejected By Finance";
                        default:
                            return "Draft";
                            break;
                    }
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Delete('${row['expenseId']}')" data-placement="top" title="Delete">
                            <i class="far fa-times-circle"></i>
                            </button>
                            <button type="button" class="btn btn-info" data-toggle="modal" 
                            onclick="getDataUpdate('${row['expenseId']}')" title="Edit" data-target="#UpdateModals">
                            <i class="far fa-check-circle"></i>
                            </button>`;
                }
            }
          
           
        ]
    });
});

function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
}
function getData(id) {
    $.ajax({
        url: "/Expenses/GetExpenseModified/" + id,
        data: "",
        success: function (result) {
            var text = ""
            text =
                `<tr>
                <td> Total </td>
                <td> : </td>
                <td> ${result.total}</td>
                </tr>
                <tr>
                    <td> Description </td>
                    <td> : </td>
                    <td>${result.description}</td>
                </tr>`
            $(".data-employ").html(text);
            console.log(result)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

$.ajax({
    "url": "/Expenses/GetAll",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})