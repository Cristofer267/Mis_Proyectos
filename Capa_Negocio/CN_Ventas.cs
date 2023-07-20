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
    public class CN_Ventas
    {
        public CD_Ventas objetoCD = new CD_Ventas();

        public DataTable MostrarVentas()
        {
            DataTable table = new DataTable();
            table = objetoCD.Mostrar();   
            return table;

        }

        public DataTable MostrarFac()
        {
            DataTable table = new DataTable();
            table = objetoCD.MostrarF();
            return table;

        }

        public void InsertVenta(string Cliente, double Total)
        {
            objetoCD.InsertFactura(Cliente, Total);

        }
        

    }
}
