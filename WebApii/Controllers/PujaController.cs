using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApii.Models;

namespace WebApii.Controllers
{
    public class PujaController : ApiController
    {
        List<Puja> pujas = new List<Puja>();
        string conexion = "server=127.0.0.1; port=3306;user id=root; password=;database=bdsubastas;";
        // FUNCION QUE UTILIZAREMOS PARA MOSTRAR EL PRECIO MÁXIMO DE LA PUJA - Ver subastas y mis subastas
        public IHttpActionResult GetPuja(int id)
        {
            bool existe = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from puja where idsubasta = " + id, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() && existe == false)
            {
                if (reader.GetInt32(0) == id)
                {
                    pujas.Add(new Puja { idsubasta = reader.GetInt32(0), idusuario = reader.GetInt32(1), preciopuja = reader.GetFloat(2), tiempo = reader.GetDateTime(3) });
                    existe = true;
                }
            }
            if (existe == false)
            {
                return NotFound();
            }
            return Ok(pujas);
        }
        // FUNCION PARA CREAR PUJA - Ver subastas y mis subastas
        [HttpGet]
        public bool InsertPuja(int idu, int ids, float p, DateTime t)
        {
            bool hecho = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO puja (idusuario, idsubasta, preciopuja, tiempo) VALUES (" + idu + ", " +ids + ", "+p+", "+ t + ")", conn);
            int res = cmd.ExecuteNonQuery();
            if (res != 0)
            {
                hecho = true;
            }
            conn.Close();
            return hecho;
        }
        // Si existe ya una puja de un comprador, se cambia y se actualiza - Ver subastas y mis subastas
        [HttpGet]
        public bool UpdatePuja(int idu, int ids, float p, DateTime t)
        {
            bool hecho = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE puja SET preciopuja = "+p+", tiempo = '"+t+"' WHERE idsubasta = " + ids + " AND idusuario = "+ idu, conn);
            int res = cmd.ExecuteNonQuery();
            if (res != 0)
            {
                hecho = true;
            }
            conn.Close();
            return hecho;
        }
        // FUNCION PARA SACAR EL LA PUJA MÁXIMA
        [HttpGet]
        public float PujaMaxima(int id)
        {
            float pujamax = -1;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select Max(preciopuja) AS 'Preciopuja' from puja where idsubasta = "+id, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                pujamax = reader.GetFloat(0);
            }
            conn.Close();
            return pujamax;
        }
    }
}
