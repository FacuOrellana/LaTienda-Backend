using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class Devolucion
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int LineaDeVentaId { get; set; }
        public virtual LineaDeVenta LineaDeVenta { get; set; }
    }
}
