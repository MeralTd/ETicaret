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
    public class KategorilersController : Controller
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        // GET: Kategorilers
        public ActionResult Index()
        {
            var kategoriler = db.Kategoriler.Include(k => k.Kampanyalar);
            return View(kategoriler.ToList());
        }
  
        // GET: Kategorilers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // GET: Kategorilers/Create
        public ActionResult Create()
        {
            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi");
            return View();
        }

        // POST: Kategorilers/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KategoriId,KategoriAdi,KampanyaId")] Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                db.Kategoriler.Add(kategoriler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi", kategoriler.KampanyaId);
            return View(kategoriler);
        }

        // GET: Kategorilers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi", kategoriler.KampanyaId);
            return View(kategoriler);
        }

        // POST: Kategorilers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategoriId,KategoriAdi,KampanyaId")] Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategoriler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KampanyaId = new SelectList(db.Kampanyalar, "KampanyaId", "KampanyaAdi", kategoriler.KampanyaId);
            return View(kategoriler);
        }

        // GET: Kategorilers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // POST: Kategorilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            db.Kategoriler.Remove(kategoriler);
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
