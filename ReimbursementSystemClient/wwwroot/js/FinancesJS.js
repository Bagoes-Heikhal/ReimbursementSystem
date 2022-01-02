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
                "data": "purpose"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['formId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-info"
                            onclick="EditExpense('${row['expenseId']}')" title="Open Form" >
                            <i class="fas fa-edit"></i>
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal"
                            onclick="getData2('${row['expenseId']}')" data-target="#exampleModal" data-placement="top" title="Reject">
                            <i class="far fa-times-circle"></i>
                            </button>
                            <button type="button" class="btn btn-info" data-toggle="modal" 
                            onclick="Approve('${row['expenseId']}')" title="Approve" data-target="#UpdateModals">
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

function getData2(id) {
    $('textarea#managercomment').val('')
    $.ajax({
        url: "/Expenses/Get/" + id,
        data: "",
        success: function (result) {
            var text = ""
            text =
                `
                    <div class="form-group col-xl-6 col-sm-6">
                        <label for="inputState">Id : <span id="expenseId"> ${result.expenseId} </span>  </label>
                    </div>

                    <div class="form-group col-xl-6 col-sm-6">
                        <label for="inputState">Total : <span id="total"> ${result.total} </span>  </label>
                    </div>
                `
            $(".reject-modal").html(text);
            console.log(result)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function Reject() {
    var expenseid = parseInt($('#expenseId').text())
    var finance = $('textarea#managercomment').val();
    Swal.fire({
        title: 'Are you sure?',
        type: "warning",
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
                    obj.commentFinace = finance;
                    obj.purpose = result2.purpose;
                    obj.description = result2.description;
                    obj.total = result2.total;
                    obj.employeeId = result2.employeeId;
                    obj.status = 8;
                    console.log(obj)
                    $.ajax({
                        url: "/Expenses/Approval/" + 3,
                        type: "Put",
                        'data': obj,
                        'dataType': 'json',
                        success: function (result2) {
                            table.ajax.reload();
                            $("#exampleModal").modal('hide');
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
            $.LoadingOverlay("show");
            setTimeout(function () {
                $.LoadingOverlay("hide");
            }, 3000);
        }
    })
   
}

function Approve(expenseid) {
    swal({
        title: "Do you want to approvee this ??",
        text: "You can't revert this!!",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Write something"
    }).then((result) => {
        console.log(result)
        if (result.value) {
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
                        url: "/Expenses/Approval/" + 4,
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
        $.LoadingOverlay("show");
        setTimeout(function () {
            $.LoadingOverlay("hide");
        }, 3000);
    })
}

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
                    if (row["description"] == null) {
                        return "No purpose"
                    }
                    return row["purpose"];
                }

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
                    if (row["description"] == null) {
                        return "No purpose"
                    }
                    return row["purpose"];
                }

            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['formId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-info"
                            onclick="EditExpense('${row['expenseId']}')" title="Open Form" >
                            <i class="fas fa-edit"></i>
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal"
                            onclick="getData2('${row['expenseId']}')" data-target="#exampleModal" data-placement="top" title="Reject">
                            <i class="far fa-times-circle"></i>
                            </button>
                            <button type="button" class="btn btn-info" data-toggle="modal" 
                            onclick="Approve('${row['expenseId']}')" title="Approve" data-target="#UpdateModals">
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
            "url": "/Expenses/GetExpensePosted/",
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
                    if (row["description"] == null) {
                        return "No purpose"
                    }
                    return row["purpose"];
                }

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



