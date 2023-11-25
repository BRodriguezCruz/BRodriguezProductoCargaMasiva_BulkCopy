using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdProvedor { get; set; }
        public string? Proveedor1 { get; set; }
        public int? Numero { get; set; }
        public string? Direccion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
