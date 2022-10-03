using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using TP1IdS_G15AccesoADatos;
using TP1IdS_G15Modelo.Entidades;
using TP1IdS_G15WebService.TokenHandlers;

namespace TP1IdS_G15WebService.CustomHTTPAttributes
{
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        private DataContext db = new DataContext();
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        public CustomAuthorizationAttribute(string configKey)
        {
            Roles = ConfigurationManager.AppSettings[configKey];
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool isAuthorized = false;
            string token;
            if (TryRetrieveToken(actionContext.Request, out token))
            {
                string userName = TokenManager.ValidateToken(token);
                User user = db.Users.Find(userName);

                string[] allowedRoles = Roles.Split(',');
                foreach (string allowedRole in allowedRoles)
                {
                    if (user.TipoUsuario.ToString() == allowedRole)
                    {
                        return true;
                    }
                }

                //using (var auth = new STUAuthorizationService(application, "PROD", new TokenObject { Token = token }))
                //{
                //    if (auth.IsAuthenticated)
                //    {
                //        using (PrincipalSearchResult<Principal> groups = auth._userPrincipal.GetAuthorizationGroups())
                //        {
                //            string[] groupsAllowed = Roles.Split(',');
                //            //if (auth.HasAccessToApplication()) //ANDA LENTÍSIMO. REESCRIBIR
                //            //{
                //            HttpContext.Current.User = auth.GetClaimsPrincipal(token);
                //            foreach (string groupAllowed in groupsAllowed)
                //            {
                //                if (groups.OfType<GroupPrincipal>().Any(g => g.Name.Equals(groupAllowed, StringComparison.OrdinalIgnoreCase)))
                //                {
                //                    return true;
                //                }
                //            }
                //            //}
                //        }
                //    }
                //}
            }
            return isAuthorized;
        }

        //public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken) //with this approach I have better control in each error message that I return.
        //{
        //    bool isAuthorized = false;
        //    string token;
        //    var application = ConfigurationManager.AppSettings["APPLICATION"];
        //    if (!TryRetrieveToken(actionContext.Request, out token))
        //    {
        //        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Authorization Token Not Found");
        //    }
        //    else
        //    {
        //        using (var auth = new STUAuthorizationService(application, "PROD", new TokenObject { Token = token }))
        //        {
        //            if (auth.IsAuthenticated)
        //            {
        //                using (PrincipalSearchResult<Principal> groups = auth._userPrincipal.GetAuthorizationGroups())
        //                {
        //                    string[] groupsAllowed = Roles.Split(',');
        //                    if (auth.HasAccessToApplication())
        //                    {
        //                        HttpContext.Current.User = auth.GetClaimsPrincipal(token);
        //                        foreach (string groupAllowed in groupsAllowed)
        //                        {
        //                            if (groups.OfType<GroupPrincipal>().Any(g => g.Name.Equals(groupAllowed, StringComparison.OrdinalIgnoreCase)))
        //                            {
        //                                isAuthorized = true;
        //                            }
        //                        }
        //                    }
        //                    if (!isAuthorized)
        //                    {
        //                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "FORBIDDEN");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Authorization Token Not Valid");
        //            }
        //        }
        //    }
        //    return base.OnAuthorizationAsync(actionContext, cancellationToken);
        //}
    }
}