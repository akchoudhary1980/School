$(document).ready(function () {
    $("#mytable").DataTable({        
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admin/FeesStructure/GetIndex",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],            
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "feesStructureID", "name": "feesStructureID", "autoWidth": true },

            {
                "data": "pictures", "name": "pictures",

                "render": function (data, type, full, meta) {
                    return "<img src='../uploadfiles/" + full.pictures + "' height='50'/>";
                },
                "orderable": false,
                "searchable": false
            },

            { "data": "className", "name": "className", "autoWidth": true },
            { "data": "totalFees", "name": "totalFees", "autoWidth": true },
           
            {
                "render": function (data, type, full) {
                    return "<a href='FeesStructure/View?id=" + full.feesStructureID + "' class='btn btn-xs btn-outline-info'><i class='fas fa-search-plus'></i></a>";
                }
            },

            {
                "render": function (data, type, full) {
                    return "<a href='FeesStructure/Edit?id=" + full.feesStructureID + "' class='btn btn-xs btn-outline-success'><i class='fas fa-edit'></i></a>";
                }
            },
            {
                "render": function (data, type, full) {
                    return "<a href='FeesStructure/Delete?id=" + full.feesStructureID + "' class='btn btn-xs btn-outline-danger'><i class='fas fa-window-close'></i></a>";
                }
            }, 
           
        ]
    });
});  

