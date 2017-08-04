using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bitirme_Ekonomistler.Models.Dto
{
    public class profilgüncellepost
    {
        public string userid { get; set; }
        public string email { get; set; }
        public string Sifre { get; set; }
        public string KullaniciAdi { get; set; }
        public string profilyazısı { get; set; }
        public string resimyolu { get; set; }
    }
}