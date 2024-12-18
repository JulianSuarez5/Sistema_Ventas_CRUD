﻿using CapaEntidad;
using CapaNegocio;
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
    public partial class Login_ : Form
    {
        public Login_()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            clsUsuario oUSUARIO = new clsN_Usuario().Listar().Where(u => u.Documento == txtDocumento.Text && u.Clave == txtClave.Text).FirstOrDefault();

            if (oUSUARIO != null)
            {
                Inicio form = new Inicio(oUSUARIO);
                form.Show();
                this.Hide();
                form.FormClosing += frm_closing;
            }
            else
            {
                MessageBox.Show("No se encontró el usuario",
                                "Error del sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frm_closing(object sender, FormClosingEventArgs e)
        {

            txtDocumento.Text = "";
            txtClave.Text = "";
            this.Show();
        }

        private void Login__Load(object sender, EventArgs e)
        {
            iniciosesion.Parent = pictureBox1;
            iniciosesion.BackColor = Color.Transparent;

            documento.Parent = pictureBox1; 
            documento.BackColor = Color.Transparent;

            clave.Parent = pictureBox1;
            clave.BackColor = Color.Transparent;

            iconPictureBox1.Parent = pictureBox1;
            iconPictureBox1.BackColor = Color.Transparent;

            sistema.Parent = pictureBox1;
            sistema.BackColor = Color.Transparent;

        }
    }
}
