$.ajax({
    "url": "/Forms/getall",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})

$(document).ready(function () {
    table = $("#Formtable").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Forms/getall",
            dataSrc: ""
        },
        "columns": [
            {
                "data": "receipt_Date",
            },
            {
                "data": "category",
            },
            {
                "data": "receipt_Date",
            },
            { "data": "payee" },
            {
                "data": "payee",
            },
            {
                "data": "total",
            }
            //{ "data": "phone" },
            //{
            //    "data": null,
            //    "render": function (data, type, row) {
            //        return `<button type="button" class="btn btn-primary" data-toggle="modal" 
            //        onclick="getData('${row['nik']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
            //        <i class="fas fa-info-circle"></i> 
            //        </button>
            //        <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Delete('${row['nik']}')" data-placement="top" title="Delete">
            //        <i class="fas fa-trash-alt"></i> 
            //        </button>
            //        <button type="button" class="btn btn-info" data-toggle="modal" 
            //        onclick="getDataUpdate('${row['nik']}')" title="Edit" data-target="#UpdateModals">
            //        <i class="fas fa-edit"></i>
            //        </button>`;
            //    }
            //}
        ],
    });
});