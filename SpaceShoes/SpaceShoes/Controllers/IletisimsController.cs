using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SpaceShoes.Models;

namespace SpaceShoes.Controllers
{
    public class IletisimsController : Controller
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        // GET: Iletisims
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Iletisim model)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("Ad: " + model.Ad);
                body.AppendLine("Soyad: " + model.Soyad);
                body.AppendLine("Tel: " + model.TelNo);
                body.AppendLine("Eposta: " + model.EMail);
                body.AppendLine("Mesaj: " + model.Mesaj);
                Gmail.SendMail(body.ToString());
                ViewBag.Success = true;
            }
            return View();
        }

        // GET: Iletisims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // GET: Iletisims/Create
        public ActionResult Create()
        {
            ViewBag.KisiId = new SelectList(db.Kisiler, "KisiId", "KisiAdi");
            return View();
        }

        // POST: Iletisims/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GelenMesaj,GidenMesaj,KisiId")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                db.Iletisim.Add(iletisim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KisiId = new SelectList(db.Kisiler, "KisiId", "KisiAdi", iletisim.KisiId);
            return View(iletisim);
        }

        // GET: Iletisims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            ViewBag.KisiId = new SelectList(db.Kisiler, "KisiId", "KisiAdi", iletisim.KisiId);
            return View(iletisim);
        }

        // POST: Iletisims/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GelenMesaj,GidenMesaj,KisiId")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iletisim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KisiId = new SelectList(db.Kisiler, "KisiId", "KisiAdi", iletisim.KisiId);
            return View(iletisim);
        }

        // GET: Iletisims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = db.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // POST: Iletisims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Iletisim iletisim = db.Iletisim.Find(id);
            db.Iletisim.Remove(iletisim);
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
