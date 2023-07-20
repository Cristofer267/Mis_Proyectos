using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    internal class CD_Conexion
    {
        //Cadena de conexion a la base de datos
        private readonly SqlConnection Conexion = new SqlConnection("server=LAPTOP-CI9H27I8;DataBase=Solo_Cueros;User=sa; Password=Credomatic.10");


        //Abrir la conexion
        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == System.Data.ConnectionState.Closed)
                  Conexion.Open();
            return Conexion;

        }

        //Cerrar la conexion
        public SqlConnection Cerrarconexion()
        {
            if (Conexion.State == System.Data.ConnectionState.Open)
                Conexion.Close();
            return Conexion;

        }




    }
}
