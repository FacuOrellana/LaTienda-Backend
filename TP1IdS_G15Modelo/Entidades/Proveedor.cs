using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }
        public string CUIT { get; set; }
        public string Domicilio { get; set; }
        public string RazonSocial { get; set; }
    }
}
