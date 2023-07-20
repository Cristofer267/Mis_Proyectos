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
        CN_Products objetoCN = new CN_Products();
        public InventarioP()
        {
            InitializeComponent();
        }

        private void InventarioP_Load(object sender, EventArgs e)
        {
            MostrarProductos();
        }
        public void MostrarProductos()
        {
            ArrayList Nombre = new ArrayList();
            ArrayList Cantidad = new ArrayList();
            DataTable lista = objetoCN.MostrarProd();
            foreach (DataRow row in lista.Rows) {
                Nombre.Add(row["Nombre"]);
                Cantidad.Add(row["Stock"]);
            }
            chartPoducts.Series[0].Points.DataBindXY(Nombre,Cantidad) ;
            

        }
    }
}
