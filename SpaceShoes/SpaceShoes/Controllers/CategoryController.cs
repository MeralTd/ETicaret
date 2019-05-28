using SpaceShoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceShoes.Controllers
{
    public class CategoryController : Controller
    {
        private E_TicaretEntities db =new E_TicaretEntities();
        // GET: Category
        public ActionResult Index(int id)
        {
            var Urunler = db.Urunler.Where(x => x.KategoriId == id);
            return View(Urunler.ToList());
        }
    }
}