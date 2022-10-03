using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1IdS_G15AccesoADatos;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15Application
{
      public class TalleManager
    {
        private DataContext db = new DataContext();

        public Talle SaveTalle(Talle talle)
        {
            Talle Talle;
            if (talle.Id == 0)
            {
                Talle = new Talle();
                Talle.Id = 0;
                Talle.Descripcion = talle.Descripcion;
                db.Talles.Add(Talle);
            }
            else
            {
                Talle = db.Talles.Find(talle.Id);
                Talle.Descripcion = talle.Descripcion;
                db.Entry(Talle).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Talle;
        }

        public Talle DeleteTalle(int id)
        {
            Talle talle = db.Talles.Find(id);
            if (talle == null)
            {
                return null;
            }

            db.Talles.Remove(talle);
            db.SaveChanges();
            return talle;
        }

        public Talle FindTalle(int id)
        {
            Talle talle = db.Talles.Find(id);
            return talle;
        }
        public List<Talle> GetTalles()
        {
            return db.Talles.ToList();
        }

        public bool TalleExists(int id)
        {
            return db.Talles.Count(e => e.Id == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}
