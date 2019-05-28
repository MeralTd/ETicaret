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
    public class KisilersController : Controller
    {
        private E_TicaretEntities db = new E_TicaretEntities();

        // GET: Kisilers
        public ActionResult Index()
        {
            var kisiler = db.Kisiler.Include(k => k.Iletisim);
            return View(kisiler.ToList());
        }
        public ActionResult GirisYap()
        {
            return View();
        }
        public ActionResult Hesap()
        {
            return View();
        }

        public ActionResult favori()
        {
            int kisi = Convert.ToInt32(Session["KisiId"]);
            var fav = db.Favoriler.Where(m => m.KisiId == kisi);
            return View(fav.ToList());
        }

        public ActionResult FavoriEkle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Favoriler favori = new Favoriler();
                favori.KisiId = Convert.ToInt32(Session["KisiId"]);
                Urunler urun = db.Urunler.Find(id);
                favori.UrunId = urun.UrunId;
                db.Favoriler.Add(favori);
                db.SaveChanges();
                Response.Write("alert('Favorilere Eklendi')");
                return RedirectToAction("favori");
            }
        }
        public ActionResult FavoriSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favoriler fa = db.Favoriler.Find(id); ;
            fa.KisiId = Convert.ToInt32(Session["KisiId"]);
            db.Favoriler.Remove(fa);
            db.SaveChanges();
            return RedirectToAction("favori");
        }
        public ActionResult sepet()
        {
           int kisi = Convert.ToInt32(Session["KisiId"]);
            var sep = db.Sepet.Where(m => m.KisiId == kisi);
            VMBasket vmBasket = new VMBasket();
            foreach (var item in sep.ToList())
            {
                vmBasket.Urun.Add(new VMUrun {
                    UrunAdi=item.Urunler.UrunAdi,
                    SatisFiyati=item.Urunler.SatisFiyat,
                    Resim=item.Urunler.Resimler.BuyukBoy
                });
                vmBasket.Toplam += item.Urunler.SatisFiyat;
            }
            vmBasket.SepetId = ((Sepet)sep).SepetId;
            return View(vmBasket);
        }
        public ActionResult Ziyaretsepet()
        {
            int? kisi = null;
            var sep = db.Sepet.Where(m => m.KisiId == kisi );
            return View(sep.ToList());
        }

        public ActionResult SepetEkle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Sepet sepet = new Sepet();
                sepet.KisiId = Convert.ToInt32(Session["KisiId"]);
                Urunler urun = db.Urunler.Find(id);
                //Urunler urun = new Urunler();
                sepet.UrunId = urun.UrunId;
                sepet.EklenmeTarihi = DateTime.Now;
                sepet.Tutar = urun.SatisFiyat;
                db.Sepet.Add(sepet);
                db.SaveChanges();
                Response.Write("alert('Sepete Eklendi')");
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult ZiyaretSepetEkle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Sepet sepet = new Sepet();
                sepet.KisiId = null;
                Urunler urun = db.Urunler.Find(id);
                //Urunler urun = new Urunler();
                sepet.UrunId = urun.UrunId;
                sepet.EklenmeTarihi = DateTime.Now;
                sepet.Tutar = urun.SatisFiyat;
                db.Sepet.Add(sepet);
                db.SaveChanges();
                Response.Write("alert('Sepete Eklendi')");
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult SepetSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet = db.Sepet.Find(id); ;
            sepet.KisiId = Convert.ToInt32(Session["KisiId"]);
            db.Sepet.Remove(sepet);
            db.SaveChanges();
            return RedirectToAction("sepet");
        }
        public ActionResult ZiyaretSepetSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet = db.Sepet.Find(id); ;
            sepet.KisiId = null;
            db.Sepet.Remove(sepet);
            db.SaveChanges();
            return RedirectToAction("Ziyaretsepet");
        }
        public ActionResult SiparisEkle(int? kisi)
        {
            if (kisi == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparisler siparis = new Siparisler();
            Kisiler ki = db.Kisiler.Find(kisi);
            siparis.KisiId = Convert.ToInt32(kisi);
            siparis.SiparisDurum = true;
            siparis.SiparisTarihi = DateTime.Now;
            siparis.SiparisTutar = Convert.ToInt32(44);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult guncelle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisiler kisiler = db.Kisiler.Find(id);
            if (kisiler == null)
            {
                return HttpNotFound();
            }

            ViewBag.KisiId = new SelectList(db.Iletisim, "KisiId", "GelenMesaj", kisiler.KisiId);

            return View(kisiler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult guncelle([Bind(Include = "KisiId,KisiAdi,KisiSoyadi,TelNo,Email,Sifre,KullaniciAdi")] Kisiler kisiler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kisiler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.KisiId = new SelectList(db.Iletisim, "KisiId", "GelenMesaj", kisiler.KisiId);

            return View(kisiler);
        }

        // GET: Kisilers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisiler kisiler = db.Kisiler.Find(id);
            if (kisiler == null)
            {
                return HttpNotFound();
            }
            return View(kisiler);
        }

        // GET: Kisilers/Create
        public ActionResult Create()
        {
           
            ViewBag.KisiId = new SelectList(db.Iletisim, "KisiId", "GelenMesaj");
            
            return View();
        }

        // POST: Kisilers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KisiId,KisiAdi,KisiSoyadi,TelNo,Email,Sifre,KullaniciAdi")] Kisiler kisiler)
        {
            if (ModelState.IsValid)
            {
                db.Kisiler.Add(kisiler);
                db.SaveChanges();
                return RedirectToAction("GirisYap");
            }

            
            ViewBag.KisiId = new SelectList(db.Iletisim, "KisiId", "GelenMesaj", kisiler.KisiId);
            
            return View(kisiler);
        }

       
        // GET: Kisilers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisiler kisiler = db.Kisiler.Find(id);
            if (kisiler == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.KisiId = new SelectList(db.Iletisim, "KisiId", "GelenMesaj", kisiler.KisiId);
            
            return View(kisiler);
        }

        // POST: Kisilers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KisiId,KisiAdi,KisiSoyadi,TelNo,Email")] Kisiler kisiler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kisiler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.KisiId = new SelectList(db.Iletisim, "KisiId", "GelenMesaj", kisiler.KisiId);
            
            return View(kisiler);
        }

        // GET: Kisilers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisiler kisiler = db.Kisiler.Find(id);
            if (kisiler == null)
            {
                return HttpNotFound();
            }
            return View(kisiler);
        }

        // POST: Kisilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kisiler kisiler = db.Kisiler.Find(id);
            db.Kisiler.Remove(kisiler);
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
