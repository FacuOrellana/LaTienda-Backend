using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15Application.Models
{
    public class ProductoDTO
    {
        public int ProductoId { get; set; }
        public string CodigoDeBarra { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public decimal MargenDeGanancia { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public int MarcaId { get; set; }
        public int RubroId { get; set; }
    }
}