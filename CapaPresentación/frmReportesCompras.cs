using CapaEntidad;
using CapaNegocio;
using CapaPresentación.Utilidades;
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
    }
}
