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
using System.Web.WebPages.Html;
using TP1IdS_G15Application;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("talles")]
    public class TallesController: ApiController
    {
        private TalleManager AppLayer = new TalleManager();

        // GET: api/Talles
        public HttpResponseMessage GetTalles()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.GetTalles());
        }

        // GET: api/Talles/5
        [ResponseType(typeof(Talle))]
        public IHttpActionResult GetTalle(int id)
        {
            Talle talle = AppLayer.FindTalle(id);
            if (talle == null)
            {
                return NotFound();
            }

            return Ok(talle);
        }

        // PUT: api/Talles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTalle(int id, Talle talle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != talle.Id)
            {
                return BadRequest();
            }


            try
            {
                AppLayer.SaveTalle(talle);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppLayer.TalleExists(id))
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

        // POST: api/Talles
        [ResponseType(typeof(Talle))]
        public IHttpActionResult PostTalle(Talle talle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppLayer.SaveTalle(talle);

            return CreatedAtRoute("DefaultApi", new { id = talle.Id }, talle);
        }

        // DELETE: api/Marcas/5
        [ResponseType(typeof(Talle))]
        public IHttpActionResult DeleteTalle(int id)
        {
            Talle talle = AppLayer.FindTalle(id);
            if (talle == null)
            {
                return NotFound();
            }

            AppLayer.DeleteTalle(id);

            return Ok(talle);
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