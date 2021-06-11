using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusterıController : Controller
    {
        // GET: Musterı
        MvcDbStokEntities1 db = new MvcDbStokEntities1();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBMUSTERILER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.TBMUSTERILER.ToList();
           // return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusterı()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusterı(TBMUSTERILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusterı");
            }
            db.TBMUSTERILER.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var musterı= db.TBMUSTERILER.Find(id);
            db.TBMUSTERILER.Remove(musterı);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mstr = db.TBMUSTERILER.Find(id);
            return View("MusteriGetir", mstr);
        }
        public ActionResult Guncelle(TBMUSTERILER p1)
        {
            var musterı = db.TBMUSTERILER.Find(p1.MUSTERIID);
            musterı.MUSTERIAD = p1.MUSTERIAD;
            musterı.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}