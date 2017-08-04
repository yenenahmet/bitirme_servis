using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bitirme_Ekonomistler.Models.Dto
{
    public class KullaniciProfil
    {


       
        public string UserId { get; set; }
        public string KullaniciAdi { get; set; }
        public string Okul { get; set; }
        public string GörevDerecesi { get; set; }
        public string ProfilYazısı { get; set; }
        public string ProfilResmi { get; set; }
        public int bitengörevSayis { get; set; }
    }
}