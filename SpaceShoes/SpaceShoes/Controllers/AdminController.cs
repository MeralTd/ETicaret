using SpaceShoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceShoes.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        E_TicaretEntities db = new E_TicaretEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Urunler()
        {
            return View(db.Urunler.ToList());
        }
        public ActionResult UrunEkle()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            ViewBag.UrunSatisSayisi = db.Siparisler.Count();
            ViewBag.ToplamUyeKaydiSayisi = db.Kisiler.Count();
            return View();

        }
    }
}