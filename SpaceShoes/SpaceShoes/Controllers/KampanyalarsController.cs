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
    public class KampanyalarsController : Controller
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        // GET: Kampanyalars
        public ActionResult Index()
        {
            var kampanyalar = db.Kampanyalar.Include(k => k.Kategoriler);
            return View(kampanyalar.ToList());
        }

        // GET: Kampanyalars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kampanyalar kampanyalar = db.Kampanyalar.Find(id);
            if (kampanyalar == null)
            {
                return HttpNotFound();
            }
            return View(kampanyalar);
        }

        // GET: Kampanyalars/Create
        public ActionResult Create()
        {
            ViewBag.KampanyaId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi");
            return View();
        }

        // POST: Kampanyalars/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KampanyaId,KampanyaAdi,IndirimOrani,BaslangicTarihi,BitisTarihi")] Kampanyalar kampanyalar)
        {
            if (ModelState.IsValid)
            {
                db.Kampanyalar.Add(kampanyalar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KampanyaId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", kampanyalar.KampanyaId);
            return View(kampanyalar);
        }

        // GET: Kampanyalars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kampanyalar kampanyalar = db.Kampanyalar.Find(id);
            if (kampanyalar == null)
            {
                return HttpNotFound();
            }
            ViewBag.KampanyaId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", kampanyalar.KampanyaId);
            return View(kampanyalar);
        }

        // POST: Kampanyalars/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KampanyaId,KampanyaAdi,IndirimOrani,BaslangicTarihi,BitisTarihi")] Kampanyalar kampanyalar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kampanyalar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KampanyaId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", kampanyalar.KampanyaId);
            return View(kampanyalar);
        }

        // GET: Kampanyalars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kampanyalar kampanyalar = db.Kampanyalar.Find(id);
            if (kampanyalar == null)
            {
                return HttpNotFound();
            }
            return View(kampanyalar);
        }

        // POST: Kampanyalars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kampanyalar kampanyalar = db.Kampanyalar.Find(id);
            db.Kampanyalar.Remove(kampanyalar);
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

