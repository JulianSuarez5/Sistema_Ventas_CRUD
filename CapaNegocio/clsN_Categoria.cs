using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class clsN_Categoria
    {
        private clsD_Categoria objclsD_Categoria = new clsD_Categoria();
        public List<clsCategoria> Listar()
        {
            return objclsD_Categoria.Listar();
        }

        public int Registrar(clsCategoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripción de la Categoria\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objclsD_Categoria.Registrar(obj, out Mensaje);
            }
        }

        public bool Actualizar(clsCategoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripción de la Categoria\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objclsD_Categoria.Actualizar(obj, out Mensaje);
            }

        }

        public bool Eliminar(clsCategoria obj, out string Mensaje)
        {
            return objclsD_Categoria.Eliminar(obj, out Mensaje);
        }
    }
}
