using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class LineaDeVenta
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "money")]
        public decimal MontoUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal
        {
            get
            {
                return Cantidad * MontoUnitario;
            }
        }
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }

    }
}
