using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;
using CapaPresentación.Utilidades;

namespace CapaPresentación.Modales
{
    public partial class frmModalClientes : Form
    {
        public clsCliente _Cliente {  get; set; }
        public frmModalClientes()
        {
            InitializeComponent();
        }

        private void frmModalClientes_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvClientes.Columns)
            {
                if (columna.Visible == true)
                    cboBusqueda.Items.Add(new clsOpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;


            List<clsCliente> liata = new clsN_Cliente().Listar();

            foreach (clsCliente item in liata)
            {
                if(item.Estado)
                    dgvClientes.Rows.Add(new object[] { item.Documento, item.NombreCompleto });
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string FiltroColumna = ((clsOpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();
            if (dgvClientes.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvClientes.Rows)
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
            foreach (DataGridViewRow row in dgvClientes.Rows)
            {
                row.Visible = true;
            }
        }

        private void dgvClientes_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int ifila = e.RowIndex;
            int icolumna = e.ColumnIndex;
            if (ifila >= 0 && icolumna >= 0)
            {
                _Cliente = new clsCliente()
                {
                    Documento = dgvClientes.Rows[ifila].Cells["Documento"].Value.ToString(),
                    NombreCompleto = dgvClientes.Rows[ifila].Cells["Nombre_Completo"].Value.ToString(),
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
