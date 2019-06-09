using ColetaApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Coleta.Integracao
{
    public class Api
    {
        private const string NomeCookie = "Autenticacao";

        public static ColetaApiClient CriaCliente()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(NomeCookie);
            var token = cookie?.Value ?? "None";

            var credenciais = new Microsoft.Rest.TokenCredentials(token, "Bearer");

            return new ColetaApiClient(new Uri(ConfigurationManager.AppSettings["ApiUrl"]), credenciais);
        }

        public static void DefineToken(string token)
        {
            HttpContext.Current.Response.Cookies.Set(new HttpCookie(NomeCookie, token)
            {
                Expires = DateTime.UtcNow.AddDays(2),
                HttpOnly = true,
                //Secure = true
            });
        }
    }
}