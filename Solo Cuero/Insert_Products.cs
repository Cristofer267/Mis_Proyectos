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
        //Instanciar la clase CN_Products
        readonly CN_Products NewProducts = new CN_Products();

        public frm_InsertProducts()
        {
            InitializeComponent();
        }

        //Declarar las variables
        public int opcion;
        public int Id;

        //Evento click para guardar los cambios
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try 
            { 
                //si es opcion 1 se creara un nuevo producto
                if(opcion == 1)
                {
                    try
                    {
                        //Instanciamos el formulio de Productos
                        frm_Products frp = Owner as frm_Products;
                        //Insertamos el nuevo producto
                        NewProducts.InsertProducts(txt_Name.Text, txt_Description.Text, txt_stock.Text, txt_category.Text, txt_price.Text, txt_size.Text, txt_color.Text);
                        MessageBox.Show("Se inserto correctamente");
                        //Actualizamos la vista
                        frp.MostrarProductos();
                        //Cerramos el formulario
                        this.Close();

                    }
                    catch {MessageBox.Show("Favor Complete todos los campos");}
                
                }
                //si es opcion 2 se editara el producto enviado
                else if (opcion == 2)
                {
                    try
                    {
                        //Instanciamos el formulio de Productos
                        frm_Products frp = Owner as frm_Products;
                        //Editamos el producto
                        NewProducts.EditarProducts(Id,txt_Name.Text, txt_Description.Text, txt_stock.Text, txt_category.Text, txt_price.Text, txt_size.Text, txt_color.Text);
                        MessageBox.Show("Se Actualizo correctamente");
                        //Actualizamos la vista
                        frp.MostrarProductos();
                        //Cerramos el formulario
                        this.Close();

                    }
                    catch {MessageBox.Show("No se pudo Actualizar, Favor intente mas tarde");}
                
                }
            }
            catch { MessageBox.Show("Hubo un Error inesperado, Favor intentar nuevamente"); }
        }

        //Evento Click del boton cancelar para cerrar el formulario
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
