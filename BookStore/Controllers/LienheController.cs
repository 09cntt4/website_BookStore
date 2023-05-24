using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class LienheController : Controller
    {
        QuanlybansachEntities db = new QuanlybansachEntities();
        // GET: Lienhe
        public ActionResult Lienhe()
        {
            return View();
        }
    }
}