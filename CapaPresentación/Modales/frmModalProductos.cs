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
    public partial class frmModalProductos : Form
    {
        public clsProducto Producto_ { get; set; }
        public frmModalProductos()
        {
            InitializeComponent();
        }

        private void frmModalProductos_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvProductos.Columns)
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

            List<clsProducto> lista = new clsN_Producto().Listar();
            foreach (clsProducto item in lista)
            {
                dgvProductos.Rows.Add(new object[]
                {
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.objCategoria.Descripcion,
                    item.Stock,
                    item.Precio_Compra,
                    item.Precio_Venta
                });
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = e.RowIndex;
            int columna = e.ColumnIndex;

            if (fila >= 0 && columna > 0) 
            {
                Producto_ = new clsProducto()
                {
                    IdProducto = Convert.ToInt32(dgvProductos.Rows[fila].Cells["Id"].Value.ToString()),
                    Codigo = dgvProductos.Rows[fila].Cells["Codigo"].Value.ToString(),
                    Nombre = dgvProductos.Rows[fila].Cells["Nombre"].Value.ToString(),
                    Stock = Convert.ToInt32(dgvProductos.Rows[fila].Cells["Stock"].Value.ToString()),
                    Precio_Compra = Convert.ToDecimal(dgvProductos.Rows[fila].Cells["PrecioCompra"].Value.ToString()),
                    Precio_Venta = Convert.ToDecimal(dgvProductos.Rows[fila].Cells["PrecioVenta"].Value.ToString()),
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }   
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string FiltroColumna = ((clsOpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();
            if (dgvProductos.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvProductos.Rows)
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
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
