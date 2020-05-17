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

        public IEnumerable<Usuario> GetAllUsuarios()    // FUNCION EN LA QUE RECOGEMOS TODOS LOS USUARIOS

        {
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from usuario", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            // LANZAMOS LA SELECT QUE NOS DEVUELVE TODOS LOS USUARIOS
            while (reader.Read())
            {
                usuarios.Add(new Usuario { id = reader.GetInt32(0), user = reader.GetString(1), pass = reader.GetString(2), email = reader.GetString(3), direccion = reader.GetString(4), localidad = reader.GetString(5), pais = reader.GetString(6), cp = reader.GetString(7), rol = reader.GetInt32(8), Habilitado = reader.GetBoolean(9) });
            }
            return usuarios;
        }

        public IHttpActionResult GetUsuario(int id)     // RECOGEMOS UN USUARIO POR ID
        {
            bool existe = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from usuario where id = " + id, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            // BUSCAMOS EL USUARIO POR SU ID
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

        public IEnumerable<Usuario> GetUsuariosMenosUno(int id) // RECOGEMOS TODOS LOS USUARIOS MENOS EL NUESTRO
        {
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from usuario where id != " + id + " AND habilitado = TRUE", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            // TODOS LOS USUARIOS QUE ESTEN HABILITADOS 
            while (reader.Read())
            {
                usuarios.Add(new Usuario { id = reader.GetInt32(0), user = reader.GetString(1), pass = reader.GetString(2), email = reader.GetString(3), direccion = reader.GetString(4), localidad = reader.GetString(5), pais = reader.GetString(6), cp = reader.GetString(7), rol = reader.GetInt32(8), Habilitado = reader.GetBoolean(9) });
            }
            return usuarios;
        }

        // InsertUsuario? u = Jose & p = Jose & e = 222 & d = Calle & l = Alicante & pais = Mozambique & cod = 22221 & rol = 1
        // FUNCION QUE SE LE PASAN TODOS LOS DATOS Y SE INSERTAN A LA BASE DE DATOS - Por defecto el permiso de admin esta deshabilitado
        [HttpGet]
        public bool InsertUsuario(string u, string p, string e, string d, string l, string pais, string cod)
        {
            bool hecho = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO usuario (username, pass, email, direccion, localidad, pais, codigop, tipousuario, habilitado) VALUES ('" + u + "', '" + p + "', '" + e + "', '" + d + "', '" + l + "', '" + pais + "', '" + cod + "', 0, TRUE)", conn);
            int res = cmd.ExecuteNonQuery();
            if (res != 0)
            {
                hecho = true;
            }
            conn.Close();
            return hecho;
        }
        // FUNCION PARA MODIFICAR LOS USUARIOS
        [HttpGet]
        public bool UpdateUsuario(int id, string u, string p, string e, string d, string l, string pais, string cod, int rol)
        {
            bool hecho = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE usuario SET username = '" + u + "', pass = '" + p + "', email = '" + e + "', direccion = '" + d + "', localidad = '" + l + "', pais = '" + pais + "', codigop = '" + cod + "', tipousuario = '" + rol + "' WHERE id = " + id, conn);
            int res = cmd.ExecuteNonQuery();
            if (res != 0)
            {
                hecho = true;
            }
            conn.Close();
            return hecho;
        }
        // FUNCION PARA DAR DE BAJA AL USUARIO
        [HttpGet]
        public bool DeshabilitarUsuario(int id)
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
        // FUNCION PARA CAMBIAR LA CONTRASEÑA DEL USUARIO
        [HttpGet]
        public bool cambiarContra(int id, string pass)
        {
            bool hecho = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE usuario SET pass = " + pass + " WHERE id = " + id, conn);
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
