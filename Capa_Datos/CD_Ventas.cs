using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

namespace Capa_Datos
{
    public class CD_Ventas
    {
        private readonly CD_Conexion Conexion = new CD_Conexion();

        SqlDataReader leer;
        readonly DataTable tabla = new DataTable();
        readonly SqlCommand comando = new SqlCommand();


        public DataTable Mostrar()
        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_MostraVentas";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            Conexion.Cerrarconexion();
            return tabla;

        }

        public DataTable MostrarF()
        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_UltimaFactura";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            Conexion.Cerrarconexion();
            return tabla;

        }

        public void InsertFactura(string Cliente,double Total)

        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_CrearFactura";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@CustomerID", Cliente);
            comando.Parameters.AddWithValue("@Total", Total);
            comando.ExecuteNonQuery();

        }
    }
}
