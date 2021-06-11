using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities1 db = new MvcDbStokEntities1();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBKATEGORILER.ToList();
            var degerler = db.TBKATEGORILER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(TBKATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBKATEGORILER.Add(p1);
            db.SaveChanges();
            return View();

        }
        public ActionResult SIL(int id)
        {
            var kategori = db.TBKATEGORILER.Find(id);
            db.TBKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBKATEGORILER.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult Guncelle(TBKATEGORILER p1)
        {
            var ktg = db.TBKATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}