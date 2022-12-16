$(document).ready(function () {
    $('#btn-contact-submit').on("click", function () {
        debugger
        $("#btn-contact-submit").prop('disabled', true);
        //var resultValidate = validateForm();
        var resultValidate = null;
        if (resultValidate == null) {
            var contact = {};
            contact.fullName = $("#ct_full_name").val();
            contact.ticketType = parseInt($("#ct_ticket_type").val());
            contact.email = $("#ct_email").val();
            contact.phoneNumber = $("#ct_sdt").val();
            contact.ticketAmount = parseInt($("#ct_ticket_number").val());
            contact.address = $("#ct_ticket_address").val();
            contact.question = $("#ct_ticket_question").val();
            try {
                $.ajax({
                    type: 'POST',
                    url: '/Home/ReceiveContact',
                    contentType: "application/json",
                    data: JSON.stringify(contact),
                    async: false,
                    success: function (res) {
                        if (res.status == 200) {
                            $("#btn-contact-submit").prop('disabled', false);
                            alert("Bạn đã đăng ký thành công!");
                            $('#contact-form').trigger("reset");
                        } else {
                            alert("Đã có lỗi xảy ra!");
                        }
                    },
                    error: function (err) {
                        console.log(err);
                        alert("Đã có lỗi xảy ra!");
                    }
                })
            } catch (e) {
                alert("Đã có lỗi xảy ra!");
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
        var verifyRecaptchaStatus = $("#hfCaptcha").val();
        //if (firstName == null || firstName.trim() == "") {
        //    return "First Name is required.";
        //} else if (lastName == null || lastName.trim() == "") {
        //    return "Last Name is required.";
        //} else
        if (email == null || email.trim() == "") {
            return "Email is required."
        } else if (!validateEmail(email)) {
            return "Email is not valid.";
        } else if (phoneNumber.trim() != null && phoneNumber.trim() != "") {
            if (!validatePhoneNumber(phoneNumber)) {
                return "PhoneNumber is not valid.";
            }
        } if (verifyRecaptchaStatus == null || verifyRecaptchaStatus == "") {
            $("#rfvCaptcha").show();
            return "Captcha validation is required."
        } else {
            return null;
        }
    }

    function validateEmail(email) {
        const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    function validatePhoneNumber(phoneNumber) {
        var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
        if (phoneNumber.match(phoneno)) {
            return true;
        }
        else {
            return false;
        }
    }
});
