using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentación
{
    public partial class frmDetalleVenta : Form
    {
        public frmDetalleVenta()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            clsVenta objVenta = new clsN_Venta().ObtenerVenta(txtBusqueda.Text);
            if (objVenta.IdVenta != 0)
            {
                txtNumeroDocumento.Text = objVenta.NumeroDocumento;
                txtFecha.Text = objVenta.FechaRegistro;
                txtTipoDocumento.Text = objVenta.TipoDocumento;
                txtUsuario.Text = objVenta.objUsuario.NombreCompleto;

                txtDocumentoCliente.Text = objVenta.DocumentoCliente;
                txtNombreCliente.Text = objVenta.NombreCliente;

                dgvDVenta.Rows.Clear();

                foreach (clsDetalle_Venta dv in objVenta.objDetalle_Venta)
                {
                    dgvDVenta.Rows.Add(new object[] {dv.oProducto.Nombre, dv.PrecioVenta, dv.Cantidad, dv.SubTotal});
                }

                txtMontoTotal.Text = objVenta.MontoTotal.ToString("0.00");
                txtMontoPago.Text = objVenta.MontoPago.ToString("0.00");
                txtMontoCambio.Text = objVenta.MontoCambio.ToString("0.00");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocumentoCliente.Text = "";
            txtNombreCliente.Text = "";

            dgvDVenta.Rows.Clear();
            txtMontoTotal.Text = "0.00";
            txtMontoPago.Text = "0.00";
            txtMontoTotal.Text = "0.00";
        }

        private void frmDetalleVenta_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
        }

        private void btnDescargarPDF_Click(object sender, EventArgs e)
        {
            if (txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = Properties.Resources.PlantillaVenta.ToString();
            clsNegocio objDatos = new clsN_Negocio().ObtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", objDatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", objDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", objDatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Text.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtNumeroDocumento.Text);

            Texto_Html = Texto_Html.Replace("@doccliente", txtDocumentoCliente.Text);
            Texto_Html = Texto_Html.Replace("@nombrecliente", txtNombreCliente.Text);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Text);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuario.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow fila in dgvDVenta.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + fila.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + fila.Cells["PrecioVenta"].Value.ToString() + "</td>";
                filas += "<td>" + fila.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + fila.Cells["Subtotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", txtMontoTotal.Text);
            Texto_Html = Texto_Html.Replace("@pagocon", txtMontoPago.Text);
            Texto_Html = Texto_Html.Replace("@cambio", txtMontoCambio.Text);

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Venta{0}.pdf", txtNumeroDocumento.Text);
            saveFile.Filter = "Pdf Files | *.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool Obtenido = true;
                    byte[] byteImage = new clsN_Negocio().ObtenerLogo(out Obtenido);

                    if (Obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);

                        img.ScaleToFit(60, 60);

                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }
                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
