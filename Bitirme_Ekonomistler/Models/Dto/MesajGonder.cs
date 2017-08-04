using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bitirme_Ekonomistler.Models.Dto
{
    public class MesajGonder
    {
        public string gönderilenUserid { get; set; }
        public string Myuserid { get; set; }
        public string Mesaj { get; set; }
    }
}