using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class Sucursal
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public virtual List<PuntoDeVenta> PuntoDeVentas { get; set; }
        public virtual List<Empleado> Empleados { get; set; }
        public virtual List<ProductoEnStock> Stock { get; set; }
        
    }
}
