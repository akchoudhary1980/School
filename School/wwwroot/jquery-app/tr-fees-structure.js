$(document).ready(function () {     
    SetInputNumericIndian('FeesAmount');

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
    $.ajax({
        type: "POST",
        url: "/Share/GetBillingCycle",
        data: { "ID": id },
        dataType: "json",
        success: function (response) {
            alert(response);
            $("#BillingCycle").val(response);     
        },       
    });
            //
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
   /* var url = "/Admin/FeesStructure/InsertRow";*/
    var model = { FeesHeadID: 1, FeesHead: "Name dfd", FeesAmount: 600, BillingCycle: 'Monthly', DueOn: '22-01-2022', ClassID: 1};
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
            + "<td>" + item.DueOn + "</td>"
            + "<td><button type='button' id=" + item.FeesStructureTransTempID + " onclick='PopRow(this.id)' class='btn btn-xs btn-outline-danger'><i class='fas fa-window-close'></i></button></td>"
            + "</tr>";
        $('#dtTable tbody').append(rows);
    });
    // Clear Item
    $('#DealIn').val("");
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