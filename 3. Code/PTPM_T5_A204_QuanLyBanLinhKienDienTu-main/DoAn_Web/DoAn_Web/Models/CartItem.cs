using DoAn_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_Web.Models
{
    public class CartItem
    {
        public LINHKIEN Product { get; set; }
        public int Quantity { get; set; }
        public int Id_account { get; set; }
    }
}