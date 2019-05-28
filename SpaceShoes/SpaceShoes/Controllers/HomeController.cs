using SpaceShoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceShoes.Controllers
{
    public class HomeController : Controller
    {
        E_TicaretEntities db = new E_TicaretEntities();
        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult UrunAra(string Ara = null)
        {
            var aranan = db.Urunler.Where(x => x.UrunAdi.Contains(Ara)).ToList();
            return View(aranan);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}