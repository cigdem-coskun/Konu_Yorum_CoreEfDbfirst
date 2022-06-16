using System;
using System.Collections.Generic;

namespace Konu_Yorum_CoreEfDbfirst.DataAccess
{
    public partial class Yorum
    {
        public int Id { get; set; }
        public string Icerik { get; set; }
        public string Yorumcu { get; set; }
        public int? Puan { get; set; }
        public int KonuId { get; set; }

        public virtual Konu Konu { get; set; }
    }
}
