using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    [MetadataType(typeof(IzdanjaKnjigaMetadata))]
    public partial class IzdanjaKnjiga
    {
        public HttpPostedFileBase FileSlikaKorica { get; set; }
        class IzdanjaKnjigaMetadata
        {
            [DisplayName("Slika korica")]
            public string SlikaKorica { get; set; }//putanja do fajla
            [Required]
            public int Godina { get; set; }
            [Required]
            public int BrojNaStanju { get; set; }
            public int BrojIzdatih { get; set; }
        }
    }
}