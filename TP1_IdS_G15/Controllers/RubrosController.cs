using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TP1IdS_G15AccesoADatos;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("")]
    public class RubrosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Rubroes
        public IQueryable<Rubro> GetRubros()
        {
            return db.Rubros;
        }

        // GET: api/Rubroes/5
        [ResponseType(typeof(Rubro))]
        public IHttpActionResult GetRubro(int id)
        {
            Rubro rubro = db.Rubros.Find(id);
            if (rubro == null)
            {
                return NotFound();
            }

            return Ok(rubro);
        }

        // PUT: api/Rubroes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRubro(int id, Rubro rubro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rubro.Id)
            {
                return BadRequest();
            }

            db.Entry(rubro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RubroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rubroes
        [ResponseType(typeof(Rubro))]
        public IHttpActionResult PostRubro(Rubro rubro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rubros.Add(rubro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rubro.Id }, rubro);
        }

        // DELETE: api/Rubroes/5
        [ResponseType(typeof(Rubro))]
        public IHttpActionResult DeleteRubro(int id)
        {
            Rubro rubro = db.Rubros.Find(id);
            if (rubro == null)
            {
                return NotFound();
            }

            db.Rubros.Remove(rubro);
            db.SaveChanges();

            return Ok(rubro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RubroExists(int id)
        {
            return db.Rubros.Count(e => e.Id == id) > 0;
        }
    }
}