$(document).ready(function () {
    $("#mytable").DataTable({        
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admission/Admission/GetIndex",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],            
            "visible": false,
            "searchable": false
        }],
        "columns": [

            { "data": "admissionID", "name": "admissionID", "autoWidth": true },

            {
                "data": "picture", "name": "picture",

                "render": function (data, type, full, meta) {
                    return "<img src='../uploadfiles/" + full.picture + "' height='50'/>";
                },
                "orderable": false,
                "searchable": false
            },
            { "data": "studentName", "name": "studentName", "autoWidth": true },
            { "data": "className", "name": "className", "autoWidth": true },  
            { "data": "fatherName", "name": "fatherName", "autoWidth": true },            
            { "data": "mobile", "name": "mobile", "autoWidth": true }, 
            
            {
                "render": function (data, type, full) {
                    return "<a href='Admission/View?id=" + full.admissionID + "' class='btn btn-xs btn-outline-info'><i class='fas fa-search-plus'></i></a>";
                }
            },

            {
                "render": function (data, type, full) {
                    return "<a href='Admission/Edit?id=" + full.admissionID + "' class='btn btn-xs btn-outline-success'><i class='fas fa-edit'></i></a>";
                }
            },
            {
                "render": function (data, type, full) {
                    return "<a href='Admission/Delete?id=" + full.admissionID + "' class='btn btn-xs btn-outline-danger'><i class='fas fa-window-close'></i></a>";
                }
            }, 
           
        ]
    });
});  

