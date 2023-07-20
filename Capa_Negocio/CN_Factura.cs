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
    public class CN_Factura
    {
        public CD_Factura objetoCD = new CD_Factura();

        public void DetalleFactura(string Producto, int Stock, double Precio)

        {
            objetoCD.LlenarDetalleFactura(Producto, Stock, Precio);
        }
    }
}
