using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class TipoFactura
    {
        [Key]
        public int Id { get; set; }
        public int NroTipoFacturaAFIP { get; set; }
        public string Descripcion { get; set; }
    }
}
