// main picture
function uploadicon(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgicon')
                .attr('src', e.target.result)
                .width(30)
                .height(30);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
// main function 
$(document).ready(function () {     
    SetDoubleIndian('FeesAmount');
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
    $.post("/Admin/FeesStructure/GetBillingCycle", { ID: id }, function (response) {
        $("#BillingCycle").val(response);
    });
}
// for Add Trans Data
function PushRow() {    
        // if button text == add
    var Cap = $('#Add').text();
    if (Cap == "Add") {       
            var istrue = IsDuplicateRow($('#FeesHeadID').val());      
            if(istrue == "True") {
                alert('Duplicate Records');
            }
            else {
                var model = { FeesHeadID: $('#FeesHeadID').val(), FeesHead: $('#FeesHead').val(), FeesAmount: DoubleFromIndianCulture($('#FeesAmount').val()), BillingCycle: $('#BillingCycle').val(), DueOn: $('#DueOn').val() };
                $.post("/Admin/FeesStructure/InsertRow", model, function (data) {
                    DisplayData(data);
                    SetTotal();
                });
            }
        }
        else {
            var oldid = $('#OldFeesHeadID').val();
            var currentid = $('#FeesHeadID').val();
                if (currentid != oldid) {
                    var istrue = IsDuplicateRow($('#FeesHeadID').val());
                    if (istrue == "True") {
                        alert('Duplicate Records');
                    }
                    else {
                        var model = { FeesStructureTransTempID: $('#FeesStructureTransTempID').val(), FeesHeadID: $('#FeesHeadID').val(), FeesHead: $('#FeesHead').val(), FeesAmount: DoubleFromIndianCulture($('#FeesAmount').val()), BillingCycle: $('#BillingCycle').val(), DueOn: $('#DueOn').val() };
                        $.post("/Admin/FeesStructure/UpdateRow", model, function (data) {
                            DisplayData(data);
                            SetTotal();
                            $('#Add').text("Add");
                        });
                    } 
                }
                else {
                    var model = { FeesStructureTransTempID: $('#FeesStructureTransTempID').val(), FeesHeadID: $('#FeesHeadID').val(), FeesHead: $('#FeesHead').val(), FeesAmount: DoubleFromIndianCulture($('#FeesAmount').val()), BillingCycle: $('#BillingCycle').val(), DueOn: $('#DueOn').val() };
                    $.post("/Admin/FeesStructure/UpdateRow", model, function (data) {
                        DisplayData(data);
                        SetTotal();
                        $('#Add').text("Add");
                    });
                }                      
        }   
}
// for Remove Trans Data
function PopRow(feesHeadID) {      
    $.post("/Admin/FeesStructure/DeleteRow", { FeesHeadID: feesHeadID }, function (data) {
        DisplayData(data);
        SetTotal();
    });
}
// for Edit Trans Data
function FetchRow(feesHeadID) {
    $('#OldFeesHeadID').val(feesHeadID);      
    $('#Add').text("Update");

    $.post("/Admin/FeesStructure/GetRow", { FeesHeadID: feesHeadID }, function (row) {       
        $('#FeesHeadID').val(row.FeesHeadID);        
        $('#FeesHead').val(row.FeesHead);
        $('#FeesAmount').val(row.FeesAmount);
        $('#BillingCycle').val(row.BillingCycle);
        $('#DueOn').val(row.DueOn);
        $('#FeesStructureTransTempID').val(row.FeesStructureTransTempID);
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
            + "<td>" + item.BillingCycle + "</td>"
            + "<td>" + ConvertToDDMMYYYY(item.DueOn) + "</td>"
            + "<td>" + ConvertToIndian(item.FeesAmount) + "</td>"
            + "<td><button type='button' id=" + item.FeesHeadID + " onclick='FetchRow(this.id)' class='btn btn-xs btn-outline-success'><i class='fas fa-edit'></i></button></td>"
            + "<td><button type='button' id=" + item.FeesHeadID + " onclick='PopRow(this.id)' class='btn btn-xs btn-outline-danger'><i class='fas fa-window-close'></i></button></td>"
            + "</tr>";
        $('#dtTable tbody').append(rows);
    });

    var row2 ="<tr>"
        +"<td align='right' colspan='4'><b>Total Fees</b></td>"
        + "<td><label id='TotalFees' for='TotalFees'></label></td>"
        + "<td></td><td></td>"
        + " </tr>";
    $('#dtTable tbody').append(row2);

    // Clear Item
    $('#FeesHeadID').val("");
    $('#FeesHead').val("");
    $('#FeesAmount').val("");
    $('#BillingCycle').val("");
    $('#DueOn').val("");
    //SetInputNumericIndian('FeesAmount');
}
// for Is Duplicate
function IsDuplicateRow(feesHeadID) {
    var result = "false";    
    $.ajax({
        type: 'POST',
        url: "/Admin/FeesStructure/IsDuplicate",
        dataType: 'json',
        data: { ID: feesHeadID },
        async: false,
        success: function (response) {
            result = response;           
        }
    });
    return result;
}
function SetTotal() {
    $.post("/Admin/FeesStructure/GetTotal", function (response) {
        $('#TotalFees').text(ConvertToIndian(response));
    });
}
// for Looad Data on Edit-- > 
function LoadRow(token) {
    $.post("/Admin/FeesStructure/FetchRows", { Token: token }, function (data) {
        DisplayData(data);
        SetTotal();
    });
}