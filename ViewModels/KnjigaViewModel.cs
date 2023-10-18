using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.ViewModels
{
    public class KnjigaViewModel
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public int AutorId { get; set; }
        public int CategoryId { get; set; }
    }
}