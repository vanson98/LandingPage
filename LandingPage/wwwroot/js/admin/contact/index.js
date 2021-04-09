$(document).ready(function () {
    $(".view-detail-contact-btn").on("click", GetDetailContact);
    // Function search contact
    function GetDetailContact() {
        var contactId = $(this).attr("contactId");
        debugger
        $.ajax({
            url: "/AdminContact/Detail?contactId=" + contactId,
            type: 'GET',
            success: function (result) {
                $("#ctm-name").text(result.fullName);
                $("#ctm-email").text(result.email);
                $("#ctm-phone").text(result.phoneNumber);
                $("#ctm-message").text(result.message);
                $("#ctm-createdDate").text(result.createdDate);
            },
            error: function (result) {
                Swal.fire('Đã có lỗi xảy ra')
            }
        });
    }
});