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
                usuarios.Add(new Usuario { id = reader.GetInt32(0), user = reader.GetString(1), pass = reader.GetString(2), email = reader.GetString(3), direccion = reader.GetString(4), localidad = reader.GetString(5), pais = reader.GetString(6), cp = reader.GetString(7), rol = reader.GetInt32(8), Habilitado = reader.GetBoolean(9) });
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
                    usuarios.Add(new Usuario { id = reader.GetInt32(0), user = reader.GetString(1), pass = reader.GetString(2), email = reader.GetString(3), direccion = reader.GetString(4), localidad = reader.GetString(5), pais = reader.GetString(6), cp = reader.GetString(7), rol = reader.GetInt32(8), Habilitado = reader.GetBoolean(9) });
                    existe = true;
                }
            }
            if (existe == false)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }
        //  InsertUsuario?u=Jose&p=Jose&e=222&d=Calle&l=Alicante&pais=Mozambique&cod=22221&rol=1
        [HttpGet]
        public bool InsertUsuario(string u, string p, string e, string d, string l, string pais, string cod, int rol)
        {
            bool hecho = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO usuario (username, pass, email, direccion, localidad, pais, codigop, tipousuario, habilitado) VALUES ('"+u+ "', '"+p+ "', '"+e+"', '"+d+"', '"+l+"', '"+pais+"', '"+cod+"', "+rol+", TRUE)", conn);
            int res = cmd.ExecuteNonQuery();    
            if (res != 0)
            {
                hecho = true;
            }
            conn.Close();
            return hecho;
        }
        [HttpGet]
        public bool UpdateUsuario(int id, string u, string p, string e, string d, string l, string pais, string cod, int rol)
        {
            bool hecho = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE usuario SET username = '"+u+"', pass = '" + p + "', email = '" + e + "', direccion = '" + d + "', localidad = '" + l + "', pais = '" + pais + "', codigop = '" + cod + "', tipousuario = '" + rol + "' WHERE id = " + id, conn);
            int res = cmd.ExecuteNonQuery();
            if (res != 0)
            {
                hecho = true;
            }
            conn.Close();
            return hecho;
        }
        [HttpGet]
        public bool DeleteUsuario(int id)
        {
            bool hecho = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE usuario SET habilitado = FALSE WHERE id = " + id, conn);
            int res = cmd.ExecuteNonQuery();
            if (res != 0)
            {
                hecho = true;
            }
            conn.Close();
            return hecho;
        }
    }
}
