$(document).ready(function () {
    $("#mytable").DataTable({
        
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admin/Country/GetIndex",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],            
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "countryID", "name": "countryID", "autoWidth": true }, 
            { "data": "countryName", "name": "countryName", "autoWidth": true },
            { "data": "region", "name": "region", "autoWidth": true },  

            {
                "render": function (data, type, full)
                {
                    return "<a href='Country/Edit?id=" + full.countryID + "' class='btn btn-xs btn-outline-success'><i class='fas fa-edit'></i></a>";                        
                }
            },
            {
                "render": function (data, type, full) {
                    return "<a href='Country/Delete?id=" + full.countryID + "' class='btn btn-xs btn-outline-danger'><i class='fas fa-window-close'></i></a>";        
                }
            },          
        ]
    });
});  

