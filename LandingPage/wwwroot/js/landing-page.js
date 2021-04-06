$(document).ready(function () {
    // Binding event
    $(".sub-img").hover(function () {
        $(this).parent().children().removeClass("sub-img-focus");
        $(this).addClass("sub-img-focus");
        // Lấy ra id và name của category 
        var categoryId = $(this).attr("categoryId");
        var productName = $(this).attr("productName");
        // Lấy ra src của sub image
        var srcSubImage = $(this).children().attr("src");
        // Set src cho ảnh to
        $("#img-product-preview-category-" + categoryId).attr("src", srcSubImage);
        // Set name cho tên sản phẩm
        $("#product-name-" + categoryId).text(productName);
    })

})