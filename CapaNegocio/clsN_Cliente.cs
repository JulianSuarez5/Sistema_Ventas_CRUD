using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class clsN_Cliente
    {
        private clsD_Cliente objclsD_Cliente = new clsD_Cliente();
        public List<clsCliente> Listar()
        {
            return objclsD_Cliente.Listar();
        }

        public int Registrar(clsCliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Cliente\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre completo del Cliente\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es necesaria el correo del Cliente\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objclsD_Cliente.Registrar(obj, out Mensaje);
            }
        }

        public bool Actualizar(clsCliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Cliente\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre completo del Cliente\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es necesaria el correo del Cliente\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objclsD_Cliente.Actualizar(obj, out Mensaje);
            }

        }

        public bool Eliminar(clsCliente obj, out string Mensaje)
        {
            return objclsD_Cliente.Eliminar(obj, out Mensaje);
        }
    }
}
