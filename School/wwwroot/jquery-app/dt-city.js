$(document).ready(function () {
    $("#mytable").DataTable({
        
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admin/City/GetIndex",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],            
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "cityID", "name": "cityID", "autoWidth": true }, 
            { "data": "cityName", "name": "cityName", "autoWidth": true },
            { "data": "stateName", "name": "stateName", "autoWidth": true },   
            
            {
                "render": function (data, type, full) {
                    return "<a href='City/Edit?id=" + full.cityID + "' class='btn btn-xs btn-outline-success'><i class='fas fa-edit'></i></a>";
                }
            },
            {
                "render": function (data, type, full) {
                    return "<a href='City/Delete?id=" + full.cityID + "' class='btn btn-xs btn-outline-danger'><i class='fas fa-window-close'></i></a>";
                }
            }, 
           
        ]
    });
});  

