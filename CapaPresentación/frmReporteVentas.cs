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
    public partial class frmReporteVentas : Form
    {
        public frmReporteVentas()
        {
            InitializeComponent();
        }

        private void frmReporteVentas_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvReporteVentas.Columns)
            {
                cboBusqueda.Items.Add(new clsOpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;
        }

        private void btnBuscarResultado_Click(object sender, EventArgs e)
        {
            List<clsReporteVentas> lista = new List<clsReporteVentas>();

            lista = new clsN_Reporte().Venta(dtpFechaInicio.Value.ToString(), dtpFechaFin.Value.ToString());

            dgvReporteVentas.Rows.Clear();

            foreach (clsReporteVentas rv in lista)
            {
                dgvReporteVentas.Rows.Add(new object[]
                {
                    rv.FechaRegistro,
                    rv.TipoDocumento,
                    rv.NumeroDocumento,
                    rv.MontoTotal,
                    rv.UsuarioRegistro,
                    rv.DocumentoCliente,
                    rv.NombreCliente,
                    rv.CodigoProducto,
                    rv.NombreProducto,
                    rv.Categoria,
                    rv.Precio_Venta,
                    rv.Cantidad,
                    rv.SubTotal
                });
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string FiltroColumna = ((clsOpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();
            if (dgvReporteVentas.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvReporteVentas.Rows)
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
            foreach (DataGridViewRow row in dgvReporteVentas.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvReporteVentas.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar",
                                 "Mensaje",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn columna in dgvReporteVentas.Columns)
                {
                    dt.Columns.Add(columna.HeaderText, typeof(string));
                }
                foreach (DataGridViewRow row in dgvReporteVentas.Rows)
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
                        });


                }
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteVentas_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
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
    }
}
