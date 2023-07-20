using Capa_Datos;
using System.Data;

namespace Capa_Negocio
{
    public class CN_Ventas
    {
        //Instanciamos la capa CD_Ventas
        public CD_Ventas objetoCD = new CD_Ventas();

        // Metodo para mostrar los productos vendidos
        public DataTable MostrarVentas()
        {
            DataTable table = new DataTable();
            table = objetoCD.Mostrar();   
            return table;

        }

        //Metod para consultar la ultima factura
        public DataTable MostrarFac()
        {
            DataTable table = new DataTable();
            table = objetoCD.MostrarF();
            return table;

        }

    }
}
