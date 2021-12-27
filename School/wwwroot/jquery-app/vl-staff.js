$(document).ready(function () {
    GetStateList();
    GetCityList();
    SetInputNumeric('MobileCP1'); 
    SetInputNumeric('MobileCP1'); 
    SetInputNumeric('MobileCP1'); 
    // for form validation
    $('form[id="dealerform"]').validate({
        rules: {
            Company: 'required',
            CP1: 'required',     
            MobileCP1: 'required',            
            EmailCP1: {
                required: true,
                email: true,
            },
            EmailCP2: {
                //required: true,
                email: true,
            },
            EmailCP3: {
                //required: true,
                email: true,
            },
        },
        messages: {           
            Company: 'Please enter company name !',
            CP1: 'Please enter contact person name !',    
            MobileCP1: 'Please enter mobile number !',       
            EmailCP1: 'Enter enter valid email !',  
            EmailCP2: 'Enter enter valid email !', 
            EmailCP3: 'Enter enter valid email !', 
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});

function uploadPicture(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#logoPicture')
                .attr('src', e.target.result)
                .width(150)
                .height(100);
        };
        reader.readAsDataURL(input.files[0]);
    }
}