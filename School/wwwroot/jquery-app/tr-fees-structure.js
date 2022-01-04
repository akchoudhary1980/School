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
