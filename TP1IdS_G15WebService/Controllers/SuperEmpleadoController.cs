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
using TP1IdS_G15Application.Model;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15WebService.Controllers
{
    [RoutePrefix("Usuarios")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SuperEmpleadoController : ApiController
    {
        private SuperEmpleadoManager AppLayer = new SuperEmpleadoManager();

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetEmpleados()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.GetEmpleados());
        }
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetEmpleado(string Legajo)
        {
            var legajo = int.Parse(Legajo);
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.FindEmpleado(legajo));
        }
        [HttpPost]
        [Route("")]
        public HttpResponseMessage PostEmpleado([FromBody] SuperEmpleadoDTO superEmpleado)
        {
            HttpResponseMessage Response;
            if (ModelState.IsValid)
            {
                AppLayer.Save(superEmpleado);
                Response = Request.CreateResponse(HttpStatusCode.OK, superEmpleado);
            }
            else
            {
                Response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Response;
        }
        [HttpPut]
        [Route("")]
        public HttpResponseMessage PutEmpleado(string Legajo, [FromBody] SuperEmpleadoDTO superEmpleadoDTO)
        {
            int legajo = int.Parse(Legajo);
            HttpResponseMessage Response;
            if (!ModelState.IsValid)
            {
                Response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            else if (superEmpleadoDTO.Legajo != legajo)
            {
                Response = Request.CreateResponse(HttpStatusCode.BadRequest, "El legajo ingresado dado en el parámetro no coincide con el legajo del empleado en el cuerpo del mensaje");
            }
            else
            {
                try
                {
                    SuperEmpleadoDTO superEmpleadoDTO1 = AppLayer.Update(superEmpleadoDTO);
                    Response = Request.CreateResponse(HttpStatusCode.OK, superEmpleadoDTO1);
                }
                catch (KeyNotFoundException)
                {
                    Response = Request.CreateResponse(HttpStatusCode.NotFound, "No se ha encontrado un empleado con el legajo dado");
                }
            }
            return Response;
        }
        [HttpDelete]
        [Route("")]
        public HttpResponseMessage DeleteEmpleado(string legajo)
        {
            HttpResponseMessage Response;
            try
            {
                var Legajo = int.Parse(legajo);
                AppLayer.DeleteEmpleado(Legajo);
                Response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (KeyNotFoundException)
            {
                Response = Request.CreateResponse(HttpStatusCode.NotFound, "No se ha encontrado un empleado para el legajo ingresado");
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