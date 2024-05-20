using CapaEntidad;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentación
{
    public partial class Inicio : Form
    {
        private static clsUsuario UsuarioActual;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;
        public Inicio(clsUsuario objUsuario = null)
        {
            if (objUsuario == null) UsuarioActual = new clsUsuario()
            {
                NombreCompleto = "ADMIN PREDETERMINADO",
                IdUsuario = 1
            };
            else

            UsuarioActual = objUsuario;
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
        }

        private void AbrirFormulario (IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;
            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }
            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.CadetBlue;

            pnlContenedor.Controls.Add(formulario);
            formulario.Show();
        }
        private void MenuUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios());
        }

        private void submenuCategoria_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(MenuMantenedor, new frmCategoria());
        }

        private void submenuProductos_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(MenuMantenedor, new frmProductos());
        }

        private void submenuRegistrarVenta_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(MenuVentas, new frmVentas(UsuarioActual));
        }

        private void submenuDetalleVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuVentas, new frmDetalleVenta());
        }

        private void submenuRegistroCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuCompras, new frmCompras(UsuarioActual));
        }

        private void submenuVerDetalleCompra_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(MenuCompras, new frmDetalleCompra());
        }

        private void MenuClientes_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes());
        }

        private void MenuProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores());
        }

        private void negocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuMantenedor, new frmNegocio());
        }

        private void submenuReporteCompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuReportes, new frmReportesCompras());
        }
        private void Inicio_Load_1(object sender, EventArgs e)
        {

           /* List<clsPermiso> ListaPermisos = new clsN_Permiso().Listar(UsuarioActual.IdUsuario);

            foreach (IconMenuItem iconmenu in menu.Items)
            {
                bool Encontrado = ListaPermisos.Any(m => m.NombreMenu == iconmenu.Name);
                if (Encontrado == false)
                {
                    iconmenu.Visible = false;
                }
            }*/

            label.Text = UsuarioActual.NombreCompleto;
        }

        private void submenuReporteVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuReportes, new frmReporteVentas());
        }
    }
}
