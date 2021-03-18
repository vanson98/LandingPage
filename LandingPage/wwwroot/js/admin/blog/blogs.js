$(document).ready(function () {
    // ==============  Config  ===============
    // Config jquery tab 
    $("#tabs").tabs();
    // Config datatable
    $('#blog-table').DataTable();
    // Config tiny CME
    tinymce.init({
        selector: "#content-textarea",
        height: 350,
        width: "100%",
        plugins: [
            "advlist autolink lists link image charmap print preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars code fullscreen",
            "insertdatetime media nonbreaking save table contextmenu directionality",
            "emoticons template paste textcolor colorpicker textpattern imagetools codesample toc",
            "autoresize"],
        toolbar: "fullscreen",
        toolbar1: "undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | superscript subscript |bullist numlist outdent indent | link image | fullscreen",
        toolbar2: "print preview media | forecolor backcolor emoticons | codesample | formula",
        image_advtab: true,
        relative_urls: false,
        setup: function (editor) {
            editor.ui.registry.addContextToolbar('imgformula', {
                predicate: function (node) {
                    return node.nodeName.toLowerCase() === 'img' && $(node).prop("class") === "fm-editor-equation";
                },
                items: 'formula',
                position: 'node',
                scope: 'node'
            });
        },
        convert_urls: false,
        custom_elements: "ins",
        extended_valid_elements: "ins[*],iframe[*]",
        file_picker_callback: function (callback, value, meta) {
            var roxyFileman = '/plugins/Roxy_Fileman/index.html';
            if (roxyFileman.indexOf("?") < 0) {
                roxyFileman += "?type=" + meta.filetype;
            }
            else {
                roxyFileman += "&type=" + meta.filetype;
            }
            //roxyFileman += '&input=' + fieldName + '&value=' + value;
            roxyFileman += '&value=' + value;

            if (tinyMCE.activeEditor.settings.language) {
                roxyFileman += '&langCode=' + tinyMCE.activeEditor.settings.language;
            }

            tinyMCE.activeEditor.windowManager.openUrl({
                title: 'Roxy Fileman',
                url: roxyFileman,
                width: 850,
                height: 650,
                onMessage: function (dialogApi, details) {
                    callback(details.content);
                    dialogApi.close();
                }
            });

            return false;
        },
        style_formats_merge: true,
        paste_data_images: true,
        style_formats: [
            { title: "High light", inline: "span", classes: "news-high-light-text" },
            { title: "Table", classes: "news-table", selector: "table" },
            { title: "Link button", selector: "a", classes: "news-link-button" }
        ],
        content_css: ["https://fonts.googleapis.com/css?family=Roboto", "/css/blog.css"]
    });
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