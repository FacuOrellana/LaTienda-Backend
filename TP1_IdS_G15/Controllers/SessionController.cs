using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TP1IdS_G15AccesoADatos;
using TP1IdS_G15Modelo.Entidades;
using TP1IdS_G15Application.Models;
using TP1IdS_G15WebService.TokenHandlers;

namespace TP1IdS_G15WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("login")]
    public class SessionController : ApiController
    {
        private DataContext db = new DataContext();

        // POST: api/Login
        [AllowAnonymous]
        [ResponseType(typeof(User))]
        [Route("")]
        public IHttpActionResult Authenticate(LoginRequest login)
        { 
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            User user = db.Users.Find(login.Username);
            if (user == null)
            {
                return NotFound();
            }

            //TODO: Validate credentials Correctly, this code is only for demo !!
            bool isCredentialValid = (login.Password == user.Password);
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                return Ok(
                    new
                    {
                        Token = token,
                        TipoUsuario = user.TipoUsuario.ToString()
                    }
                );
            }
            else
            {
                return Unauthorized();
            }
        }


        [ResponseType(typeof(User))]
        [Route("")]
        public HttpResponseMessage GetUser(string token)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string userName = null;
            try
            {
                var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));

                SecurityToken securityToken;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                // Extract and assign Current Principal and user
                ClaimsPrincipal CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                userName = CurrentPrincipal.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
            }
            catch (SecurityTokenValidationException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse(statusCode, userName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.UserName == id) > 0;
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}
