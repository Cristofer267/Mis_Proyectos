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
            MostrarFacturas();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void MostrarFacturas()
        {
            CN_Products objeto = new CN_Products();
            dataGridView1.DataSource = objeto.MostrarProd();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
        }
       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text!= "")
            {
                dataGridView1.CurrentCell = null;
                foreach(DataGridViewRow r in dataGridView1.Rows)
                {
                    r.Visible = false; 
                }
                foreach(DataGridViewRow r in dataGridView1.Rows)
                {
                    foreach(DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(textBox1.Text.ToUpper()) == 0)
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

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Facturar objeto = Owner as Facturar;
            objeto.tbx_Articulo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            objeto.lbl_Precio.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            objeto.lbl_ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
        }
    }
}
