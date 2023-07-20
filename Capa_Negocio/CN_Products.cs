using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capa_Datos;
using System.Data; 



namespace Capa_Negocio
{
      public class CN_Products
    {

        public CD_Products objetoCD = new CD_Products();

        // Metodo Tipo Datateable 

        public DataTable MostrarProd()
        {
            DataTable table = new DataTable();
            table = objetoCD.Mostrar();
            return table;
           
        }
        public bool CantProd(int Id, int Stock)
        {
            bool leer;
            leer = objetoCD.Cantidades(Id, Stock);
            return leer;

        }


        public void InsertProducts(string Nombre, string Descripcion, string Stock, string Categoria, string Precio, string size, string color)
        {
            objetoCD.Insert(Nombre, Descripcion, Convert.ToInt32(Stock), Categoria, Convert.ToDouble(Precio), size , color);
        
        }


        

        public void EditarProducts(int Id, string Nombre, string Descripcion, string Stock, string Categoria, string Precio, string size, string color)
        {
            objetoCD.Editar(Id, Nombre, Descripcion, Convert.ToInt32(Stock), Categoria, Convert.ToDouble(Precio), size, color);

        }
        public void EliminarArticulos(int Id)
        {
            objetoCD.Eliminar(Id);
        }


        public void AlterarDatos(int Id, int Stock)
        {
            objetoCD.AlterarDatos(Id, Stock);
        }
        //Testing files are loading to Github
        


    }
}
    