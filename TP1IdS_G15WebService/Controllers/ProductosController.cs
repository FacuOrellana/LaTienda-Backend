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
using TP1IdS_G15WebService.CustomHTTPAttributes;
using TP1IdS_G15Application.Models;
using TP1IdS_G15Application;

namespace TP1IdS_G15WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("productos")]
    public class ProductosController : ApiController
    {
        ProductoManager AppLayer = new ProductoManager();

        // GET: api/Productos
        [Route("")]
        public HttpResponseMessage GetProducto()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.GetProductos().Select(p => new
            {
                CodigoDeBarra = p.CodigoDeBarra,
                Descripcion = p.Descripcion,
                PrecioVenta = p.PrecioVenta,
                Marca = p.Marca.Descripcion
            }));
        }

        // GET: api/Productos/5
        [Route("")]
        [ResponseType(typeof(Producto))]
        public IHttpActionResult GetProducto(string CodigoDeBarra)
        {
            var producto = AppLayer.FindProducto(CodigoDeBarra);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Productos/?id=5
        //[HttpPut]
        //[Route("Modify")]
        //public HttpResponseMessage ModifyProducto(int id, [FromBody] ProductoDTO productoDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    if (id != productoDTO.CodigoDeBarra)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    Producto producto = db.Productos.Where(prod => prod.CodigoDeBarra == productoDTO.CodigoDeBarra).ToList().First();

        //    producto.Marca = db.Marcas.Where(marc => marc.Id == productoDTO.MarcaId).ToList().First();
        //    producto.Rubro = db.Rubros.Where(rubr => rubr.Id == productoDTO.RubroId).ToList().First();

        //    db.Entry(producto).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductoExists(id))
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, producto);
        //}

        //[HttpPost]
        //[Route("initialize")]
        //[CustomAuthorization("ROLES_VENDEDOR")]
        //public HttpResponseMessage CreateProducto(ProductoDTO productoDTO)
        //{
        //    Marca marca = null;
        //    Rubro rubro = null;
        //    try
        //    {
        //        marca = db.Marcas.Where(marc => marc.Id == productoDTO.MarcaId).ToList().First();
        //        rubro = db.Rubros.Where(rubr => rubr.Id == productoDTO.RubroId).ToList().First();
        //    }
        //    catch (Exception)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }
        //    var producto = new Producto(productoDTO.CodigoDeBarra, productoDTO.Descripcion, productoDTO.Costo, productoDTO.MargenDeGanancia, productoDTO.PorcentajeIVA, marca, rubro);
        //    return Request.CreateResponse(HttpStatusCode.OK, producto);
        //}

        // POST: api/Productos
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateNewProducto(ProductoDTO productoDTO)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, productoDTO);
            }
            var producto = AppLayer.CreateNew(productoDTO);

            return Request.CreateResponse(HttpStatusCode.OK, producto);
        }

        // PUT: api/Productos/
        [HttpPut]
        [Route("")]
        public HttpResponseMessage UpdateProducto(ProductoDTO productoDTO)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, productoDTO);
            }
            var producto = AppLayer.Update(productoDTO);

            return Request.CreateResponse(HttpStatusCode.OK, producto);
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(Producto))]
        [HttpDelete]
        [Route("")]
        public IHttpActionResult DeleteProducto(string CodigoDeBarra)
        {
            var producto = AppLayer.DeleteProducto(CodigoDeBarra);
            if(producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
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