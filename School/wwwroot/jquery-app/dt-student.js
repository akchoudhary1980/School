$(document).ready(function () {
    $("#mytable").DataTable({        
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admission/Student/GetIndex",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],            
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "studentID", "name": "studentID", "autoWidth": true },
            { "data": "studentName", "name": "studentName", "autoWidth": true },
            { "data": "fatherName", "name": "fatherName", "autoWidth": true },
            { "data": "city", "name": "city", "autoWidth": true },  
            { "data": "mobile", "name": "mobile", "autoWidth": true }, 
            { "data": "whatsApp", "name": "whatsApp", "autoWidth": true },
            {
                "render": function (data, type, full) {
                    return "<a href='Student/View?id=" + full.studentID + "' class='btn btn-xs btn-outline-info'><i class='fas fa-search-plus'></i></a>";
                }
            },

            {
                "render": function (data, type, full) {
                    return "<a href='Student/Edit?id=" + full.studentID + "' class='btn btn-xs btn-outline-success'><i class='fas fa-edit'></i></a>";
                }
            },
            {
                "render": function (data, type, full) {
                    return "<a href='Student/Delete?id=" + full.studentID + "' class='btn btn-xs btn-outline-danger'><i class='fas fa-window-close'></i></a>";
                }
            }, 
           
        ]
    });
});  

