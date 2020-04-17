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
    public class UsuarioController : ApiController
    {
        List<Usuario> usuarios = new List<Usuario>();
        public IEnumerable<Usuario> GetAllUsuarios()
        {
            string conexion = "server=127.0.0.1; port=3306;user id=root; password=;database=bdsubastas;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from usuario", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                usuarios.Add(new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8), reader.GetBoolean(9)));
            }
            return usuarios;
        }
    }
}
