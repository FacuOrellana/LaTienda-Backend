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
    public class MarcaManager : IDisposable
    {
        private DataContext db = new DataContext();

        public Marca SaveMarca(Marca marca)
        {
            Marca Marca;
            if (marca.Id == 0)
            {
                Marca = new Marca();
                Marca.Id = 0;
                Marca.Descripcion = marca.Descripcion;
                db.Marcas.Add(Marca);
            }
            else
            {
                Marca = db.Marcas.Find(marca.Id);
                Marca.Descripcion = marca.Descripcion;
                db.Entry(Marca).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Marca;
        }

        public Marca DeleteMarca(int id)
        {
            Marca Marca = db.Marcas.Find(id);
            if (Marca == null)
            {
                return null;
            }

            db.Marcas.Remove(Marca);
            db.SaveChanges();
            return Marca;
        }

        public Marca FindMarca(int id)
        {
            Marca Marca = db.Marcas.Find(id);
            return Marca;
        }
        public List<Marca> GetMarcas()
        {
            return db.Marcas.ToList();
        }

        public bool MarcaExists(int id)
        {
            return db.Marcas.Count(e => e.Id == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
