using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1IdS_G15AccesoADatos;
using TP1IdS_G15Application.Models;
using TP1IdS_G15Application.TokenHandlers;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15Application
{
    public class SessionsManager : IDisposable
    {
        private DataContext db = new DataContext();
        public User Find(string userName)
        {
            return db.Users.Find(userName);
        }
        private string TryRetrievePdVId(string ipv4)
        {
            try
            {
                var pvid = ConfigurationManager.AppSettings.GetValues("origins:" + ipv4).FirstOrDefault();
                return pvid;
            }
            catch(Exception)
            {
                throw new SettingsPropertyNotFoundException("No se reconoce el Punto de Venta. Es posible que su IP haya cambiado");
            }
        }
        public LoginResponse Authenticate (LoginRequest login)
        {
            User user = Find(login.Username);
            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            //TODO: Validate credentials Correctly, this code is only for demo !!
            bool isCredentialValid = (login.Password == user.Password);
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                string pdvid = TryRetrievePdVId(login.IPv4);
                int puntoDeVentaId = Convert.ToInt32(pdvid);
                PuntoDeVenta puntoDeVenta = db.PuntosDeVenta.Find(puntoDeVentaId);
                var sesion = new Sesion()
                {
                    IsActive = true,
                    PuntoDeVentaId = puntoDeVentaId,
                    PuntoDeVenta = puntoDeVenta,
                    UserName = user.UserName,
                    User = user,
                    DateTime = DateTime.Now,
                };
                db.Sesiones.Add(sesion);
                db.SaveChanges();
                return new LoginResponse()
                {
                    Token = token,
                    TipoUsuario = user.TipoUsuario.ToString(),
                    SessionId = sesion.Id
                };
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        public bool LogOut(int SessionId)
        {
            var sesion = db.Sesiones.Find(SessionId);
            sesion.IsActive = false;
            db.Entry(sesion).State = EntityState.Modified;
            db.SaveChanges();
            if (sesion == null)
                return false;
            return true;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
