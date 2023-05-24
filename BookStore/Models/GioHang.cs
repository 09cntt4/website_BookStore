using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;


namespace BookStore.Models
{
   
    public class GioHang
    {
        QuanlybansachEntities db = new QuanlybansachEntities();
        public int iMasach { set; get; }
        public String sTensach { set; get; }
        public String sAnhbia { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        public GioHang(int Masach)
        {
            iMasach = Masach;
            SACH sach=db.SACHes.Single(n=>n.Masach==iMasach);
            sTensach = sach.Tensach;
            sAnhbia= sach.Anhbia;
            dDongia=Double.Parse(sach.Giaban.ToString());
            iSoluong = 1;

        }

    }
}