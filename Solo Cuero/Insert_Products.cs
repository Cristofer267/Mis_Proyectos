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
    public partial class frm_InsertProducts : Form
    {
        readonly CN_Products NewProducts = new CN_Products();

        

        public frm_InsertProducts()
        {
            InitializeComponent();
        }
        public int opcion;
        public int Id;

        private void btn_Save_Click(object sender, EventArgs e)
        {
            
            if(opcion == 1)
            {
                try
                {
                    frm_Products frp = Owner as frm_Products;
                    NewProducts.InsertProducts(txt_Name.Text, txt_Description.Text, txt_stock.Text, txt_category.Text, txt_price.Text, txt_size.Text, txt_color.Text);
                    MessageBox.Show("Se inserto correctamente");
                    frp.MostrarProductos();
                    this.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Favor Complete todos los campos");
                }
                
            }
            else if (opcion == 2)
            {
                try
                {
                    frm_Products frp = Owner as frm_Products;
                    NewProducts.EditarProducts(Id,txt_Name.Text, txt_Description.Text, txt_stock.Text, txt_category.Text, txt_price.Text, txt_size.Text, txt_color.Text);
                    MessageBox.Show("Se Actualizo correctamente");
                    frp.MostrarProductos();
                    this.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo Actualizar, Favor intente mas tarde");
                }
                
            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_InsertProducts_Load(object sender, EventArgs e)
        {

        }





    }
}
