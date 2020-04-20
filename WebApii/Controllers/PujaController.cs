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
        public IEnumerable<Puja> GetAllUsuarios()
        {
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from puja", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                pujas.Add(new Puja { idsubasta = reader.GetInt32(0), idusuario = reader.GetInt32(1), preciopuja = reader.GetFloat(2), tiempo = reader.GetDateTime(3) });
            }
            return pujas;
        }
        public IHttpActionResult GetUsuario(int id)
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
    }
}
