using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bitirme_Ekonomistler.Models.Dto
{
    public class GörevTamamlamaPostYorum_iceAktarma
    {
        public string userid { get; set; }
        public string görevid { get; set; }
        public string adimid { get; set; }

        public string yorum { get; set; }
        public string yorumyapanid { get; set; }
        public string yorumyapanad { get; set; }

        public string yorumyapanProfilresmi { get; set; }
        
    }
}