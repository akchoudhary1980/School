$(document).ready(function () {
    $("#mytable").DataTable({        
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admin/State/GetIndex",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],            
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "stateID", "name": "stateID", "autoWidth": true }, 
            { "data": "stateName", "name": "stateName", "autoWidth": true },
            { "data": "stateType", "name": "stateType", "autoWidth": true },    
            { "data": "countryName", "name": "countryName", "autoWidth": true },   

            {
                "render": function (data, type, full) {
                    return "<a href='State/Edit?id=" + full.stateID + "' class='btn btn-xs btn-outline-success'><i class='fas fa-edit'></i></a>";
                }
            },
            {
                "render": function (data, type, full) {
                    return "<a href='State/Delete?id=" + full.stateID + "' class='btn btn-xs btn-outline-danger'><i class='fas fa-window-close'></i></a>";
                }
            }, 
           
        ]
    });
});  

