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
    public class FavorilersController : Controller
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        // GET: Favorilers
        public ActionResult favori()
        {
            var favoriler = db.Favoriler.Include(f => f.Kisiler).Include(f => f.Urunler);
            return View(favoriler.ToList());
        }

        /*[Route("Favori")]
        public ActionResult Giris()
        {
            return View();
        }
        [Route("Favori")]
        [HttpPost]
        public ActionResult FavoriEkles(int UrunId, int KisiId)
        {
            db.Favoriler.Add(new Favoriler()
            {
                UrunId = UrunId,
                KisiId = KisiId,
            });
            db.SaveChanges();
            return Content("ok");
        }
        
            public ActionResult FavoriEkle(int UrunId, int KisiId)
        {
            db.Favoriler.Add(new Favoriler()
            {
                UrunId = UrunId,
                KisiId = KisiId,
            });
            db.SaveChanges();
            return Content("ok");
        }
        */
        // GET: Favorilers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favoriler favoriler = db.Favoriler.Find(id);
            if (favoriler == null)
            {
                return HttpNotFound();
            }
            return View(favoriler);
        }

        // GET: Favorilers/Create
        public ActionResult Create(int KisiId, int UrunId)
        {
            ViewBag.KisiId = new SelectList(db.Kisiler, "KisiId", "KisiAdi");
            ViewBag.UrunId = new SelectList(db.Urunler, "UrunId", "UrunAdi");
            return View();
        }

        // POST: Favorilers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FavoriId,UrunId,KisiId")] Favoriler favoriler)
        {
            if (ModelState.IsValid)
            {
                db.Favoriler.Add(favoriler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KisiId = new SelectList(db.Kisiler, "KisiId", "KisiAdi", favoriler.KisiId);
            ViewBag.UrunId = new SelectList(db.Urunler, "UrunId", "UrunAdi", favoriler.UrunId);
            return View(favoriler);
        }

        // GET: Favorilers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favoriler favoriler = db.Favoriler.Find(id);
            if (favoriler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KisiId = new SelectList(db.Kisiler, "KisiId", "KisiAdi", favoriler.KisiId);
            ViewBag.UrunId = new SelectList(db.Urunler, "UrunId", "UrunAdi", favoriler.UrunId);
            return View(favoriler);
        }

        // POST: Favorilers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FavoriId,UrunId,KisiId")] Favoriler favoriler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favoriler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KisiId = new SelectList(db.Kisiler, "KisiId", "KisiAdi", favoriler.KisiId);
            ViewBag.UrunId = new SelectList(db.Urunler, "UrunId", "UrunAdi", favoriler.UrunId);
            return View(favoriler);
        }

        // GET: Favorilers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favoriler favoriler = db.Favoriler.Find(id);
            if (favoriler == null)
            {
                return HttpNotFound();
            }
            return View(favoriler);
        }

        // POST: Favorilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Favoriler favoriler = db.Favoriler.Find(id);
            db.Favoriler.Remove(favoriler);
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
