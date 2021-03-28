$(document).ready(function () {
    // Xoas blog
    $(".delete-btn").on("click", DeleteBlog)
    function DeleteBlog() {
        var blogId = parseInt($(this)[0].id);
        $.ajax({
            url: "AdminBlog/DeleteBlog?blogId=" + blogId,
            contentType: 'application/json',
            type: 'DELETE',
            success: function (result) {
                if (result.statusCode == 500) {
                    $.notify('Xóa thất bại', { globalPosition: 'top center', className: 'error' });
                } else {
                    $("#record-" + blogId).remove();
                    $.notify('Xóa thành công', { globalPosition: 'top center', className: 'success' });
                }
            },
            error: function (result) {
                Swal.fire('Đã có lỗi xảy ra')
            }
        });
    }
})