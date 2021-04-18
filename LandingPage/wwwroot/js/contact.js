$(document).ready(function () {
    $('#btn-contact-submit').on("click", function () {
        $("#btn-contact-submit").prop('disabled', true);
        var resultValidate = validateForm();
        if (resultValidate == null) {
            var contact = {};
            contact.firstName = $("#contact-first-name-2").val();
            contact.lastName = $("#contact-last-name-2").val();
            contact.email = $("#contact-email-2").val();
            contact.phoneNumber = $("#contact-phone-2").val();
            contact.message = $("#contact-message-2").val();
            try {
                $.ajax({
                    type: 'POST',
                    url: '/EximaniContact/ReceiveContact',
                    contentType: "application/json",
                    data: JSON.stringify(contact),
                    async: false,
                    success: function (res) {
                        if (res.status == 200) {
                            $("#btn-contact-submit").prop('disabled', false);
                            alert("Success");
                            window.location = "/EximaniContact"
                        } else {
                            alert("Error! An error occurred. Please try again later");
                        }
                    },
                    error: function (err) {
                        console.log(err);
                        alert("Error! An error occurred. Please try again later");
                    }
                })
            } catch (e) {
                alert("Error! An error occurred. Please try again later");
                console.log(e);
            }
        } else {
            $("#btn-contact-submit").prop('disabled', false);
            alert(resultValidate);
        }
       
    });

    function validateForm() {
        var firstName = $("#contact-first-name-2").val();
        var lastName = $("#contact-last-name-2").val();
        var email = $("#contact-email-2").val();
        var phoneNumber = $("#contact-phone-2").val();
        //if (firstName == null || firstName.trim() == "") {
        //    return "First Name is required.";
        //} else if (lastName == null || lastName.trim() == "") {
        //    return "Last Name is required.";
        //} else
        if (email == null || email.trim() == "") {
            return "Email is required."
        }else if (!validateEmail(email)) {
            return "Email is not valid.";
        } {
            return null;
        }
    }

    function validateEmail(email) {
        const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    function validatePhoneNumber(inputtxt) {
        var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
        if (inputtxt.value.match(phoneno)) {
            return true;
        }
        else {
            return false;
        }
    }
});
