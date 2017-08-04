using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bitirme_Ekonomistler.Models.Dto
{
    public class GörevtamamlamaPost
    {
        
        public string UserId { get; set; }    
        public string GörevId { get; set; }
        public string TamamlanmaDurumu { get; set; }
        public string AdımId { get; set; }
    }
}