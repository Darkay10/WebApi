using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Web.Http;
using WebApii.Models;

namespace WebApii.Controllers
{
    public class UsuarioController : ApiController
    {
        List<Usuario> usuarios = new List<Usuario>();
        string conexion = "server=127.0.0.1; port=3306;user id=root; password=;database=bdsubastas;";
        public IEnumerable<Usuario> GetAllUsuarios()
        {
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from usuario", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                usuarios.Add(new Usuario { id = reader.GetInt32(0), user = reader.GetString(1), pass = reader.GetString(2), email = reader.GetString(3), direccion = reader.GetString(4), localidad = reader.GetString(5), cp = reader.GetString(6), pais = reader.GetString(7), rol = reader.GetInt32(8), Habilitado = reader.GetBoolean(9) });
            }
            return usuarios;
        }

        public IHttpActionResult GetUsuario(int id)
        {
            bool existe = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from usuario where id = " + id, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() && existe == false)
            {
                if (reader.GetInt32(0) == id)
                {
                    usuarios.Add(new Usuario { id = reader.GetInt32(0), user = reader.GetString(1), pass = reader.GetString(2), email = reader.GetString(3), direccion = reader.GetString(4), localidad = reader.GetString(5), cp = reader.GetString(6), pais = reader.GetString(7), rol = reader.GetInt32(8), Habilitado = reader.GetBoolean(9) });
                    existe = true;
                }
            }
            if (existe == false)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }
    }
}
