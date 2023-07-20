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
    public class CD_Factura
    {
        private readonly CD_Conexion Conexion = new CD_Conexion();

        SqlDataReader leer;
        readonly DataTable tabla = new DataTable();
        readonly SqlCommand comando = new SqlCommand();

        public void LlenarDetalleFactura(string Producto, int Stock, double Precio)
        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "SP_Sales_detail";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Producto", Producto);
            comando.Parameters.AddWithValue("@Amount", Stock);
            comando.Parameters.AddWithValue("@Price", Precio);
            comando.ExecuteNonQuery();

        }
    }

    
}
