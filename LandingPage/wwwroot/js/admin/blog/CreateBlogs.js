$(document).ready(function () {
    // ==============  Config  ===============
    // Property 
    var cropImageDialog;
    var blogAvatar = $('#img-cropper');
    // Config jquery tab 
    $("#tabs").tabs();
    
    // Config tiny CME
    $('#content-textarea').tinymce({
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
        convert_urls: false,
        custom_elements: "ins",
        extended_valid_elements: "ins[*],iframe[*]",
        file_browser_callback: function (field_name, url, type, win) {
            var roxyFileman = '/plugins/fileman/index.html';
            if (roxyFileman.indexOf("?") < 0) {
                roxyFileman += "?type=" + type;
            }
            else {
                roxyFileman += "&type=" + type;
            }
            roxyFileman += '&input=' + field_name + '&value=' + win.document.getElementById(field_name).value;
            if (tinyMCE.activeEditor.settings.language) {
                roxyFileman += '&langCode=' + tinyMCE.activeEditor.settings.language;
            }
            tinyMCE.activeEditor.windowManager.open({
                file: roxyFileman,
                title: 'Roxy Fileman',
                width: 850,
                height: 650,
                resizable: "yes",
                plugins: "media",
                inline: "yes",
                close_previous: "no"
            }, { window: win, input: field_name });
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
    })
    // Config chosen select
    $(".category").chosen();
    // Config crop image dialog 
    cropImageDialog = $("#crop-img-dialog").dialog({
        autoOpen: false,
        resizable: false,
        height: 500,
        width: 800,
        modal: true,
        buttons: [
            {
                text: "Save",
                click: function () { alert("save"); },
                class: "sampleClass1",
                style: "color:Red"
            },
            {
                text: "Cancel",
                click: function () {
                    cropImageDialog.dialog("close");
                },
                class: "sampleClass2"
            }
        ],
        close: function () {
          
        }
    });
    // Config cropper
    blogAvatar.cropper({
        viewMode: 0,
        crop: function (event) {
            console.log(event.detail.x);
            console.log(event.detail.y);
            console.log(event.detail.width);
            console.log(event.detail.height);
            console.log(event.detail.rotate);
            console.log(event.detail.scaleX);
            console.log(event.detail.scaleY);
        }
    });
    var avatarCropper = blogAvatar.data('cropper');
    //===============  Binding event  ==============
    $("#btn-create-blog").on("click", CreateNewBlog)
    $("#open-crop-img-btn").on("click", OpenCropImageDialog)
    $("#update-image-file").on("change", HandleFiles);
    $("#cut-img-btn").on("click",GetCropImage)
    // =============   Function  =============
    // Mở dialog cắt ảnh đại diện của dialog
    function OpenCropImageDialog() {
        cropImageDialog.dialog("open");
    }
    // Tạo mới blog
    function CreateNewBlog() {
        var blogTitle = $('#title-input').val();
        var blogCategoryId = $('#category-select').val().trim() != "" ? parseInt($('#category-select').val()) : 0;
        var blogShortDescription = $('#short-description-input').val();
        var blogContent = tinymce.get("content-textarea").getContent();
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
                window.location = "localhost:44318/adminblog"
            },
            error: function (result) {
                Swal.fire('Đã có lỗi xảy ra')
            }
        });
    }
    // Xử lý file
    function HandleFiles() {
        debugger
        var file = $("#update-image-file")[0].files[0];
        // convert file to base64
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function () {
            //$('#img-cropper').attr('src', reader.result);
            avatarCropper.replace(reader.result);
        };
        reader.onerror = function (error) {
            alert("Upload image faild");
        };
    }
    function GetCropImage() {
        var croppedimage = avatarCropper.getCroppedCanvas().toDataURL("image/png");
        $('#blog-avatar').attr('src', croppedimage);
    }
})