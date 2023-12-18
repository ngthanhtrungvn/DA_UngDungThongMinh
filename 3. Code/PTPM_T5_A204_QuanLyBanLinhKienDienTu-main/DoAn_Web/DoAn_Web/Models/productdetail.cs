using DoAn_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_Web.Models
{
    public class Productdetail
    {
        public LINHKIEN Product { get; set; }
        public List<LINHKIEN> LstProducts_Categories { get; set; }
    }
}