using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1IdS_G15AccesoADatos;
using TP1IdS_G15Application.Models;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15Application
{
    public class ProductoManager : IDisposable
    {
        private DataContext db = new DataContext();

        public Producto CreateNew(ProductoDTO productoDTO)
        {
            var marca = db.Marcas.Where(marc => marc.Id == productoDTO.MarcaId).ToList().First();
            var rubro = db.Rubros.Where(rubr => rubr.Id == productoDTO.RubroId).ToList().First();
            Producto producto;
            producto = new Producto(productoDTO.CodigoDeBarra, productoDTO.Descripcion, productoDTO.Costo, productoDTO.MargenDeGanancia, productoDTO.PorcentajeIVA, marca, rubro);
            db.Productos.Add(producto);
            db.SaveChanges();
            return producto;
        }
        public Producto Update(ProductoDTO productoDTO)
        {
            Producto producto;
            producto = FindProducto(productoDTO.CodigoDeBarra);
            var marca = db.Marcas.Where(marc => marc.Id == productoDTO.MarcaId).ToList().First();
            var rubro = db.Rubros.Where(rubr => rubr.Id == productoDTO.RubroId).ToList().First();
            producto.Costo = productoDTO.Costo;
            producto.Descripcion = productoDTO.Descripcion;
            producto.MargenDeGanancia = productoDTO.MargenDeGanancia;
            producto.MarcaId = productoDTO.MarcaId;
            producto.Marca = marca;
            producto.RubroId = productoDTO.RubroId;
            producto.Rubro = rubro;
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
            return producto;
        }
        public Producto DeleteProducto(string CodigoDeBarra)
        {
            Producto producto = FindProducto(CodigoDeBarra);
            if (producto == null)
            {
                return null;
            }

            db.Productos.Remove(producto);
            db.SaveChanges();
            return producto;
        }

        public Producto FindProducto(string CodigoDeBarra)
        {
            Producto producto = db.Productos.Where(p => p.CodigoDeBarra == CodigoDeBarra).FirstOrDefault();
            return producto;
        }
        public List<Producto> GetProductos()
        {
            return db.Productos.ToList();
        }

        private bool ProductoExists(int id)
        {
            return db.Productos.Count(e => e.Id == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
