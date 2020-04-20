using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApii.Models
{
    public class Puja
    {
        public Subasta subasta { get; set; }
        public Usuario pujador { get; set; }
        public float cantidad { get; set; }
        public DateTime fecha { get; set; }
    }
}