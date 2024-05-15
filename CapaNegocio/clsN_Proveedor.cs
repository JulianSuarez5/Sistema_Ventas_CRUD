using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class clsN_Proveedor
    {
        private clsD_Proveedor objclsD_Proveedor = new clsD_Proveedor();
        public List<clsProveedor> Listar()
        {
            return objclsD_Proveedor.Listar();
        }

        public int Registrar(clsProveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Proveedor\n";
            }

            if (obj.RazonSocial == "")
            {
                Mensaje += "Es necesario la razón social del Proveedor\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es necesario el correo del Proveedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objclsD_Proveedor.Registrar(obj, out Mensaje);
            }
        }

        public bool Actualizar(clsProveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Proveedor\n";
            }

            if (obj.RazonSocial == "")
            {
                Mensaje += "Es necesario la razón social del Proveedor\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es necesario el correo del Proveedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objclsD_Proveedor.Actualizar(obj, out Mensaje);
            }

        }

        public bool Eliminar(clsProveedor obj, out string Mensaje)
        {
            return objclsD_Proveedor.Eliminar(obj, out Mensaje);
        }
    }
}
