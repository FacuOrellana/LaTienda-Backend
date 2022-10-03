using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class Sesion
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public virtual User User { get; set; }
        public int PuntoDeVentaId { get; set; }
        public virtual PuntoDeVenta PuntoDeVenta { get; set; }
    }
}
