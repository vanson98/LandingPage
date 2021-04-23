$(document).ready(function () {
    // Xoas blog
    $(".delete-btn").on("click", DeleteBlog)
    function DeleteBlog() {
        var blogId = parseInt($(this)[0].id);
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
                    url: "AdminBlog/DeleteBlog?blogId=" + blogId,
                    type: 'GET',
                    success: function (result) {
                        if (result.statusCode == 200) {
                            Swal.fire(
                                'Deleted!',
                                'This blog has been deleted.',
                                'success'
                            )
                            $("#record-" + blogId).remove();
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
})