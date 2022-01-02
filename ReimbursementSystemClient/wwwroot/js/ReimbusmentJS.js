$.ajax({
    url: "/Expenses/GetExpense",
    type: "Get",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})

function InsertExpense() {
    var obj = new Object();
    obj.Status = 4;
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
    $("#Expense-table").DataTable({
        "processing": true,
        "responsive": true,
        "ajax": {
            "url": "/Expenses/GetExpense",
            dataSrc: ""
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
        "columns": [
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["submitted"]);
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "#" + row["expenseId"];
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
                    if (row["purpose"] == null) {
                        return "No Purpose"
                    }
                    return row["purpose"];
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return status(row["status"])
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    var draft = `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                             <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Delete('${row['expenseId']}')" data-placement="top" title="Delete">
                            <i class="fas fa-trash-alt"></i>
                            </button>
                            <button type="button" class="btn btn-info"
                            onclick="EditExpense('${row['expenseId']}')" title="Edit" >
                            <i class="fas fa-edit"></i>
                            </button>`
                    var nondraft = `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['expenseId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i>
                            </button> `
                    if (row["status"] != 4) {
                        return nondraft
                    } else {
                        return draft
                    }
                }
            }
        ],
        success: function (result) {
            console.log(result)
        },
        error: function (error) {
            console.log(error)
        }
    });
});
   
function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
}

function Delete(id) {
    console.log(id)
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Expenses/Delete/" + id,
                type: "Delete",
                success: function (result) {
                    console.log(result)
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    table.ajax.reload()
                },
                error: function (error) {
                    alert("Delete Fail");
                }
            });
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
                `
                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">ExpenseId : <span id="expenseId"> ${result.expenseId} </span>  </label>
                </div>

                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Status : <span id="expenseId"> ${status(result.status)} </span>  </label>
                </div>

                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Total : <span id="total"> ${result.total} </span>  </label>
                </div>
                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Submited : <span id="total"> ${dateConversion(result.submitted)} </span>  </label>
                </div>`
            $(".infoo").html(text);
            $("#desc").html(result.description)

            tableformdetail(result.expenseId)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function EditExpense(expenseid) {
    console.log(expenseid)
    $.ajax({
        url: "/Expenses/EditExpense/" + expenseid,
        success: function (result) {
            console.log(result)
            window.location.href = "/Reimbusments/Expense";
            
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function tableformdetail(expenseid) {
    $.ajax({
        url: "/forms/getform/" + expenseid,
        data: "",
        success: function (result2) {
            console.log(result2)
            var text = ""
            for (var i = 0; i < result2.length; i++) {
                text +=
                    `<tr>
                    <td>${dateConversion(result2[0].receipt_Date)}</td>
                    <td>${cata(result2[0].category)}</td>
                    <td>${result2[0].total}</td>
                    <td><a href="${result2[0].attachments}" >attachments</a></td>
                    </tr>`
            }
            $("#datail").html(text);
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function cata(cat) {
    switch (cat) {
        case 0:
            return "Transportation";
        case 1:
            return "Parking";
        case 2:
            return "Medical";
        case 3:
            return "Lodging";
        default:
            return "~Empty~";
            break;
    }
}

