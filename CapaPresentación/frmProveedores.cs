﻿using CapaEntidad;
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
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
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

            foreach (DataGridViewColumn columna in dgvProveedores.Columns)
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
            List<clsProveedor> lista = new clsN_Proveedor().Listar();
            foreach (clsProveedor item in lista)
            {
                dgvProveedores.Rows.Add(new object[]
                {
                    "",item.IdProveedor,item.Documento,item.RazonSocial,item.Correo,item.Telefono,
                    item.Estado == true? 1 : 0,
                    item.Estado == true? "Activo" : "No Activo"
                });
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            clsProveedor obj = new clsProveedor()
            {
                IdProveedor = Convert.ToInt32(txtId.Text),
                Documento = txtDocumento.Text,
                RazonSocial = txtRazonSocial.Text,
                Correo = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Estado = Convert.ToInt32(((clsOpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
            };

            if (obj.IdProveedor == 0)
            {
                int IdGenerado = new clsN_Proveedor().Registrar(obj, out Mensaje);

                if (IdGenerado != 0)
                {
                    dgvProveedores.Rows.Add(new object[]
                    {
                    "",
                    IdGenerado,
                    txtDocumento.Text,
                    txtRazonSocial.Text,
                    txtCorreo.Text,
                    txtTelefono.Text,
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
                bool Resultado = new clsN_Proveedor().Actualizar(obj, out Mensaje);
                if (Resultado)
                {
                    DataGridViewRow row = dgvProveedores.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["Documento"].Value = txtDocumento.Text;
                    row.Cells["RazonSocial"].Value = txtRazonSocial.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
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
            txtRazonSocial.Text = "";
            txtDocumento.Text = "";
            txtId.Text = "0";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            cboEstado.SelectedIndex = 0;
            txtDocumento.Select();
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
            if (dgvProveedores.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvProveedores.Rows[indice].Cells["Id"].Value.ToString();
                    txtDocumento.Text = dgvProveedores.Rows[indice].Cells["Documento"].Value.ToString();
                    txtRazonSocial.Text = dgvProveedores.Rows[indice].Cells["RazonSocial"].Value.ToString();
                    txtCorreo.Text = dgvProveedores.Rows[indice].Cells["Correo"].Value.ToString();
                    txtTelefono.Text = dgvProveedores.Rows[indice].Cells["Telefono"].Value.ToString();

                    foreach (clsOpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvProveedores.Rows[indice].Cells["EstadoValor"].Value))
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
                if (MessageBox.Show("¿Desea eliminar este cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;
                    clsProveedor obj = new clsProveedor()
                    {
                        IdProveedor = Convert.ToInt32(txtId.Text)
                    };

                    bool Respuesta = new clsN_Proveedor().Eliminar(obj, out Mensaje);
                    if (Respuesta)
                    {
                        dgvProveedores.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
