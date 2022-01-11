$(document).ready(function () {
    GetStateList();
    GetCityList();
    SetInputMobile('#Mobile'); 
    SetInputMobile('#WhatsApp');    
    // for form validation
    $('form[id="vlform"]').validate({
        rules: {
            //DateOfBirth: 'required',
            StudentName: 'required',
            FatherName: 'required',     
            MotherName: 'required',
            Mobile: {
                required: true,
                minlength: 10,
            },
           
            Email: {
                //required: true,
                email: true,
            },
            WhatsApp: {               
                minlength: 10,
            },            
        },
        messages: {           
            //DateOfBirth: 'Please enter student date of birth !',
            StudentName: 'Please enter student name !',
            FatherName: 'Please enter father name !',    
            MotherName: 'Please enter mother name !',       
            Mobile: 'Enter enter valid 10 digit mobile !',  
            Email: 'Enter enter valid email !', 
            WhatsApp: 'Enter enter valid 10 digit whatsup !', 
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});

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