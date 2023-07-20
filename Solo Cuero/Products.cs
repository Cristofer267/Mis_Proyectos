using System;
using System.Windows.Forms;
using Capa_Negocio;

namespace Solo_Cuero
{
    public partial class frm_Products : Form
    {
        public frm_Products()
        {
            InitializeComponent();
            //Llamamos al metodo MostrarProductos
            MostrarProductos();
        }
        
        //Metodo para mostrar los productos existentes
        public void MostrarProductos ()
        {
            CN_Products objetoCNa = new CN_Products();
            DGV_Productos.DataSource = objetoCNa.MostrarProd();
            DGV_Productos.Columns[0].Visible = false;

        }
        //Evento click para insertar 
        private void Btn_Agregar_Click(object sender, EventArgs e)
        {
            frm_InsertProducts InsertProducts = new frm_InsertProducts();
            AddOwnedForm(InsertProducts);
            InsertProducts.opcion = 1;
            InsertProducts.ShowDialog();

        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            frm_InsertProducts InsertProducts = new frm_InsertProducts();
            InsertProducts.opcion = 2;
            
            try
            {
                if (DGV_Productos.SelectedRows.Count > 0)
                {
                    InsertProducts.Id = Convert.ToInt32(DGV_Productos.CurrentRow.Cells[0].Value.ToString());
                    InsertProducts.txt_Name.Text = DGV_Productos.CurrentRow.Cells[1].Value.ToString();
                    InsertProducts.txt_Description.Text = DGV_Productos.CurrentRow.Cells[2].Value.ToString();
                    InsertProducts.txt_stock.Text = DGV_Productos.CurrentRow.Cells[3].Value.ToString();
                    InsertProducts.txt_category.Text = DGV_Productos.CurrentRow.Cells[4].Value.ToString();
                    InsertProducts.txt_price.Text = DGV_Productos.CurrentRow.Cells[5].Value.ToString();
                    InsertProducts.txt_size.Text = DGV_Productos.CurrentRow.Cells[6].Value.ToString();
                    InsertProducts.txt_color.Text = DGV_Productos.CurrentRow.Cells[7].Value.ToString();
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
                    
                    if (DGV_Productos.SelectedRows.Count > 0)
                    {
                        CN_Products objetoCNb = new CN_Products();
                        int Id = Convert.ToInt32(DGV_Productos.CurrentRow.Cells[0].Value.ToString());
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
 