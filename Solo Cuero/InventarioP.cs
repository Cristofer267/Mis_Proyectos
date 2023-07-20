using Capa_Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solo_Cuero
{
    public partial class InventarioP : Form
    {
        //Instanciar la clase CN_Products
        readonly CN_Products objetoCN = new CN_Products();
        public InventarioP()
        {
            InitializeComponent();
            MostrarProductos();
        }
        
        //Metodo para mostrar mediante grafico las cantidades por producto
        public void MostrarProductos()
        {
            try 
            { 
                //Declaramos variables de areglos de lista para almacenar los productos y agregarlos al grafico
                ArrayList Nombre = new ArrayList();
                ArrayList Cantidad = new ArrayList();
                DataTable lista = objetoCN.MostrarProd();
                //Ciclo que inserta los productos al grafico
                foreach (DataRow row in lista.Rows) {
                    Nombre.Add(row["Nombre"]);
                    Cantidad.Add(row["Stock"]);
                }
                chartPoducts.Series[0].Points.DataBindXY(Nombre,Cantidad) ;
            } 
            catch { MessageBox.Show("Hubo un Error inesperado, Favor intentar nuevamente"); }
            
        }
    }
}
