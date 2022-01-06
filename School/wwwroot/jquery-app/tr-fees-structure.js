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
    var row = { FeesHeadID: '1', FeesHead: "Name", FeesAmount: 600, BillingCycle: 'Monthly', DueOn:'22-01-2022' };
    $.ajax({
        url: '/Admin/FeesStructure/InsertRow',
        type: 'POST',
        data: JSON.stringify(row),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false,
        success: function (msg) {
            alert(msg);
        }
    });

    //var itemid = $('#ItemID').val();

    //var arr = { City: 'Moscow', Age: 25 };


    //if (itemid = "") {
    //    alert('Please select Item from list');
    //}
    //else {
    //    $.ajax({
    //        type: 'POST',
    //        url: '/DealersManage/Dealers/InsertRow',
    //        dataType: 'json',
    //        data: { 'ID': $('#ItemID').val(), "__RequestVerificationToken": $('input[name=__RequestVerificationToken]').val() },
    //        success: function (data) {
    //            DisplayData(data);
    //        }
    //    });
    //}
}
// for Remove Trans Data
function PopRow(serno) {
    $.ajax({
        type: 'POST',
        url: "/DealersManage/Dealers/DeleteRow",
        dataType: 'json',
        data: { iSer: serno, "__RequestVerificationToken": $('input[name=__RequestVerificationToken]').val() },
        success: function (data) {
            DisplayData(data);
        }
    });
}
// for Display Data-- > 
function DisplayData(data) {
    var counter = 0;
    $("#dtTable tbody tr").remove();
    var items = '';
    $.each(data, function (i, item) {
        counter = counter + 1;
        var rows = "<tr>"
            + "<td>" + counter + "</td>"
            + "<td>" + item.ItemName + "</td>"
            + "<td><button type='button' id=" + item.SerNo + " onclick='PopRow(this.id)' class='btn btn-danger btn-mini btn-outline-primary'><i class='icofont icofont-ui-close'></i></button></td>"
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