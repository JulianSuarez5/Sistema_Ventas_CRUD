using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentación.Utilidades;
using CapaEntidad;
using CapaNegocio;
using System.Xml.Linq;
using System.Diagnostics.Eventing.Reader;

namespace CapaPresentación
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
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

            List<clsRol> listaRol = new clsN_Rol().Listar();
            foreach (clsRol item in listaRol)
            {
                cboRol.Items.Add(new clsOpcionCombo()
                {
                    Valor = item.IdRol,
                    Texto = item.Descripcion

                });
            }
            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvUsuarios.Columns)
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
            List<clsUsuario> listaUsuario = new clsN_Usuario().Listar();
            foreach (clsUsuario item in listaUsuario)
            {
                dgvUsuarios.Rows.Add(new object[]
            {
                    "",item.IdUsuario,item.Documento,item.NombreCompleto,item.Correo,item.Clave,
                    item.objRol.IdRol,
                    item.objRol.Descripcion,
                    item.Estado == true? 1 : 0,
                    item.Estado == true? "Activo" : "No Activo"
                });
            }
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            clsUsuario objUsuario = new clsUsuario()
            {
                IdUsuario = Convert.ToInt32(txtId.Text),
                Documento = txtDocumento.Text,
                NombreCompleto = txtNombreCompleto.Text,
                Correo = txtCorreo.Text,
                Clave = txtClave.Text,
                objRol = new clsRol() { IdRol = Convert.ToInt32(((clsOpcionCombo)cboRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((clsOpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
            };

            if (objUsuario.IdUsuario == 0)
            {
                int IdUsuarioGenerado = new clsN_Usuario().Registrar(objUsuario, out Mensaje);

                if (IdUsuarioGenerado != 0)
                {
                    dgvUsuarios.Rows.Add(new object[]
                    {
                    "",
                    IdUsuarioGenerado,
                    txtDocumento.Text,
                    txtNombreCompleto.Text,
                    txtCorreo.Text,
                    txtClave.Text,
                    ((clsOpcionCombo)cboRol.SelectedItem).Valor.ToString(),
                    ((clsOpcionCombo)cboRol.SelectedItem).Texto.ToString(),
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
                bool Resultado = new clsN_Usuario().Actualizar(objUsuario, out Mensaje);
                if (Resultado)
                {
                    DataGridViewRow row = dgvUsuarios.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["Documento"].Value = txtDocumento.Text;
                    row.Cells["Nombre_Completo"].Value = txtNombreCompleto.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text;
                    row.Cells["Clave"].Value = txtClave.Text;
                    row.Cells["Id_Rol"].Value = ((clsOpcionCombo)cboRol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((clsOpcionCombo)cboRol.SelectedItem).Texto.ToString();
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
            txtNombreCompleto.Text = "";
            txtDocumento.Text = "";
            txtId.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
            txtConfirmarClave.Text = "";
            cboEstado.SelectedIndex = 0;
            cboRol.SelectedIndex = 0;
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
            if (dgvUsuarios.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvUsuarios.Rows[indice].Cells["Id"].Value.ToString();
                    txtDocumento.Text = dgvUsuarios.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvUsuarios.Rows[indice].Cells["Nombre_Completo"].Value.ToString();
                    txtCorreo.Text = dgvUsuarios.Rows[indice].Cells["Correo"].Value.ToString();
                    txtClave.Text = dgvUsuarios.Rows[indice].Cells["Clave"].Value.ToString();
                    txtConfirmarClave.Text = dgvUsuarios.Rows[indice].Cells["Clave"].Value.ToString();

                    foreach (clsOpcionCombo oc in cboRol.Items)
                    {
                        if (Convert.ToInt32 (oc.Valor) == Convert.ToInt32(dgvUsuarios.Rows[indice].Cells["Id_Rol"].Value))
                        {
                            int indice_combo = cboRol.Items.IndexOf(oc);
                            cboRol.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (clsOpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvUsuarios.Rows[indice].Cells["EstadoValor"].Value))
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
                if(MessageBox.Show("¿Desea eliminar este usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;
                    clsUsuario objUsuario = new clsUsuario()
                    {
                        IdUsuario = Convert.ToInt32(txtId.Text)
                    };

                    bool Respuesta = new clsN_Usuario().Eliminar(objUsuario, out Mensaje);
                    if (Respuesta)
                    {
                        dgvUsuarios.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
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
            if (dgvUsuarios.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvUsuarios.Rows)
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
            foreach (DataGridViewRow row in dgvUsuarios.Rows)
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
