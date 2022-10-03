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
    public class RubrosManager
    {
        private DataContext db = new DataContext();

        public Rubro SaveRubro(Rubro rubro)
        {
            Rubro Rubro;
            if (rubro.Id == 0)
            {
                Rubro = new Rubro();
                Rubro.Id = 0;
                Rubro.Descripcion = rubro.Descripcion;
                db.Rubros.Add(Rubro);
            }
            else
            {
                Rubro = db.Rubros.Find(rubro.Id);
                Rubro.Descripcion = rubro.Descripcion;
                db.Entry(Rubro).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Rubro;
        }

        public Rubro DeleteRubro(int id)
        {
            Rubro rubro = db.Rubros.Find(id);
            if (rubro == null)
            {
                return null;
            }

            db.Rubros.Remove(rubro);
            db.SaveChanges();
            return rubro;
        }

        public Rubro FindRubro(int id)
        {
            Rubro rubro = db.Rubros.Find(id);
            return rubro;
        }
        public List<Rubro> GetRubros()
        {
            return db.Rubros.ToList();
        }

        public bool RubroExists(int id)
        {
            return db.Rubros.Count(e => e.Id == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
