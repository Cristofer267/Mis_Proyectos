using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Negocio;

namespace Solo_Cuero
{
    public partial class frm_Products : Form
    {

        readonly CN_Products objetoCN = new CN_Products();
        public frm_Products()
        {
            InitializeComponent();
            MostrarProductos();

        }
           
        private void Products_Load(object sender, EventArgs e)
        {
            
        }

         public void MostrarProductos ()
        {
            CN_Products objetoCNa = new CN_Products();
            DGV_Facturar.DataSource = objetoCNa.MostrarProd();
            DGV_Facturar.Columns[0].Visible = false;

        }

        private void pb_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_Products_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            frm_InsertProducts InsertProducts = new frm_InsertProducts();
            AddOwnedForm(InsertProducts);
            InsertProducts.opcion = 1;
            InsertProducts.ShowDialog();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            frm_InsertProducts InsertProducts = new frm_InsertProducts();
            InsertProducts.opcion = 2;
            
            try
            {
                if (DGV_Facturar.SelectedRows.Count > 0)
                {
                    InsertProducts.Id = Convert.ToInt32(DGV_Facturar.CurrentRow.Cells[0].Value.ToString());
                    InsertProducts.txt_Name.Text = DGV_Facturar.CurrentRow.Cells[1].Value.ToString();
                    InsertProducts.txt_Description.Text = DGV_Facturar.CurrentRow.Cells[2].Value.ToString();
                    InsertProducts.txt_stock.Text = DGV_Facturar.CurrentRow.Cells[3].Value.ToString();
                    InsertProducts.txt_category.Text = DGV_Facturar.CurrentRow.Cells[4].Value.ToString();
                    InsertProducts.txt_price.Text = DGV_Facturar.CurrentRow.Cells[5].Value.ToString();
                    InsertProducts.txt_size.Text = DGV_Facturar.CurrentRow.Cells[6].Value.ToString();
                    InsertProducts.txt_color.Text = DGV_Facturar.CurrentRow.Cells[7].Value.ToString();
                }
                else
                    MessageBox.Show("Seleccione la fila a editar");
            }
            catch { MessageBox.Show("Error"); }
            AddOwnedForm(InsertProducts);
            InsertProducts.ShowDialog();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //try
            //{
                DialogResult result =
                MessageBox.Show("Desea eliminar el Producto", "Eliminar", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    
                    if (DGV_Facturar.SelectedRows.Count > 0)
                    {
                        CN_Products objetoCNb = new CN_Products();
                        int Id = Convert.ToInt32(DGV_Facturar.CurrentRow.Cells[0].Value.ToString());
                        objetoCNb.EliminarArticulos(Id);
                        MessageBox.Show("Producto Eliminado");
                        MostrarProductos();

                    }
                    else
                        MessageBox.Show("Seleccione la fila a Eliminar");
                }
            //}
            //catch { MessageBox.Show("Error verificar"); }
        }
    }
}
 