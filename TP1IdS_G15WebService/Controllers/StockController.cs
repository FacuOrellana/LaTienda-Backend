using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TP1IdS_G15Application;
using TP1IdS_G15Application.Model;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("stock")]
    public class StockController : ApiController
    {
        StockManager AppLayer = new StockManager();
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetStocks()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.GetStocks().Select(p => new
            {
                p.Id,
                Producto = p.Producto.Descripcion,
                Talle = p.Talle.Descripcion,
                Color = p.Color.Descripcion,
                SucursalId = p.SucursalId,
                p.Cantidad
            }));
        }
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetStock(int sucursalId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AppLayer.GetStock(sucursalId).Select(p => new
            {
                p.Id,
                Producto = p.Producto.Descripcion,
                Talle = p.Talle.Descripcion,
                Color = p.Color.Descripcion,
                SucursalId = p.SucursalId,
                p.Cantidad
            }));
        }
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateNewStock([FromBody] ProductoEnStockDTO pesdto)
        {
            try
            {
                ProductoEnStock p = AppLayer.CreateNewProductoEnStock(pesdto);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    p.Id
                });
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpPatch]
        [Route("")]
        public HttpResponseMessage ModifyStock(int stockId, int quantity)
        {
            ProductoEnStock p = AppLayer.ModifyQuantity(stockId, quantity);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                p.Id,
                Producto = p.Producto.Descripcion,
                Talle = p.Talle.Descripcion,
                Color = p.Color.Descripcion,
                SucursalId = p.SucursalId,
                Sucursal = p.Sucursal.Nombre,
                p.Cantidad
            });
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
