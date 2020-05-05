using BireyselCalisma2.Models;
using BireyselCalisma2.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;


public class HomeController : Controller
{
    bireyselcalisma2Entities db = new bireyselcalisma2Entities(); // vt nin bir örneğini aldı
    public ActionResult Index()
    {
        //var model = db.Kategori.Where(x => x.categoryType.Contains(searching) || searching == "").ToList();
        var model = db.Kullanıcı.Where(x => x.kAdı == "").ToList();

        ViewBag.Kullanıcı = model;

        return View();
    }

}