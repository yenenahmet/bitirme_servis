using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bitirme_Ekonomistler.Models.Dto
{
    public class BaskasınınProfilineGeç
    {
        
        public string KullaniciAdi { get; set; }
        public string Okul { get; set; }
        public string GörevDerecesi { get; set; }

        public string ProfilYazısı { get; set; }
        public string ProfilResmi { get; set; }
        public int MyUserid { get; set; }

        public int istekGönderilenid { get; set; } // geçtiği Kulanıcının id si
        public int bitengörevSayis { get; set; }
    }
}