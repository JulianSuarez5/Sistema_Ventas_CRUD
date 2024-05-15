using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class clsN_Permiso
    {
        private clsD_Permiso objclsD_Permiso = new clsD_Permiso();
        public List<clsPermiso> Listar(int IdUsuario)
        {
            return objclsD_Permiso.Listar(IdUsuario);
        }
    }
}
