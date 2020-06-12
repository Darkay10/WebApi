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
        // FUNCION PARA CREAR PUJA - Ver subastas y mis subastas
        [HttpGet]
        public bool InsertPuja(int idu, int ids, float p)
        {
            bool hecho = false;
            bool mayor = false;
            int exist = 1;
            MySqlConnection co = new MySqlConnection(conexion);
            co.Open();
            MySqlCommand e = new MySqlCommand("select MAX(preciopuja) AS 'maximototal' from puja where idsubasta = " + ids, co);
            MySqlDataReader lec = e.ExecuteReader();
            while (lec.Read())
            {
                if (p > lec.GetFloat(0))
                {
                    mayor = true;
                }
            }
            co.Close();
            if (mayor == true)
            {         
                MySqlConnection connection = new MySqlConnection(conexion);
                connection.Open();
                MySqlCommand existe = new MySqlCommand("select * from puja where idsubasta = " + ids + " AND idusuario = " + idu, connection);
                MySqlDataReader reader = existe.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetFloat(2) < p)
                        exist = 2;
                    else
                        exist = 3;
                }
                connection.Close();
                MySqlConnection conn = new MySqlConnection(conexion);
                conn.Open();
                if (exist == 1)
                { 
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO puja (idusuario, idsubasta, preciopuja, tiempo) VALUES (" + idu + ", " + ids + ", " + p + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "')", conn);
                    int res = cmd.ExecuteNonQuery();
                    if (res != 0)
                    {
                        hecho = true;
                    }
                }
                else if (exist == 2)
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE puja SET preciopuja = " + p + ", tiempo = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE idsubasta = " + ids + " AND idusuario = " + idu, conn);
                    int res = cmd.ExecuteNonQuery();
                    if (res != 0)
                    {
                        hecho = true;
                    }
                }
                conn.Close();
            }
            return hecho;
        }
    }
}
