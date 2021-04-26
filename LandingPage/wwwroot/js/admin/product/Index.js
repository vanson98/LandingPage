
$(document).ready(function () {
    $(".btn-delete-product").on("click", function () {
        var productId = parseInt($(this)[0].id);
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
                    url: "AdminProduct/Delete?productId=" + productId,
                    type: 'GET',
                    success: function (result) {
                        if (result.statusCode == 200) {
                            Swal.fire(
                                'Deleted!',
                                'This product has been deleted.',
                                'success'
                            )
                            $("#record-" + productId).remove();
                        } else {
                            Swal.fire(
                                'Error!',
                                'An error has occurred',
                                'error'
                            )
                        }
                    },
                    error: function (result) {
                        if (result.status == 401 || result.status == 403) {
                            Swal.fire(
                                'Lỗi!',
                                'Đã hết phiên đăng nhập, yêu cầu bạn đăng nhập lại',
                                'error'
                            )
                        } else {
                            Swal.fire(
                                'Lỗi!',
                                'Đã có lỗi sảy ra',
                                'error'
                            )
                        }
                    }
                });
            }
        })
    })
})