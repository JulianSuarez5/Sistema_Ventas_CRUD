using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class clsN_Venta
    {
        private clsD_Venta objclsD_Venta = new clsD_Venta();

        public bool RestarStock(int idproducto, int cantidad)
        {
            return objclsD_Venta.RestarStock(idproducto, cantidad);
        }

        public bool SumarStock(int idproducto, int cantidad)
        {
            return objclsD_Venta.SumarStock(idproducto, cantidad);
        }

        public int ObtenerConsecutivo()
        {
            return objclsD_Venta.ObtenerConsecutivo();
        }

        public bool Registrar(clsVenta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objclsD_Venta.Registrar(obj, DetalleVenta, out Mensaje);
        }

        public clsVenta ObtenerVenta(string numero)
        {
            clsVenta objventa = objclsD_Venta.ObtenerVenta(numero);

            if (objventa.IdVenta != 0)
            {
                List <clsDetalle_Venta> objDetalleVenta = objclsD_Venta.ObtenerDetalleVenta(objventa.IdVenta);
                objventa.objDetalle_Venta = objDetalleVenta;
            }
            return objventa;
        }
    }
}
