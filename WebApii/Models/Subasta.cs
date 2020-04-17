using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApii.Models
{
    public class Subasta
    {
        private string id;
        private string articulo;
        private float precio;
        private bool finalizado;
        private Usuario vendedor;
        private Usuario comprador;
        private DateTime fin;
        private DateTime comienzo;
        private string imagen;
        private string desripcion;
        private string categoria;

        public Subasta(string id, string articulo, float precio, Usuario vendedor, DateTime fin, DateTime comienzo,
            string imagen, string desripcion, string categoria)
        {
            this.Id = id;
            this.Articulo = articulo;
            this.Precio = precio;
            this.Finalizado = false;
            this.Vendedor = vendedor;
            this.Comprador = null;
            this.Fin = fin;
            this.Comienzo = comienzo;
            this.Imagen = imagen;
            this.Desripcion = desripcion;
            this.Categoria = categoria;
        }

        public Subasta(string id)
        {
            this.Id = id;
            this.Articulo = "";
            this.Precio = 0;
            this.Finalizado = false;
            this.Vendedor = null;
            this.Comprador = null;
            this.Fin = DateTime.Now;
            this.Comienzo = DateTime.Now;
            this.Imagen = "";
            this.Desripcion = "";
            this.Categoria = "";
        }

        public string Id { get => id; set => id = value; }
        public string Articulo { get => articulo; set => articulo = value; }
        public float Precio { get => precio; set => precio = value; }
        public bool Finalizado { get => finalizado; set => finalizado = value; }
        public DateTime Fin { get => fin; set => fin = value; }
        public DateTime Comienzo { get => comienzo; set => comienzo = value; }
        public string Imagen { get => imagen; set => imagen = value; }
        public string Desripcion { get => desripcion; set => desripcion = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        internal Usuario Vendedor { get => vendedor; set => vendedor = value; }
        internal Usuario Comprador { get => comprador; set => comprador = value; }
    }
}