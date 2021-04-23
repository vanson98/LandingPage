$(document).ready(function () {
    $(".view-detail-contact-btn").on("click", GetDetailContact);
    $(".delete-contact-btn").on("click", DeleteContact);
    // Function search contact
    function GetDetailContact() {
        var contactId = $(this).attr("contactId");
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

    function DeleteContact() {
        var contactId = $(this).attr("contactId");
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: "/AdminContact/Delete?id=" + contactId,
                    type: 'GET',
                    success: function (result) {
                        if (result.status == 200) {
                            Swal.fire(
                                'Deleted!',
                                'This contact has been deleted.',
                                'success'
                            )
                            $("#record-" + contactId).remove();
                        } else {
                            Swal.fire(
                                'Error!',
                                'An error has occurred',
                                'error'
                            )
                        }
                    },
                    error: function (result) {
                        Swal.fire(
                            'Error!',
                            'An error has occurred',
                            'error'
                        )
                    }
                });
               
            }
        })
    }
});