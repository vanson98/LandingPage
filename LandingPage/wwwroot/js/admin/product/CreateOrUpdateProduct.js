﻿$(document).ready(function () {
    // ==============  Config  ===============
    // Property 
    var cropImageDialog;
    var mainCropImg = $('#main-crop-img');
    var subCropImg = $('#sub-crop-img')
    var mainImgBase64;
    var subImgBase64 = [];
    // Config jquery tab 
    $("#tabs").tabs();

    // Config tiny CME
    $('#prod-content-textarea').tinymce({
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

    // Config cropper
    mainCropImg.cropper({
        aspectRatio: 4 / 3,
        minContainerHeight: 500,
        minContainerWidth: 766,
        maximize: true,
        viewMode: 0
    });

    subCropImg.cropper({
        aspectRatio: 4 / 3,
        minContainerHeight: 500,
        minContainerWidth: 766,
        maximize: true,
        viewMode: 0
    });

    var mainImgCropper = mainCropImg.data('cropper');
    var subImgCropper = subCropImg.data('cropper');

    //===============  Binding event  ==============
    $("#btn-create-blog").on("click", SaveProduct)
    $("#open-crop-img-btn").on("click", OpenCropImageDialog)
    $("#update-main-image-file").on("change", function (event) { HandleFiles("main") });
    $("#update-sub-image-file").on("change", function (event) { HandleFiles("sub") });
    $("#crop-main-img-btn").on("click", AddMainCropImageSrc)
    $("#crop-sub-img-btn").on("click", AddSubCropImagesSrc)
    // =============   Function  =============
    // Mở dialog cắt ảnh đại diện của dialog
    function OpenCropImageDialog() {
        cropImageDialog.dialog("open");
    }
    // Save product
    function SaveProduct() {
        debugger
        var productId = parseInt($("#blog-id").val());
        var productCode = $("#prod-code-input").val();
        var productName = $("#prod-name-input").val();
        var description = $("#prod-description-textarea").val();
        var content = tinymce.get("prod-content-textarea").getContent();
        var status = $('#prod-status-checkbox').is(":checked"); 
        var metaKeyWord = $('#meta-key-word-input').val();
        var metaDescription = $('#meta-description-input').val();
        var metaTitle = $('#meta-title-input').val();
        var parentId = $("#prod-parent-code-select").val(); // check
        var mainImageBase64 = mainImgBase64;
        var productCategoryId = $('#prod-category-select').val().trim() != "" ? parseInt($('#category-select').val()) : 0;
        var imagesBase64 = subImgBase64;
       
      
        var blog = {
            id: blogId,
            title: blogTitle,
            shortDescription: blogShortDescription,
            urlImage: "test",
            content: blogContent,
            published: isPublished,
            //blogCategoryId: blogCategoryId,
            metaKeyWord: metaKeyWord,
            metaDescription: metaDescription,
            metaTitle: metaTitle
        }
        var url = "/AdminBlog/SaveBlog"
        if (blogId !== null || blogId !== "") {
            url = "/AdminBlog/UpdateBlog";
        }
        $.ajax({
            url: url,
            contentType: 'application/json',
            type: 'POST',
            data: JSON.stringify(blog),
            success: function (result) {
                window.location.href = result.urlRedirect;
            },
            error: function (result) {
                Swal.fire('Đã có lỗi xảy ra')
            }
        });
    }

    // Xử lý file
    function HandleFiles(type) {
        var file;
        if (type == "main") {
            file = $("#update-main-image-file")[0].files[0];
        } else {
            file = $("#update-sub-image-file")[0].files[0];
        }
        // convert file to base64
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function () {
            if (type == "main") {
                mainImgCropper.replace(reader.result)
            } else {
                subImgCropper.replace(reader.result);
            }
        };
        reader.onerror = function (error) {
            alert("Upload image faild");
        };
    }

    // Lấy ảnh sau khi crop và gán vào src cho main image
    function AddMainCropImageSrc() {
        var mainImageSrc = mainImgCropper.getCroppedCanvas().toDataURL("image/png");
        $('#prod-main-img').attr('src', mainImageSrc);
        mainImgBase64 = mainImageSrc;
    }

    // Lấy ảnh sau khi crop và append vào list image
    function AddSubCropImagesSrc() {
        var subImageSrc = subImgCropper.getCroppedCanvas().toDataURL("image/png");
        $('#list-sub-img').append("<img src='" + subImageSrc + "' />")
        subImgBase64.push(subImageSrc);
    }

})