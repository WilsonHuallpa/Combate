using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace biblioteca
{
    public static class PersonajeDAO
    {
        static SqlConnection connection;
        static SqlCommand command; //Quien lleva la consulta
        static SqlDataReader reader; //Quien trae los datos.

        static PersonajeDAO()
        {
            connection = new SqlConnection(@"Data Source = DESKTOP-JOPMB0N;
                                Database = COMBATE_DB;
                                Trusted_Connection = True;");

            command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.Connection = connection;
        }
        public static Personaje ObtenerPersonajePorId(decimal id)
        {
            Personaje personaje = null;
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = connection;
                comando.CommandType = CommandType.Text;
                comando.CommandText = $"Select * From Personajes where ID = {id}";

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                SqlDataReader datosDevueltos = comando.ExecuteReader();

                while (datosDevueltos.Read())
                {
                    switch (datosDevueltos["clase"].ToString())
                    {
                        case "1":
                            personaje = new Guerrero(decimal.Parse(datosDevueltos["id"].ToString()),
                                                datosDevueltos["nombre"].ToString(),
                                              short.Parse(datosDevueltos["nivel"].ToString()));

                            break;
                        case "2":
                            personaje = new Hechicero(decimal.Parse(datosDevueltos["id"].ToString()),
                                               datosDevueltos["nombre"].ToString(),
                                             short.Parse(datosDevueltos["nivel"].ToString()));
                            break;
                        default:
                            personaje = null;
                            break;
                    }
                    if (datosDevueltos["Titulo"].ToString() != "NULL")
                    {
                        personaje.Titulo = datosDevueltos["Titulo"].ToString();
                    }
                }
                datosDevueltos.Close();
                return personaje;
            }
            catch (Exception)
            {
                throw new Exception("Error de conexión a la base de datos");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    
    }
}
