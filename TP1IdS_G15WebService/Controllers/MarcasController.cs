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
using TP1IdS_G15Application;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("marcas")]
    public class MarcasController : ApiController
    {
        private MarcaManager AppLayer = new MarcaManager();

        // GET: api/Marcas
        public HttpResponseMessage GetMarcas()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.GetMarcas());
        }

        // GET: api/Marcas/5
        [ResponseType(typeof(Marca))]
        public IHttpActionResult GetMarca(int id)
        {
            Marca marca = AppLayer.FindMarca(id);
            if (marca == null)
            {
                return NotFound();
            }

            return Ok(marca);
        }

        // PUT: api/Marcas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMarca(int id, [FromBody] Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marca.Id)
            {
                return BadRequest();
            }

            try
            {
                Marca Marca = AppLayer.SaveMarca(marca);
                return Ok(Marca);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppLayer.MarcaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/Marcas
        [ResponseType(typeof(Marca))]
        public IHttpActionResult PostMarca(Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppLayer.SaveMarca(marca);

            return CreatedAtRoute("DefaultApi", new { id = marca.Id }, marca);
        }

        // DELETE: api/Marcas/5
        [ResponseType(typeof(Marca))]
        public IHttpActionResult DeleteMarca(int id)
        {
            Marca marca = AppLayer.FindMarca(id);
            if (marca == null)
            {
                return NotFound();
            }

            AppLayer.DeleteMarca(id);

            return Ok(marca);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AppLayer.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}