using BireyselCalisma2.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BireyselCalisma2.Controllers
{
    public class CategoryController : Controller
    {
        bireyselcalisma2Entities db = new bireyselcalisma2Entities(); // vt nin bir örneğini aldı

        // GET: Category
        public ActionResult Index(string searching = "")
        {
            //bu get metodu   

            var model = db.Kategori.Where(x => x.categoryType.Contains(searching) || searching == "").ToList();

            
            ViewBag.Kategori = model;
            return View(model);//veri çekti view e model olarak veriyi gönderdi.           
        }

        public ActionResult Add()
        {
            //bu get metodu   
            return View();//veri çekti view e model olarak veriyi gönderdi.           
        }
        [HttpPost]
        public ActionResult Add(Kategori model)
        {
            
            db.Kategori.Add(model); //modeli vt ye basma işlemi
            db.SaveChanges(); //kaydetmeden vt de  gözükmez

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //bu get metodu
            var model = db.Kategori.Where(s => s.CategoryID == id).FirstOrDefault();// lamda expraition ile verdiğin rasgele s harfinin verdiğin id ile çekilmesi ve FirstOrDefault ile sadece o kaydın listelenemsi
            db.Kategori.Remove(model);
            if (db.SaveChanges() > 0)
            {
                return Json("true");
            }
            else
            {
                return Json("false");
            }

        }

        public ActionResult Edit(int id)
        {
            //bu get metodu
            var model = db.Kategori.Where(s => s.CategoryID == id).FirstOrDefault();// lamda expraition ile verdiğin rasgele s harfinin verdiğin id ile çekilmesi ve FirstOrDefault ile sadece o kaydın listelenemsi


           
            return View(model);//veri çekti view e model olarak veriyi gönderdi.
        }
        [HttpPost]
        public ActionResult Edit(Kategori kategori)
        {
            //bu get metodu
            var model = db.Kategori.Where(s => s.CategoryID == kategori.CategoryID).FirstOrDefault();// lamda expraition ile verdiğin rasgele s harfinin verdiğin modelimizin adı ile çekilmesi ve FirstOrDefault ile sadece o kaydın listelenemsi


            model.categoryType = kategori.categoryType;
            db.SaveChanges();
            return View(model);//veri çekti view e model olarak veriyi gönderdi.
        }



    }
}