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
    public class ColoresController: ApiController
    {
        private ColorManager AppLayer = new ColorManager();

        // GET: api/Colores
        public HttpResponseMessage GetColores()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.GetColores());
        }

        // GET: api/Colores/5
        [ResponseType(typeof(Color))]
        public IHttpActionResult GetColor(int id)
        {
            Color color = AppLayer.FindColor(id);
            if (color == null)
            {
                return NotFound();
            }

            return Ok(color);
        }

        // PUT: api/Colores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutColor(int id, Color color)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != color.Id)
            {
                return BadRequest();
            }


            try
            {
                AppLayer.SaveColor(color);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppLayer.ColorExists(id))
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

        // POST: api/Colores
        [ResponseType(typeof(Color))]
        public IHttpActionResult PostTalle(Color color)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppLayer.SaveColor(color);

            return CreatedAtRoute("DefaultApi", new { id = color.Id }, color);
        }

        // DELETE: api/Colores/5
        [ResponseType(typeof(Color))]
        public IHttpActionResult DeleteTalle(int id)
        {
            Color color = AppLayer.FindColor(id);
            if (color == null)
            {
                return NotFound();
            }

            AppLayer.DeleteColor(id);

            return Ok(color);
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