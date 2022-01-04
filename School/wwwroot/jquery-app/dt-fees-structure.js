$(document).ready(function () {
    $("#mytable").DataTable({        
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admin/Staff/GetIndex",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],            
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "staffID", "name": "staffID", "autoWidth": true }, 
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "desginationName", "name": "desginationName", "autoWidth": true },    
            { "data": "city", "name": "city", "autoWidth": true },  
            { "data": "mobile", "name": "mobile", "autoWidth": true }, 

            {
                "render": function (data, type, full) {
                    return "<a href='Staff/View?id=" + full.staffID + "' class='btn btn-xs btn-outline-info'><i class='fas fa-search-plus'></i></a>";
                }
            },

            {
                "render": function (data, type, full) {
                    return "<a href='Staff/Edit?id=" + full.staffID + "' class='btn btn-xs btn-outline-success'><i class='fas fa-edit'></i></a>";
                }
            },
            {
                "render": function (data, type, full) {
                    return "<a href='Staff/Delete?id=" + full.staffID + "' class='btn btn-xs btn-outline-danger'><i class='fas fa-window-close'></i></a>";
                }
            }, 
           
        ]
    });
});  

