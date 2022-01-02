﻿function column() {
    return `<th scope="col" class="text-light text-center">Name</th>
            <th scope="col" class="text-light text-center">Date Request</th>
            <th scope="col" class="text-light text-center">Total</th>
            <th scope="col" class="text-light text-center">Purpose</th>
            <th scope="col" class="text-light text-center status">Action</th>`
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
            return "Approved By Manager <br> Waiting for Finance Approval";
        case 6:
            return "Approved By Finance";
        case 7:
            return "Rejected At Phase 1"; 
        case 8:
            return "Rejected By Finance";
        case 9:
            return "Approved By Manager <br> Waiting for Super Manager Approval";
        case 10:
            return "Approved By Super Manager <br> Waiting for Finance Approval";
        case 11:
            return "Approved By Super Manager <br> Waiting for Director Approval ";
        case 12:
            return "Approved By Director <br> Waiting for Finance Approval";
        default:
            return "Draft";
            break;
    }
}

function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
};

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

function getData(id) {
    $.ajax({
        url: "/Expenses/Get/" + id,
        data: "",
        success: function (result) {
            console.log(result)
            var text = ""
            text =
                `
                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">ExpenseId : <span id="Eid"> ${result.expenseId} </span>  </label>
                </div>

                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Status : <span id="stat"> ${status(result.status)} </span>  </label>
                </div>

                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Total : <span id="total"> ${result.total} </span>  </label>
                </div>
                <div class="form-group col-xl-6 col-sm-6">
                    <label for="inputState">Submited : <span id="date"> ${dateConversion(result.submitted)} </span>  </label>
                </div>`
            $("#info").html(text);
            $("#desc").html(result.description)

            tableformdetail(result.expenseId)
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