using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1IdS_G15AccesoADatos.AFIPWebService;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15AccesoADatos
{
    public class AFIPController
    {
        private static WrapperAutorizacion.Autorizacion _Ticket;
        public static void GetTicket()
        {
            if(_Ticket == null)
            {
                var webReference = new WrapperAutorizacion.LoginServiceClient();
                _Ticket = webReference.SolicitarAutorizacion("40CA85E4-283F-4C8B-BD07-2E60FE5354ED");
            }
        }

        public static void FE(Venta venta)
        {
            GetTicket();
            var webReference = new ServiceSoapClient();
            var Auth = new FEAuthRequest();
            Auth.Cuit = _Ticket.Cuit;
            //Auth.Cuit = 20000000001;
            Auth.Token = _Ticket.Token;
            //Auth.Token = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgc3JjPSJDTj13c2FhaG9tbywgTz1BRklQLCBDPUFSLCBTRVJJQUxOVU1CRVI9Q1VJVCAzMzY5MzQ1MDIzOSIgZHN0PSJDTj13c2ZlLCBPPUFGSVAsIEM9QVIiIHVuaXF1ZV9pZD0iMTMzNDczNDc4NCIgZ2VuX3RpbWU9IjE2NDc1MjM2MDEiIGV4cF90aW1lPSIxNjQ3NTY2ODYxIi8+CiAgICA8b3BlcmF0aW9uIHR5cGU9ImxvZ2luIiB2YWx1ZT0iZ3JhbnRlZCI+CiAgICAgICAgPGxvZ2luIGVudGl0eT0iMzM2OTM0NTAyMzkiIHNlcnZpY2U9IndzZmUiIHVpZD0iU0VSSUFMTlVNQkVSPUNVSVQgMjAyNTk5MjcxNjIsIENOPXNnZWZqdjA0IiBhdXRobWV0aG9kPSJjbXMiIHJlZ21ldGhvZD0iMjIiPgogICAgICAgICAgICA8cmVsYXRpb25zPgogICAgICAgICAgICAgICAgPHJlbGF0aW9uIGtleT0iMjAwMDAwMDAwMDEiIHJlbHR5cGU9IjQiLz4KICAgICAgICAgICAgICAgIDxyZWxhdGlvbiBrZXk9IjIwMTc2MTM3NzU1IiByZWx0eXBlPSI0Ii8+CiAgICAgICAgICAgICAgICA8cmVsYXRpb24ga2V5PSIyMDI1OTkyNzE2MiIgcmVsdHlwZT0iNCIvPgogICAgICAgICAgICAgICAgPHJlbGF0aW9uIGtleT0iMjAzMjIwNjczNTciIHJlbHR5cGU9IjQiLz4KICAgICAgICAgICAgICAgIDxyZWxhdGlvbiBrZXk9IjIwMzYyMjQxODA1IiByZWx0eXBlPSI0Ii8+CiAgICAgICAgICAgICAgICA8cmVsYXRpb24ga2V5PSIyMDM3MTkxODgxOCIgcmVsdHlwZT0iNCIvPgogICAgICAgICAgICAgICAgPHJlbGF0aW9uIGtleT0iMzA3MTIwMzQ2MDkiIHJlbHR5cGU9IjQiLz4KICAgICAgICAgICAgICAgIDxyZWxhdGlvbiBrZXk9IjMzNzA4ODc5MjI5IiByZWx0eXBlPSI0Ii8+CiAgICAgICAgICAgIDwvcmVsYXRpb25zPgogICAgICAgIDwvbG9naW4+CiAgICA8L29wZXJhdGlvbj4KPC9zc28+Cg==";
            Auth.Sign = _Ticket.Sign;
            //Auth.Sign = "Nl2L9q+R5SN3wlXOurVmtITLXmJuwJRJiFev7MT1VKLTGKWxY5MxlfpSCy6bMYX3D+NWTcaP36bBAUj4DDMJbBK1UFj8lKuww0Kb+Mf1+Hvxihk0fRI24wNTB5NCCvOV688MeYNRCwDg9+keCvtIrjnXnIhqvLx1NJg6WK7ZlP0=";

            var FECAERequest = new FECAERequest();

            var Cabecera = new FECAECabRequest();
            Cabecera.CantReg = venta.LineasDeVentas.Count;
            //Cabecera.PtoVta = (int)venta.PuntoDeVenta.NumeroPDV;
            Cabecera.PtoVta = 15;
            Cabecera.CbteTipo = venta.TipoFactura.Id;
            FECAERequest.FeCabReq = Cabecera;

            FECAEDetRequest[] FeDetReq;
            var FeDetReqList = new List<FECAEDetRequest>();

            //if (venta.NroFacturaAfip == 0)
            //{
                venta.NroFacturaAfip = GetUltimoNroCbteAFIP(Auth, venta.PuntoDeVenta.NumeroPDV, venta.TipoFacturaId) + 1;
            //}

            foreach (LineaDeVenta ldv in venta.LineasDeVentas)
            {
                var Det = new FECAEDetRequest();
                Det.Concepto = 1;
                Det.DocTipo = 80;
                Det.DocNro = Convert.ToInt64(venta.Cliente.Cuit);
                Det.CbteFch = venta.Fecha.ToString("yyyyMMdd");
                Det.MonId = "PES";
                Det.MonCotiz = 1;

                Det.CbteDesde = venta.NroFacturaAfip;
                Det.CbteHasta = venta.NroFacturaAfip;

                Det.ImpTotal = (double) ldv.SubTotal;
                Det.ImpTotConc = 0;
                Det.ImpNeto = (double)ldv.Producto.NetoGravado;
                Det.ImpOpEx = 0;
                Det.ImpIVA = (double)(ldv.Producto.IVA * ldv.Cantidad);
                Det.ImpTrib = 0;

                FeDetReqList.Add(Det);
            }
            FeDetReq = FeDetReqList.ToArray();
            FECAERequest.FeDetReq = FeDetReq;

            var Response = webReference.FECAESolicitar(Auth, FECAERequest);
            if (Response.Errors != null)
            {
                throw new Exception();
            }

            //MessageBox.Show(Response.FeDetResp[0].Resultado);
        }

        public static long GetUltimoNroCbteAFIP(FEAuthRequest Auth, int ptoVeta, int cbteTipo)
        {
            var webReference = new ServiceSoapClient();
            FERecuperaLastCbteResponse Response = webReference.FECompUltimoAutorizado(Auth, ptoVeta, cbteTipo);
            return Response.CbteNro;
        }
    }
}
