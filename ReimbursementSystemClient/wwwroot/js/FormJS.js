
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

function Update() {
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
        url: "/Forms/Put",
        type: "Put",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            console.log(result)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function Insert() {
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
        success: function (result) {
            window.location.href = "/Reimbusments/Expense"
        },
        error: function (error) {
            console.log(error)
        }
    })
    return false;
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

//var $j = jQuery.noConflict();

//function picker() {
//    $j("#datepicker").datepicker({
//        autoclose: true,
//        todayHighlight: true
//    }).datepicker('update', new Date());
//    $("#datepicker1").datepicker({
//        autoclose: true,
//        todayHighlight: true
//    }).datepicker('update', new Date());
//    $("#datepicker2").datepicker({
//        autoclose: true,
//        todayHighlight: true
//    }).datepicker('update', new Date());
//};


