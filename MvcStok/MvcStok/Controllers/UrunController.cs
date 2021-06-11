using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities1 db = new MvcDbStokEntities1();
        public ActionResult Index()
        {
            var degerler = db.TBURUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TBKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TBURUNLER p2)
        {
            var ktg = db.TBKATEGORILER.Where(m => m.KATEGORIID == p2.TBKATEGORILER.KATEGORIID).FirstOrDefault();
            p2.TBKATEGORILER = ktg;
            db.TBURUNLER.Add(p2);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBURUNLER.Find(id);
            db.TBURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBURUNLER p)
        {
            var urun = db.TBURUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
           // urun.URUNKATEGORI = p.URUNKATEGORI;
            urun.MARKA = p.MARKA;
            urun.STOK = p.STOK;
            urun.FIYAT = p.FIYAT;
            var ktg = db.TBKATEGORILER.Where(m => m.KATEGORIID == p.TBKATEGORILER.KATEGORIID).FirstOrDefault();
           urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}