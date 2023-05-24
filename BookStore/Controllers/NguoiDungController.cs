using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{

    public class NguoiDungController : Controller
    {
        QuanlybansachEntities db = new QuanlybansachEntities();
        // GET: NguoiDung
     
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {

                //them đữ liệu vào csdl
                db.KHACHHANGs.Add(kh);
                //luu vao csdl
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]

        public ActionResult DangNhap(FormCollection collection)
        {
            //String Tendn = collection["TenDN"].ToString();
            //String MatKhau = collection["Matkhau"].ToString();
            var Tendn = collection["TenDN"].ToString();
            var MatKhau = collection["Matkhau"].ToString();


            if (String.IsNullOrEmpty(Tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(MatKhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(n => n.Taikhoan == Tendn && n.Matkhau == MatKhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng bạn đã đăng nhập thành công!";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "BookStore");

                }
                else
                {
                    ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không chính xác!";

                }
                return View();
            }
            return View();

        }
       
    }
}

