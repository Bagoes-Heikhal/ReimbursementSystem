//$.ajax({
//    url: "/Form/InsertForm",
//    type: "Post",
//    'data': obj,
//    'dataType': 'json',
//    success: function (result) {
//        console.log(result)
//    },
//    error: function (error) {
//        console.log(error)
//    }
//})

function SaveExit() {

    //var GetImages = $('[name="file"]');
    //console.log(GetImages[0].files[0])
    var data = {
        Receipt_Date : $("#Receipt_Date").val(),
        Start_Date: $("#Start_Date").val(),
        End_Date: $("#End_Date").val(),
        Category: $("#Category").val(),
        Payee: $("#Payee").val(),
        Description: $("#Description").val(),
        Total: $("#Total").val(),
        //Attachments : GetImages[0].files[0]
    };

    console.log(data)

    $.ajax({
        url: "/Forms/InsertForm",
        type: "Post",
        'data': data,
        'dataType': 'json',
        //processData: false,
        success: function (result) {
            window.location.href = "/Reimbusments/Expense"
        },
        error: function (error) {
            console.log(error)
        }
    })
    return false;
}

function Update() {
    $.ajax({
        url: "/Forms/FormCall",
        success: function (result) {
            var obj = new Object();
            obj.fromId = result;
            obj.Receipt_Date = $("#Receipt_Date").val();
            obj.Start_Date = $("#Start_Date").val();
            obj.End_Date = $("#End_Date").val();
            obj.Category = $("#Category").val();
            obj.Payee = $("#Payee").val();
            obj.Description = $("#Description").val();
            obj.Total = $("#Total").val();
            obj.Attachments = $("#Attachments").val();
            console.log(obj)
            $.ajax({
                url: "/Forms/PutEditFrom",
                type: "Put",
                'data': obj,
                'dataType': 'json',
                success: function (result) {
                    window.location.href = "/Reimbusments/Expense"
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

function AddAnother() {
    var obj = new Object();
    obj.Receipt_Date = $("#Receipt_Date").val();
    obj.Start_Date = $("#Start_Date").val();
    obj.End_Date = $("#End_Date").val();
    obj.Category = $("#Category").val();
    obj.Payee = $("#Payee").val();
    obj.Description = $("#Description").val();
    obj.Total = $("#Total").val();
    obj.Attachments = $("#Attachments").val();
    console.log(obj)

    $.ajax({
        url: "/Forms/InsertForm",
        type: "Post",
        'data': obj,
        'dataType': 'json',
        processData: false,
        contentType: false,
        success: function (result) {
            window.location.href = "/Reimbusments/Form"
        },
        error: function (error) {
            console.log(error)
        }
    })
    return false;
}

function Cancel() {
    window.location.href = "/Reimbusments/Expense"
}

$(document).ready(function () {
    $.ajax({
        url: "/Forms/FormCall",
        success: function (result) {
            console.log(result)
            if (result != null) {
                $(".Save-Exit").attr("onclick", "return Update()")
                $(".Save-Exit").html("Update")
            } 
           
            $.ajax({
                url: "/Forms/Get/" + result,
                type: "Get",
                data: "",
                success: function (result) {
                    $("#Receipt_Date").attr("value", dateInputConversion(result.receipt_Date))
                    $("#Start_Date").attr("value", dateInputConversion(result.start_Date))
                    $("#End_Date").attr("value", dateInputConversion(result.end_Date))
                    Category(result.category)
                    $("#Payee").attr("value", result.payee)
                    $("#Description").html(result.description)
                    $("#Total").attr("value", result.total)
                    $("#Attachments").attr("value", result.attachments)
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

    var readURL = function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('.profile-image').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }

    //$(".file-upload").on('change click', function (e) {
    //    e.stopPropagation();
    //    readURL(this);
    //});

    //$(".upload-button").on('click', function () {
    //    $(".file-upload").click();
    //});
});

function Category(selected) {
   
    $("#Category option").each(function (i) {
        if (i-1 == selected) {
            $("#Category").val(i - 1).attr('selected', 'selected');
        }
    });
}

function dateInputConversion(dates) {
    var date = new Date(dates)
    var newDate = date.getFullYear() + '-' + ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '-' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate()))
    console.log(newDate)
    return newDate
}


