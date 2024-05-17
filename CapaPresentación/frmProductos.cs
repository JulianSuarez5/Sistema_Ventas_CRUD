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
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentación
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            cboEstado.Items.Add(new clsOpcionCombo()
            {
                Valor = 1,
                Texto = "Activo"

            });

            cboEstado.Items.Add(new clsOpcionCombo()
            {
                Valor = 0,
                Texto = "No Activo"

            });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            List<clsCategoria> listaCategoria = new clsN_Categoria().Listar();
            foreach (clsCategoria item in listaCategoria)
            {
                cboCategoria.Items.Add(new clsOpcionCombo()
                {
                    Valor = item.IdCategoria,
                    Texto = item.Descripcion

                });
            }
            cboCategoria.DisplayMember = "Texto";
            cboCategoria.ValueMember = "Valor";
            cboCategoria.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvProductos.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
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
            List<clsProducto> lista = new clsN_Producto().Listar();
            foreach (clsProducto item in lista)
            {
                dgvProductos.Rows.Add(new object[]
            {
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.objCategoria.IdCategoria,
                    item.objCategoria.Descripcion,
                    item.Stock, 
                    item.Precio_Compra,
                    item.Precio_Venta,
                    item.Estado == true? 1 : 0,
                    item.Estado == true? "Activo" : "No Activo"
                });
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            clsProducto obj = new clsProducto()
            {
                IdProducto = Convert.ToInt32(txtId.Text),
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                objCategoria = new clsCategoria() { IdCategoria = Convert.ToInt32(((clsOpcionCombo)cboCategoria.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((clsOpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
            };

            if (obj.IdProducto == 0)
            {
                int IdGenerado = new clsN_Producto().Registrar(obj, out Mensaje);

                if (IdGenerado != 0)
                {
                    dgvProductos.Rows.Add(new object[]
                    {
                        "",
                        IdGenerado,
                        txtCodigo.Text,
                        txtNombre.Text,
                        txtDescripcion.Text,
                        ((clsOpcionCombo)cboCategoria.SelectedItem).Valor.ToString(),
                        ((clsOpcionCombo)cboCategoria.SelectedItem).Texto.ToString(),
                        "0",
                        "0.00",
                        "0.00",
                        ((clsOpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                        ((clsOpcionCombo)cboEstado.SelectedItem).Texto.ToString(),
                    });
                    Limpiar();
                }

                else
                {
                    MessageBox.Show(Mensaje);
                }
            }
            else
            {
                bool Resultado = new clsN_Producto().Actualizar(obj, out Mensaje);
                if (Resultado)
                {
                    DataGridViewRow row = dgvProductos.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["Codigo"].Value = txtCodigo.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Descripcion"].Value = txtDescripcion.Text;
                    row.Cells["Id_Categoria"].Value = ((clsOpcionCombo)cboCategoria.SelectedItem).Valor.ToString();
                    row.Cells["Categoria"].Value = ((clsOpcionCombo)cboCategoria.SelectedItem).Texto.ToString();
                    row.Cells["EstadoValor"].Value = ((clsOpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((clsOpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(Mensaje);
                }
            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtId.Text = "0";
            txtDescripcion.Text = "";
            cboEstado.SelectedIndex = 0;
            cboCategoria.SelectedIndex = 0;
            txtCodigo.Select();
        }

        private void dgvUsuarios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var W = Properties.Resources.check30__1_.Width;
                var h = Properties.Resources.check30__1_.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - W) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check30__1_, new Rectangle(x, y, W, h));
                e.Handled = true;
            }
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvProductos.Rows[indice].Cells["Id"].Value.ToString();
                    txtCodigo.Text = dgvProductos.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtNombre.Text = dgvProductos.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtDescripcion.Text = dgvProductos.Rows[indice].Cells["Descripcion"].Value.ToString();

                    foreach (clsOpcionCombo oc in cboCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvProductos.Rows[indice].Cells["Id_Categoria"].Value))
                        {
                            int indice_combo = cboCategoria.Items.IndexOf(oc);
                            cboCategoria.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (clsOpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvProductos.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar este producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;
                    clsProducto obj = new clsProducto()
                    {
                        IdProducto = Convert.ToInt32(txtId.Text)
                    };

                    bool Respuesta = new clsN_Producto().Eliminar(obj, out Mensaje);
                    if (Respuesta)
                    {
                        dgvProductos.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.Rows.Count < 1)
            {
                MessageBox.Show("No hay para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn columna in dgvProductos.Columns)
                {
                    if (columna.HeaderText != "" && columna.Visible)
                        dt.Columns.Add(columna.HeaderText, typeof(string));
                }
                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    if (row.Visible)
                        dt.Rows.Add(new object[]
                        {
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                        });


                }
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
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
