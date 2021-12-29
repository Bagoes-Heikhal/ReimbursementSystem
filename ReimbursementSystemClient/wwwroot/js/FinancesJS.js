function column() {
    return `<th scope="col" class="text-light text-center">Name</th>
            <th scope="col" class="text-light text-center">Date Request</th>
            <th scope="col" class="text-light text-center">Total</th>
            <th scope="col" class="text-light text-center">Purpose</th>
            <th scope="col" class="text-light text-center status">Action</th>`
}

$.ajax({
    url: "/Expenses/GetExpenseFinanceReject",
    type: "GET",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
});


$(document).ready(function () {

    table = $("#tabelExpense").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseFinance",
            "type": "GET",
            "datatype": "json",
            dataSrc: ""
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        "columns": [
            {
                "data": "name"
            },
            {
                //ganti submited date
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["dateTime"]);
                }
            },
            {
                "data": "total"
            },
            {
                "data": "purpose"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['formId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Reject('${row['expenseId']}')" data-placement="top" title="Delete">
                            <i class="far fa-times-circle"></i>
                            </button>
                            <button type="button" class="btn btn-info" data-toggle="modal" 
                            onclick="Approve('${row['expenseId']}')" title="Edit" data-target="#UpdateModals">
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

function Reject(expenseid) {
    Swal.fire({
        title: 'Are you sure?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Reject it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Expenses/Get/" + expenseid,
                type: "Get",
                success: function (result2) {
                    var obj = new Object();
                    obj.expenseId = expenseid;
                    obj.approver = result2.approver;
                    obj.commentManager = result2.commentManager;
                    obj.commentFinace = result2.commentFinace;
                    obj.purpose = result2.purpose;
                    obj.description = result2.description;
                    obj.total = result2.total;
                    obj.employeeId = result2.employeeId;
                    obj.status = 8;
                    console.log(obj)
                    $.ajax({
                        url: "/Expenses/Put",
                        type: "Put",
                        'data': obj,
                        'dataType': 'json',
                        success: function (result2) {
                            table.ajax.reload();
                            console.log(result2);
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
        }
    })
}

function getData(id) {
    $.ajax({
        url: "/Expenses/Get/" + id,
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
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function Approve(expenseid) {
    $.ajax({
        url: "/Expenses/Get/" + expenseid,
        type: "Get",
        success: function (result) {
            var obj = new Object();
            obj.expenseId = expenseid;
            obj.approver = result.approver;
            obj.commentManager = result.commentManager;
            obj.commentFinace = result.commentFinace;
            obj.purpose = result.purpose;
            obj.description = result.description;
            obj.total = result.total;
            obj.employeeId = result.employeeId;
            obj.status = 6;
            console.log(obj)
            $.ajax({
                url: "/Expenses/Put",
                type: "Put",
                'data': obj,
                'dataType': 'json',
                success: function (result2) {
                    table.ajax.reload();
                    console.log(result2);
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
}

/*$('.button').click(function () { $("#tabelExpense").DataTable().clear().destroy(); })*/

function RejectTable() {
    $('.status').html("Action");

    if ($.fn.DataTable.isDataTable('#tabelExpense')) {
        $('#tabelExpense').DataTable().destroy();
    }
    $('#tabelExpense tbody').empty();

    $(".column-tab").html(column());

    $("#tabelExpense").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseFinanceReject",
            "type": "GET",
            "datatype": "json",
            dataSrc: ""
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        "columns": [
            {
                "data": "name"
            },
            {
                //ganti submited date
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["dateTime"]);
                }
            },
            {
                "data": "total"
            },
            {
                "data": "purpose"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['formId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>`;
                }
            }
        ]
    });
    
}

function RequestTable() {
    
    $('.status').html("Action");
    if ($.fn.DataTable.isDataTable('#tabelExpense')) {
        $('#tabelExpense').DataTable().destroy();
    }
    $('#tabelExpense tbody').empty();

    $(".column-tab").html(column());



    $("#tabelExpense").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseFinance",
            "type": "GET",
            "datatype": "json",
            dataSrc: ""
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        "columns": [
            {
                "data": "name"
            },
            {
                //ganti submited date
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["dateTime"]);
                }
            },
            {
                "data": "total"
            },
            {
                "data": "purpose"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['formId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Reject('${row['expenseId']}')" data-placement="top" title="Delete">
                            <i class="far fa-times-circle"></i>
                            </button>
                            <button type="button" class="btn btn-info" data-toggle="modal" 
                            onclick="Approve('${row['expenseId']}')" title="Edit" data-target="#UpdateModals">
                            <i class="far fa-check-circle"></i>
                            </button>`;
                }
            }


        ]
    });
}

function AllTable() {

    if ($.fn.DataTable.isDataTable('#tabelExpense')) {
        $('#tabelExpense').DataTable().destroy();
    }

    $('#tabelExpense tbody').empty();

    if ($('.action').length == 0) {
        $('#tabelExpense').html()
    }

    var text = column() + `<th scope="col" class="text-light text-center action">Action</th>`
    $(".column-tab").html(text);

    $('.status').html("Status");

    $("#tabelExpense").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Expenses/GetExpenseFinanceAll/",
            "type": "GET",
            "datatype": "json",
            dataSrc: ""
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        "columns": [
            {
                "data": "name"
            },
            {
                //ganti submited date
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["dateTime"]);
                }
            },
            {
                "data": "total"
            },
            {
                "data": "purpose"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return status(row["status"]);
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['formId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> `;
                }

            }

        ]
    });
}

function remove(str) {
        // Get target th with the name you want to remove.
        var target = $('table').find('th[data-name="' + str + '"]');
        // Find its index among other ths 
        var index = (target).index();
        // For each tr, remove all th and td that match the index.
        $('table tr').find('th:eq(' + index + '),td:eq(' + index + ')').remove();
}

function status(stat) {
    switch (stat) {
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
