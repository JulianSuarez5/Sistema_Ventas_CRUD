namespace CapaPresentación
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.menutitulo = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.MenuUsuarios = new FontAwesome.Sharp.IconMenuItem();
            this.MenuMantenedor = new FontAwesome.Sharp.IconMenuItem();
            this.submenuCategoria = new FontAwesome.Sharp.IconMenuItem();
            this.submenuProductos = new FontAwesome.Sharp.IconMenuItem();
            this.negocio = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuVentas = new FontAwesome.Sharp.IconMenuItem();
            this.submenuRegistrarVenta = new FontAwesome.Sharp.IconMenuItem();
            this.submenuDetalleVentas = new FontAwesome.Sharp.IconMenuItem();
            this.MenuCompras = new FontAwesome.Sharp.IconMenuItem();
            this.submenuRegistroCompra = new FontAwesome.Sharp.IconMenuItem();
            this.submenuVerDetalleCompra = new FontAwesome.Sharp.IconMenuItem();
            this.MenuClientes = new FontAwesome.Sharp.IconMenuItem();
            this.MenuProveedores = new FontAwesome.Sharp.IconMenuItem();
            this.MenuReportes = new FontAwesome.Sharp.IconMenuItem();
            this.submenuReporteCompras = new System.Windows.Forms.ToolStripMenuItem();
            this.submenuReporteVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAcercaDe = new FontAwesome.Sharp.IconMenuItem();
            this.pnlContenedor = new System.Windows.Forms.Panel();
            this.btnSalir = new FontAwesome.Sharp.IconButton();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menutitulo
            // 
            this.menutitulo.AutoSize = false;
            this.menutitulo.BackColor = System.Drawing.Color.CadetBlue;
            this.menutitulo.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menutitulo.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menutitulo.Location = new System.Drawing.Point(0, 0);
            this.menutitulo.Name = "menutitulo";
            this.menutitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menutitulo.Size = new System.Drawing.Size(1919, 93);
            this.menutitulo.TabIndex = 1;
            this.menutitulo.Text = "menuStrip2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.CadetBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 46);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sistema de ventas";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.CadetBlue;
            this.label.ForeColor = System.Drawing.Color.White;
            this.label.Location = new System.Drawing.Point(1460, 43);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(64, 20);
            this.label.TabIndex = 4;
            this.label.Text = "Usuario";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.CadetBlue;
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(1346, 43);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(68, 20);
            this.lblUsuario.TabIndex = 5;
            this.lblUsuario.Text = "Usuario:";
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.BackColor = System.Drawing.Color.White;
            this.menu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.menu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuUsuarios,
            this.MenuMantenedor,
            this.MenuVentas,
            this.MenuCompras,
            this.MenuClientes,
            this.MenuProveedores,
            this.MenuReportes,
            this.MenuAcercaDe});
            this.menu.Location = new System.Drawing.Point(0, 93);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1919, 88);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // MenuUsuarios
            // 
            this.MenuUsuarios.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.MenuUsuarios.IconChar = FontAwesome.Sharp.IconChar.UserGear;
            this.MenuUsuarios.IconColor = System.Drawing.Color.Black;
            this.MenuUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuUsuarios.IconSize = 50;
            this.MenuUsuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuUsuarios.Name = "MenuUsuarios";
            this.MenuUsuarios.Size = new System.Drawing.Size(111, 82);
            this.MenuUsuarios.Text = "Usuarios";
            this.MenuUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.MenuUsuarios.Click += new System.EventHandler(this.MenuUsuarios_Click);
            // 
            // MenuMantenedor
            // 
            this.MenuMantenedor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenuCategoria,
            this.submenuProductos,
            this.negocio});
            this.MenuMantenedor.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.MenuMantenedor.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.MenuMantenedor.IconColor = System.Drawing.Color.Black;
            this.MenuMantenedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuMantenedor.IconSize = 50;
            this.MenuMantenedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuMantenedor.Name = "MenuMantenedor";
            this.MenuMantenedor.Size = new System.Drawing.Size(149, 82);
            this.MenuMantenedor.Text = "Mantenedor";
            this.MenuMantenedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenuCategoria
            // 
            this.submenuCategoria.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuCategoria.IconColor = System.Drawing.Color.Black;
            this.submenuCategoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuCategoria.Name = "submenuCategoria";
            this.submenuCategoria.Size = new System.Drawing.Size(210, 38);
            this.submenuCategoria.Text = "Categoria";
            this.submenuCategoria.Click += new System.EventHandler(this.submenuCategoria_Click_1);
            // 
            // submenuProductos
            // 
            this.submenuProductos.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuProductos.IconColor = System.Drawing.Color.Black;
            this.submenuProductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuProductos.Name = "submenuProductos";
            this.submenuProductos.Size = new System.Drawing.Size(210, 38);
            this.submenuProductos.Text = "Producto";
            this.submenuProductos.Click += new System.EventHandler(this.submenuProductos_Click_1);
            // 
            // negocio
            // 
            this.negocio.Name = "negocio";
            this.negocio.Size = new System.Drawing.Size(210, 38);
            this.negocio.Text = "Negocio";
            this.negocio.Click += new System.EventHandler(this.negocio_Click);
            // 
            // MenuVentas
            // 
            this.MenuVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenuRegistrarVenta,
            this.submenuDetalleVentas});
            this.MenuVentas.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.MenuVentas.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.MenuVentas.IconColor = System.Drawing.Color.Black;
            this.MenuVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuVentas.IconSize = 50;
            this.MenuVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuVentas.Name = "MenuVentas";
            this.MenuVentas.Size = new System.Drawing.Size(93, 82);
            this.MenuVentas.Text = "Ventas";
            this.MenuVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenuRegistrarVenta
            // 
            this.submenuRegistrarVenta.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuRegistrarVenta.IconColor = System.Drawing.Color.Black;
            this.submenuRegistrarVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuRegistrarVenta.Name = "submenuRegistrarVenta";
            this.submenuRegistrarVenta.Size = new System.Drawing.Size(310, 38);
            this.submenuRegistrarVenta.Text = "Registrar";
            this.submenuRegistrarVenta.Click += new System.EventHandler(this.submenuRegistrarVenta_Click_1);
            // 
            // submenuDetalleVentas
            // 
            this.submenuDetalleVentas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuDetalleVentas.IconColor = System.Drawing.Color.Black;
            this.submenuDetalleVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuDetalleVentas.Name = "submenuDetalleVentas";
            this.submenuDetalleVentas.Size = new System.Drawing.Size(310, 38);
            this.submenuDetalleVentas.Text = "Ver detalle de venta";
            this.submenuDetalleVentas.Click += new System.EventHandler(this.submenuDetalleVentas_Click);
            // 
            // MenuCompras
            // 
            this.MenuCompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenuRegistroCompra,
            this.submenuVerDetalleCompra});
            this.MenuCompras.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.MenuCompras.IconChar = FontAwesome.Sharp.IconChar.DollyFlatbed;
            this.MenuCompras.IconColor = System.Drawing.Color.Black;
            this.MenuCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuCompras.IconSize = 50;
            this.MenuCompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuCompras.Name = "MenuCompras";
            this.MenuCompras.Size = new System.Drawing.Size(116, 82);
            this.MenuCompras.Text = "Compras";
            this.MenuCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenuRegistroCompra
            // 
            this.submenuRegistroCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuRegistroCompra.IconColor = System.Drawing.Color.Black;
            this.submenuRegistroCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuRegistroCompra.Name = "submenuRegistroCompra";
            this.submenuRegistroCompra.Size = new System.Drawing.Size(331, 38);
            this.submenuRegistroCompra.Text = "Registrar Compra";
            this.submenuRegistroCompra.Click += new System.EventHandler(this.submenuRegistroCompra_Click);
            // 
            // submenuVerDetalleCompra
            // 
            this.submenuVerDetalleCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuVerDetalleCompra.IconColor = System.Drawing.Color.Black;
            this.submenuVerDetalleCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuVerDetalleCompra.Name = "submenuVerDetalleCompra";
            this.submenuVerDetalleCompra.Size = new System.Drawing.Size(331, 38);
            this.submenuVerDetalleCompra.Text = "Ver detalle de compra";
            this.submenuVerDetalleCompra.Click += new System.EventHandler(this.submenuVerDetalleCompra_Click_1);
            // 
            // MenuClientes
            // 
            this.MenuClientes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.MenuClientes.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.MenuClientes.IconColor = System.Drawing.Color.Black;
            this.MenuClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuClientes.IconSize = 50;
            this.MenuClientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuClientes.Name = "MenuClientes";
            this.MenuClientes.Size = new System.Drawing.Size(105, 82);
            this.MenuClientes.Text = "Clientes";
            this.MenuClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.MenuClientes.Click += new System.EventHandler(this.MenuClientes_Click_1);
            // 
            // MenuProveedores
            // 
            this.MenuProveedores.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.MenuProveedores.IconChar = FontAwesome.Sharp.IconChar.AddressCard;
            this.MenuProveedores.IconColor = System.Drawing.Color.Black;
            this.MenuProveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuProveedores.IconSize = 50;
            this.MenuProveedores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuProveedores.Name = "MenuProveedores";
            this.MenuProveedores.Size = new System.Drawing.Size(152, 82);
            this.MenuProveedores.Text = "Proveedores";
            this.MenuProveedores.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.MenuProveedores.Click += new System.EventHandler(this.MenuProveedores_Click);
            // 
            // MenuReportes
            // 
            this.MenuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenuReporteCompras,
            this.submenuReporteVentas});
            this.MenuReportes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.MenuReportes.IconChar = FontAwesome.Sharp.IconChar.BarChart;
            this.MenuReportes.IconColor = System.Drawing.Color.Black;
            this.MenuReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuReportes.IconSize = 50;
            this.MenuReportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuReportes.Name = "MenuReportes";
            this.MenuReportes.Size = new System.Drawing.Size(115, 82);
            this.MenuReportes.Text = "Reportes";
            this.MenuReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenuReporteCompras
            // 
            this.submenuReporteCompras.Name = "submenuReporteCompras";
            this.submenuReporteCompras.Size = new System.Drawing.Size(286, 38);
            this.submenuReporteCompras.Text = "Reporte Compras";
            this.submenuReporteCompras.Click += new System.EventHandler(this.submenuReporteCompras_Click);
            // 
            // submenuReporteVentas
            // 
            this.submenuReporteVentas.Name = "submenuReporteVentas";
            this.submenuReporteVentas.Size = new System.Drawing.Size(286, 38);
            this.submenuReporteVentas.Text = "Reporte Ventas";
            this.submenuReporteVentas.Click += new System.EventHandler(this.submenuReporteVentas_Click);
            // 
            // MenuAcercaDe
            // 
            this.MenuAcercaDe.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.MenuAcercaDe.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            this.MenuAcercaDe.IconColor = System.Drawing.Color.Black;
            this.MenuAcercaDe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuAcercaDe.IconSize = 50;
            this.MenuAcercaDe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuAcercaDe.Name = "MenuAcercaDe";
            this.MenuAcercaDe.Size = new System.Drawing.Size(125, 82);
            this.MenuAcercaDe.Text = "Acerca de";
            this.MenuAcercaDe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.MenuAcercaDe.Click += new System.EventHandler(this.MenuAcercaDe_Click);
            // 
            // pnlContenedor
            // 
            this.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedor.Location = new System.Drawing.Point(0, 181);
            this.pnlContenedor.Name = "pnlContenedor";
            this.pnlContenedor.Size = new System.Drawing.Size(1919, 788);
            this.pnlContenedor.TabIndex = 3;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.CadetBlue;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.btnSalir.IconColor = System.Drawing.Color.White;
            this.btnSalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSalir.IconSize = 60;
            this.btnSalir.Location = new System.Drawing.Point(1784, 22);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(89, 63);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1919, 969);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.label);
            this.Controls.Add(this.pnlContenedor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.menutitulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bienvenido a la App de Sistema de Ventas";
            this.Load += new System.EventHandler(this.Inicio_Load_1);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menutitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Panel pnlContenedor;
        private FontAwesome.Sharp.IconMenuItem MenuReportes;
        private FontAwesome.Sharp.IconMenuItem MenuUsuarios;
        private FontAwesome.Sharp.IconMenuItem MenuMantenedor;
        private FontAwesome.Sharp.IconMenuItem submenuCategoria;
        private FontAwesome.Sharp.IconMenuItem submenuProductos;
        private System.Windows.Forms.ToolStripMenuItem negocio;
        private FontAwesome.Sharp.IconMenuItem MenuProveedores;
        private FontAwesome.Sharp.IconMenuItem MenuAcercaDe;
        private FontAwesome.Sharp.IconMenuItem MenuCompras;
        private FontAwesome.Sharp.IconMenuItem submenuRegistroCompra;
        private FontAwesome.Sharp.IconMenuItem submenuVerDetalleCompra;
        private FontAwesome.Sharp.IconMenuItem MenuVentas;
        private FontAwesome.Sharp.IconMenuItem submenuRegistrarVenta;
        private FontAwesome.Sharp.IconMenuItem submenuDetalleVentas;
        private FontAwesome.Sharp.IconMenuItem MenuClientes;
        private System.Windows.Forms.ToolStripMenuItem submenuReporteCompras;
        private System.Windows.Forms.ToolStripMenuItem submenuReporteVentas;
        private FontAwesome.Sharp.IconButton btnSalir;
    }
}

