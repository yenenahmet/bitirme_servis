using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bitirme_Ekonomistler.Models.Dto
{
    public class BildirimleriDöndür
    {
        public string Myid { get; set; }
        public string BildirimiGöderenId { get; set; }
        public string TarihZaman { get; set; }
        public string Yorum { get; set; }
        public string GörevId { get; set; }
        public string AdimId { get; set; }
        public string bildirimeBakıldımı { get; set; }
        public string begeni { get; set; }
        public string ArkadasEkledin { get; set; }
        public string ArkadaslıkİsteğiGeldi { get; set; }
        public string MesajGeldi { get; set; }
        public string KullaniciAdi { get; set; }
        public string ProfilResmi { get; set; }

    }
}