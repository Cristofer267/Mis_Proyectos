using Capa_Negocio;
using System;
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
    public partial class ListProduc : Form
    {
        public ListProduc()
        {
            InitializeComponent();
            MostrarProducts();
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void MostrarProducts()
        {
            CN_Products objeto = new CN_Products();
            Dgv_Products.DataSource = objeto.MostrarProd();
            Dgv_Products.Columns[0].Visible = false;
            Dgv_Products.Columns[2].Visible = false;
            Dgv_Products.Columns[3].Visible = false;
            Dgv_Products.Columns[4].Visible = false;
            Dgv_Products.Columns[5].Visible = false;
            Dgv_Products.Columns[6].Visible = false;
            Dgv_Products.Columns[7].Visible = false;
        }
       

        private void Tbx_Buscar_TextChanged(object sender, EventArgs e)
        {
            if (Tbx_Buscar.Text!= "")
            {
                Dgv_Products.CurrentCell = null;
                foreach(DataGridViewRow r in Dgv_Products.Rows)
                {
                    r.Visible = false; 
                }
                foreach(DataGridViewRow r in Dgv_Products.Rows)
                {
                    foreach(DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(Tbx_Buscar.Text.ToUpper()) == 0)
                        {
                            r.Visible =true;
                            break;
                        }
                    }
                }
            }
            else
            {
                MostrarFacturas();
            }
        }

        private void Dgv_Products_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Facturar objeto = Owner as Facturar;
            objeto.tbx_Articulo.Text = Dgv_Products.CurrentRow.Cells[1].Value.ToString();
            objeto.lbl_Precio.Text = Dgv_Products.CurrentRow.Cells[5].Value.ToString();
            objeto.lbl_ID.Text = Dgv_Products.CurrentRow.Cells[0].Value.ToString();
            this.Close();
        }
    }
}
