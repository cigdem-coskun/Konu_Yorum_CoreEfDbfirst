using Konu_Yorum_CoreEfDbfirst.DataAccess;
using Konu_Yorum_CoreEfDbfirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace Konu_Yorum_CoreEfDbfirst.Controllers
{
    public class KonularYorumlarJoinController : Controller
    {
        private BA_KonuYorumCoreContext _context = new BA_KonuYorumCoreContext();
        public IActionResult InnerJoin()
        {
            IQueryable<Konu> konuQuery = _context.Konu.AsQueryable();
            IQueryable<Yorum> yorumQuery = _context.Yorum.AsQueryable();
            var joinQuery = from konu in konuQuery
                            join yorum in yorumQuery
                            on konu.Id equals yorum.KonuId
                            orderby yorum.Puan descending,yorum.Yorumcu
                            select new KonuYorumInnerJoinModel()
                            {
                                Aciklama = konu.Aciklama,
                                Baslik = konu.Baslik,
                                Icerik = yorum.Icerik,
                                Puan = yorum.Puan,
                                Yorumcu = yorum.Yorumcu,
                                PuanDurumu=yorum.Puan<3 ?"Kötü":yorum.Puan==3 ?"Orta":"İyi"


                            };
            var model = joinQuery.ToList();
            return View(model);
        }

        public IActionResult LeftOuterJoin()
        {
            IQueryable<Konu> konuQuery=_context.Konu.AsQueryable();
            IQueryable<Yorum> yorumQuery=_context.Yorum.AsQueryable();
            var joinQuery = from konu in konuQuery
                            join yorum in yorumQuery
                            on konu.Id equals yorum.KonuId into konuYorumJoin
                            from subKonuYorumJoin in konuYorumJoin.DefaultIfEmpty()
                            orderby subKonuYorumJoin.Puan descending, subKonuYorumJoin.Yorumcu
                            select new KonuYorumLeftOuterJoinModel()
                            {
                                 Baslik=konu.Baslik,
                                 Aciklama=konu.Aciklama,
                                 Icerik=subKonuYorumJoin.Icerik,
                                 Yorumcu=subKonuYorumJoin.Yorumcu,
                                 Puan=subKonuYorumJoin.Puan

                            };
            var model = joinQuery.ToList();
            return View(model);

        }
    }
}
