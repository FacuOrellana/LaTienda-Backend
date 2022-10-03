using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    [Table("Productos")]
    public class Producto
    {

        #region members
        [Column(TypeName = "money")]
        private decimal _porcentajeIVA;
        [Column(TypeName = "money")]
        private decimal _margenDeGanancia;
        #endregion

        #region Constructors
        public Producto()
        {
        }

        public Producto(string _CodigoDeBarra, string _Descripcion, decimal _Costo, decimal _MargenDeGanancia, decimal _PorcentajeIVA, Marca _Marca, Rubro _Rubro)
        {
            CodigoDeBarra = _CodigoDeBarra;
            Descripcion = _Descripcion;
            Costo = _Costo;
            MargenDeGanancia = _MargenDeGanancia;
            PorcentajeIVA = _PorcentajeIVA;
            Rubro = _Rubro;
            Marca = _Marca;
        }
        #endregion

        #region properties
        [Key]
        public int Id { get; set; }
        public string CodigoDeBarra { get; set; }
        public string Descripcion { get; set; }
        [Column(TypeName = "money")]
        public decimal Costo { get; set; }
        public decimal MargenDeGanancia
        {
            get
            {
                return _margenDeGanancia * 100;
            }
            set
            {
                _margenDeGanancia = (value / 100);
            }
        }
        public decimal NetoGravado
        {
            get
            {
                return Costo + (Costo * _margenDeGanancia);
            }
        }
        public decimal PorcentajeIVA
        {
            get
            {
                return _porcentajeIVA * 100;
            }
            set
            {
                if (value < 0) throw new Exception("El impuesto IVA no puede ser inferior a 0 por ciento");
                _porcentajeIVA = (value / 100);
            }
        }
        public decimal IVA
        {
            get
            {
                return NetoGravado * _porcentajeIVA;
            }
        }
        public decimal PrecioVenta
        {
            get
            {
                return NetoGravado + IVA;
            }
        }
        public int MarcaId { get; set; }
        public virtual Marca Marca { get; set; }
        public int RubroId { get; set; }
        public virtual Rubro Rubro { get; set; }
        #endregion
    }
}
