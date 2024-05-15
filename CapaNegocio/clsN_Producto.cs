using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class clsN_Producto
    {
        private clsD_Producto objclsD_Producto = new clsD_Producto();
        public List<clsProducto> Listar()
        {
            return objclsD_Producto.Listar();
        }

        public int Registrar(clsProducto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario el código del Producto\n";
            }

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del Producto\n";
            }

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripción del Producto\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objclsD_Producto.Registrar(obj, out Mensaje);
            }
        }

        public bool Actualizar(clsProducto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario el código del Producto\n";
            }

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del Producto\n";
            }

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripción del Producto\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }

            else
            {
                return objclsD_Producto.Actualizar(obj, out Mensaje);
            }

        }

        public bool Eliminar(clsProducto obj, out string Mensaje)
        {
            return objclsD_Producto.Eliminar(obj, out Mensaje);
        }
    }
}
