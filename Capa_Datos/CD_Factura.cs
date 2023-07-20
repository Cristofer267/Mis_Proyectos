using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos
{
    public class CD_Factura
    {
        //Instanciar la clase conexion
        private readonly CD_Conexion Conexion = new CD_Conexion();

        //Variables para consultas a la base de datos
        readonly SqlCommand comando = new SqlCommand();

        //metodo para crear una factura
        public void InsertFactura(string Cliente, double Total)
        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_CrearFactura";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@CustomerID", Cliente);
            comando.Parameters.AddWithValue("@Total", Total);
            comando.ExecuteNonQuery();

        }

        //metodo para llenar el detalle de Factura
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
