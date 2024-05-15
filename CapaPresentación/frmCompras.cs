using CapaEntidad;
using CapaNegocio;
using CapaPresentación.Modales;
using CapaPresentación.Utilidades;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class frmCompras : Form
    {
        private clsUsuario Usuario_;
        public frmCompras(clsUsuario ObjUsuario = null)
        {
            Usuario_ = ObjUsuario;
            InitializeComponent();
        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Add(new clsOpcionCombo()
            {
                Valor = "Factura ordinaria",
                Texto = "Factura ordinaria"

            });

            cboTipoDocumento.Items.Add(new clsOpcionCombo()
            {
                Valor = "Factura electrónica",
                Texto = "Factura electrónica"

            });
            cboTipoDocumento.DisplayMember = "Texto";
            cboTipoDocumento.ValueMember = "Valor";
            cboTipoDocumento.SelectedIndex = 0;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtIdProveedor.Text = "0";
            txtIdProducto.Text = "0";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var formulario = new frmModalProveedor())
            {
                var resultado = formulario.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    txtIdProveedor.Text = formulario.Proveedor_.IdProveedor.ToString();
                    txtDocProveedor.Text = formulario.Proveedor_.Documento;
                    txtNombreProveedor.Text = formulario.Proveedor_.RazonSocial;
                }
                else
                {
                    txtDocProveedor.Select();
                }
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            using (var formulario = new frmModalProductos())
            {
                var resultado = formulario.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    txtIdProducto.Text = formulario.Producto_.IdProducto.ToString();
                    txtCodProducto.Text = formulario.Producto_.Codigo;
                    txtNombreProducto.Text = formulario.Producto_.Nombre;
                    txtPrecioCompra.Select();
                }
                else
                {
                    txtCodProducto.Select();
                }
            }
        }

        private void txtCodProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                clsProducto objProducto = new clsN_Producto().Listar().Where(p => p.Codigo == txtCodProducto.Text && p.Estado == true).FirstOrDefault();
                if (objProducto != null)
                {
                    txtCodProducto.BackColor = System.Drawing.Color.Honeydew;
                    txtIdProducto.Text = objProducto.IdProducto.ToString();
                    txtNombreProducto.Text = objProducto.Nombre;
                    txtPrecioCompra.Select();
                }
                else
                {
                    txtCodProducto.BackColor = System.Drawing.Color.MistyRose;
                    txtIdProducto.Text = "0";
                    txtNombreProducto.Text = "";
                }
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            decimal Precio_Compra = 0;
            decimal Precio_Venta = 0;
            bool Existe_Producto = false;

            // Verificar que se haya seleccionado un producto
            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto",
                                 "Mensaje",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Exclamation);
                return; // Salir del método si no se ha seleccionado un producto
            }

            // Verificar que los campos de texto no estén vacíos
            if (string.IsNullOrWhiteSpace(txtPrecioCompra.Text) || string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
            {
                MessageBox.Show("Debe ingresar los precios de compra y venta",
                                "Mensaje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return; // Salir del método si los campos de texto están vacíos
            }

            // Validar el formato de los precios y convertirlos a decimal
            if (!decimal.TryParse(txtPrecioCompra.Text, out Precio_Compra))
            {
                MessageBox.Show("Precio de compra con formato incorrecto",
                                "Mensaje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                txtPrecioCompra.Select();
                return;
            }

            if (!decimal.TryParse(txtPrecioVenta.Text, out Precio_Venta))
            {
                MessageBox.Show("Precio de venta con formato incorrecto",
                                "Mensaje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                txtPrecioVenta.Select();
                return;
            }

            // Verificar si el producto ya existe en el DataGridView
            foreach (DataGridViewRow fila in dgvProductos.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtIdProducto.Text)
                {
                    Existe_Producto = true;
                    MessageBox.Show("El producto ya ha sido agregado",
                                    "Mensaje",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    break;
                }
            }

            // Si el producto no existe, agregarlo al DataGridView
            if (!Existe_Producto)
            {
                dgvProductos.Rows.Add(new object[]
                {
            txtIdProducto.Text,
            txtNombreProducto.Text,
            Precio_Compra.ToString("0.00"),
            Precio_Venta.ToString("0.00"),
            nudCantidad.Value.ToString(),
            (nudCantidad.Value * Precio_Compra).ToString("0.00")
                });
                CalcularTotalApAGAR();
                LimpiarControles();
                txtCodProducto.Select();
            }
        }

        private void LimpiarControles()
        {
            txtIdProducto.Text = "0";
            txtCodProducto.Text = "";
            txtCodProducto.BackColor = System.Drawing.Color.White;
            txtNombreProducto.Text = "";
            txtPrecioCompra.Text = "0";
            txtPrecioVenta.Text = "0";
            nudCantidad.Value = 1;
        }

        private void CalcularTotalApAGAR()
        {
            decimal Total = 0;
            if (dgvProductos.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgvProductos.Rows)
                    Total += Convert.ToDecimal(fila.Cells["Subtotal"].Value.ToString()); 
            }
            txtTotalaPagar.Text = Total.ToString("0.00");
        }

        private void dgvProductos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 6)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var W = Properties.Resources.delete30_2.Width;
                var h = Properties.Resources.delete30_2.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - W) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete30_2, new Rectangle(x, y, W, h));
                e.Handled = true;
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    dgvProductos.Rows.RemoveAt(indice);
                    CalcularTotalApAGAR();
                }
            }
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecioCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else 
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecioVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32 (txtIdProveedor.Text) ==0)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvProductos.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la compra", "Mensaje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dtDetalleCompra = new DataTable();
            dtDetalleCompra.Columns.Add("IdProducto", typeof(int));
            dtDetalleCompra.Columns.Add("PrecioCompra", typeof(decimal));
            dtDetalleCompra.Columns.Add("PrecioVenta", typeof(decimal));
            dtDetalleCompra.Columns.Add("Cantidad", typeof(int));
            dtDetalleCompra.Columns.Add("MontoTotal", typeof(decimal));

            foreach (DataGridViewRow fila  in dgvProductos.Rows)
            {
                dtDetalleCompra.Rows.Add(new object[]
                {
                    Convert.ToInt32(fila.Cells["IdProducto"].Value.ToString()),
                    fila.Cells["PrecioCompra"].Value.ToString(),
                    fila.Cells["PrecioVenta"].Value.ToString(),
                    fila.Cells["Cantidad"].Value.ToString(),
                    fila.Cells["Subtotal"].Value.ToString()
                });
            }

            int IdConsecutivo = new clsN_Compra().ObtenerConsecutivo();
            string nrodocumento = string.Format("{0:00000}", IdConsecutivo);

            clsCompra objCompra = new clsCompra()
            {
                objUsuario = new clsUsuario() { IdUsuario = Usuario_.IdUsuario},
                objProveedor = new clsProveedor() {IdProveedor = Convert.ToInt32(txtIdProveedor.Text)},
                TipoDocumento = ((clsOpcionCombo)cboTipoDocumento.SelectedItem).Texto,
                NumeroDocumento = nrodocumento,
                MontoTotal = Convert.ToDecimal(txtTotalaPagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new clsN_Compra().Registrar(objCompra, dtDetalleCompra, out mensaje);

            if (respuesta)
            {
                var resultado = MessageBox.Show("Numero de compra generada:\n" + nrodocumento + 
                                                "\n\n¿Desea copiar al portapapeles?","Mensaje", 
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes)
                    Clipboard.SetText(nrodocumento);

                txtIdProveedor.Text = "0";
                txtDocProveedor.Text = "";
                txtNombreProveedor.Text = "";
                dgvProductos.Rows.Clear();
                CalcularTotalApAGAR();
            }

            else
            {
                MessageBox.Show(mensaje, "Mensaje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }
    }
}
