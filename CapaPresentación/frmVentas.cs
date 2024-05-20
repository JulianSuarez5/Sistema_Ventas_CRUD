using CapaEntidad;
using CapaNegocio;
using CapaPresentación.Modales;
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
    public partial class frmVentas : Form
    {
        private clsUsuario _Usuario;
        public frmVentas(clsUsuario objUsuario = null)
        {
            _Usuario = objUsuario;
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Add(new clsOpcionCombo() { Valor = "Factura Electrónica", Texto = "Factura Electrónica"});
            cboTipoDocumento.Items.Add(new clsOpcionCombo() { Valor = "Factura Ordinaria", Texto = "Factura Ordinaria"});
            cboTipoDocumento.DisplayMember = "Texto";
            cboTipoDocumento.ValueMember = "Valor";
            cboTipoDocumento.SelectedIndex = 0;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdProducto.Text = "0";

            txtPagaCon.Text = "0";
            txtTotalaPagar.Text = "0";
            txtCambio.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var formulario = new frmModalClientes())
            {
                var resultado = formulario.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    txtDocCliente.Text = formulario._Cliente.Documento;
                    txtNombreCliente.Text = formulario._Cliente.NombreCompleto;
                    txtCodProducto.Select();
                }
                else
                {
                    txtDocCliente.Select();
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
                    txtPrecio.Text = formulario.Producto_.Precio_Venta.ToString("0.00");
                    txtStock.Text = formulario.Producto_.Stock.ToString();
                    txtNombreProducto.Text = formulario.Producto_.Nombre;
                    nudCantidad.Select();
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
                    txtCodProducto.BackColor = Color.Honeydew;
                    txtIdProducto.Text = objProducto.IdProducto.ToString();
                    txtNombreProducto.Text = objProducto.Nombre;
                    txtPrecio.Text = objProducto.Precio_Venta.ToString("0.00");
                    txtStock.Text = objProducto.Stock.ToString();
                    nudCantidad.Select();
                }
                else
                {
                    txtCodProducto.BackColor = Color.MistyRose;
                    txtIdProducto.Text = "0";
                    txtNombreProducto.Text = "";
                    txtPrecio.Text ="";
                    txtStock.Text = "";
                    nudCantidad.Value = -1;
                }
            }
        }

        private void btnAgregarVenta_Click(object sender, EventArgs e)
        {
            decimal Precio = 0;
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
            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Debe ingresar los precios de compra y venta",
                                "Mensaje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return; // Salir del método si los campos de texto están vacíos
            }

            // Validar el formato de los precios y convertirlos a decimal
            if (!decimal.TryParse(txtPrecio.Text, out Precio))
            {
                MessageBox.Show("Precio de compra con formato incorrecto",
                                "Mensaje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                                txtPrecio.Select();
                return;
            }

            if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(nudCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor al Stock",
                                "Mensaje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            // Verificar si el producto ya existe en el DataGridView
            foreach (DataGridViewRow fila in dgvVentas.Rows)
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
                bool respuesta = new clsN_Venta().RestarStock(
                    Convert.ToInt32(txtIdProducto.Text),
                    Convert.ToInt32(nudCantidad.Value.ToString()));

                if (respuesta)
                {
                    dgvVentas.Rows.Add(new object[]
                    {
                        txtIdProducto.Text,
                        txtNombreProducto.Text,
                        Precio.ToString("0.00"),
                        nudCantidad.Value.ToString(),
                        (nudCantidad.Value * Precio).ToString("0.00")
                    });
                    CalcularTotalApAGAR();
                    LimpiarControles();
                    txtCodProducto.Focus();
                }
            }
        }
        private void CalcularTotalApAGAR()
        {
            decimal Total = 0;
            if (dgvVentas.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgvVentas.Rows)
                    Total += Convert.ToDecimal(fila.Cells["Subtotal"].Value.ToString());
            }
            txtTotalaPagar.Text = Total.ToString("0.00");
        }

        private void LimpiarControles()
        {
            txtIdProducto.Text = "0";
            txtCodProducto.Text = "";
            txtCodProducto.BackColor = System.Drawing.Color.White;
            txtNombreProducto.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            nudCantidad.Value = 1;
        }

        private void dgvVentas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 5)
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

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVentas.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    bool respuesta = new clsN_Venta().SumarStock(
                        Convert.ToInt32(dgvVentas.Rows[indice].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dgvVentas.Rows[indice].Cells["Cantidad"].Value.ToString()));

                    if (respuesta)
                    {
                        dgvVentas.Rows.RemoveAt(indice);
                        CalcularTotalApAGAR();
                    }
                }
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void txtPagaCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPagaCon.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void CalcularCambio()
        {
            if (txtTotalaPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta",
                                "Mensaje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            decimal PagaCon;
            decimal Total = Convert.ToDecimal(txtTotalaPagar.Text);

            if (txtPagaCon.Text.Trim() =="")
            {
                txtPagaCon.Text = "0";
            }

            if (decimal.TryParse(txtPagaCon.Text.Trim(), out PagaCon))
            {
                if(PagaCon < Total)
                {
                    txtCambio.Text = "0.00";
                }
                else
                {
                    decimal cambio = PagaCon - Total;
                    txtCambio.Text = cambio.ToString("0.00");
                }
            }
        }

        private void txtPagaCon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                CalcularCambio();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtDocCliente.Text == "")
            {
                MessageBox.Show("Debe ingrwsar el documento del cliente",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (txtNombreCliente.Text == "")
            {
                MessageBox.Show("Debe ingrsar el nombre del cliente",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvVentas.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la venta",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            DataTable Detalle_Ventas = new DataTable();

            Detalle_Ventas.Columns.Add("IdProducto", typeof(int));
            Detalle_Ventas.Columns.Add("PrecioVenta", typeof(double));
            Detalle_Ventas.Columns.Add("Cantidad", typeof(int));
            Detalle_Ventas.Columns.Add("Subtotal", typeof(decimal));

            foreach (DataGridViewRow fila in dgvVentas.Rows)
            {
                Detalle_Ventas.Rows.Add(new object[]
                {
                    fila.Cells["IdProducto"].Value.ToString(),
                    fila.Cells["Precio"].Value.ToString(),
                    fila.Cells["Cantidad"].Value.ToString(),
                    fila.Cells["Subtotal"].Value.ToString(),
                });
            }

            int idconsecutivo = new clsN_Venta().ObtenerConsecutivo();
            string numerodocumento = string.Format("{0:00000}", idconsecutivo);
            CalcularCambio();

            clsVenta objVenta = new clsVenta()
            {
                objUsuario = new clsUsuario() { IdUsuario = _Usuario.IdUsuario },
                TipoDocumento = ((clsOpcionCombo)cboTipoDocumento.SelectedItem).Texto,
                NumeroDocumento = numerodocumento,
                DocumentoCliente = txtDocCliente.Text,
                NombreCliente = txtNombreCliente.Text,
                MontoPago = Convert.ToDecimal(txtPagaCon.Text),
                MontoCambio = Convert.ToDecimal(txtCambio.Text),
                MontoTotal = Convert.ToDecimal(txtTotalaPagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new clsN_Venta().Registrar(objVenta, Detalle_Ventas, out mensaje);

            if (respuesta)
            {
                var resultado = MessageBox.Show("Numero de venta generado:\n" + numerodocumento + "\n\n¿desea copiar al portapapeles",
                                                 "Mensaje",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes)
                    Clipboard.SetText(numerodocumento);

                txtDocCliente.Text = "";
                txtNombreCliente.Text = "";
                dgvVentas.Rows.Clear();
                CalcularTotalApAGAR();
                txtCambio.Text = "";
                txtPagaCon.Text = "";
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
