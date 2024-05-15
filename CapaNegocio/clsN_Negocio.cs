using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class clsN_Negocio
    {
        private clsD_Negocio objclsD_Negocio = new clsD_Negocio();
        public clsNegocio ObtenerDatos()
        {
            return objclsD_Negocio.ObtenerDatos();
        }

        public bool GuardarDatos(clsNegocio obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre\n";
            }

            if (obj.RUC == "")
            {
                Mensaje += "Es necesario el RUC\n";
            }

            if (obj.Direccion == "")
            {
                Mensaje += "Es necesaria la direccioón\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objclsD_Negocio.GuardarDatos(obj, out Mensaje);
            }
        }

        public byte [] ObtenerDatos(out bool Obtenido)
        {
            return objclsD_Negocio.ObtenerLogo(out Obtenido);
        }

        public bool ActualizarLogo (byte[] imagen, out string mensaje)
        {
            return objclsD_Negocio.ActualizarLogo(imagen, out mensaje);
        }
    }
}
