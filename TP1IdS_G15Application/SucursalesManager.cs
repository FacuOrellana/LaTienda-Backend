using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1IdS_G15AccesoADatos;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15Application
{
    public class SucursalesManager : IDisposable
    {
        private DataContext db = new DataContext();

        public List<Sucursal> GetSucursales()
        {
            return db.Sucursales.ToList();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
