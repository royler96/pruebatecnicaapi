using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebApi.App_Start;
using WebGrease;
using WebApi.DataAccess.Models;
using Microsoft.AspNet.Identity.Owin;

namespace WebApi.Providers
{
    public class ApplicationAuthProvider : OAuthAuthorizationServerProvider
    {
        
        public ApplicationAuthProvider()
        {
            
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId = string.Empty;
            string clientSecret = string.Empty;

            /*if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.SetError("invalid_client", "Client credentials could not be retrieved through the Authorization header.");
                return Task.FromResult<object>(null);
            }*/
            context.OwinContext.Set<string>("ta:client", "1");
            context.Validated();
            return Task.FromResult<object>(null); 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            BD_COMPLEJOSEntities ctxBD = new BD_COMPLEJOSEntities();
            try
            {
                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                
                var user = await userManager.FindAsync(context.UserName, context.Password);
                                

                if (user == null)
                {                    
                    context.SetError("invalid_grant", "Usuario y/o contraseña incorrectos.");
                    return;
                }
                else
                {                    
                    if (user.activo == null || user.activo == false)
                    {                        
                        context.SetError("invalid_grant", "Usuario o contraseña incorrectos.");
                        return;
                    }
                }
               
                    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);

                    context.OwinContext.Set<string>("ta:idusuario", user.id_usuario.ToString());
                    context.OwinContext.Set<string>("ta:username", user.username.ToString());

                    // oAuthIdentity.AddClaim(new Claim("datos", "0"));

                string Username = Convert.ToString(user.username);                    
                   ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);
                    AuthenticationProperties properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "Username", Username
                    }
                });
                    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                    context.Validated(ticket);
                    context.Request.Context.Authentication.SignIn(cookiesIdentity);

            }
            catch (Exception e)
            {                
                context.SetError("error_system", "Error for login." + e.InnerException.Message);
                return;
            }

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            // Summary:
            //     Called before the TokenEndpoint redirects its response to the caller.            

            return Task.FromResult<object>(null);
        }
        

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["client_id"];
            var currentClient = context.ClientId;
            if (originalClient != currentClient)
            {                
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }
            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));
            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
            return Task.FromResult<object>(null);
        }
    }
}