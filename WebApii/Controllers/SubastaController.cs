using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Web.Http;
using WebApii.Models;


namespace WebApii.Controllers
{
    public class SubastaController : ApiController
    {
        List<Subasta> subastas = new List<Subasta>();
        string conexion = "server=127.0.0.1; port=3306;user id=root; password=;database=bdsubastas;";
        public IEnumerable<Subasta> GetAllUsuarios()
        {
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from subasta", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                subastas.Add(new Subasta { id = reader.GetInt32(0), articulo = reader.GetString(1), precio = reader.GetFloat(2), finalizado = reader.GetBoolean(3), vendedor = reader.GetInt32(4), comprador = reader.GetInt32(5), comienzo = reader.GetDateTime(6), fin = reader.GetDateTime(7), imagen = reader.GetString(8), descripcion = reader.GetString(9), categoria = reader.GetString(10) });
            }
            return subastas;
        }
        public IHttpActionResult GetUsuario(int id)
        {
            bool existe = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from subasta where id = " + id, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() && existe == false)
            {
                if (reader.GetInt32(0) == id)
                {
                    subastas.Add(new Subasta { id = reader.GetInt32(0), articulo = reader.GetString(1), precio = reader.GetFloat(2), finalizado = reader.GetBoolean(3), vendedor = reader.GetInt32(4), comprador = reader.GetInt32(5), comienzo = reader.GetDateTime(6), fin = reader.GetDateTime(7), imagen = reader.GetString(8), descripcion = reader.GetString(9), categoria = reader.GetString(10) });
                    existe = true;
                }
            }
            if (existe == false)
            {
                return NotFound();
            }
            return Ok(subastas);
        }
    }
}
