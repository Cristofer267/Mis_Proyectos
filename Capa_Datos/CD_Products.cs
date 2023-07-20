using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos
{
    public class CD_Products
    {
        //Instanciar la clase conexion
        private readonly CD_Conexion Conexion = new CD_Conexion();

        //Variables para consultas a la base de datos
        SqlDataReader leer;
        readonly DataTable tabla = new DataTable();
        readonly SqlCommand comando = new SqlCommand();

        //Metodo para mostrar los productos existentes
        public DataTable Mostrar()
        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_MostrarProductos";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            Conexion.Cerrarconexion();
            return tabla;

        }
        //metodo para validar que las cantidades solicitadas sean las existentes, retorna un boleano con por cada consulta
        public bool Cantidades(int Id, int Stock)
        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_ValidaProducts";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);
            comando.Parameters.AddWithValue("@Stock", Stock);
            leer = comando.ExecuteReader();
            bool lectura;
            lectura = leer.Read();
            Conexion.Cerrarconexion();
            return lectura;

        }

        //Metodo para insertar productos nuevos
        public void Insert ( string Nombre, string Descripcion, int Stock, string Categoria ,double Precio , string size, string color )

        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_InsertProducts";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Nombre", Nombre);
            comando.Parameters.AddWithValue("@Descripcion", Descripcion);
            comando.Parameters.AddWithValue("@Stock", Stock);
            comando.Parameters.AddWithValue("@Categoria", Categoria);
            comando.Parameters.AddWithValue("@Precio", Precio);
            comando.Parameters.AddWithValue("@Size", size);
            comando.Parameters.AddWithValue("@Color", color);
            comando.ExecuteNonQuery();

        }
        
        //Metodo para editar los productos conforme Id
        public void Editar(int Id, string Nombre, string Descripcion, int Stock, string Categoria, double Precio, string size, string color)

        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_EditarProducts";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);
            comando.Parameters.AddWithValue("@Nombre", Nombre);
            comando.Parameters.AddWithValue("@Descripcion", Descripcion);
            comando.Parameters.AddWithValue("@Stock", Stock);
            comando.Parameters.AddWithValue("@Categoria", Categoria);
            comando.Parameters.AddWithValue("@Precio", Precio);
            comando.Parameters.AddWithValue("@Size", size);
            comando.Parameters.AddWithValue("@Color", color);
            comando.ExecuteNonQuery();
        }

        //Metodo para eliminar Productos
        public void Eliminar(int Id)

        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_EliminarProducts";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);
            comando.ExecuteNonQuery();
        }

        //Metodo para eliminar Cantidades Vendidas
        public void AlterarDatos(int Id, int Stock)
        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_AlterarProducts";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);
            comando.Parameters.AddWithValue("@Stock", Stock);
            comando.ExecuteNonQuery();
        }


    }
}
