$(document).ready(function () {    
    // Address Same
    $("#sameaddress").change(function () {
        if (this.checked) {
            //Do stuff
            var address = $("#CurrentAddress").val();
            $("#PermanetAddress").val(address);
            $("#PermanetAddress").attr('readonly', true); 
        }
        else {
            $("#PermanetAddress").val("");
            $("#PermanetAddress").attr('readonly', false); 
        }
    });

    // Address mobile
    $("#samemobile").change(function () {
        if (this.checked) {
            //Do stuff
            var mobile = $("#Mobile").val();
            $("#WhatsApp").val(mobile);
            $("#WhatsApp").attr('readonly', true);
        }
        else {
            $("#WhatsApp").val("");
            $("#WhatsApp").attr('readonly', false);
        }
    });     
})


