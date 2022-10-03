using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1IdS_G15AccesoADatos;
using TP1IdS_G15Application.Model;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15Application
{
    public class StockManager
    {
        private DataContext db = new DataContext();

        public List<ProductoEnStock> GetStocks()
        {
            return db.ProductosEnStock.ToList();
        }
        public List<ProductoEnStock> GetStock(int sucursalId)
        {
            return db.ProductosEnStock.Where(p => p.SucursalId == sucursalId).ToList();
        }
        public ProductoEnStock GetProductoEnStock(int id)
        {
            return db.ProductosEnStock.Find(id);
        }
        public ProductoEnStock GetStock(int sucursalId, int ProductoId, int ColorId, int TalleId)
        {
            var productos = db.ProductosEnStock.Where(p => p.SucursalId == sucursalId && p.ProductoId == ProductoId && p.ColorId == ColorId && p.TalleId == TalleId);
            return productos.Count()>0 ? productos.First() : null;
        }
        public ProductoEnStock CreateNewProductoEnStock(ProductoEnStockDTO pesdto)
        {
            var stockRepetido = GetStock(pesdto.SucursalId, pesdto.ProductoId, pesdto.ColorId, pesdto.TalleId);
            if (stockRepetido != null)
            {
                throw new Exception("Esta combinación de sucursal, producto, color, y talle ya se encuentra registrada. StockId: " + stockRepetido.Id);
            }
            ProductoEnStock p = new ProductoEnStock()
            {
                Cantidad = pesdto.Cantidad,
                ProductoId = pesdto.ProductoId,
                ColorId = pesdto.ColorId,
                SucursalId = pesdto.SucursalId,
                TalleId = pesdto.TalleId,
            };
            db.ProductosEnStock.Add(p);
            db.SaveChanges();
            p = db.ProductosEnStock.Find(p.Id);
            return p;
        }
        public ProductoEnStock ModifyQuantity(int productoEnStockId, int quantity)
        {
            ProductoEnStock productoEnStock = GetProductoEnStock(productoEnStockId);
            productoEnStock.Cantidad = quantity;
            db.Entry(productoEnStock).State = EntityState.Modified;
            db.SaveChanges();
            return productoEnStock;
        }
        public ProductoEnStock AddQuantity(int productoEnStockId, int quantity)
        {
            ProductoEnStock productoEnStock = GetProductoEnStock(productoEnStockId);
            productoEnStock.AddQuantity(quantity);
            db.Entry(productoEnStock).State = EntityState.Modified;
            db.SaveChanges();
            return productoEnStock;
        }
        public ProductoEnStock SubtractQuantity(int productoEnStockId, int quantity)
        {
            ProductoEnStock productoEnStock = GetProductoEnStock(productoEnStockId);
            productoEnStock.SubtractQuantity(quantity);
            db.Entry(productoEnStock).State = EntityState.Modified;
            db.SaveChanges();
            return productoEnStock;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
