$(document).ready(function () {     
    /*SetInputNumericIndian('FeesAmount');*/
    $(document).ready(function () {
        $("#FeesHead").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Share/FeesHeadAutoComplete",
                    type: "POST",
                    dataType: "json",
                    data: { Prefix: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item.FeesHeadName, value: item.FeesHeadName, id: item.FeesHeadID };
                        }))
                    },
                })
            },
            select: function (event, ui) {               
                $("#FeesHeadID").val(ui.item.id);     
                GetBillingCycle(ui.item.id);
            },
        });
    })    
})
function GetBillingCycle(id) {

    $.post("/Share/GetBillingCycle", { ID: id }, function (response) {
        $("#BillingCycle").val(response);
    });
}
// main picture
function uploadpassort(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#img-passport')
                .attr('src', e.target.result)
                .width(100)
                .height(150);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

// for add Trans Data
function PushRow() {    
    var model = { FeesHeadID: $('#FeesHeadID').val(), FeesHead: $('#FeesHead').val(), FeesAmount: $('#FeesAmount').val(), BillingCycle: $('#BillingCycle').val(), DueOn: $('#DueOn').val()};    
    $.post("/Admin/FeesStructure/InsertRow", model, function (data) {
        DisplayData(data);
    });
}
// for Remove Trans Data
function PopRow(Ser) {      
    $.post("/Admin/FeesStructure/DeleteRow", { iSer: Ser }, function (data) {
        DisplayData(data);
    });
}
// for Display Data-- > 
function DisplayData(data) {
    var Counter = 0;
    $("#dtTable tbody tr").remove();
    var items = '';
    $.each(data, function (i, item) {
        Counter = Counter + 1;
        var rows = "<tr>"
            + "<td>" + Counter + "</td>"
            + "<td>" + item.FeesHead + "</td>"
            + "<td>" + item.FeesAmount + "</td>"
            + "<td>" + item.BillingCycle + "</td>"
            + "<td>" + ToDDMMYYYY(item.DueOn) + "</td>"
            + "<td><button type='button' id=" + item.FeesStructureTransTempID + " onclick='PopRow(this.id)' class='btn btn-xs btn-outline-danger'><i class='fas fa-window-close'></i></button></td>"
            + "</tr>";
        $('#dtTable tbody').append(rows);
    });
    // Clear Item
    $('#FeesHead').val("");
}
// for Looad Data on Edit-- > 
function LoadRow(itemid) {
    $.ajax({
        type: 'POST',
        url: "/DealersManage/Dealers/FetchRow",
        dataType: 'json',
        data: { iID: itemid },
        success: function (data) {
            DisplayData(data);
        }
    });
}

function ToDDMMYYYY(stingdate) {
    alert(stingdate);
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(stingdate);
    var mydate = new Date(parseFloat(results[1]));
    year = mydate.getFullYear();
    month = mydate.getMonth() + 1;
    day = mydate.getDate();

    if (day < 10) {
        day = '0' + day;
    }
    if (month < 10) {
        month = '0' + month;
    }
    var outdate = day + '/' + month + '/' + year;
    return outdate;
}