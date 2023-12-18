using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_Web.Models;
using System.Configuration;
using System.Net.Mail;

namespace DoAn_Web.Controllers
{
    public class CartController : Controller
    {

        public const string cartSession = "cartSession";
        private readonly QLLKDataContext db = new QLLKDataContext();
        public ActionResult Index()
        {
           
            var cart = Session[cartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            string TongTien = list.Sum(x => x.Quantity * (x.Product.DONGIA)).ToString();
            ViewBag.TongTien = TongTien;
            return View(list);
        }
        [HttpPost]
        public JsonResult DeleteItem(int ProductId)
        {
            var cart = Session[cartSession];
            var p = (from P in db.LINHKIENs where P.MALINHKIEN == ProductId select P).FirstOrDefault();
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.MALINHKIEN == ProductId))
                {
                    list.RemoveAll(r => r.Product.MALINHKIEN == ProductId);
                    Session[cartSession] = list;
                    return Json(new
                    {
                        status = 1,
                        sumQuantity = list.Sum(x => x.Quantity),
                        sumMoney = Libary.Instances.ConvertVND(list.Sum(x => x.Quantity *  x.Product.DONGIA).ToString()),
                        message = "Xoá sản phẩm thành công."
                    }
                    );
                }
            }
            return Json(new
            {
                status = 0,
                message = "Có lỗi trong quá trình xoá sản phẩm."
            });

        }
        [HttpPost]
        public JsonResult AddItem(int ProductId, int Quantity)
        {
            if (Session["account_client"] == null)
                return Json(new { status = -1, message = "Mời bạn đăng nhập" });
            else
                try
                {
                    var cart = Session[cartSession];
                    ACCOUNT acc_id = (ACCOUNT)Session["account_client"];
                    var p = (from P in db.LINHKIENs where P.MALINHKIEN == ProductId select P).FirstOrDefault();
                    if (Quantity > p.SOLUONGCON)
                        return Json(new
                        {
                            status = -3,
                            message = "Không đủ hàng"
                        });
                    //tim kiem san pham trong db, Id=1
                    if (cart != null)
                    {//gio da co sp
                        var list = (List<CartItem>)cart;
                        if (list.Exists(x => x.Product.MALINHKIEN == ProductId))
                        {
                            foreach (var item in list)
                            {
                                if (item.Product.MALINHKIEN == ProductId)
                                {
                                    if ((item.Quantity + Quantity) > p.SOLUONGCON)
                                        return Json(new
                                        {
                                            status = -3,
                                            message = "Không đủ hàng"
                                        });
                                    item.Quantity += Quantity;
                                    item.Id_account = (int)(acc_id.Makh);
                                    return Json(new
                                    {
                                        status = 1,
                                        sumQuantity = list.Sum(x => x.Quantity),
                                        message = "Thêm thành công"
                                    });
                                }

                            }
                        }
                        else
                        {
                            var item = new CartItem
                            {
                                Product = p,
                                Quantity = Quantity,
                                Id_account = (int)acc_id.Makh
                            };
                            list.Add(item);
                            Session[cartSession] = list; //save
                            return Json(new
                            {
                                status = 1,
                                sumQuantity = list.Sum(x => x.Quantity),
                                message = "Thêm thành công"
                            });
                        }

                    }
                    else
                    { //gio hang moi
                        var item = new CartItem
                        {
                            Product = p,
                            Quantity = Quantity,
                            Id_account = (int)acc_id.Makh
                        };
                        var list = new List<CartItem>
                        {
                            item
                        };
                        Session[cartSession] = list;//save 
                        return Json(new
                        {
                            status = 1,
                            sumQuantity = Quantity,
                            message = "Thêm thành công"
                        });
                    }
                }
                catch (Exception)
                {
                }
            return Json(new { status = -2, message = "Thêm thất bại" });

        }
        [HttpPost]
        public JsonResult UpdateItem(int ProductId, int Quantity)
        {
            var cart = Session[cartSession];
            var p = (from P in db.LINHKIENs where P.MALINHKIEN == ProductId select P).FirstOrDefault();
            if (Quantity > p.SOLUONGCON)
                return Json(new
                {
                    status = -3,
                    message = "Không đủ hàng"
                });
            decimal? subtotal = 0;
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.MALINHKIEN == ProductId))
                {
                    if (Quantity <= 0)
                        list.RemoveAll(r => r.Product.MALINHKIEN == ProductId);
                    else
                        foreach (var item in list)
                        {
                            if (item.Product.MALINHKIEN == ProductId)
                            {
                                if (Quantity > p.SOLUONGCON)
                                    return Json(new
                                    {
                                        status = -3,
                                        message = "Không đủ hàng"
                                    });
                                item.Quantity = Quantity;
                                subtotal = (decimal?)(Quantity * item.Product.DONGIA);
                            }
                        }
                }

                Session[cartSession] = list;
                return Json(new
                {
                    status = 1,
                    sumQuantity = list.Sum(x => x.Quantity),
                    sumMoney = Libary.Instances.ConvertVND(list.Sum(x => x.Quantity * (x.Product.DONGIA)).ToString()),
                    total = Libary.Instances.ConvertVND(subtotal.ToString())
                });
            }
            return Json(new { status = 0, message = "Có lỗi xảy ra" });


        }

        public ActionResult Checkout()
        {
            List<CartItem> cart = Session["cartSession"] as List<CartItem>;
            ACCOUNT acc = (ACCOUNT)Session["account_client"];
            double? gg = db.LOAIKHs.SingleOrDefault(x => x.MALOAIKH == 
                                db.KHACHHANGs.SingleOrDefault(y => y.Makh == acc.Makh).maloaikh).GIAMGIA;
            var kh = (from K in db.KHACHHANGs where K.Makh == acc.Makh select K).FirstOrDefault();
            string TongTien = (cart.Sum(x => x.Quantity * (x.Product.DONGIA)) * (1 - gg)).ToString();
            ViewBag.TongTien = TongTien;
            ViewBag.ThanhTien = (cart.Sum(x => x.Quantity * (x.Product.DONGIA))).ToString();
            ViewBag.giamgia = (gg * 100).ToString() + "%";
            ViewBag.kh = kh;
            return View();
        }
        [HttpPost]

        public JsonResult Pay(HOADON order, KHACHHANG kh, string email)
        {
            order.NGAYLAP = DateTime.Parse(DateTime.Now.ToShortDateString());
            order.MAKH = (Session["account_client"] as ACCOUNT).Makh;
            order.giamgia = db.LOAIKHs.SingleOrDefault(x => x.MALOAIKH ==
                                db.KHACHHANGs.SingleOrDefault(y => y.Makh == order.MAKH).maloaikh).GIAMGIA;
            var cart = Session[cartSession] as List<CartItem>;
            double? thanhtien = cart.Sum(x => x.Quantity * x.Product.DONGIA);
            order.tongtien = thanhtien * (1 - order.giamgia);
            #region body mail
            string body = @"<!DOCTYPE html>
<html lang='en'>
<head>
  <meta charset='UTF-8'>
  <meta http-equiv='X-UA-Compatible' content='IE=edge'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>Document</title>
  <style>
    body {
      padding: 4px;
      margin: 0;
      box-sizing: border-box;
      font-size: 1.2rem;

    }

    .container {
      border: 1px solid #ccc;

    }

    .table {
      border-top:1px solid #ccc;
      
    }

    .row {
      display: flex;
      justify-content: space-between;
    }

    .row>span {
      width: 20%;
      text-align: center;
    }

    .row span:nth-child(4) {
      width: 10%;

    }

    .row span:nth-child(2) {
      width: 30%;

    }

    .thead span {
      border-bottom: 3px solid #D19C97;
      line-height: 3rem;
    }

    .tbody .row>span {
      display: flex;
      line-height: 1.5rem;
      justify-content: center;
      align-items: center;
    }

    .border_right-none {
      border-right: none !important;
    }

    .row>span {
      border-right: 1px solid #ccc;
    }

    img {
      width: 100%;
    }

    .tbody .row>span>span {
      margin: auto;
    }

    .tbody .row {
      border-bottom: 1px solid #ccc;
    }
    

    .footer {
      padding: 8px;
    display: flex;
    }
    .footer_text{
      font-weight: 600;
      margin-right:auto ;
    }
    .header{
      padding: 4px;
      background: #D19C97;
    }
    .header h1,.header h3,.header h5,.header p{
      text-transform: uppercase;
      padding: 0;
      margin: 0;
      text-align: center;
      color: white;
      margin-bottom: 4px;
    }
    .header_detail{
          color: white;
          display: flex;
          flex-wrap: wrap;
        }
        .header_detail > div{
          width: 50%;
          text-align: center;
        }
  </style>
</head>

<body>
  <div class='container'>
    <div class='header'>
      <h1 >CÔNG TY LINH KIỆN ĐIỆN TỬ</h1>
      <h3>Thông tin đơn hàng</h3>
      <h5 style='margin:0'>Ngày đặt: " + DateTime.Now.ToString() + @"</h5>
      <div class='header_detail'>
        <div>
          <span>Họ tên:</span>
          <span>" + kh.TENKH + @"</span>
        </div>
        <div>
          <span>Email:</span>
          <span>" + email + @"</span>
        </div>       
      </div>
         <div class='header_detail'>        
        <div>
          <span>Số điện thoại:</span>
          <span>" + kh.SDT + @"</span>
        </div>
        <div>
          <span>Địa chỉ nhận hàng:</span>
          <span>" + kh.DIACHI + @"</span>
        </div>
      </div>
    </div>
    <div class='table'>
      <div class='thead'>
        <div class='row'>
          <span>Ảnh</span>
          <span>Tên sản phẩm</span>
          <span>Giá</span>
          <span>Số lượng</span>
          <span class='border_right-none'>Thành tiền</span>
        </div>
      </div>
      <div class='tbody'>";
            #endregion
            db.HOADONs.InsertOnSubmit(order);
            db.SubmitChanges();
            int idorder = (from P in db.HOADONs select P).OrderByDescending(x => x.MAHD).FirstOrDefault().MAHD;
            string gg = order.giamgia * 100 + "%";
            List<string> urlImage = new List<string>();
            int i = 0;
            foreach (var item in cart)
            {
                urlImage.Add("~/Assets/Client/img/" + item.Product.HINHANH.Replace(" ", "%20"));
                CHITIETHD orderdetail = new CHITIETHD
                {
                    MAHD = idorder,
                    MALINHKIEN = item.Product.MALINHKIEN,
                    SOLUONG = item.Quantity,
                    DONGIA = item.Product.DONGIA
                };
                orderdetail.THANHTIEN = orderdetail.DONGIA * orderdetail.SOLUONG;
                #region body mail
                body += @"
                        <div class='row'>
                  <span><img
                      src='cid:" + "image" +i++ + @"'
                      alt='Error'></span>
                  <span><span>" + item.Product.TENLINHKIEN + @"</span></span>
                  <span><span>" + Libary.Instances.ConvertVND(orderdetail.DONGIA.ToString()) + @"</span></span>
                  <span><span>" + item.Quantity + @"</span></span>
                  <span class='border_right-none'><span>" + Libary.Instances.ConvertVND((orderdetail.DONGIA * orderdetail.SOLUONG).ToString()) + @"</span></span>
                </div>";
                #endregion
                db.CHITIETHDs.InsertOnSubmit(orderdetail);
                db.SubmitChanges();
            }
            #region body mail
            body += String.Format(@"</div>
    </div>
    <div class='footer'>
      <span class='footer_text'>
        Tổng sản phẩm
      </span>
      <span>
        {0}
      </span>
    </div>
    <div class='footer'>
      <span class='footer_text'>
        Thành tiền
      </span>
      <span>
        {1}
      </span>
    </div>
    <div class='footer'>
      <span class='footer_text'>
        Giảm giá
      </span>
      <span>
        {2}
      </span>
    </div>
    <div class='footer'>
      <span class='footer_text'>
        Tổng tiền
      </span>
      <span>
       {3}
      </span>
    </div>
    <div class='header'>
      <p>Xin cảm ơn quý khách đã ủng hộ shop.</p>
      <p style='margin:0;'>Hẹn gặp lại quý khách.</p>
      
    </div>
  </div>

</body>

</html>", cart.Sum(x => x.Quantity), Libary.Instances.ConvertVND(thanhtien.ToString()), gg,
                            Libary.Instances.ConvertVND((thanhtien * (1 - order.giamgia)).ToString()));
            #endregion
            Session[cartSession] = null;
            Libary.Instances.SendMail(email, "Thông tin đơn hàng " + idorder, body, urlImage);

            return Json(new
            {
                status = 1
            });


        }
    }
}