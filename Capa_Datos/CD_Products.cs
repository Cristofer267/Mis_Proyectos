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
    public class CD_Products
    {

        private readonly CD_Conexion Conexion = new CD_Conexion();

        SqlDataReader leer;
        readonly DataTable tabla = new DataTable();
        readonly SqlCommand comando = new SqlCommand();


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
        public void Eliminar(int Id)

        {
            comando.Connection = Conexion.AbrirConexion();
            comando.CommandText = "sp_EliminarProducts";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);
            comando.ExecuteNonQuery();
        }

       
        
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
