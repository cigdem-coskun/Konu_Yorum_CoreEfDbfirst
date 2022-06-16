using Konu_Yorum_CoreEfDbfirst.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Konu_Yorum_CoreEfDbfirst.Controllers
{
    public class YorumController : Controller
    {
        private BA_KonuYorumCoreContext _dbcontext = new BA_KonuYorumCoreContext();
        public IActionResult Index()
        {
            List<Yorum> yorumlar = _dbcontext.Yorum.Include(yorum=>yorum.Konu).OrderBy(p=>p.Puan).ThenBy(yorum=>yorum.Yorumcu).ToList();

            return View(yorumlar);
        }

        public IActionResult Details(int id)
        {
            Yorum yorum = _dbcontext.Yorum.SingleOrDefault(p=>p.Id==id);
            return View(yorum);
        }

        public IActionResult Create()
        {
            List<Konu>konular= _dbcontext.Konu.OrderBy(konu=>konu.Baslik).ToList();
         ViewBag.Konular= new SelectList(konular,"Id","Baslik");//DropDowlist
            return View();
        }

        [HttpPost]
        public IActionResult Create(Yorum yorum)
        {
            if (string.IsNullOrWhiteSpace(yorum.Icerik) )
            {
                ViewBag.Mesaj = "içerik boş geçilemez";
                ViewBag.KonuId = new SelectList(_dbcontext.Konu.OrderBy(k=>k.Baslik).ToList(),"Id","Baslik",yorum.KonuId);
                return View(yorum);

            }
            if (yorum.Icerik.Length>500)
            {
                ViewBag.Mesaj = "İçerik en fazla 100 karakter olmalıdır";
                return View(yorum);

            }

            if (string.IsNullOrWhiteSpace(yorum.Yorumcu))
            {
                ViewBag.Mesaj = "yorumcu boş geçilemez";
                return View(yorum);

            }
            if (yorum.Yorumcu.Length > 50)
            {
                ViewBag.Mesaj = "Yorumcu en fazla 50 karakter olmalıdır";
                return View(yorum);

            }

            //if (yorum.Puan!=null)
            if(yorum.Puan.HasValue)
            {
                if (yorum.Puan.Value>=5 || yorum.Puan.Value<=1)
                {
                    ViewBag.Mesaj = "puan 1-5 arasında olmalıdır.";
                    return View(yorum);
                }

            }
            _dbcontext.Yorum.Add(yorum);
            _dbcontext.SaveChanges();
            TempData["YorumMesaj"] = "Yorum başarıyla eklendi";
            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id )
        {
            Yorum yorum = _dbcontext.Yorum.SingleOrDefault(P=>P.Id==id);
            ViewBag.KonuId = new SelectList(_dbcontext.Konu.OrderBy(konu=>konu.Baslik).ToList(),"Id","Baslik",yorum.KonuId);//DropDowlis
            return View(yorum);

        }
        [HttpPost]
        public IActionResult Edit(Yorum yorum)
        {
            if (string.IsNullOrWhiteSpace(yorum.Icerik))
            {
                ViewBag.Mesaj = "içerik boş geçilemez";
                ViewBag.KonuId = new SelectList(_dbcontext.Konu.OrderBy(k => k.Baslik).ToList(), "Id", "Baslik", yorum.KonuId);
                return View(yorum);
            }
            if (yorum.Icerik.Length > 500)
            {
                ViewBag.Mesaj = "İçerik en fazla 100 karakter olmalıdır";
                return View(yorum);
            }
            if (string.IsNullOrWhiteSpace(yorum.Yorumcu))
            {
                ViewBag.Mesaj = "yorumcu boş geçilemez";
                return View(yorum);
            }
            if (yorum.Yorumcu.Length > 50)
            {
                ViewBag.Mesaj = "Yorumcu en fazla 50 karakter olmalıdır";
                return View(yorum);
            }
            //if (yorum.Puan!=null)
            if (yorum.Puan.HasValue)
            {
                if (yorum.Puan.Value >= 5 || yorum.Puan.Value <= 1)
                {
                    ViewBag.Mesaj = "puan 1-5 arasında olmalıdır.";
                    return View(yorum);
                }
            }

            Yorum mevcutYorum = _dbcontext.Yorum.SingleOrDefault(mevcutYorum=>mevcutYorum.Id==yorum.Id);
            mevcutYorum.Icerik = yorum.Icerik;
            mevcutYorum.Yorumcu = yorum.Yorumcu;
            mevcutYorum.Puan = yorum.Puan;
            mevcutYorum.KonuId = yorum.KonuId;

            //List<Konu> konular = _dbcontext.Konu.OrderBy(konu => konu.Baslik).ToList();
            //ViewBag.Konular = new SelectList(konular, "Id", "Baslik");//DropDowlis
            _dbcontext.Yorum.Update(mevcutYorum);
            _dbcontext.SaveChanges();
            TempData["YorumMesaj"] = "Yorum başarıyla güncellendi";
            return RedirectToAction("Index");
            //return View("Index");
        }

        public IActionResult Delete(int id)
        {
            Yorum delete = _dbcontext.Yorum.Include(delete=>delete.Konu).SingleOrDefault(p=>p.Id==id);
            return View(delete);
            //_dbcontext.Yorum.Remove(delete);
            //_dbcontext.SaveChanges();
            //TempData["Mesaj"] = "silme işlemi başarı ile tamalanmıştır";
            //return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConFirmed(int id)
        {
            Yorum delete = _dbcontext.Yorum.SingleOrDefault(p => p.Id == id);
            _dbcontext.Yorum.Remove(delete);
            _dbcontext.SaveChanges();
            TempData["YorumMesaj"] = "yorum silme işlemi başarı ile tamalanmıştır";
            return RedirectToAction("Index");
        }
    }
}
