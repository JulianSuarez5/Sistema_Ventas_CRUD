using CapaEntidad;
using CapaNegocio;
using CapaPresentación.Utilidades;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentación
{
    public partial class frmReportesCompras : Form
    {
        public frmReportesCompras()
        {
            InitializeComponent();
        }

        private void frmReportesCompras_Load(object sender, EventArgs e)
        {
            List<clsProveedor> lista = new clsN_Proveedor().Listar();

            cboProveedor.Items.Add(new clsOpcionCombo() { Valor = 0, Texto = "TODOS" });
            foreach (clsProveedor item in lista)
            {
                cboProveedor.Items.Add(new clsOpcionCombo() { Valor = item.IdProveedor, Texto = item.RazonSocial });
            }
            cboProveedor.DisplayMember = "Texto";
            cboProveedor.ValueMember = "Valor";
            cboProveedor.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvReportCompra.Columns)
            {
                cboBusqueda.Items.Add(new clsOpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;
        }

        private void btnBuscarResultado_Click(object sender, EventArgs e)
        {
            int idproveedor = Convert.ToInt32(((clsOpcionCombo)cboProveedor.SelectedItem).Valor.ToString());

            List<clsReporteCompra> lista = new List<clsReporteCompra>();

            lista = new clsN_Reporte().Compra(
                dtpFechaInicio.Value.ToString(),
                dtpFechaFin.Value.ToString(),
                idproveedor
                );


            dgvReportCompra.Rows.Clear();

            foreach (clsReporteCompra rc in lista)
            {
                dgvReportCompra.Rows.Add(new object[] {
                    rc.FechaRegistro,
                    rc.TipoDocumento,
                    rc.NumeroDocumento,
                    rc.MontoTotal,
                    rc.UsuarioRegistro,
                    rc.DocumentoProveedor,
                    rc.RazonSocial,
                    rc.CodigoProducto,
                    rc.NombreProducto,
                    rc.Categoria,
                    rc.PrecioCompra,
                    rc.PrecioVenta,
                    rc.Cantidad,
                    rc.SubTotal
                });
        }   }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvReportCompra.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar",
                                 "Mensaje",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn columna in dgvReportCompra.Columns)
                {
                        dt.Columns.Add(columna.HeaderText, typeof(string));
                }
                foreach (DataGridViewRow row in dgvReportCompra.Rows)
                {
                    if (row.Visible)
                        dt.Rows.Add(new object[]
                        {
                            row.Cells[0].Value.ToString(),
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[10].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            row.Cells[12].Value.ToString(),
                            row.Cells[13].Value.ToString(),
                        });


                }
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteCompra_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                saveFile.Filter = "Excel Files | *.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(saveFile.FileName);
                        MessageBox.Show("Reporte generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {

                        MessageBox.Show("Error al generar el reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string FiltroColumna = ((clsOpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();
            if (dgvReportCompra.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvReportCompra.Rows)
                {
                    if (row.Cells[FiltroColumna].Value.ToString().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;
                    }
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvReportCompra.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
