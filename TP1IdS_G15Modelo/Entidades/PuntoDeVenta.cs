using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class PuntoDeVenta
    {
        [Key]
        public int Id { get; set; }
        public int NumeroPDV { get; set; }
        public int SucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }
    }
}
