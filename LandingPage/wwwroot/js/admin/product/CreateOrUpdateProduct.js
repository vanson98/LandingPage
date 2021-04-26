﻿$(document).ready(function () {
    // ==============  Config  ===============
    // Property 
    var cropImageDialog;
    var mainCropImg = $('#main-crop-img');
    var subCropImg = $('#sub-crop-img');
    var productId = $("#product-id").val() == "" ? null : parseInt($("#product-id").val());;
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

    // Config cropper
    mainCropImg.cropper({
        aspectRatio: 1 / 1,
        minContainerHeight: 500,
        minContainerWidth: 766,
        maximize: true,
        viewMode: 0
    });

    subCropImg.cropper({
        aspectRatio: 1 / 1,
        minContainerHeight: 500,
        minContainerWidth: 766,
        maximize: true,
        viewMode: 0
    });

    var mainImgCropper = mainCropImg.data('cropper');
    var subImgCropper = subCropImg.data('cropper');

    //===============  Binding event  ==============
    $("#btn-save-product").on("click", SaveProduct)
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
        $("#btn-save-product").prop('disabled', true);
        var productCode = $("#prod-code-input").val();
        var productName = $("#prod-name-input").val();
        var description = $("#prod-description-textarea").val();
        var content = tinymce.get("prod-content-textarea").getContent();
        var status = $('#prod-status-checkbox').is(":checked");
        var metaKeyWord = $('#meta-key-word-input').val();
        var metaDescription = $('#meta-description-input').val();
        var metaTitle = $('#meta-title-input').val();
        var productCategoryId = $("#prod-category-select").val();
        // Xử lý ảnh
        var listImage = GetAllImageBase64();
        // Khởi tạo product
        var newProduct = {
            id: productId,
            productCode: productCode,
            name: productName,
            description: description,
            content: content,
            status: status,
            metaKeyWord: metaKeyWord,
            metaDescription: metaDescription,
            metaTitle: metaTitle,
            productCategoryId: parseInt(productCategoryId),
            listImage: listImage
        }
        var url = "/AdminProduct/SaveProduct"
        if (productId !== null) {
            url = "/AdminProduct/UpdateProduct";
        }
        $.ajax({
            url: url,
            contentType: 'application/json',
            type: 'POST',
            data: JSON.stringify(newProduct),
            success: function (result) {
                if (result.statusCode == 202) {
                    window.location.href = result.urlRedirect;
                } else {
                    Swal.fire('Đã có lỗi xảy ra');
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

    // Convert toàn bộ ảnh sang base64
    function GetAllImageBase64() {
        var listImageBase64 = [];
        var mainImg = $("#prod-main-img")
        var mainImageBase64 = null;
        // Xử lý ảnh chính
        if (mainImg.attr("product-id") == null) {
            mainImageBase64 = $("#prod-main-img").attr("src");
        } else {
            mainImageBase64 = getBase64ImageFromUrl(document.getElementById("prod-main-img"));
        }
        if (mainImageBase64 != null && mainImageBase64 != "") {
            listImageBase64.push({
                isMainImage: true,
                base64: mainImageBase64
            });
        }
        // Xử lý ảnh phụ
        var listSubImage = $(".sub-img");
        for (var i = 0; i < listSubImage.length; i++) {
            var subImgBase64 = null;
            if ($(listSubImage[i]).attr("product-id") == null) {
                subImgBase64 = listSubImage[i].src
            } else {
                subImgBase64 = getBase64ImageFromUrl(listSubImage[i])
            }
            if (subImgBase64 != null && subImgBase64 != "") {
                listImageBase64.push({
                    isMainImage: false,
                    base64: subImgBase64
                })
            }
        }
        return listImageBase64;
    }

    function getBase64ImageFromUrl(img) {
        var canvas = document.createElement("canvas");
        canvas.width = img.naturalWidth;
        canvas.height = img.naturalHeight;
        var ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0);
        var dataURL = canvas.toDataURL("image/png");
        return dataURL;
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
        var newMainImage = "<img src='" + mainImageSrc + "' id='prod-main-img'/>"
        $("#prod-main-img-container").html(newMainImage);
        $('#main-img-crop-dialog').modal('hide');
    }

    // Lấy ảnh sau khi crop và append vào list image
    function AddSubCropImagesSrc() {
        var subImageSrc = subImgCropper.getCroppedCanvas().toDataURL("image/png");
        var domContainerImg = `
             <div>
                <img src='${  subImageSrc }'  class='sub-img'/>
                <button class="btn btn-warning">Delete</button>
             </div>
        `
        $('#list-sub-img').append(domContainerImg);
        $('#sub-img-crop-dialog').modal('hide');
    }
})


function DeleteImage(e) {
    var caller = e.target;
    $(caller).parent().remove();
}