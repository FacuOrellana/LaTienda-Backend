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
    public class ColorManager : IDisposable
    {
        private DataContext db = new DataContext();

        public Color SaveColor(Color color)
        {
            Color Color;
            if (color.Id == 0)
            {
                Color = new Color();
                Color.Id = 0;
                Color.Descripcion = color.Descripcion;
                db.Colores.Add(Color);
            }
            else
            {
                Color = db.Colores.Find(color.Id);
                Color.Descripcion = color.Descripcion;
                db.Entry(Color).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Color;
        }

        public Color DeleteColor(int id)
        {
            Color color = db.Colores.Find(id);
            if (color == null)
            {
                return null;
            }

            db.Colores.Remove(color);
            db.SaveChanges();
            return color;
        }

        public Color FindColor(int id)
        {
            Color color = db.Colores.Find(id);
            return color;
        }
        public List<Color> GetColores()
        {
            return db.Colores.ToList();
        }

        public bool ColorExists(int id)
        {
            return db.Colores.Count(e => e.Id == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
