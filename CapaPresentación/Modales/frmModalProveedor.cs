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

namespace CapaPresentación.Modales
{
    public partial class frmModalProveedor : Form
    {
        public clsProveedor Proveedor_ { get; set; }
        public frmModalProveedor()
        {
            InitializeComponent();
        }

        private void frmModalProveedor_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvProveedores.Columns)
            {
                if (columna.Visible == true)
                {
                    cboBusqueda.Items.Add(new clsOpcionCombo()
                    {
                        Valor = columna.Name,
                        Texto = columna.HeaderText

                    });
                }
                cboBusqueda.DisplayMember = "Texto";
                cboBusqueda.ValueMember = "Valor";
                if (cboBusqueda.Items.Count > 0)
                {
                    cboBusqueda.SelectedIndex = 0;
                }
            }

            //Mostrar todos los usuarios
            List<clsProveedor> lista = new clsN_Proveedor().Listar();
            foreach (clsProveedor item in lista)
            {
                dgvProveedores.Rows.Add(new object[]
                {
                    item.IdProveedor,item.Documento,item.RazonSocial
                });
            }
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = e.RowIndex;
            int columna = e.ColumnIndex;

            if (fila >= 0 && columna > 0)
            {
                Proveedor_ = new clsProveedor
                {
                    IdProveedor = Convert.ToInt32(dgvProveedores.Rows[fila].Cells["Id"].Value.ToString()),
                    Documento = dgvProveedores.Rows[fila].Cells["Documento"].Value.ToString(),
                    RazonSocial = dgvProveedores.Rows[fila].Cells["RazonSocial"].Value.ToString(),
                };
                this.DialogResult = DialogResult.OK;

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string FiltroColumna = ((clsOpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();
            if (dgvProveedores.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvProveedores.Rows)
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
            foreach (DataGridViewRow row in dgvProveedores.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
