using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1IdS_G15AccesoADatos;
using TP1IdS_G15Application.Model;
using TP1IdS_G15Application.TokenHandlers;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15Application
{
    public class VentasManager : IDisposable
    {
        private DataContext db = new DataContext();

        public VentaDTO Save(VentaDTO venta, string token)
        {
            Venta Venta;
            try
            {
                Cliente c = db.Clientes.Find(venta.ClienteCUIT);
                string userName = TokenManager.ValidateToken(token);
                Sesion s = db.Sesiones.Where(se => (se.UserName == userName && se.IsActive)).FirstOrDefault();
                PuntoDeVenta pdv = s.PuntoDeVenta;
                Empleado e = db.Empleados.Where(em => em.UserName == userName).FirstOrDefault();
                //TipoFactura tf = db.TiposDeFactura.Find(venta.TipoFacturaId); //cambiar
                TipoFactura tf = db.TiposDeFactura.Where(d=> d.NroTipoFacturaAFIP == venta.TipoFacturaId).FirstOrDefault();
                List<LineaDeVenta> LineasDeVenta = new List<LineaDeVenta>();
                decimal MontoTotal = 0;
                foreach(LineaDeVentaDTO LdVdto in venta.LineasDeVenta)
                {
                    var p = db.Productos.Where(pr => pr.CodigoDeBarra == LdVdto.CodigoDeBarra).First();
                    LineaDeVenta ldv = new LineaDeVenta()
                    {
                        ProductoId = p.Id,
                        Producto = p,
                        MontoUnitario = p.PrecioVenta,
                        Cantidad = LdVdto.Cantidad,
                    };
                    MontoTotal += ldv.SubTotal;
                    LineasDeVenta.Add(ldv);
                }
                long NroFacturaAfip = 0;
                if (db.Ventas.Count() > 0)
                {
                    NroFacturaAfip = db.Ventas.Max(v => v.NroFacturaAfip) + 1;
                }

                Venta = new Venta()
                {
                    Id = 0,
                    Cuit = venta.ClienteCUIT,
                    Cliente = c,
                    PuntoDeVenta = pdv,
                    PuntoDeVentaId = pdv.Id,
                    Vendedor = e,
                    Legajo = e.Legajo,
                    Fecha = DateTime.Now,
                    MedioDePago = venta.MedioDePago,
                    TipoFactura = tf,
                    TipoFacturaId = venta.TipoFacturaId,
                    Monto = (decimal)MontoTotal,
                    LineasDeVentas = LineasDeVenta,
                    NroFacturaAfip = NroFacturaAfip,
                };

                AFIPController.FE(Venta);

                db.Ventas.Add(Venta);
                db.SaveChanges();
                venta.Id = Venta.Id;
                return venta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Venta FindVenta(int id)
        {
            Venta Venta = db.Ventas.Find(id);
            return Venta;
        }
        public List<Venta> GetVentas()
        {
            return db.Ventas.ToList();
        }

        public bool VentaExists(int id)
        {
            return db.Ventas.Count(e => e.Id == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
