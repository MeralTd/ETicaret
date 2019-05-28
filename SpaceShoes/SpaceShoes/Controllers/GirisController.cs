using Newtonsoft.Json;
using SpaceShoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SpaceShoes.Controllers
{
    public class GirisController : Controller
    {
        E_TicaretEntities e = new E_TicaretEntities();
        // GET: Giris
        public ActionResult Index()
        {
            return View();
        }

        [Route("Uye-Giris")]
        public ActionResult Giris()
        {
            return View();
        }
        [Route("Uye-Giris")]
        [HttpPost]
        public ActionResult Giris(Kisiler kisi, string sifre)
        {
            var login = e.Kisiler.Where(m => m.Email == kisi.Email).FirstOrDefault();
            if (login.Email == kisi.Email && login.Sifre == kisi.Sifre)
            {
                Session["KisiId"] = login.KisiId;
                Session["KisiAdi"] = login.KisiAdi;
                Session["KisiSoyadi"] = login.KisiSoyadi;
                Session["KisiEmail"] = login.Email;
                Session["KisiTel"] = login.TelNo;
                Session["RolId"] = login.RolId;
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return RedirectToAction("GirisYap", "Kisilers");
            }

        }

        public ActionResult Cikis()
        {
            Session["KisiId"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult logOut()
        {
            FormsAuthentication.SignOut();
            foreach (var cookie in Request.Cookies.AllKeys)
            {
                Request.Cookies.Remove(cookie);
            }
            if (Request.Cookies["KCookie"] != null)
            {
                var c = new HttpCookie("KCookie");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return RedirectToAction("Index", "Login");
        }
       
        /*public ActionResult SignUp(Kisiler user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    e.KullaniciEkle(user.KisiAdi, user.KisiSoyadi, user.KullaniciAdi, user.Email, user.Sifre);
                    return RedirectToAction("../Home/Index", user);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kullanici eklenemedi..");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Alanlar boş geçilemez..");
                return View(user);
            }
            /* var data=veri.Kisiler.Where(x => x.Email == Email && x.Sifre == Sifre).ToList();
             if (data.Count == 1)
             {
                 Session["LoginUser"] = data.FirstOrDefault();
                 return Redirect("/Topuklu");
             }
             else
             {
                 return View();
             }  

        } */
    
    }

}
