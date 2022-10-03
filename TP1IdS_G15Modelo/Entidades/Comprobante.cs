using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class Comprobante
    {
        [Key]
        public int Id { get; set; }
        public int VentaId { get; set; }
        public virtual TipoComprobante TipoComprobante { get; set; }
    }
}
