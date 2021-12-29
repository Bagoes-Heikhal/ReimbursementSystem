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

function SaveExit() {
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
});

function Category(selected) {
   
    $("#Category option").each(function (i) {
        if (i-1 == selected) {
            //console.log(this.selected)
            //console.log(this.value)
            //console.log(this.text)
            //$("#Category option[value= 3 ]").attr('selected', 'selected');

            // Or just...
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


