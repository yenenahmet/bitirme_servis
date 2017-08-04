using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bitirme_Ekonomistler.Models.Dto
{
    public class MesajKutusu
    {
            public string MyUserid { get; set; }
            public string mesajgelenUserid { get; set; }
            public string SonMesaj { get; set; }
            public string SonMesajZamanı { get; set; }
            public string ProfilResmi { get; set; }
            public string KullaniciAdi { get; set; }

    }
}