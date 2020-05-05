using BireyselCalisma2.Models;
using BireyselCalisma2.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BireyselCalisma2.Controllers
{
    public class ArticleController : Controller
    {
        bireyselcalisma2Entities db = new bireyselcalisma2Entities(); // vt nin bir örneğini aldı
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            ViewBag.kategorilist = db.Kategori.ToList();
            //bu get metodu
            return View();
        }

        [HttpPost]
        public ActionResult Add(Makale Model)
        {
            try
            {
                //makale ve model dediğimizde bağlamış olduğumuz add.cshtml sayfasındaki model içerisini dolduruyor
                //bu set metodu
                
                db.Makale.Add(Model); //modeli vt ye basma işlemi              
                db.SaveChanges(); //kaydetmeden vt de  gözükmez
            }
            catch (DbEntityValidationException hata) //gerçek hatanın ne olduğunu bu tipte olursa verir...
            {
                foreach (var ht in hata.EntityValidationErrors)
                {
                    Response.Write(ht.Entry.Entity.GetType().Name + " " + ht.Entry.State);
                    foreach (var valerror in ht.ValidationErrors)
                    {
                        Response.Write(valerror.PropertyName + " " + valerror.ErrorMessage);
                    }
                }
            }

            
            return RedirectToAction("Listele");
        }

        public ActionResult Listele(string searching = "")
        {
            //bu get metodu


            var model = db.Makale.Where(x => x.MTitle.Contains(searching) || searching == "")

                .Select(s => new CustomMakale//yeni tip tanımlanmdı   // s her bir makalenin bulunduğu satırı temsil eder.
                {
                    KategoriAd = s.Kategori.categoryType,
                    Makale = (Makale)s  // tabloları eşitleme
                    //new Makale { CategoryID = s.CategoryID, MakaleId = s.MakaleId, MDetail = s.MDetail, MTitle = s.MTitle } // tabloları eşitleme
                })
                .ToList();

            return View(model);//veri çekti view e model olarak veriyi gönderdi.
        }

        public ActionResult Edit(int id)
        {
            //bu get metodu
            var model = db.Makale.Where(s => s.MakaleId == id).FirstOrDefault();// lamda expraition ile verdiğin rasgele s harfinin verdiğin id ile çekilmesi ve FirstOrDefault ile sadece o kaydın listelenemsi
            ViewBag.kategorilist = db.Kategori.ToList();
            return View(model);//veri çekti view e model olarak veriyi gönderdi.
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Makale makale)
        {
            //bu get metodu
            var model = db.Makale.Where(s => s.MakaleId == makale.MakaleId).FirstOrDefault();// lamda expraition ile verdiğin rasgele s harfinin verdiğin modelimizin adı ile çekilmesi ve FirstOrDefault ile sadece o kaydın listelenemsi

            model.MTitle = makale.MTitle;
            model.MDetail = makale.MDetail;
            model.CategoryID = makale.CategoryID;
            db.SaveChanges();
            return RedirectToAction("Edit", "Article"); // return View() yöntemi ile yeni bir request(istek) oluşturmadan ve sayfa URL’sini değiştirmeden belirtilen view’i render etmektedir. Buna karşın RedirectToAction yeni bir request oluşturarak ve Browser’ın address çubunu güncelleyerek çalışmaktadır. Buna benzer şekilde Return Redirect metodu da yeni bir istek oluşturur ve adress barı günceller. 


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //bu get metodu

            var model = db.Makale.Where(s => s.MakaleId == id).FirstOrDefault();// lamda expraition ile verdiğin rasgele s harfinin verdiğin id ile çekilmesi ve FirstOrDefault ile sadece o kaydın listelenemsi

            db.Makale.Remove(model);

            if (db.SaveChanges() > 0)
            {
                return Json("true");
            }
            else
            {
                return Json("false");
            }

        }
    }
}