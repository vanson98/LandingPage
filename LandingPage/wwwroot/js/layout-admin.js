// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var loadFile = function (event) {
    var output = document.getElementById('output');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
};
var isMainImage = function (id, event) {
    $(event).parent().parent().parent().parent().parent().find('.isMainImage').eq(id).trigger('click');
}
$("input[data-type='currency']").on({
    input: function () {
        formatCurrency($(this));
    }
});
$("input[data-type='quantity']").on({
    input: function () {
        formatQuantity($(this));
    }
});
function createAttribute() {
    return ''
}
function formatCurrency(input) {
    var input_val = input.val();
    var original_len = input_val.length;
    // vị trí con trỏ đầu tiên
    var caret_pos = input.prop("selectionStart");
    // thay các ký tự ko phải [0-9] = '' và cứ mỗi 3 ký tự thêm 1 ký tự '.'
    input_val = input_val.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    var updated_len = input_val.length;
    for (var i = 0; i < updated_len - 1; i++) {
        // thay thế các ký tự 0 và . ở vị trí đầu tiên = ký tự '' đến khi chỉ còn 1 ký tự 0 duy nhất
        input_val = input_val.replace(/^0|^\./, '');
    }
    input.val(input_val);
    caret_pos = updated_len - original_len + caret_pos;
    // thiết lập vị trí con trỏ sau khi input
    input[0].setSelectionRange(caret_pos , caret_pos );
}

function formatQuantity(input) {
    var input_val = input.val();
    var original_len = input_val.length;
    // vị trí con trỏ đầu tiên
    var caret_pos = input.prop("selectionStart");
    // thay các ký tự ko phải [0-9] = ''
    input_val = input_val.replace(/\D/g, "")
    var updated_len = input_val.length;
    for (var i = 0; i < updated_len - 1; i++) {
        // thay thế các ký tự 0 và . ở vị trí đầu tiên = ký tự '' đến khi chỉ còn 1 ký tự 0 duy nhất
        input_val = input_val.replace(/^0|^\./, '');
    }
    input.val(input_val);
    caret_pos = updated_len - original_len + caret_pos;
    // thiết lập vị trí con trỏ sau khi input
    input[0].setSelectionRange(caret_pos , caret_pos );
}
$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});
showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
            $('.modal-body').find('.button').click(function () {
                $(this).parent().find('input[type="file"]').trigger('click');
            });
            $('.modal-body').find('.button-container input[type="file"].single').change(function (event) {
                var row = $(this).parent().parent().parent();
                row.find('.picture-input').remove();
                var reader = new FileReader();
                reader.onload = function (event) {
                    row.find('.pic').append('<div class="picture-input"></div>')
                    var img = '<div class="picture"><img src="' + event.target.result + '" ></div> ';
                    row.find(".picture-input").append(img);
                }
                reader.readAsDataURL(this.files[0]);
            });
            $('.modal-body').find('.button-container input[type="file"][multiple]').change(function (event) {
                var row = $(this).parent().parent().parent();
                var col = $(this).parent().parent().parent().parent();
                col.find('.row:last-child').remove();
                row.find('.picture-input').remove();
                $('p.error').remove();
                if (this.files) {
                    var filesAmount = this.files.length;
                    if (filesAmount > 5) {
                        $(this).val('');
                        row.find('.col-md-8').html('<p class="error">Chỉ được chọn tối đa 5 hình</p>')
                    } else {
                        for (i = 0; i < filesAmount; i++) {
                            var reader = new FileReader();
                            reader.onload = function (event) {
                                if (row.find('.picture-input').length) {
                                } else {
                                    row.find('.col-md-8').append('<div class="picture-input"></div>')

                                }
                                var length = row.find('.picture').length
                                var img = '<div class="picture"><img src="' + event.target.result + '" onclick="isMainImage(' + length + ',this)" ></div> ';
                                row.find(".picture-input").append(img);
                            }
                            reader.readAsDataURL(this.files[i]);
                        }

                        for (j = 0; j < filesAmount; j++) {
                            if (j == 0) {
                                col.append('<div class="row main-pictures" style="margin-top:20px"><div class="col-md-2"><label>Ảnh chính</label></div><div class="col-md-8 mr-auto"><div class="main-picture"></div></div></div>')
                            }
                            var mainImg = '<div class="is-main-picture"><input type="radio" class="isMainImage" name="ProductImage.isMainImage" value="' + this.files[j].name + '" ></div>';
                            col.find('.main-picture').append(mainImg);

                        }
                    }
                }
            });
            $('script[src="/lib/jquery-validation/dist/jquery.validate.min.js"]').remove();
            var reloadJs1 = document.createElement("script");
            reloadJs1.src = '/lib/jquery-validation/dist/jquery.validate.min.js';
            $("body").append(reloadJs1);
            $('script[src="/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"]').remove();
            var reloadJs2 = document.createElement("script");
            reloadJs2.src = '/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js';
            $("body").append(reloadJs2);
            //$('[data-toggle="datepicker"]').datepicker({
            //    autoHide: true,
            //    zIndex: 2048,
            //    dateFormat: 'dd-mm-yy',
            //});
            $('[data-toggle="datepicker"]').datepicker({ format: 'dd/mm/yyyy' }).val();
        }
    })

}
$(document).ready(function () {
    $(".paragraph").click(function () {
        $(this).toggleClass("mini_paragraph");
    });
    $("input[data-type='currency']").on({
        input: function () {
            formatCurrency($(this));
        }
    });
    $("input[data-type='quantity']").on({
        input: function () {
            formatQuantity($(this));
        }
    });
});
$(function () {
    var PlaceHolderElement = $('#PlaceHolderElement');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })
    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
        })
    })
})
jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            async: false,
            success: function (res) {
                if (res.isValid) {
                    $("#view-all").html(res.html);
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');
                    $("#form-modal").modal('hide');
                    $.notify('Lưu lại thành công', { globalPosition: 'top center', className:'success' })
                } else {
                    $("#form-modal .modal-body").html(res.html);
                    $('[data-toggle="datepicker"]').datepicker({ format: 'dd/mm/yyyy' }).val();
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
        $('script[src="/lib/jquery-validation/dist/jquery.validate.min.js"]').remove();
        var reloadJs1 = document.createElement("script");
        reloadJs1.src = '/lib/jquery-validation/dist/jquery.validate.min.js';
        $("body").append(reloadJs1);
        $('script[src="/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"]').remove();
        var reloadJs2 = document.createElement("script");
        reloadJs2.src = '/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js';
        $("body").append(reloadJs2);
        
        $('#data-table').DataTable({
            stateSave: true,
            "bDestroy": true,
            "language": {
                "search": "Tìm Kiếm:",
                "lengthMenu": "Hiển thị _MENU_ bản ghi",
                "emptyTable": "Bảng không có dữ liệu",
                "info": "Hiển thị từ _START_ đến _END_ của _TOTAL_ bản ghi",
                "infoEmpty": "Hiển thị 0 bản ghi",
                "zeroRecords": "Không có bản ghi nào được tìm thấy",
                "paginate": {
                    "next": ">",
                    "previous": "<"
                }
            },
            lengthMenu: [10, 15, 20, 30,40],
        });
        
    } catch(e){
        console.log(e);
    }
    return false;
}
jQueryAjaxDelete = form => {
    if (confirm('Bạn có chắc chắn xóa không ')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                async: false,
                success: function (res) {
                        $("#view-all").html(res.html);
                    $.notify('Xóa thành công', { globalPosition: 'top center', className: 'success' })
                },
                error: function (err) {
                    console.log(err);
                }
            })
        } catch (e) {
            console.log(e);
        }
        $('#data-table').DataTable({
            stateSave: true,
            "bDestroy": true,
            "language": {
                "search": "Tìm Kiếm:",
                "lengthMenu": "Hiển thị _MENU_ bản ghi",
                "emptyTable": "Bảng không có dữ liệu",
                "info": "Hiển thị từ _START_ đến _END_ của _TOTAL_ bản ghi",
                "infoEmpty": "Hiển thị 0 bản ghi",
                "zeroRecords": "Không có bản ghi nào được tìm thấy",
                "paginate": {
                    "next": ">",
                    "previous": "<"
                }
            },
            lengthMenu: [10, 15, 20, 30, 40],
        });
    }
    //Tránh việc chạy vào sự kiện submit
    return false;
}
    $(document).ready(function () {
        $('#data-table').DataTable({
            stateSave: true,
            "bDestroy": true,
            "language": {
                "search": "Tìm Kiếm:",
                "lengthMenu": "Hiển thị _MENU_ bản ghi",
                "emptyTable": "Bảng không có dữ liệu",
                "info": "Hiển thị từ _START_ đến _END_ của _TOTAL_ bản ghi",
                "infoEmpty": "Hiển thị 0 bản ghi",
                "zeroRecords": "Không có bản ghi nào được tìm thấy",
                "paginate": {
                    "next": ">",
                    "previous": "<"
                },
            },
            lengthMenu: [10, 15, 20, 30, 40],
        });
        
    });
$("#add-attr").click(function () {
    var item = $(".attribute").first().clone().addClass('attribute-item');
    item.show();
    var index = $(".attribute-item").length ;
    item.find('#ColorId').attr('name', 'listAttribute['+index+'].ProductColorId')
    item.find('#SizeId').attr('name', 'listAttribute['+index+'].ProductSizeId')
    item.find('#Price').attr('name', 'listAttribute['+index+'].Price')
    item.find('#DiscountPrice').attr('name', 'listAttribute['+index+'].DiscountPrice')
    item.find('#CountStock').attr('name', 'listAttribute[' + index +'].CountStock')
    $('#attr').append(item);
    $(item).find(".btn-close").click(function () {
        var index = $('.attribute-item').index($(this).parent());
        var length = $('.attribute-item').length
        for (i = index; i < length; i++) {
            var item = $('.attribute-item').eq(i);
            var j = i - 1
            item.find('#ColorId').attr('name', 'listAttribute[' + j + '].ProductColorId')
            item.find('#SizeId').attr('name', 'listAttribute[' + j + '].ProductSizeId')
            item.find('#Price').attr('name', 'listAttribute[' + j + '].Price')
            item.find('#DiscountPrice').attr('name', 'listAttribute[' + j + '].DiscountPrice')
            item.find('#CountStock').attr('name', 'listAttribute[' + j + '].CountStock')
        }
        $(this).parent().remove();
    })
    $("input[data-type='currency']").on({
        input: function () {
            formatCurrency($(this));
        }
    });
    $("input[data-type='quantity']").on({
        input: function () {
            formatQuantity($(this));
        }
    });
})
$("#remove-attr").click(function () {
    $(".attribute-item").remove();
})
$("#create-attr").click(function () {
    $(".attribute-item").remove();
    var listcolor = [];
    var listsize = [];
    var price = $("input[type=text][name=price-filter]").val();
    var discountprice = $("input[type=text][name=discount-filter]").val();
    var countstock = $("input[type=text][name=stock-filter]").val();
    $("input[type=checkbox][name=color-filter]:checked").each(function () {
        listcolor.push($(this).val());
    });
    $("input[type=checkbox][name=size-filter]:checked").each(function () {
        listsize.push($(this).val());
    });
    if (listcolor.length != 0 && listsize.length != 0) {
        var index =0
        for (var i = 0; i < listcolor.length; i++) {
            for (var j = 0; j < listsize.length; j++) {
                var item = $(".attribute").first().clone().addClass('attribute-item');
                item.show();
                item.find('#ColorId option').first().removeAttr("selected");
                item.find('#ColorId option[value="' + listcolor[i] + '"]').prop("selected", "selected");
                item.find('#SizeId option').first().removeAttr("checked");
                item.find('#SizeId option[value="' + listsize[j] + '"]').prop("selected", "selected");
                item.find('input[type=text][id=Price]').val(price);
                item.find('input[type=text][id=DiscountPrice]').val(discountprice);
                item.find('input[type=text][id=CountStock]').val(countstock);
                item.find('#ColorId').attr('name', 'listAttribute[' + index + '].ProductColorId')
                item.find('#SizeId').attr('name', 'listAttribute[' + index + '].ProductSizeId')
                item.find('#Price').attr('name', 'listAttribute[' + index + '].Price')
                item.find('#DiscountPrice').attr('name', 'listAttribute[' + index + '].DiscountPrice')
                item.find('#CountStock').attr('name', 'listAttribute[' + index + '].CountStock')
                $('#attr').append(item);
                index++;
            }
        }
    } else if (listcolor.length != 0 && listsize.length == 0) {
        var index = 0
        for (var i = 0; i < listcolor.length; i++) {
            var item = $(".attribute").first().clone().addClass('attribute-item');
            item.show();
            item.find('#ColorId option').first().removeAttr("selected");
            item.find('#ColorId option[value="' + listcolor[i] + '"]').prop("selected", "selected");
            item.find('input[type=text][id=Price]').val(price);
            item.find('input[type=text][id=DiscountPrice]').val(discountprice);
            item.find('input[type=text][id=CountStock]').val(countstock);
            item.find('#ColorId').attr('name', 'listAttribute[' + index + '].ProductColorId')
            item.find('#SizeId').attr('name', 'listAttribute[' + index + '].ProductSizeId')
            item.find('#Price').attr('name', 'listAttribute[' + index + '].Price')
            item.find('#DiscountPrice').attr('name', 'listAttribute[' + index + '].DiscountPrice')
            item.find('#CountStock').attr('name', 'listAttribute[' + index + '].CountStock')
            $('#attr').append(item);
            index++;
        }
    } else if (listcolor.length == 0 && listsize.length != 0) {
        var index = 0
        for (var i = 0; i < listsize.length; i++) {
            var item = $(".attribute").first().clone().addClass('attribute-item');
            item.show();
            item.find('#SizeId option').first().removeAttr("selected");
            item.find('#SizeId option[value="' + listsize[i] + '"]').prop("selected", "selected");
            item.find('input[type=text][id=Price]').val(price);
            item.find('input[type=text][id=DiscountPrice]').val(discountprice);
            item.find('input[type=text][id=CountStock]').val(countstock);
            item.find('#ColorId').attr('name', 'listAttribute[' + index + '].ProductColorId')
            item.find('#SizeId').attr('name', 'listAttribute[' + index + '].ProductSizeId')
            item.find('#Price').attr('name', 'listAttribute[' + index + '].Price')
            item.find('#DiscountPrice').attr('name', 'listAttribute[' + index + '].DiscountPrice')
            item.find('#CountStock').attr('name', 'listAttribute[' + index + '].CountStock')
            $('#attr').append(item);
            index++;
        }
    } else {
        var index = 0
        var item = $(".attribute").first().clone().addClass('attribute-item');
        item.show();
        item.find('input[type=text][id=Price]').val(price);
        item.find('input[type=text][id=DiscountPrice]').val(discountprice);
        item.find('input[type=text][id=CountStock]').val(countstock);
        item.find('#ColorId').attr('name', 'listAttribute[' + index + '].ProductColorId')
        item.find('#SizeId').attr('name', 'listAttribute[' + index + '].ProductSizeId')
        item.find('#Price').attr('name', 'listAttribute[' + index + '].Price')
        item.find('#DiscountPrice').attr('name', 'listAttribute[' + index + '].DiscountPrice')
        item.find('#CountStock').attr('name', 'listAttribute[' + index + '].CountStock')
        $('#attr').append(item);
    }
    $(".attribute-item .btn-close").click(function () {
        var index = $('.attribute-item').index($(this).parent());
        var length = $('.attribute-item').length
        for (i = index; i < length; i++) {
            var item = $('.attribute-item').eq(i);
            var j =i-1
            item.find('#ColorId').attr('name', 'listAttribute[' + j + '].ProductColorId')
            item.find('#SizeId').attr('name', 'listAttribute[' + j + '].ProductSizeId')
            item.find('#Price').attr('name', 'listAttribute[' + j + '].Price')
            item.find('#DiscountPrice').attr('name', 'listAttribute[' + j + '].DiscountPrice')
            item.find('#CountStock').attr('name', 'listAttribute[' + j+ '].CountStock')
        }
        $(this).parent().remove();

    })
    $("input[data-type='currency']").on({
        input: function () {
            formatCurrency($(this));
        }
    });
    $("input[data-type='quantity']").on({
        input: function () {
            formatQuantity($(this));
        }
    });
})

$("#add-img").click(function () {
    var item = $(".image-product").first().clone().addClass("image-product-item");
    item.show();
    var index = $(".image-product-item").length ;
    item.find('#ColorId').attr('name', 'listImage[' + index + '].ProductColorId')
    item.find('#ImageFiles').attr('name', 'listImage[' + index + '].ImageFiles')
    $('#img').append(item);
    $(item).find(".btn-close").click(function () {
        var index = $('.image-product-item').index($(this).parent());
        var length = $('.image-product-item').length
        for (i = index; i < length; i++) {
            var item = $('.image-product-item').eq(i);
            var j = i - 1
            item.find('#ColorId').attr('name', 'listImage[' + j + '].ProductColorId')
            item.find('#ImageFiles').attr('name', 'listImage[' + j + '].ImageFiles')
        }
        $(this).parent().remove();
    })
    $(item).find('.button').click(function () {
        $(this).parent().find('input[type="file"]').trigger('click');
    });
    $(item).find('.button-container input[type="file"]').change(function (event) {
        var row = $(this).parent().parent().parent();
        var col = $(this).parent().parent().parent().parent();
        col.find('.row:last-child').remove();
        row.find('.picture-input').remove();
        $('p.error').remove();
        if (this.files) {
            var filesAmount = this.files.length;
            if (filesAmount > 5) {
                $(this).val('');
                row.find('.col-md-8').html('<p class="error">Chỉ được chọn tối đa 5 hình</p>')
            } else {
                for (i = 0; i < filesAmount; i++) {
                    var reader = new FileReader();
                    reader.onload = function (event) {
                        if (row.find('.picture-input').length) {
                        } else {
                            row.find('.col-md-8').append('<div class="picture-input"></div>')
                            
                        }
                        var length = row.find('.picture').length
                        var img = '<div class="picture"><img src="' + event.target.result + '" onclick="isMainImage(' + length + ',this)" ></div> ';
                        row.find(".picture-input").append(img);
                    }
                    reader.readAsDataURL(this.files[i]);
                }
                
                for (j = 0; j < filesAmount; j++) {
                    if (j == 0) {
                        col.append('<div class="row main-pictures" style="margin-top:20px"><div class="col-md-2"><label>Ảnh chính</label></div><div class="col-md-8 mr-auto"><div class="main-picture"></div></div></div>')
                    }
                    var mainImg = '<div class="is-main-picture"><input type="radio" class="isMainImage" name="listImage[' + index + '].isMainImage" value="' + this.files[j].name + '" ></div>';
                    col.find('.main-picture').append(mainImg);

                }
                
                
            }
        }
    });
})