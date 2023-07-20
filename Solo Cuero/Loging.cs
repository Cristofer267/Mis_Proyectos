using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Solo_Cuero
{
    public partial class frm_login : Form
    {
        public frm_login()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0T2MQSL;Initial Catalog=Solo_Cueros;Integrated Security=True");

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Loging_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lbl_UserName_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

            DialogResult res;
            res = MessageBox.Show("Quiere cerrar el progama", "exit", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            
            if (res == DialogResult.Yes)

            {
                 Application.Exit();
            }

            else
            {
                this.Show();
            }
                
        }

        private void btn_login_Click(object sender, EventArgs e)
        {


          string username, user_password;

            username = txt_UserName.Text;
            user_password = txt_Password.Text;

            try

            { 

                string querry = "SELECT * FROM Users WHERE UserName = '"+txt_UserName.Text+"'AND Password =  '"+txt_Password.Text+ "'  ";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);

                if (dataTable.Rows.Count > 0 )
                {
                    username = txt_UserName.Text;
                    user_password = txt_Password.Text;

                    // siguiente form a cargar

                    this.Hide();
                    frm_menu menu = new frm_menu();
                    menu.ShowDialog();
                    


                }

                else
                {
                    MessageBox.Show("Usuario o Contraseña es incorrecta.", "Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_Password.Clear();
                    txt_UserName.Clear();

                    // Pare que regrese al campo usuario
                    txt_UserName.Focus();

                }
            
            }

            catch 
            {

                MessageBox.Show("Error");

            }

            finally
            { conn.Close(); }






        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_Password_KeyDown(object sender, KeyEventArgs e)
        {
        









        }
    }
}
