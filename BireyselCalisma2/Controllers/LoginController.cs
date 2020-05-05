using BireyselCalisma2.Models;
using BireyselCalisma2.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BireyselCalisma2.Controllers
{
    [Authorize] //logine ait tüm metotları bloklar login olmadan işlem yapamaması için kullanırlır.
    public class LoginController : Controller
    {

        bireyselcalisma2Entities db = new bireyselcalisma2Entities();
        // GET: Login

        [AllowAnonymous]//izin
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)//login mi diye kontrol eder.
            {
                return RedirectToAction("Index", "Home");
            }
            //ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index", "Dashboard");   hangi sayfadan logine tıkladıysan o sayfaya geri dönmesi için kullanılacak kod

            return View();
        }

        [AllowAnonymous]//izin
        [HttpPost]
        public ActionResult Login(CustomKullanici model)
        {
            
            
            if (ModelState.IsValid)
            {

                
                //Aşağıdaki if komutu gönderilen mail ve şifre doğrultusunda kullanıcı kontrolu yapar. Eğer kullanıcı var ise login olur.

                var _kullanici = db.Kullanıcı.FirstOrDefault(a => a.kAdı == model.Kullanıcı.kAdı && a.ksifre == model.Kullanıcı.ksifre);

                if (_kullanici != null)
                {
                    FormsAuthentication.SetAuthCookie(_kullanici.kAdı, true);
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı veya şifre hatalı!");
                }
            }


            return View();
        }
        //şuan bu bloklu
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


    }
}