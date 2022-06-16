using Konu_Yorum_CoreEfDbfirst.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Konu_Yorum_CoreEfDbfirst.Controllers
{
    public class KonuController : Controller
    {
       private  BA_KonuYorumCoreContext _dbcontext = new BA_KonuYorumCoreContext();
        public IActionResult Index()
        {
            List<Konu> konular = _dbcontext.Konu.ToList();
            return View(konular);
        }

        public IActionResult Details(int id)
        {
           Konu konu= _dbcontext.Konu.Find(id);
            return View(konu);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Konu konu)
        {
            if(string.IsNullOrWhiteSpace(konu.Baslik))
            {
               // ViewData["Mesaj"] = "Başlık boş girilemez";
                ViewBag.Mesaj = "başlık boş girilemez";
                return View(konu);
            }
            
            _dbcontext.Konu.Add(konu);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Konu konu = _dbcontext.Konu.SingleOrDefault(p=>p.Id==id);
            return View(konu);
        }
        [HttpPost]
        public IActionResult Edit(  Konu konu)
        {
            if (string.IsNullOrWhiteSpace(konu.Baslik))
            {
                // ViewData["Mesaj"] = "Başlık boş girilemez";
                ViewBag.Mesaj = "başlık boş geçilemez";
                return View(konu);
            }
            if (konu.Baslik.Length > 100)
            {
                ViewBag.Mesaj = "başlık en fazla 100 karakter olmalıdır.";
                return View(konu);
            }
            if (string.IsNullOrWhiteSpace(konu.Aciklama) && konu.Aciklama.Length > 200)
            {
                ViewBag.Mesaj = "Açıklama en fazla 200 karakter olmalıdır.";
                return View(konu);
            }

            Konu mevcutKonu = _dbcontext.Konu.SingleOrDefault(mevcutkonu=>mevcutkonu.Id==konu.Id);
            mevcutKonu.Baslik = konu.Baslik;
            mevcutKonu.Aciklama = konu.Aciklama;
            _dbcontext.Konu.Update(mevcutKonu);
            _dbcontext.SaveChanges();
            return  RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
         Konu delete= _dbcontext.Konu.Include(V=>V.Yorum).SingleOrDefault(p=>p.Id==id);

            //if (delete.Yorum!=null && delete.Yorum.Count>0)//yorum kayıtları dolu ise
            //{
            //    foreach (Yorum yorum in delete.Yorum)
            //    {
            //        _dbcontext.Yorum.Remove(yorum);
            //    }
            //}
            //_dbcontext.Konu.Remove(delete);
            //_dbcontext.Yorum.RemoveRange(delete.Yorum);
            if (delete.Yorum !=null && delete.Yorum.Count>0)
            {
                TempData["Mesaj"] = "Slinmek istenen konu ile ilişkili yorum kayıtları bulunmaktadır.";
                return RedirectToAction("Index");
            }
            else
            {
                _dbcontext.Konu.Remove(delete);
                _dbcontext.SaveChanges();
            }
           
            //ViewBag.Mesaj = "Kayıt silindi";
            return RedirectToAction("Index");
        }
    }
}
