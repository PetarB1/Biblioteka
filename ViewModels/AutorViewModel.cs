using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteka.ViewModels
{
    public class AutorViewModel
    {
        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Polje Ime je obavezno.")]
        public string Ime { get; set; }

        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "Polje Prezime je obavezno.")]
        public string Prezime { get; set; }
    }
}