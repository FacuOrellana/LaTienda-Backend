using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class LineaDeCompra
    {
        [Key]
        public int Id { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public int ProductoEnStockId { get; set; }
        public virtual ProductoEnStock ProductoEnStock { get; set; }
    }
}
