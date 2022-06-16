namespace Konu_Yorum_CoreEfDbfirst.Models
{
    public class KonuYorumInnerJoinModel
    {
        //public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        //public int Id { get; set; }
        public string Icerik { get; set; }
        public string Yorumcu { get; set; }
        public int? Puan { get; set; }
        //public int KonuId { get; set; }
        public string PuanDurumu { get; set; }
    }
}
