
function InsertForm() {
    $.ajax({
        url: "/Forms/NewForm/",
        success: function (result) {
            window.location.href = result;
        },
        error: function (error) {
            console.log(error)
        }
    });
}

$(document).ready(function () {
    $.ajax({
        url: "/Expenses/ExpenseCall",
        success: function (result) {
            console.log(result)
            $(".expense-title span").html(result);

            table = $("#Formtable").DataTable({
                responsive: true,
                "ajax": {
                    "url": "/forms/getform/" + result,
                    type: "Get",
                    dataSrc: ""
                },
                "columnDefs": [
                    { "className": "dt-center", "targets": "_all" }
                ],
                "columns": [
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return dateConversion(row["receipt_Date"])
                        }
                    },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            switch (row["category"]) {
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
                    },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            if (row["type"] == null) {
                                return "~Empty~"
                            }
                            return row["type"];
                        }
                    },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            if (row["payee"] == null) {
                                return "~Empty~"
                            }
                            return row["payee"];
                        }
                    },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            if (row["description"] == null) {
                                return "No Description"
                            }
                            return row["description"];
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
                            return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                            onclick="getData('${row['formId']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                            <i class="fas fa-info-circle"></i> 
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Delete('${row['formId']}')" data-placement="top" title="Delete">
                            <i class="fas fa-trash-alt"></i> 
                            </button>
                            <button type="button" class="btn btn-info" data-toggle="modal" 
                            onclick="EditForm('${row['formId']}')" title="Edit" data-target="#UpdateModals">
                            <i class="fas fa-edit"></i>
                            </button>`;
                        }
                    }
                ]
            });

            $.ajax({
                url: "/Expenses/Get/" + result,
                type: "Get",
                data: "",
                success: function (result) {
                    $("#Description").html(result.description)
                    $("#Purpose").attr("value", result.purpose)
                },
                error: function (error) {
                    console.log(error)
                }
            })

            $.ajax({
                url: "/forms/TotalExpenseForm/" + result,
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
    obj.total = $("#Total").val();
    obj.status = 3;
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
    obj.total = $("#Total").val();
    obj.status = 4;
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
                url: "/Forms/Delete/" + id,
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
        url: "/Forms/Get/" + id,
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

function EditForm(formid) {
    console.log(formid)
    $.ajax({
        url: "/Forms/EditForm/" + formid,
        success: function (result) {
            console.log(result)
            window.location.href = "/Reimbusments/Form";

        },
        error: function (error) {
            console.log(error)
        }
    })
}

////$.ajax({
////    url: "/Expenses/GetID/" ,
////    success: function (result) {
////        $(".expense-title span").html(result.expenseID)
////    },
////    error: function (error) {
////        console.log(error)
////    }
////})

