using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApii.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public string localidad { get; set; }
        public string cp { get; set; }
        public string pais { get; set; }
        public int rol { get; set; }
        public bool Habilitado { get; set; }
    }
}