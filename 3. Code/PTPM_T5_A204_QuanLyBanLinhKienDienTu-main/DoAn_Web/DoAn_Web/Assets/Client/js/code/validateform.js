/*Hàm xử lý login từ tài khoản đăng kí*/
function login() {
    let username = $('#username_login').val();
    let password = $('#password_login').val();
    let dom ='#btnlogin';
    disable(dom);
    $.ajax({
        url: "/Account/Login",
        data: JSON.stringify({ username, password }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (data) {
            if (data.status != 1)
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: data.message,
                    position: 'topRight'
                });
            else
                window.location.href = '/';
            enable(dom);

        },
        error: function (data) {
            iziToast.error({
                timeout: 1500,
                title: 'Lỗi',
                message: 'Lỗi chưa xác định',
                position: 'topRight'
            });
            enable(dom);

        }
    })
}
/*Hàm upload ảnh lên server*/
function upLoad(dom) {
    var fileUpload = $(dom).get(0);
    var files = fileUpload.files;

    // Create  a FormData object
    var fileData = new FormData();

    // if there are multiple files , loop through each files
    for (var i = 0; i < files.length; i++) {
        fileData.append(files[i].name, files[i]);
    }

    // Adding more keys/values here if need
    fileData.append('Test', "Test Object values");

    $.ajax({
        url: '/Account/UploadImg', //URL to upload files 
        type: "POST", //as we will be posting files and other method POST is used
        processData: false, //remember to set processData and ContentType to false, otherwise you may get an error
        contentType: false,
        data: fileData,
        success: function (result) {
        },
        error: function (err) {
            iziToast.error({
                timeout: 1500,
                title: 'Lỗi',
                message: err.statusText,
                position: 'topRight'
            });
        }
    });

}
//Nhập email lấy lại mật khẩu
function confirmEmail() {
    let email = $('#keywordemail').val();

    disable('#btnsubmitforget');
    $.ajax({
        url: "/Account/ForgetPass",
        data: JSON.stringify({ email }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (data) {
            if (data.status == "-1")
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: data.message,
                    position: 'topRight'
                });
            else if (data.status == "1") {
                iziToast.info({
                    timeout: 1500,
                    title: 'Thông tin',
                    message: data.message,
                    position: 'topRight'
                });
                $('#btncloseforget').click();
                $("#myModal5").modal({ backdrop: "static" });
            }
            else
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: data.message,
                    position: 'topRight'
                });
            enable('#btnsubmitforget');

        },
        error: function (data) {
            iziToast.error({
                timeout: 1500,
                title: 'Lỗi',
                message: 'Lỗi chưa xác định.',
                position: 'topRight'
            });

        }
    })

}


/*Hàm gửi lại mã xác nhận*/
function sendAgain() {
    disable('#btnsendagainregister');
    disable('#btnsendagainforgetpass');
    $.ajax({
        url: "/Account/SendAgain",
        data: JSON.stringify({}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (data) {
            console.log(data)
            if (data == "0")
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi hệ thống không thể gửi mail.',
                    position: 'topRight'
                });
            else
                iziToast.info({
                    timeout: 1500,
                    title: 'Thông tin',
                    message: 'Mời bạn check email để lấy mã xác nhận mới.Xin cảm ơn.',
                    position: 'topRight'
                });
            enable('#btnsendagainregister');
            enable('#btnsendagainforgetpass');

        },
        error: function (data) {
            iziToast.error({
                timeout: 1500,
                title: 'Lỗi',
                message: 'Lỗi chưa xác định.',
                position: 'topRight'
            });
        }
    })
}

/*Hàm xác nhận mã từ gmail khi đăng kí*/
function submit() {
    let code = "";
    code = $('#keywordforget').val();
    if (code == "")
        iziToast.warning({
            timeout: 1500,
            title: 'Cảnh báo',
            message: 'Bạn chưa nhập mã xác nhập.',
            position: 'topRight'
        });
    else {
        disable('#btnsubmitforgetpass');
        $.ajax({
            url: '/Account/CodeForget',
            data: JSON.stringify({ code }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
            success: function (data) {
                console.log(data)
                if (data.status == "0") {
                    iziToast.error({
                        timeout: 1500,
                        title: 'Lỗi',
                        message: data.message,
                        position: 'topRight'
                    });
                }
                else {
                    iziToast.success({
                        timeout: 1500,
                        title: 'Thành công',
                        message: data.message,
                        position: 'topRight'
                    });
                    $('#btncloseforgetpass').click();
                    $("#myModal6").modal({ backdrop: "static" });
                }
                enable('#btnsubmitforgetpass');
            },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });

            }
        })
    }
}

/*Hàm đăng kí tài khoản trên website*/
function register() {
    var kh = new Object();
    kh.Makh = null;
    kh.maloaikh = 3;
    console.log($('#Gioitinh input:radio:checked').val());
    if ($('#Gioitinh input:radio:checked').val() == 0)
        kh.GIOITINH = false;
    else kh.GIOITINH = true;
    kh.SDT = $('#phone').val();
    kh.DIACHI = $('#address').val();
    kh.TENKH = $('#fullname').val();
    var acc = new Object();
    acc.image = $('#imageregister').val().split("\\").pop();
    acc.email = $('#email_register').val();
    acc.username = $('#username_register').val();
    acc.password = $('#password_register').val();
    if (acc.image != null && acc.image.length != 0)
        upLoad("#imageregister");
    let dom = '#btnregister';
    disable(dom);
    $.ajax({
        url: "/Account/Register",
        data: JSON.stringify({ kh,acc }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (data) {
            if (data.status != 1)
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: data.message,
                    position: 'topRight'
                });
            else {
                $('#link-login').click();
                iziToast.success({
                    timeout: 1500,
                    title: 'Thành công',
                    message: data.message,
                    position: 'topRight'
                });
            }
            enable(dom);


        },
        error: function (data) {
            iziToast.error({
                timeout: 1500,
                title: 'Lỗi',
                message: 'Lỗi chưa xác định.',
                position: 'topRight'
            });
            enable(dom);

        }
    })

}
$(function () {
    //custom validate phone
    jQuery.validator.addMethod('phone', function (value, element) {
        return validatePhone(element.value)
    });

    //validate và xử lý đăng nhập
    $("form[name='login']").validate({
        rules: {
            username_login: "required",
            password_login: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            username_login: "Tên đăng nhập không được để trống.",
            password_login: {
                required: "Mật khẩu không được để trống.",
                minlength: "Mật khẩu của bạn phải dài ít nhất 5 kí tự."
            }
        },
        submitHandler: function (form) {
            login();
        }
    });
    //validate và xử lý đăng kí
    $("form[name='register']").validate({
        rules: {
            username_register: "required",
            password_register: {
                required: true,
                minlength: 5
            },
            confirm_password: {
                required: true,
                minlength: 5,
                equalTo: "#password_register"

            },
            email_register: {
                required: true,
                email: true
            },
            phone: {
                required: true,
                phone: true
            },
            address: "required",
            fullname: "required"

        },
        messages: {
            username_register: "Tên đăng nhập không được để trống.",


            password_register: {
                required: "Mật khẩu không được để trống.",
                minlength: "Mật khẩu của bạn phải dài ít nhất 5 kí tự."
            },
            confirm_password: {
                required: "Xác nhận mật khẩu không được để trống.",
                minlength: "Xác nhận mật khẩu của bạn phải dài ít nhất 5 kí tự.",
                equalTo: "Xác nhận mật khẩu không giống nhau."
            },
            email_register: {
                required: "Email không được để trống.",
                email: "Email không đúng định dạng."
            },
            phone: {
                required: "Số điện thoại không được để trống.",
                phone: "Số điện thoại không đúng định dạng."
            },
            address: "Địa chỉ không được để trống.",
            fullname: "Họ tên không được để trống."
        },
        submitHandler: function (form) {
            register();
        }
    });



    //Xác nhận đúng email để lấy mật khẩu
    $("form[name='forgetpassemail']").validate({
        rules: {
            keywordemail: {
                required: true,
                email: true
            }
        },
        messages: {
            keywordemail: {
                required: "Email không được để trống.",
                email: "Email không đúng định dạng."
            }
        },
        submitHandler: function (form) {
            confirmEmail();
        }
    });
    //Xác nhận mã từ gmail quên mật khẩu
    $("form[name='submitforgetpass']").validate({
        rules: {
            keywordforget: {
                required: true
            }
        },
        messages: {
            keywordforget: {
                required: "Mã xác nhận không được để trống."
            }
        },
        submitHandler: function (form) {
            submit("forget");
        }
    });


    //Đổi mật khẩu từ email quên mật khẩu
    $("form[name='submitchangepass']").validate({
        rules: {
            passnew: {
                required: true,
                minlength: 5
            },
            prepass: {
                required: true,
                minlength: 5,
                equalTo: "#passnew"
            }
        },
        messages: {
            passnew: {
                required: "Mật khẩu mới không được để trống.",
                minlength: "Mật khẩu mới phải dài ít nhất 5 kí tự."
            },
            prepass: {
                required: "Nhập lại mật khẩu không được để trống.",
                minlength: "Nhập lại mật khẩu phải dài ít nhất 5 kí tự.",
                equalTo: "Nhập lại mật khẩu không giống nhau."
            }
        },
        submitHandler: function (form) {
            changePassEmail();
        }
    });
    function changePassEmail() {
        let passNew = $('#passnew').val();
        let prepass = $('#prepass').val();

        disable('#btnsubmitchangepass');
        $.ajax({
            url: "/Account/ChangePasswordEmail",
            data: JSON.stringify({ passNew }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        if (data.status == "-1")
                            iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                                message: 'Lỗi hệ thống vui lòng thử lại sau.',
                                position: 'topRight'
                            });
                        else {
                            iziToast.success({
                                timeout: 1500,
                                title: 'Thành công',
                                message: 'Đổi mật khẩu thành công.',
                                position: 'topRight'
                            });
                            $('#btnclosechangepass').click();
                        }
                        enable('#btnsubmitchangepass');

                    },
            error: function (data) {
                enable('#btnsubmitchangepass');
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });
            }
        })
    }
    $('#btnsendagainforgetpass').click(sendAgain);
});