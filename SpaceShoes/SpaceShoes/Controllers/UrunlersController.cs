using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpaceShoes.Models;

namespace SpaceShoes.Controllers
{
    public class UrunlersController : Controller
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        // GET: Urunlers
        public ActionResult Index()
        {
            var urunler = db.Urunler.Include(u => u.Kampanyalar).Include(u => u.Kategoriler).Include(u => u.Markalar).Include(u => u.Resimler);
            return View(urunler.ToList());
        }
        public ActionResult Topuklu()
        {
            var data = db.Urunler.OrderByDescending(x => x.KategoriId).ToList();
            return View(data);
        }
        public ActionResult GetProduct(int id)
        {
            var product = db.Urunler.Where(p => p.UrunId == id).Take(1).ToList();
            return View("GetProduct", product);
        }
        // GET: Urunlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunlers/Create
        public ActionResult Create()
        {
            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi");
            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi");
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi");
            ViewBag.MarkaId = new SelectList(db.Markalar, "MarkaId", "MarkaAdi");
            ViewBag.ResimId = new SelectList(db.Resimler, "ResimId", "BuyukBoy");
            return View();
        }

        // POST: Urunlers/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunId,UrunAdi,SatisFiyat,UrunAciklama,KategoriId,MarkaId,ResimId,KampanyaId")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                db.Urunler.Add(urunler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi", urunler.KampanyaId);
            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi", urunler.KampanyaId);
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", urunler.KategoriId);
            ViewBag.MarkaId = new SelectList(db.Markalar, "MarkaId", "MarkaAdi", urunler.MarkaId);
            ViewBag.ResimId = new SelectList(db.Resimler, "ResimId", "BuyukBoy", urunler.ResimId);
            return View(urunler);
        }

        // GET: Urunlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi", urunler.KampanyaId);
            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi", urunler.KampanyaId);
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", urunler.KategoriId);
            ViewBag.MarkaId = new SelectList(db.Markalar, "MarkaId", "MarkaAdi", urunler.MarkaId);
            ViewBag.ResimId = new SelectList(db.Resimler, "ResimId", "BuyukBoy", urunler.ResimId);
            return View(urunler);
        }

        // POST: Urunlers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunId,UrunAdi,SatisFiyat,UrunAciklama,KategoriId,MarkaId,ResimId,KampanyaId")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi", urunler.KampanyaId);
            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi", urunler.KampanyaId);
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", urunler.KategoriId);
            ViewBag.MarkaId = new SelectList(db.Markalar, "MarkaId", "MarkaAdi", urunler.MarkaId);
            ViewBag.ResimId = new SelectList(db.Resimler, "ResimId", "BuyukBoy", urunler.ResimId);
            return View(urunler);
        }

        // GET: Urunlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: Urunlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
