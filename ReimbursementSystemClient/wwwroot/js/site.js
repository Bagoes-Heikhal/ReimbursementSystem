function column() {
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