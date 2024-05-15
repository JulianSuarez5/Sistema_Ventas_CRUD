using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class clsN_Rol
    {
        private clsD_Rol objclsD_Rol = new clsD_Rol();
        public List<clsRol> Listar()
        {
            return objclsD_Rol.Listar();
        }
    }
}
