using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApii.Models
{
    public class Puja
    {
        public int idsubasta { get; set; }
        public int idusuario { get; set; }
        public float preciopuja { get; set; }
        public DateTime tiempo { get; set; }
    }
}