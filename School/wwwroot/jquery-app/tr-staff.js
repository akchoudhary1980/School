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


$(document).ready(function () {
    // Address Same
    $("#sameaddress").change(function () {
        if (this.checked) {
            //Do stuff
            var address = $("#CurrentAddress").val();
            $("#PermanetAddress").val(address);
            $("#PermanetAddress").attr('disabled', true); 
        }
        else {
            $("#PermanetAddress").val("");
            $("#PermanetAddress").attr('disabled', false); 
        }
    });

    // Address mobile
    $("#samemobile").change(function () {
        if (this.checked) {
            //Do stuff
            var mobile = $("#Mobile").val();
            $("#WhatsApp").val(mobile);
            $("#WhatsApp").attr('disabled', true);
        }
        else {
            $("#WhatsApp").val("");
            $("#WhatsApp").attr('disabled', false);
        }
    });

     
})