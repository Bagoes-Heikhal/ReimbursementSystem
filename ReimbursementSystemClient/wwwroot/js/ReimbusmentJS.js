//function getDataUpdate(nik) {
//    $.ajax({
//        url: "/Employees/get/" + nik,
//        success: function (result) {
//            console.log(result)
//            var data = result
//            $("#updatenik").attr("value", data.nik)
//            $("#updatefirstName").attr("value", data.firstName)
//            $("#updatelastName").attr("value", data.lastName)
//            $("#updateemail").attr("value", data.email)
//            $("#updatephone").attr("value", data.phone)
//            $("#updatesalary").attr("value", data.salary)
//            $("#updatedateBirth").attr("value", data.birthDate)
//            $("#updategender").attr("value", data.gender)
//        },
//        error: function (error) {
//            console.log(error)
//        }
//    })
//}


function InsertExpense() {
    var obj = new Object();
    obj.Status = 2;
    console.log(obj)

    $.ajax({
        url: "/Expenses/Post",
        type: "Post",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            console.log(result)
            window.location.href = "/Reimbusment/Expense";
        },
        error: function (error) {
            console.log(error)
        }
    })
    return false;
}
}

   

