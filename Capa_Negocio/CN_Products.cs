using System;
using Capa_Datos;
using System.Data; 

namespace Capa_Negocio
{
      public class CN_Products
    {
        //Instanciamos la capa CD_Products
        public CD_Products objetoCD = new CD_Products();

        //Metodo para consultar los productos
        public DataTable MostrarProd()
        {
            DataTable table = objetoCD.Mostrar();
            return table;
        }

        //Metodo para recibir los datos del detalle de cantidades
        public bool CantProd(int Id, int Stock)
        {
            bool leer;
            leer = objetoCD.Cantidades(Id, Stock);
            return leer;
        }

        //Metodo para recibir los datos del nuevo producto
        public void InsertProducts(string Nombre, string Descripcion, string Stock, string Categoria, string Precio, string size, string color)
        {
            objetoCD.Insert(Nombre, Descripcion, Convert.ToInt32(Stock), Categoria, Convert.ToDouble(Precio), size , color);
        
        }

        //Metodo para recibir los datos del producto que se modificara
        public void EditarProducts(int Id, string Nombre, string Descripcion, string Stock, string Categoria, string Precio, string size, string color)
        {
            objetoCD.Editar(Id, Nombre, Descripcion, Convert.ToInt32(Stock), Categoria, Convert.ToDouble(Precio), size, color);

        }

        //Metodo para recibir los datos del producto a eliminar
        public void EliminarArticulos(int Id)
        {
            objetoCD.Eliminar(Id);
        }

        //Metodo para recibir las cantidades a eliminar
        public void AlterarDatos(int Id, int Stock)
        {
            objetoCD.AlterarDatos(Id, Stock);
        }

    }
}
    