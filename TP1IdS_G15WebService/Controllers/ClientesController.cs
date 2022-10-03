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
    [RoutePrefix("clientes")]
    public class ClientesController : ApiController
    {
        private ClientesManager AppLayer = new ClientesManager();

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetClientes()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.GetAll());
        }
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetCliente(string CUIT)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.Get(CUIT));
        }
        [HttpPost]
        [Route("")]
        public HttpResponseMessage PostCliente([FromBody] Cliente Cliente)
        {
            HttpResponseMessage Response;
            if (ModelState.IsValid)
            {
                Cliente cliente = AppLayer.CreateNew(Cliente);
                Response = Request.CreateResponse(HttpStatusCode.OK, cliente);
            }
            else
            {
                Response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Response;
        }
        [HttpPut]
        [Route("")]
        public HttpResponseMessage PutCliente(string Cuit, [FromBody] Cliente Cliente)
        {
            HttpResponseMessage Response;
            if (!ModelState.IsValid)
            {
                Response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            else if (Cliente.Cuit != Cuit)
            {
                Response = Request.CreateResponse(HttpStatusCode.BadRequest, "La CUIT dada en el parámetro no coincide con la CUIT del cliente en el Cuerpo del mensaje");
            }
            else
            {
                try
                {
                    Cliente cliente = AppLayer.Update(Cliente);
                    Response = Request.CreateResponse(HttpStatusCode.OK, cliente);
                }
                catch (KeyNotFoundException)
                {
                    Response = Request.CreateResponse(HttpStatusCode.NotFound, "No se ha encontrado un cliente con la CUIT dada");
                }
            }
            return Response;
        }
        [HttpDelete]
        [Route("")]
        public HttpResponseMessage DeleteCliente(string Cuit)
        {
            HttpResponseMessage Response;
            try
            {
                AppLayer.Delete(Cuit);
                Response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (KeyNotFoundException)
            {
                Response = Request.CreateResponse(HttpStatusCode.NotFound, "No se ha encontrado un cliente con la CUIT dada");
            }
            return Response;
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
