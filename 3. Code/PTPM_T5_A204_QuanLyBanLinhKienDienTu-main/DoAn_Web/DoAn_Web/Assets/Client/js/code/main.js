/*Kiểm tra số điện thoại hợp lệ*/
function validatePhone($Phone) {
    let filter = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
    console.log($Phone);
    return filter.test($Phone);
}
const validateEmail = (email) => {
    return email.match(
      /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
};
/*Hàm disable button khi chờ server xử lý */
function disable(dom) {
    $(dom).attr('disabled', 'true');
    $(dom).attr('data-text', $(dom).text());
    $(dom).html(`<span class="spinner-grow spinner-grow-sm" ></span> <span>Loading...</span>`);
}
/*Hàm disable button khi chờ server xử lý */
function enable(dom) {
    $(dom + ' span').hide();
    $(dom).removeAttr('disabled');
    $(dom).html($(dom).data('text'));
}
//Hàm xử lý format tiền tệ sang vnd
function formatCurrency(nStr, decSeperate, groupSeperate) {
    nStr += '';
    x = nStr.split(decSeperate);
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    let rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + groupSeperate + '$2');
    }
    return x1 + x2;
}

$(document).ready(function () {
    $('#txtkeyword').keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
        }
    })
});
$(function () {
    //Hàm tìm kiếm sản phẩm
    function search() {
        let keyword = $('#txtkeyword').val();
        if (keyword.trim() == '')
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Bạn chưa nhập từ khoá.',
                position: 'topRight'
            });
        else
            location.href = '/tim-kiem?tukhoa=' + keyword;
    }
    //Event tìm kiếm sản phẩm
    $('#btnsearch').click(search)
    $('#txtkeyword').keypress(function (e) {
        if (e.which == 13) {
            search()
        }
    })

    //Hàm xử lý thêm giỏ hàng
    function addCart(ProductId, Quantity) {
        if (Quantity <= 0) {
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Số lượng phải lớn hơn 0',
                position: 'topRight'
            });
            return;
        }
        $.ajax({
            url: "/Cart/AddItem",
            data: JSON.stringify({ ProductId, Quantity }),
                    contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == -1) {
                    swal("Thông báo", "Mời bạn đăng nhập!", "warning")
                    .then((value) => {
                        location.href = "/dang-nhap-dang-ki";
                    });
                }
                else
                    if (data.status == 1) {
                        iziToast.success({
                            timeout: 1500,
                            title: 'Thành công',
                            message: data.message,
                            position: 'topRight'
                        });
                        $('.badge').text(data.sumQuantity)
                    }
                    else {
                        swal("Thông báo", data.message, "info")
                    }

            },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });
            }
        });
    }
    //Thêm giỏ hàng
    $('.addcart').off('click').click(function (e) {
        e.preventDefault();
        let ProductId = $(this).data('id');
        let Quantity = $(this).data('value');
        addCart(ProductId, Quantity);
    })
    //thêm giỏ hàng có số lượng
    $('#btnaddcart').click(function () {
        let Quantity = $('#txtquantity-product').val();
        let ProductId = $(this).data('id');
        addCart(ProductId, Quantity);
    })
    //xoá phần tử trong giỏ hàng
    function deletecart(ProductId) {
        $.ajax({
            url: "/Cart/DeleteItem",
            data: JSON.stringify({ ProductId}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == "1") {
                    $('#cart-' + ProductId).hide(500);
                    $('.badge ').text(data.sumQuantity);
                    $('#summoney-cart').text(data.sumMoney);
                    iziToast.success({
                        timeout: 1500,
                        title: 'Thành công',
                        message: data.message,
                        position: 'topRight'
                    });
                }
                else
                    iziToast.error({
                        timeout: 1500,
                        title: 'Lỗi',
                        message: data.message,
                        position: 'topRight'
                    });

            },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định',
                    position: 'topRight'
                });
            }

        });
    }
    //sự kiện xoá 1 sản phẩm trong giỏ hàng
    $('.delete-cart').off('click').click(function () {
        let ProductId = $(this).data('id');
        swal({
            title: "Bạn chắc chắn xoá sản phẩm này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                deletecart(ProductId);
            }
        });
    })
    //hàm xử lý tăng giảm số lượng trong giỏ hàng
    function changeQuantityCart(ProductId, Quantity, dom) {
        $.ajax({
            url: "/Cart/UpdateItem",
            data: JSON.stringify({ ProductId, Quantity }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        if (data.status == "1") {
                            if (Quantity <= 0)
                                $('#cart-' + ProductId).hide(500);
                            dom.data('val', dom.val());
                            $('#total-' + ProductId).text(data.total);
                            $('#summoney-cart').text(data.sumMoney);
                            $('.badge ').text(data.sumQuantity);

                        }
                        else
                            if (data.status == -3) {
                                dom.val(dom.data('val'));
                                iziToast.warning({
                                    timeout: 1500,
                                    title: 'Cảnh báo',
                                    message: data.message,
                                    position: 'topRight'
                                });
                            }
                            else
                                iziToast.error({
                                    timeout: 1500,
                                    title: 'Lỗi',
                                    message: 'Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.',
                                    position: 'topRight'
                                });
                    },
            error: function (data) {

            }

        });
    }
    //event xử lý tăng hoặc giảm số lượng  của input
    $('.txtquantity-product').on('change', function () {
        let ProductId = $(this).data('id');
        let Quantity = $(this).val();
        Quantity = Number(Quantity);
        if (isNaN(Quantity))
        {
            iziToast.error({
                timeout: 1500,
                title: 'Lỗi',
                message: 'Bạn phải nhập số lượng là số.',
                position: 'topRight'
            });
            return;
        }
        let dom = $(this);
        changeQuantityCart(ProductId, Quantity, dom);
    })
    //event xử lý tăng hoặc giảm số lượng của button
    $('.quantity button').on('click', function () {
        let button = $(this);
        let ProductId = $(this).data('id');
        let oldValue = button.parent().parent().find('input').val();
        let newVal;
        if (button.hasClass('btn-plus')) {
            newVal = parseFloat(oldValue) + 1;
        } else {
            if (oldValue > 0) {
                newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
            }
        }
        button.parent().parent().find('input').val(newVal);
        changeQuantityCart(ProductId, newVal, button.parent().parent().find('input'));
    });
    //xử lý xác nhận đơn đặt hàng
    function Pay() {
        var order = new Object();
        order.ispay = 0;
        var khachhang = new Object();
        khachhang.fullname = $('#cart-fullname').val();
        khachhang.phone = $('#cart-phone').val();
        var email = $('#cart-email').val();
        khachhang.address = $('#cart-address').val();
        if (order.fullname == '') {
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Mời bạn nhập họ tên.',
                position: 'topRight'
            });
        }
        else if (order.phone == '') {
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Mời bạn nhập số điện thoại.',
                position: 'topRight'
            });
        }       
        else if (order.email == '') {
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Mời bạn nhập email.',
                position: 'topRight'
            });
        }
        else if (order.address == '') {
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Mời bạn chọn địa chỉ.',
                position: 'topRight'
            });
        }
        else {
            disable('#btnpay');
            $.ajax({
                url: '/Cart/Pay',
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ order, khachhang, email}),
                        success: function (data) {
                    enable('#btnpay');

                    if(data.status==1)
                    {
                        swal({
                        title: "Thông báo",
                        text: "Đặt hàng thành công.Vui lòng check email.",
                            icon: "success",
                            button: "Ok",
                        });
                        $('#checkout').remove();

                    }
                    else {
                        swal({
                            title: "Thông báo",
                            text: "Lỗi hệ thống vui lòng thử lại sau,",
                            icon: "error",
                            button: "Ok",
                        });
                    }

                        },
                error: function (data) {
                    enable('#btnpay');
                }
            });
        }
    }
    $('#btnpay').click(Pay);
    //Bật modal cho quên mật khẩu
    $('#btnfogetpass').click(function (e) {
        e.preventDefault();
        $("#myModal4").modal({ backdrop: "static" });
    });
})