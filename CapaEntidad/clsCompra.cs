using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class clsCompra
    {
        public int Id { get; set; }
        public clsUsuario objUsuario { get; set; }
        public clsProveedor objProveedor { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal MontoTotal { get; set; }
        public List<clsDetalle_Compra> objDetalleCompra { get; set; }
        public string FechaRegistro { get; set; }
    }
}
