$(document).ready(function () {
    // Binding event
    $(".sub-img").hover(function () {
        $(this).parent().children().removeClass("sub-img-focus");
        $(this).addClass("sub-img-focus");
        // Lấy ra id và name của category 
        var categoryId = $(this).attr("categoryId");
        var productName = $(this).attr("productName");
        // Lấy ra src,alt,title của sub image
        var srcSubImage = $(this).children().attr("src");
        var altSubImage = $(this).children().attr("alt");
        var titleSubImage = $(this).children().attr("title");
        // Set src va alt,title cho ảnh to
        $("#img-product-preview-category-" + categoryId).attr("src", srcSubImage);
        $("#img-product-preview-category-" + categoryId).attr("alt", altSubImage);
        $("#img-product-preview-category-" + categoryId).attr("title", titleSubImage);
        // Set a tag
        var hrefProduct = $(this).attr("href");
        var titleProduct = $(this).attr("title");
        $("#img-product-preview-category-" + categoryId).parent().attr("href", hrefProduct);
        $("#img-product-preview-category-" + categoryId).parent().attr("title", titleProduct);
        // Set name cho tên sản phẩm
        $("#product-name-" + categoryId).text(productName);
        $("#product-name-" + categoryId).attr("href", hrefProduct);
        $("#product-name-" + categoryId).attr("title", titleProduct);
    })
    $(".sub-dt-image").hover(function () {
        $(this).parent().children().removeClass("focus-detail-img");
        $(this).addClass("focus-detail-img");
        // Lấy ra src của sub image
        var srcSubImage = $(this).attr("src");
        // Set src cho ảnh chính
        $("#main-img-preview").attr("src", srcSubImage);
    })
    $("#subscribe-mail-btn").on("click", function () {
        $("#subscribe-mail-btn").prop('disabled', true);
        var email = $("#subscribe-form-2-email").val();
        if (validateEmail(email)) {
            var contact = {};
            contact.email = email;
            try {
                $.ajax({
                    type: 'POST',
                    url: '/EximaniContact/ReceiveContact',
                    contentType: "application/json",
                    data: JSON.stringify(contact),
                    async: false,
                    success: function (res) {
                        if (res.status == 200) {
                            $("#subscribe-mail-btn").prop('disabled', false);
                            alert("Subscribe success.");
                            $("#subscribe-form-2-email").val(null);
                        } else {
                            alert("Error! An error occurred. Please try again later");
                        }
                    },
                    error: function (err) {
                        alert("Error! An error occurred. Please try again later");
                        console.log(err);
                    }
                })
            } catch (e) {
                alert("Error! An error occurred. Please try again later");
                console.log(e);
            }
        } else {
            $("#subscribe-mail-btn").prop('disabled', false);
            alert("Email is not valid.");
        }

    });

    function validateEmail(email) {
        if (email != null || email.trim() != "") {
            const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        } else {
            return false;
        }
        
    }
})