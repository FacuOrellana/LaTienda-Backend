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
    public class ClientesManager : IDisposable
    {
        private DataContext db = new DataContext();
        public List<Cliente> GetAll()
        {
            return db.Clientes.ToList();
        }
        public Cliente Get(string Cuit)
        {
            return db.Clientes.Find(Cuit);
        }
        public Cliente CreateNew(Cliente cliente)
        {
            db.Clientes.Add(cliente);
            db.SaveChanges();
            return cliente;
        }
        public Cliente Update(Cliente cliente)
        {
            Cliente Cliente = db.Clientes.Find(cliente.Cuit);
            if(Cliente == null)
            {
                throw new KeyNotFoundException();
            }
            Cliente.Condicion = cliente.Condicion;
            Cliente.Domicilio = cliente.Domicilio;
            Cliente.NombreApellido = cliente.NombreApellido;
            Cliente.Telefono = cliente.Telefono;
            db.Entry(Cliente).State = EntityState.Modified;
            db.SaveChanges();
            return Cliente;
        }
        public void Delete(string CUIT)
        {
            Cliente Cliente = db.Clientes.Find(CUIT);
            if(Cliente == null)
            {
                throw new KeyNotFoundException();
            }
            db.Clientes.Remove(Cliente);
            db.Entry(Cliente).State = EntityState.Deleted;
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
