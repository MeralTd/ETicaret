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
    public class SepetsController : Controller
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        // GET: Sepets
        public ActionResult Index()
        {
            var sepet = db.Sepet.Include(s => s.Urunler);
            return View(sepet.ToList());
        }

        // GET: Sepets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet = db.Sepet.Find(id);
            if (sepet == null)
            {
                return HttpNotFound();
            }
            return View(sepet);
        }

        // GET: Sepets/Create
        public ActionResult Create()
        {
            ViewBag.UrunId = new SelectList(db.Urunler, "UrunId", "UrunAdi");
            return View();
        }

        // POST: Sepets/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SepetId,UrunId,Adet,EklenmeTarihi,Tutar")] Sepet sepet)
        {
            if (ModelState.IsValid)
            {
                db.Sepet.Add(sepet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UrunId = new SelectList(db.Urunler, "UrunId", "UrunAdi", sepet.UrunId);
            return View(sepet);
        }

        // GET: Sepets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet = db.Sepet.Find(id);
            if (sepet == null)
            {
                return HttpNotFound();
            }
            ViewBag.UrunId = new SelectList(db.Urunler, "UrunId", "UrunAdi", sepet.UrunId);
            return View(sepet);
        }

        // POST: Sepets/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SepetId,UrunId,Adet,EklenmeTarihi,Tutar")] Sepet sepet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sepet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UrunId = new SelectList(db.Urunler, "UrunId", "UrunAdi", sepet.UrunId);
            return View(sepet);
        }

        // GET: Sepets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet = db.Sepet.Find(id);
            if (sepet == null)
            {
                return HttpNotFound();
            }
            return View(sepet);
        }

        // POST: Sepets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sepet sepet = db.Sepet.Find(id);
            db.Sepet.Remove(sepet);
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
