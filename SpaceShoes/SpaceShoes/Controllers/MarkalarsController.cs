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
    public class MarkalarsController : Controller
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        // GET: Markalars
        public ActionResult Index()
        {
            return View(db.Markalar.ToList());
        }

        // GET: Markalars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markalar markalar = db.Markalar.Find(id);
            if (markalar == null)
            {
                return HttpNotFound();
            }
            return View(markalar);
        }

        // GET: Markalars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Markalars/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarkaId,MarkaAdi")] Markalar markalar)
        {
            if (ModelState.IsValid)
            {
                db.Markalar.Add(markalar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(markalar);
        }

        // GET: Markalars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markalar markalar = db.Markalar.Find(id);
            if (markalar == null)
            {
                return HttpNotFound();
            }
            return View(markalar);
        }

        // POST: Markalars/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarkaId,MarkaAdi")] Markalar markalar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markalar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(markalar);
        }

        // GET: Markalars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markalar markalar = db.Markalar.Find(id);
            if (markalar == null)
            {
                return HttpNotFound();
            }
            return View(markalar);
        }

        // POST: Markalars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Markalar markalar = db.Markalar.Find(id);
            db.Markalar.Remove(markalar);
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
