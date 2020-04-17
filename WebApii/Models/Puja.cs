using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApii.Models
{
    public class Puja
    {
        private Subasta subasta;
        private Usuario pujador;
        private float cantidad;
        private DateTime fecha;

        public Puja(Subasta subasta, Usuario pujador, float cantidad, DateTime fecha)
        {
            this.Subasta = subasta;
            this.Pujador = pujador;
            this.Cantidad = cantidad;
            this.Fecha = fecha;
        }

        public float Cantidad { get => cantidad; set => cantidad = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        internal Subasta Subasta { get => subasta; set => subasta = value; }
        internal Usuario Pujador { get => pujador; set => pujador = value; }
    }
}