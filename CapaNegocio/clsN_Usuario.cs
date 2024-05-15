using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class clsN_Usuario
    {
        private clsD_Usuario objclsD_Usuario = new clsD_Usuario();
        public List<clsUsuario> Listar()
        {
            return objclsD_Usuario.Listar();
        }

        public int Registrar(clsUsuario obj, out string Mensaje) 
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre completo del usuario\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Es necesaria la clave del usuario\n";
            }

            if(Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objclsD_Usuario.Registrar(obj, out Mensaje);
            }
        }

        public bool Actualizar(clsUsuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre completo del usuario\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Es necesaria la clave del usuario\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objclsD_Usuario.Actualizar(obj, out Mensaje);
            }
            
        }

        public bool Eliminar(clsUsuario obj, out string Mensaje)
        {
            return objclsD_Usuario.Eliminar(obj, out Mensaje);
        }
    }
}
