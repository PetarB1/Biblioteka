using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.ViewModels
{
    public class IzdanjeKnjigeViewModel
    {

        public List<IzdanjaKnjiga> Izdanja { get; set; }
        public string IzabranoIzdanje { get; set; }
    }
}