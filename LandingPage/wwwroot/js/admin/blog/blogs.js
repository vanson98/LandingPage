$(document).ready(function () {
    // ==============  Config  ===============
    // Config jquery tab 
    $("#tabs").tabs();
    // Config datatable
    $('#blog-table').DataTable();
    // Config tiny CME
    tinymce.init({ selector: '#content-textarea' });
    var tinyMCEContent = tinymce.get("content-textarea");
    // Config Chosen
    $(".category").chosen();
    //===============  Binding event  ==============
    $("#btn-create-blog").on("click",CreateNewBlog)
    // =============   Function  =============
    // Tạo mới blog
    function CreateNewBlog() {
        var blogTitle = $('#title-input').val();
        var blogCategoryId = $('#category-select').val().trim() != "" ? parseInt($('#category-select').val()) : 0;
        var blogShortDescription = $('#short-description-input').val();
        var blogContent = tinyMCEContent.getContent();
        var isPublished = $('#publish-checkbox').is(":checked");
        var metaTitle = $('#meta-title-input').val();
        var metaDescription = $('#meta-description-input').val();
        var metaKeyWord = $('#meta-key-word-input').val();
        var newBlog = {
            title: blogTitle,
            shortDescription: blogShortDescription,
            urlImage: "test",
            content: blogContent,
            published: isPublished,
            blogCategoryId: blogCategoryId,
            metaKeyWord: metaKeyWord,
            metaDescription: metaDescription,
            metaTitle: metaTitle
        }
        $.ajax({
            url: "/AdminBlog/SaveBlog",
            contentType: 'application/json',
            type: 'POST',
            data: JSON.stringify(newBlog),
            success: function (result) {
                debugger
            },
            error: function (result) {

            }
        });
    }
})