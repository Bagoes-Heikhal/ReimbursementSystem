
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


function Insert() {
    var obj = new Object();
    obj.Receipt_Date = $("#Receipt_Date").val();
    obj.Start_Date = $("#Start_Date").val();
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
            console.log(result)
        },
        error: function (error) {
            console.log(error)
        }
    })
    return false;
}

