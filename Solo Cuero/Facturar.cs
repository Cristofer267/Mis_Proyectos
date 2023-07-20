using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Collections;
using System.Collections.Specialized;
using iTextSharp.tool.xml.html;
using System.Data.SqlClient;
using System.Deployment.Internal;

namespace Solo_Cuero
{
    public partial class Facturar : Form
    {
        public Facturar()
        {
            InitializeComponent();
            autocompletar();
            DGV_Facturar.Columns[4].Visible = false;
        }

        void autocompletar()
        {
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            CN_Products objeto = new CN_Products();

            var aur = objeto.MostrarProd();

            for (int i = 0; i < aur.Rows.Count; i++)
            {
                lista.Add(aur.Rows[i]["Nombre"].ToString());
            }

            tbx_Articulo.AutoCompleteCustomSource = lista;

        }



        public void Sumartotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in DGV_Facturar.Rows)
            {
                total += Convert.ToDecimal(row.Cells[3].Value);
            }
            lbl_Total.Text = total.ToString();
        }




        private void tbx_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            if (tbx_Articulo.Text == string.Empty || tbx_Cantidad.Text == string.Empty || lbl_Precio.Text == string.Empty || lbl_ID.Text == string.Empty)
            {

                MessageBox.Show("Favor completar los datos");
            }
            else
            {
                int RowsEscribir = DGV_Facturar.Rows.Count;

                DGV_Facturar.Rows.Add(1);

                DGV_Facturar.Rows[RowsEscribir].Cells[0].Value = tbx_Articulo.Text;
                DGV_Facturar.Rows[RowsEscribir].Cells[1].Value = tbx_Cantidad.Text;
                DGV_Facturar.Rows[RowsEscribir].Cells[2].Value = lbl_Precio.Text;
                DGV_Facturar.Rows[RowsEscribir].Cells[3].Value = double.Parse (lbl_Precio.Text) * double.Parse (tbx_Cantidad.Text);
                DGV_Facturar.Rows[RowsEscribir].Cells[4].Value = lbl_ID.Text;

                Sumartotal();
                tbx_Articulo.Clear();
                tbx_Cantidad.Clear();
                lbl_Precio.Text = "0";
                tbx_Articulo.TabIndex = 1;

            }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {

                if (DGV_Facturar.RowCount >= 1 && tbx_Cliente.Text.Length >= 1)
                {
                    bool continuar = true;
                
                    foreach (DataGridViewRow row in DGV_Facturar.Rows)
                    {

                        CN_Products cantidad = new CN_Products();
                        string Id = row.Cells[4].Value.ToString();
                        string Cantidad = row.Cells[1].Value.ToString();
                        bool leer;
                        leer = cantidad.CantProd(Convert.ToInt32(Id), Convert.ToInt32(Cantidad));
                        if (leer == false)
                        {
                            continuar = false;  
                            break;
                        }
                        
                    }

                    if (continuar == true)
                    {

                        CN_Products objetoa = new CN_Products();
                        CN_Ventas objetoe = new CN_Ventas();
                        EliminarCantidades();
                        objetoe.InsertVenta(tbx_Cliente.Text, double.Parse(lbl_Total.Text));
                        
                        
                        string cliente = tbx_Cliente.Text;
                        double total = double.Parse(lbl_Total.Text);
                        int articulos = 0;
                        int count = DGV_Facturar.RowCount;
                        string[] ProductoA = new string[count];
                        string[] CantidadA = new string[count];
                        string[] PrecioA = new string[count];
                        int count1 = 0;

                        foreach (DataGridViewRow row in DGV_Facturar.Rows)
                        {
                            ProductoA[count1] = row.Cells["Articulo"].Value.ToString();
                            CantidadA[count1] = row.Cells["Cantidad"].Value.ToString();
                            PrecioA[count1] = row.Cells["Precio"].Value.ToString();
                            CN_Factura objetoB = new CN_Factura();
                            objetoB.DetalleFactura (ProductoA[count1], Convert.ToInt32(CantidadA[count1]), Convert.ToDouble(PrecioA[count1]));
                            count++;
                        }

                        foreach (DataGridViewRow row in DGV_Facturar.Rows)
                        {
                            string Productovendido = row.Cells[0].Value.ToString();
                            int Cantidadvendida = Convert.ToInt32(row.Cells[1].Value.ToString());
                            double Precioventa = Convert.ToDouble(row.Cells[2].Value.ToString());
                        }

                        foreach (DataGridViewRow row in DGV_Facturar.Rows)
                        {
                            articulos += Convert.ToInt32(row.Cells[1].Value);
                        }

                        MostrarFact();
                        int idf = 0;
                        foreach (DataGridViewRow row in DGV_ID.Rows)
                        {
                            idf = Convert.ToInt32(row.Cells[0].Value);
                        break;
                        }
                        lbl_ID.Text = idf.ToString();

                        

                        SaveFileDialog savefile = new SaveFileDialog();
                        savefile.FileName = string.Format("{0}.pdf", "Factura "+lbl_ID.Text);

                        string PaginaHTML_Texto = Properties.Resources.Plantilla.ToString();
                        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CLIENTE", tbx_Cliente.Text);
                        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

                        string filas = string.Empty;
                        foreach (DataGridViewRow row in DGV_Facturar.Rows)
                        {
                            filas += "<tr>";
                            filas += "<td>" + row.Cells[0].Value.ToString() + "</td>";
                            filas += "<td>" + row.Cells[1].Value.ToString() + "</td>";
                            filas += "<td>" + row.Cells[2].Value.ToString() + "</td>";
                            filas += "<td>" + row.Cells[3].Value.ToString() + "</td>";
                            filas += "</tr>";
                        }

                        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@Factura", lbl_ID.Text);
                        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas);
                        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", total.ToString());

                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(new Phrase(""));

                                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.Solo_cuero_Logo, System.Drawing.Imaging.ImageFormat.Png);
                                img.ScaleToFit(60, 60);
                                img.Alignment = iTextSharp.text.Image.UNDERLYING;

                                img.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 60);
                                pdfDoc.Add(img);


                                using (StringReader sr = new StringReader(PaginaHTML_Texto))
                                {
                                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                                }

                                pdfDoc.Close();
                                stream.Close();
                            }

                        }


                        MessageBox.Show("Venta Realizada");
                        DGV_Facturar.Rows.Clear();
                        tbx_Cliente.Clear();
                        lbl_Total.Text = "0";

                    }
                    else
                    
                        MessageBox.Show("Error, verifique las cantidades existentes");


                }
                else
                {
                    MessageBox.Show("Favor Validar que todos los campos esten Completados");
                    tbx_Cliente.Focus();
                }
            }

            catch
            {
                MessageBox.Show("Uno de los Productos se ingreso 2 veces y provoco un error, Favor revisar el inventario y revertir las Alteraciones");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListProduc open = new ListProduc();
            AddOwnedForm(open);
            open.ShowDialog();
        }

        private void Facturar_Load(object sender, EventArgs e)
        {

        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGV_Facturar.SelectedRows.Count > 0)
                {
                    DGV_Facturar.Rows.Remove(DGV_Facturar.CurrentRow);
                    Sumartotal();
                }
                else
                    MessageBox.Show("Seleccione la fila a Eliminar");
            }
            catch { MessageBox.Show("Error, No hay productos seleccionados"); }
        }
        public void EliminarCantidades()
        {
           
            foreach (DataGridViewRow row in DGV_Facturar.Rows)
            {
                CN_Products eliminar = new CN_Products();
                string Id = row.Cells[4].Value.ToString();
                string Cantidad = row.Cells[1].Value.ToString();

                eliminar.AlterarDatos(Convert.ToInt32(Id), Convert.ToInt32(Cantidad));
            }

        }

        private void tbx_Articulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 00 && e.KeyChar < 255))
            {
                e.Handled = true;
                return;
            }
        }
        public void MostrarFact()
        {
            CN_Ventas objetoCNa = new CN_Ventas();
            DGV_ID.DataSource = objetoCNa.MostrarFac();

        }
    }
}
