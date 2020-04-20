using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApii.Models
{
    public class Subasta
    {
        public int id { get; set; }
        public string articulo { get; set; }
        public float precio { get; set; }
        public bool finalizado { get; set; }
        public int vendedor { get; set; }
        public int comprador { get; set; }
        public DateTime comienzo { get; set; }
        public DateTime fin { get; set; }
        public string imagen { get; set; }
        public string descripcion { get; set; }
        public string categoria { get; set; }
    }
}