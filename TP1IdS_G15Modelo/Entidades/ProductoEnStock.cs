using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public class ProductoEnStock
    {
        [Key]
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int SucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int TalleId { get; set; }
        public virtual Talle Talle { get; set; }
        public void AddQuantity(int quantity)
        {
            Cantidad += quantity;
        }
        public void SubtractQuantity(int quantity)
        {
            if(quantity < Cantidad)
            {
                throw new ArgumentOutOfRangeException("No hay suficiente stock para restar esta cantidad");
            }
            else
            {
                Cantidad -= quantity;
            }
        }
    }
}
