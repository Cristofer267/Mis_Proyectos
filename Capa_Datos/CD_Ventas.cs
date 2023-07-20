using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos
{
    public class CD_Ventas
    {
        //Instanciar la clase conexion
        private readonly CD_Conexion Conexion = new CD_Conexion();

        //Variables para consultas a la base de datos
        SqlDataReader leer;
        readonly DataTable tabla = new DataTable();
        readonly SqlCommand comando = new SqlCommand();

        //Metodo para consultar todos los articulos que se han vendidos
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

        //Metodo para mostrar la ultima factura creada, este dato se utiliza para agregar al pdf su numero de factura
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

        
    }
}
