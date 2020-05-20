using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApii.Models;


namespace WebApii.Controllers
{
    public class SubastaController : ApiController
    {
        List<Subasta> subastas = new List<Subasta>();
        string conexion = "server=127.0.0.1; port=3306;user id=root; password=;database=bdsubastas;";

        public IEnumerable<Subasta> GetAllSubastasMenosUno(int id)     // FUNCION EN LA QUE RECOGEMOS TODAS LAS SUBASTAS, MENOS LAS NUESTRAS - Ver subastas
        {
            float max = -1;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from subasta where idvendedor != " +id+ " AND finalizado = FALSE", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MySqlConnection connection = new MySqlConnection(conexion);
                connection.Open();
                MySqlCommand pujaMax = new MySqlCommand("select Max(preciopuja) AS 'Preciopuja' from puja where idsubasta = " + reader.GetInt32(0), connection);
                MySqlDataReader maximo = pujaMax.ExecuteReader();
                while (maximo.Read())
                {
                    if (!maximo.IsDBNull(0))
                    {
                        max = maximo.GetFloat(0);
                    }
                }
                connection.Close();
                if (max > 0)
                {
                    subastas.Add(new Subasta { id = reader.GetInt32(0), articulo = reader.GetString(1), precio = max, finalizado = reader.GetBoolean(3), vendedor = reader.GetInt32(4), comprador = reader.GetInt32(5), comienzo = reader.GetDateTime(6), fin = reader.GetDateTime(7), imagen = reader.GetString(8), descripcion = reader.GetString(9), categoria = reader.GetString(10) });
                }
                else
                {
                    subastas.Add(new Subasta { id = reader.GetInt32(0), articulo = reader.GetString(1), precio = reader.GetFloat(2), finalizado = reader.GetBoolean(3), vendedor = reader.GetInt32(4), comprador = reader.GetInt32(5), comienzo = reader.GetDateTime(6), fin = reader.GetDateTime(7), imagen = reader.GetString(8), descripcion = reader.GetString(9), categoria = reader.GetString(10) });
                }
            }
            return subastas;
        }

        public IEnumerable<Subasta> GetSubastaMias(int id) // SELECCIONAMOS UNA SUBASTA POR ID - Mis subastas
        {
            float max = -1;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from subasta where idvendedor = " + id + " AND finalizado = FALSE", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MySqlConnection connection = new MySqlConnection(conexion);
                connection.Open();
                MySqlCommand pujaMax = new MySqlCommand("select Max(preciopuja) AS 'Preciopuja' from puja where idsubasta = " + reader.GetInt32(0), connection);
                MySqlDataReader maximo = pujaMax.ExecuteReader();
                while (maximo.Read())
                {
                    if (!maximo.IsDBNull(0))
                    {
                        max = maximo.GetFloat(0);
                    }
                }
                connection.Close();
                if (max > 0)
                {
                    subastas.Add(new Subasta { id = reader.GetInt32(0), articulo = reader.GetString(1), precio = max, finalizado = reader.GetBoolean(3), vendedor = reader.GetInt32(4), comprador = reader.GetInt32(5), comienzo = reader.GetDateTime(6), fin = reader.GetDateTime(7), imagen = reader.GetString(8), descripcion = reader.GetString(9), categoria = reader.GetString(10) });
                }
                else
                {
                    subastas.Add(new Subasta { id = reader.GetInt32(0), articulo = reader.GetString(1), precio = reader.GetFloat(2), finalizado = reader.GetBoolean(3), vendedor = reader.GetInt32(4), comprador = reader.GetInt32(5), comienzo = reader.GetDateTime(6), fin = reader.GetDateTime(7), imagen = reader.GetString(8), descripcion = reader.GetString(9), categoria = reader.GetString(10) });
                }
                max = -1;
            }
            return subastas;
        }

        public IEnumerable<Subasta> GetSubastasPujadas(int id) // SELECCIONAMOS UNA SUBASTA POR ID - Mis subastas
        {
            float max = -1;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from puja where idusuario = " + id, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MySqlConnection connection = new MySqlConnection(conexion);
                connection.Open();
                MySqlCommand suba = new MySqlCommand("select * from subasta where id = "+ reader.GetInt32(0), connection);
                MySqlDataReader lectura = suba.ExecuteReader();
                while (lectura.Read())
                {
                    MySqlConnection connection2 = new MySqlConnection(conexion);
                    connection2.Open();
                    MySqlCommand pujaMax = new MySqlCommand("select Max(preciopuja) AS 'Preciopuja' from puja where idsubasta = " + lectura.GetInt32(0), connection2);
                    MySqlDataReader maximo = pujaMax.ExecuteReader();
                    while (maximo.Read())
                    {
                        if (!maximo.IsDBNull(0))
                        {
                            max = maximo.GetFloat(0);
                        }
                    }
                    connection2.Close();
                    if (max > 0)
                    {
                        subastas.Add(new Subasta { id = lectura.GetInt32(0), articulo = lectura.GetString(1), precio = max, finalizado = lectura.GetBoolean(3), vendedor = lectura.GetInt32(4), comprador = lectura.GetInt32(5), comienzo = lectura.GetDateTime(6), fin = lectura.GetDateTime(7), imagen = lectura.GetString(8), descripcion = lectura.GetString(9), categoria = lectura.GetString(10) });
                    }
                    else
                    {
                        subastas.Add(new Subasta { id = lectura.GetInt32(0), articulo = lectura.GetString(1), precio = lectura.GetFloat(2), finalizado = lectura.GetBoolean(3), vendedor = lectura.GetInt32(4), comprador = lectura.GetInt32(5), comienzo = lectura.GetDateTime(6), fin = lectura.GetDateTime(7), imagen = lectura.GetString(8), descripcion = lectura.GetString(9), categoria = lectura.GetString(10) });
                    }
                }
                connection.Close();
            }
            conn.Close();
            return subastas;
        }

        public IHttpActionResult GetSubasta(int id) // SELECCIONAMOS UNA SUBASTA POR ID
        {
            bool existe = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from subasta where id = " + id + " AND finalizado = FALSE", conn);
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
        // FUNCION PARA CREAR SUBASTA - Crear subasta
        [HttpGet]
        public bool InsertSubasta(string a, string c, string d, string i, float p, int idv, string ti, string tf)
        {
            // EL IDCOMPRADOR ES EL MISMO QUE EL VENDEDOR HASTA QUE SE TERMINE LA SUBASTA Y SE COMPRE EL PRODUCTO
            bool hecho = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO subasta (articulo, precio, finalizado, idvendedor, idcomprador, tiempoinicio, tiempofinal, imagen, descripcion, categoria) VALUES ('" + a + "', " + p + ", FALSE, "+ idv +", "+idv+",'" + ti + "', '" + tf + "', '" + i + "', '" + d + "', '" + c + "')", conn);
            int res = cmd.ExecuteNonQuery();
            if (res != 0)
            {
                hecho = true;
            }
            conn.Close();
            return hecho;
        }
        // ESTA EN DUDA DE SU USO POR EL TEMA DE MODIFICAR LA SUBASTA
        [HttpGet]
        public bool UpdateSubasta(int id, string a, string c, string d, string i, float p, string ti, string tf)
        {
            bool hecho = false;
            bool exist = false;
            MySqlConnection connection = new MySqlConnection(conexion);
            connection.Open();
            MySqlCommand comprobando = new MySqlCommand("select * from puja where idsubasta = " + id, connection);
            MySqlDataReader reader = comprobando.ExecuteReader();
            while (reader.Read())
            {
                exist = true;                
            }
            connection.Close();
            if (exist == false)
            {
                MySqlConnection conn = new MySqlConnection(conexion);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE subasta SET articulo = '" + a + "', precio = '" + p + "', tiempoinicio = '" + ti + "', tiempofinal = '" + tf + "', imagen = '" + i + "', descripcion = '" + d + "', categoria = '" + c + "' WHERE id = " + id, conn);
                int res = cmd.ExecuteNonQuery();
                if (res != 0)
                {
                    hecho = true;
                }
                conn.Close();
            }
            return hecho;
        }
        // FUNCION PARA BORRAR SUBASTA - Eliminar subasta - Listado 
        [HttpGet]
        public bool DeleteSubasta(int id)   // FUNCION PARA ELIMINAR NOS SUBASTA
        {
            bool hecho = false;
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE subasta SET finalizado = TRUE WHERE id = " + id, conn);
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