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
    public class ResimlersController : Controller
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        // GET: Resimlers
        public ActionResult Index()
        {
            return View(db.Resimler.ToList());
        }

        // GET: Resimlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resimler resimler = db.Resimler.Find(id);
            if (resimler == null)
            {
                return HttpNotFound();
            }
            return View(resimler);
        }

        // GET: Resimlers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resimlers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResimId,BuyukBoy,OrtaBoy,KucukBoy")] Resimler resimler)
        {
            if (ModelState.IsValid)
            {
                db.Resimler.Add(resimler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resimler);
        }

        // GET: Resimlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resimler resimler = db.Resimler.Find(id);
            if (resimler == null)
            {
                return HttpNotFound();
            }
            return View(resimler);
        }

        // POST: Resimlers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResimId,BuyukBoy,OrtaBoy,KucukBoy")] Resimler resimler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resimler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resimler);
        }

        // GET: Resimlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resimler resimler = db.Resimler.Find(id);
            if (resimler == null)
            {
                return HttpNotFound();
            }
            return View(resimler);
        }

        // POST: Resimlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resimler resimler = db.Resimler.Find(id);
            db.Resimler.Remove(resimler);
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
