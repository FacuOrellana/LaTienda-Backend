using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class Empleado
    {
        [Key]
        public int Legajo { get; set; }
        public string Nombre { get; set; }
        public string UserName { get; set; }
        public virtual User User { get; set; }
        public int SucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual List<Venta> ventas { get; set; }
    }
}
