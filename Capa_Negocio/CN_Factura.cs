using Capa_Datos;

namespace Capa_Negocio
{
    public class CN_Factura
    {
        //instanciamos la capa CD_Factura
        public CD_Factura objetoCD = new CD_Factura();

        //Metodo para recibir los datos del detalle de factura
        public void DetalleFactura(string Producto, int Stock, double Precio)
        {
            objetoCD.LlenarDetalleFactura(Producto, Stock, Precio);
        }

        //Metodo para recibir los datos de la nueva factura
        public void InsertFactura(string Cliente, double Total)
        {
            objetoCD.InsertFactura(Cliente, Total);

        }
    }
}
