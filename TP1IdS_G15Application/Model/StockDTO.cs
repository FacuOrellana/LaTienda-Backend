using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Application.Model
{
    public class ProductoEnStockDTO
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int SucursalId { get; set; }
        public int ProductoId { get; set; }
        public int TalleId { get; set; }
        public int ColorId { get; set; }
    }
}
