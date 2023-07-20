using System;
using System.IO;
using System.Windows.Forms;
using Capa_Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Solo_Cuero
{
    public partial class Facturar : Form
    {
        public Facturar()
        {
            InitializeComponent();

            //Ocultamos la columna del ID del Producto en el DataGripview
            try
            {
                DGV_Facturar.Columns[4].Visible = false;
            }
            catch { MessageBox.Show("Hubo un Error inesperado, Favor intentar nuevamente"); }
            
        }

        //Método para sumar el total de la factura y visualizarla en un label
        public void Sumartotal()
        {
            try
            {
                decimal total = 0;
                foreach (DataGridViewRow row in DGV_Facturar.Rows)
                {
                    total += Convert.ToDecimal(row.Cells[3].Value);
                }
                lbl_Total.Text = total.ToString();
            }
            catch { MessageBox.Show("Hubo un Error inesperado, Favor intentar nuevamente"); }
        }

        //Método para eliminar las cantidades a facturar
        public void EliminarCantidades()
        {
            try
            {
                foreach (DataGridViewRow row in DGV_Facturar.Rows)
                {
                    CN_Products eliminar = new CN_Products();
                    string Id = row.Cells[4].Value.ToString();
                    string Cantidad = row.Cells[1].Value.ToString();
                    eliminar.AlterarDatos(Convert.ToInt32(Id), Convert.ToInt32(Cantidad));
                }
            }
            catch { MessageBox.Show("Hubo un Error inesperado, Favor intentar nuevamente"); }

        }

        //Método para mostrar la ultima factura
        public void MostrarFact()
        {
            try
            {
                CN_Ventas objetoCNa = new CN_Ventas();
                DGV_ID.DataSource = objetoCNa.MostrarFac();
            }
            catch { MessageBox.Show("Hubo un Error inesperado, Favor intentar nuevamente"); }

        }

        //Restricción de inserción de solo números
        private void tbx_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        //Restricción de Caracteres, el usuario no podra ingresar datos no exitentes
        private void tbx_Articulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 00 && e.KeyChar < 255))
            {
                e.Handled = true;
                return;
            }
        }

        //Evento Click del Boton para agregar productos para su venta
        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            try 
            { 
                //Si se ingresa el articulo y la cantidad a vender proceder si no mandar a completar los datos requeridos
                if (tbx_Articulo.Text == string.Empty || tbx_Cantidad.Text == string.Empty || lbl_Precio.Text == string.Empty || lbl_ID.Text == string.Empty)
                {
                    MessageBox.Show("Favor completar los datos");

                }
                else
                {
                    //Variable variable que captura la cantidad de columnas existentes en el DGV
                    int RowsEscribir = DGV_Facturar.Rows.Count;
                
                    //Sumamos 1 fila mas donde se insertaran los nuevos valores
                    DGV_Facturar.Rows.Add(1);

                    //Escribimos los nuebos valores en el DGV
                    DGV_Facturar.Rows[RowsEscribir].Cells[0].Value = tbx_Articulo.Text;
                    DGV_Facturar.Rows[RowsEscribir].Cells[1].Value = tbx_Cantidad.Text;
                    DGV_Facturar.Rows[RowsEscribir].Cells[2].Value = lbl_Precio.Text;
                    DGV_Facturar.Rows[RowsEscribir].Cells[3].Value = double.Parse (lbl_Precio.Text) * double.Parse (tbx_Cantidad.Text);
                    DGV_Facturar.Rows[RowsEscribir].Cells[4].Value = lbl_ID.Text;

                    //Sumamos el monto total de la factura
                    Sumartotal();

                    //Limpiamos los textbox y reinicamos la Tabulacion 
                    tbx_Articulo.Clear();
                    tbx_Cantidad.Clear();
                    lbl_Precio.Text = "0";
                    tbx_Articulo.TabIndex = 1;
                }
            }
            catch { MessageBox.Show("Hubo un Error inesperado, Favor intentar nuevamente"); }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                //Si ya tenemos productos para vender y seleccionado el cliente procedamos con las validaciones para facturar sino enviar error
                if (DGV_Facturar.RowCount >= 1 && tbx_Cliente.Text.Length >= 1)
                {
                    //Boleano que capturara si hay productos que con existencias validas o no validas
                    bool continuar = true;

                    //Ciclo para determinar si los productos a facturar no sobrepasan las existencias 
                    foreach (DataGridViewRow row in DGV_Facturar.Rows)
                    {
                        //Instanciamos la clase CN_Products para validar las cantidades
                        CN_Products cantidad = new CN_Products();
                        string Id = row.Cells[4].Value.ToString();
                        string Cantidad = row.Cells[1].Value.ToString();
                        bool leer;
                        leer = cantidad.CantProd(Convert.ToInt32(Id), Convert.ToInt32(Cantidad));
                        //Si la cantidad es valida sera true de lo contrario sera false y la variable cambiara de valor para cancelar el proceso y enviar error
                        if (leer == false)
                        {
                            continuar = false;  
                            break;
                        }
                        
                    }
                    //Si el boleano es falso no se procede a facturar y enviamos el error, de lo contrario continuamos continuamos con la facturacion
                    if (continuar == true)
                    {
                        //Instanciamos nuestras clase para factura
                        CN_Factura objetoF = new CN_Factura();
                        //Eliminamos las cantidades de los productos mediante el metodo EliminarCantidades
                        EliminarCantidades();
                        //Enviamos a insertar la nueva Factura
                        objetoF.InsertFactura(tbx_Cliente.Text, double.Parse(lbl_Total.Text));

                        //Declaramos las variables
                        int count = DGV_Facturar.RowCount;
                        string ProductoA;
                        string CantidadA;
                        string PrecioA;
                        
                        //Insertamos todos los productos al detalle de factura mediante un ciclo
                        foreach (DataGridViewRow row in DGV_Facturar.Rows)
                        {
                            //hacemos una nueva instancia de CN_Factura
                            CN_Factura objetoFa = new CN_Factura();
                            //Agregamos el detalle
                            ProductoA = row.Cells["Articulo"].Value.ToString();
                            CantidadA = row.Cells["Cantidad"].Value.ToString();
                            PrecioA = row.Cells["Precio"].Value.ToString();
                            //Enviamos a llenar el detalle de factura
                            objetoFa.DetalleFactura (ProductoA, Convert.ToInt32(CantidadA), Convert.ToDouble(PrecioA));
                            
                        }
                        //Llamamos al metodo MostrarFact para tomar el numero de factura y agregarlo al pdf
                        MostrarFact();
                        int idf = 0;
                        foreach (DataGridViewRow row in DGV_ID.Rows)
                        {
                            idf = Convert.ToInt32(row.Cells[0].Value);
                            break;
                        }
                        lbl_ID.Text = idf.ToString();

                        //Abrimos El Explorador de Archivos para seleccionar el lugar donde se guardara la factura
                        SaveFileDialog savefile = new SaveFileDialog();
                        //Especificamos como se llamara el documento y su tipo pdf
                        savefile.FileName = string.Format("{0}.pdf", "Factura "+lbl_ID.Text);

                        //rellenamos la PaginaHtml con los datos de la factura
                        string PaginaHTML_Texto = Properties.Resources.Plantilla.ToString();
                        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CLIENTE", tbx_Cliente.Text);
                        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

                        //Agregamos a cada fila su respectivo producto
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

                        //Agregamos el numero de factura, las filas y el total de la venta
                        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@Factura", lbl_ID.Text);
                        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas);
                        PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", lbl_Total.Text);

                        //Si el usuario acepta guardar la factura, proceder a crearla
                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                            {
                                //Tamaño de la factura
                                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(new Phrase(""));
                                //Agregar el logo a la Factura
                                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.Solo_cuero_Logo, System.Drawing.Imaging.ImageFormat.Png);
                                img.ScaleToFit(60, 60);
                                img.Alignment = iTextSharp.text.Image.UNDERLYING;
                                //posicionamiento de la imagen
                                img.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 60);
                                pdfDoc.Add(img);

                                using (StringReader sr = new StringReader(PaginaHTML_Texto))
                                {
                                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                                }

                                //Cerramos el Explorador de archivos y el documento
                                pdfDoc.Close();
                                stream.Close();
                            }

                        }

                        //Notificamos la venta exitosa y limpiamos el formulario
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

        //Abrimos el Formulacio con los productos disponibles
        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            try
            {
                ListProduc open = new ListProduc();
                AddOwnedForm(open);
                open.ShowDialog();
            }
            catch {MessageBox.Show("Hubo un Error inesperado, Favor intentar nuevamente");}

        }

        //Evento Click del Boton para eliminar productos que se iban a vender 
        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //Si hay una fila seleccionada eliminarla sino enviar mensaje de error
                if (DGV_Facturar.SelectedRows.Count > 0)
                {
                    DGV_Facturar.Rows.Remove(DGV_Facturar.CurrentRow);
                    Sumartotal();
                }
                else
                    MessageBox.Show("Seleccione la fila a Eliminar");
            }
            catch { MessageBox.Show("Error, No hay un producto seleccionado"); }
        }
        
    }
}
