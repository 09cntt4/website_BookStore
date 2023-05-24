using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PagedList;
using PagedList.Mvc;


namespace BookStore.Controllers
{
    public class AdminController : Controller
    {

        QuanlybansachEntities db = new QuanlybansachEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QuanLySanPham(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 20;//cho mooxi trang 5quyển

            return View(db.SACHes.ToList().OrderBy(n => n.Tensach).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            //gasnws giá trị ngươi dung
            var Tendn = f["UserAdmin"].ToString();
            var MatKhau = f["PassAdmin"].ToString();


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
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == Tendn && n.PassAdmin == MatKhau);
                if (ad != null)
                {
                    //ViewBag.ThongBao = "Chúc mừng bạn đã đăng nhập thành công!";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không chính xác!";

                }

            }
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult ThemMoiSach()
        {

            //đưa dữ liệu vào
            //lấy ds từ table chude,sắp xếp tăng dần theo tên chử đề
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "Tenchude");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiSach(SACH sach, HttpPostedFileBase fileupload)
        {
            //đưa dữ liệu vào dropdownlist
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "Tenchude");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            if (fileupload == null)
            {
                ViewBag.Thongbao = "vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //lưu tên file,thêm thư viện using system.io
                    var fileName = Path.GetFileName(fileupload.FileName);
                    //luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/content/img"), fileName);
                    //ktra xem sanpham co ton tai chua
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        //luu hinh anrh vao duong dan
                        fileupload.SaveAs(path);
                    }
                    sach.Anhbia = fileName;
                    //luu vào csdl
                    db.SACHes.Add(sach);
                    db.SaveChanges();

                }

                return RedirectToAction("QuanLySanPham");
            }
        }

        public ActionResult Chitietsach(int id)
        {
            //lay ra đối tượng sách theo mac
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            return View(sach);
        }
        public ActionResult Xoasach(int id)
        {
            //lẤY RA DỐI TƯỢNG DANH SÁCH CẦN XÓA
            SACH sach=db.SACHes.SingleOrDefault(n=>n.Masach==id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost,ActionName("Xoasach")]
        public ActionResult Xacnhanxoa(int id)
        {
            //lẤY RA DỐI TƯỢNG DANH SÁCH CẦN XÓA
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SACHes.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("QuanLySanPham");
        }
        public ActionResult Suasach(int id)
        {
            //đưa dữ liệu vào dropdownlist
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "Tenchude");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            //lẤY RA DỐI TƯỢNG DANH SÁCH CẦN XÓA
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(sach);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasach(SACH sach, HttpPostedFileBase fileupload)
        {
            //đưa dữ liệu vào dropdownlist
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "Tenchude");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            if (fileupload == null)
            {
                ViewBag.Thongbao = "vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //lưu tên file,thêm thư viện using system.io
                    var fileName = Path.GetFileName(fileupload.FileName);
                    //luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/content/img"), fileName);
                    //ktra xem sanpham co ton tai chua
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        //luu hinh anrh vao duong dan
                        fileupload.SaveAs(path);
                    }
                    sach.Anhbia = fileName;
                    //luu vào csdl
                    UpdateModel(sach);
                  
                    db.SaveChanges();

                }

                return RedirectToAction("QuanLySanPham");
            }
        }
    
    }
}