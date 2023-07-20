using Capa_Negocio;
using System.Windows.Forms;

namespace Solo_Cuero
{
    public partial class Ventas : Form
    {
        public Ventas()
        {
            InitializeComponent();
            try
            {
                //Llamamos al metodo MostrarVentas
                MostrarVentas();
            }
            catch { MessageBox.Show("Hubo un Error inesperado, Favor intentar nuevamente"); }
            
        }

        //Creamos el metodo para mostrar las ventas
        public void MostrarVentas()
        {
            try
            {
                CN_Ventas objetoCN = new CN_Ventas();
                DGV_Ventas.DataSource = objetoCN.MostrarVentas();
            }
            catch { MessageBox.Show("Hubo un Error inesperado, Favor intentar nuevamente"); }
            
        }
    }
}
