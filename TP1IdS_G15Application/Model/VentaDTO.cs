using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15Application.Model
{
    public class VentaDTO
    {
        public int Id { get; set; }
        //public long NroFacturaAfip { get; set; }
        public int PuntoDeVentaId { get; set; }
        public MedioDePago MedioDePago { get; set; }
        public string ClienteCUIT { get; set; }
        public string User { get; set; }
        public int TipoFacturaId { get; set; }
        public List<LineaDeVentaDTO> LineasDeVenta { get; set; }
    }
}
