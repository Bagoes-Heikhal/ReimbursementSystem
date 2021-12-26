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

function getData(nik) {
    $.ajax({
        url: "https://localhost:44345/API/Employees/" + nik,
        success: function (result) {
            var data = result.result
            console.log(data.firstName)
            var text = ""
            text =
                `<tr>
                <td> Name </td>
                <td> : </td>
                <td> ${data.firstName} ${data.lastName}</td>
            </tr>
            <tr>
                <td> NIK </td>
                <td> : </td>
                <td>${nik}</td>
            </tr>
            <tr>
                <td> Gender </td>
                <td> : </td>
                <td>${data.gender}</td>
            </tr>`
            $(".data-employ").html(text);
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function ValidateEmployee() {
    window.addEventListener('load', function () {
        $.ajax({
            url: "https://localhost:44345/API/Employees/" + nik,
            success: function (result) {
                var data = result.result
                console.log(data.firstName)
                var text = ""
                text =
                    `<tr>
                    <td> Name </td>
                    <td> : </td>
                    <td> ${data.firstName} ${data.lastName}</td>
                </tr>
                <tr>
                    <td> NIK </td>
                    <td> : </td>
                    <td>${nik}</td>
                </tr>
                <tr>
                    <td> Gender </td>
                    <td> : </td>
                    <td>${data.gender}</td>
                </tr>`
                $(".data-employ").html(text);
            },
            error: function (error) {
                console.log(error)
            }
        })
    }, false);
}


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

   

