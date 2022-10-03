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
    public class SuperEmpleadoManager : IDisposable
    {
        private DataContext db = new DataContext();

        public SuperEmpleadoDTO Save(SuperEmpleadoDTO superEmpleadoDTO)
        {
            var usuario = new User()
            {
                Email = superEmpleadoDTO.Email,
                UserName = superEmpleadoDTO.UserName,
                Password = superEmpleadoDTO.Password,
                TipoUsuario = superEmpleadoDTO.TipoUsuario

            };

            var empleado = new Empleado()
            {
                Nombre = superEmpleadoDTO.ApellidoYNombre,
                Legajo = superEmpleadoDTO.Legajo,
                User = usuario,
                Sucursal = db.Sucursales.First()
            };

            db.Empleados.Add(empleado);
            db.Users.Add(usuario);
            db.SaveChanges();

            return superEmpleadoDTO;
        }



        public SuperEmpleadoDTO DeleteEmpleado(int legajo)
        {
            Empleado empleado = db.Empleados.Find(legajo);
            if (empleado == null)
            {
                return null;
            }

            var superEmpleado = new SuperEmpleadoDTO()
            {
                ApellidoYNombre = empleado.Nombre,
                TipoUsuario = empleado.User.TipoUsuario,
                Legajo = empleado.Legajo,
                UserName = empleado.User.UserName,
                Password = empleado.User.Password
            };

            db.Users.Remove(empleado.User);
            db.Empleados.Remove(empleado);
            db.SaveChanges();

            return superEmpleado;
        }

        public SuperEmpleadoDTO Update(SuperEmpleadoDTO superEmpleadoDTO)
        {
            Empleado empleado = db.Empleados.Find(superEmpleadoDTO.Legajo);
            if (empleado == null)
            {
                throw new KeyNotFoundException();
            }
            empleado.Nombre = superEmpleadoDTO.ApellidoYNombre;
            empleado.User.Email = superEmpleadoDTO.Email;
            empleado.Legajo = superEmpleadoDTO.Legajo;
            empleado.User.Password= superEmpleadoDTO.Password;           
            empleado.User.TipoUsuario = superEmpleadoDTO.TipoUsuario;

            var user = new User()
            {
                Password = superEmpleadoDTO.Password,
                Email = superEmpleadoDTO.Email,
                TipoUsuario = superEmpleadoDTO.TipoUsuario,
                UserName = superEmpleadoDTO.UserName,
            };

            db.Entry(empleado).State = EntityState.Modified;
            db.SaveChanges();

            return superEmpleadoDTO;
        }


        public SuperEmpleadoDTO FindEmpleado(int legajo)
        {
            var empleado = db.Empleados.Find(legajo);
            var e = new SuperEmpleadoDTO();
            e.ApellidoYNombre = empleado.Nombre;
            e.TipoUsuario = empleado.User.TipoUsuario;
            e.Legajo = empleado.Legajo;
            e.UserName = empleado.User.UserName;
            e.Email = empleado.User.Email;
            e.Password = empleado.User.Password;

            return e;
        }

        public List<object> GetEmpleados()
        {
            var lista = new List<object>();

            var lista2 = db.Empleados.ToList();

            foreach (var empleado in lista2)
            {
                var e = new
                {
                    ApellidoYNombre = empleado.Nombre,
                    Legajo = empleado.Legajo,
                    UserName = empleado.User.UserName,
                    Email = empleado.User.Email,
                    TipoUsuario = empleado.User.TipoUsuario.ToString(),
                };
                lista.Add(e);
            }


            return lista;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
