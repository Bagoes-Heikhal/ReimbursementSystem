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
    table = $("#Expense-table").DataTable({
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
                    return dateConversion(row["dateTime"]);
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
                    <i class="fas fa-trash-alt"></i> 
                    </button>
                    <button type="button" class="btn btn-info" data-toggle="modal" 
                    onclick="EditExpense('${row['expenseId']}')" title="Edit" data-target="#UpdateModals">
                    <i class="fas fa-edit"></i>
                    </button>`;
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

function getDataUpdate(id) {
    $.ajax({
        url: "/Expenses/Get/" + id,
        type: "Get",
        success: function (result) {
            console.log(result)
            /*var data = result*/
            //$("#updatenik").attr("value", data.nik)
            //$("#updatefirstName").attr("value", data.firstName)
            //$("#updatelastName").attr("value", data.lastName)
            //$("#updateemail").attr("value", data.email)
            //$("#updatephone").attr("value", data.phone)
            //$("#updatesalary").attr("value", data.salary)
            //$("#updatedateBirth").attr("value", data.birthDate)
            //$("#updategender").attr("value", data.gender)
        },
        error: function (error) {
            console.log(error)
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