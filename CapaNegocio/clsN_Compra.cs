﻿using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class clsN_Compra
    {
        private clsD_Compra objclsD_Compra = new clsD_Compra();
        public int ObtenerConsecutivo()
        {
            return objclsD_Compra.ObtenerConsecutivo();
        }

        public bool Registrar(clsCompra obj, DataTable DetalleCompra, out string Mensaje)
        {
                return objclsD_Compra.RegistrarCompra(obj, DetalleCompra, out Mensaje);
        }
    }
}
