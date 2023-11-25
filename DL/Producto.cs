using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Producto
    {
        public int IdProdcuto { get; set; }
        public string? Nombre { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public int? Stock { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? IdProovedor { get; set; }

        public virtual Proveedor? IdProovedorNavigation { get; set; }
    }
}
