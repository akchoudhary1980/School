$(document).ready(function () {    
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
            formsubmit();
        }
    });
});

