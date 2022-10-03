using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TP1IdS_G15Application;

namespace TP1IdS_G15WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("sucursales")]
    public class SucursalesController : ApiController
    {
        SucursalesManager AppLayer = new SucursalesManager();

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetSucursales()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.GetSucursales().Select(s => new
            {
                s.Id,
                s.Nombre,
                s.Ubicacion,
            }));
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
