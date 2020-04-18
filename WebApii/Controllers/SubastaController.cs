using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Web.Http;
using WebApii.Models;


namespace WebApii.Controllers
{
    public class SubastaController : ApiController
    {
        List<Subasta> subastas = new List<Subasta>();
        public IEnumerable<Subasta> GetAllUsuarios()
        {
            string conexion = "server=127.0.0.1; port=3306;user id=root; password=;database=bdsubastas;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from subasta", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                subastas.Add(new Subasta(reader.GetInt32(0), reader.GetString(1), reader.GetFloat(2), reader.GetBoolean(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDateTime(6), reader.GetDateTime(7), reader.GetString(8), reader.GetString(9), reader.GetString(10)));
            }
            return subastas;
        }
    }
}
