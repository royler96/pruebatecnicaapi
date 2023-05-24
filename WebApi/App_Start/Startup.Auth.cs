using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Models;
using WebApi.Providers;
using Microsoft.Owin.Security.DataProtection;

[assembly: OwinStartup(typeof(WebApi.App_Start.Startup))]

namespace WebApi.App_Start
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }//aqui
        public static string PublicClientId { get; private set; }
        public static OAuthBearerAuthenticationOptions OAuthBearerOptionsMpApp { get; private set; }//aqui
        //public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);//aqui
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);//aqui
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());//aqui
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);//aqui

            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                //The Path For generating the Toekn
                TokenEndpointPath = new PathString("/Token"),
                //Setting the Token Expired Time (2 HOURS)
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                //MyAuthorizationServerProvider class will validate the user credentials
                Provider = new ApplicationAuthProvider(),

                AccessTokenFormat = new TicketDataFormat(app.CreateDataProtector(
                            typeof(OAuthAuthorizationServerMiddleware).Namespace,
                            "Access_Token", "v1")),
                RefreshTokenFormat = new TicketDataFormat(app.CreateDataProtector(
                            typeof(OAuthAuthorizationServerMiddleware).Namespace,
                            "Refresh_Token", "v1")),
                AccessTokenProvider = new AuthenticationTokenProvider(),
                RefreshTokenProvider = new AuthenticationTokenProvider(),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            };//aqui

            //app.UseOAuthAuthorizationServer(options);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseOAuthBearerTokens(OAuthOptions);

            //app.UseOAuthAuthorizationServer(OAuthOptionsJobInfo);//aqui

            OAuthBearerOptionsMpApp = new OAuthBearerAuthenticationOptions();//aqui
            OAuthBearerOptionsMpApp.AccessTokenFormat = OAuthOptions.AccessTokenFormat;//aqui
            OAuthBearerOptionsMpApp.AccessTokenProvider = OAuthOptions.AccessTokenProvider;//aqui//aqui
            OAuthBearerOptionsMpApp.AuthenticationMode = OAuthOptions.AuthenticationMode;
            OAuthBearerOptionsMpApp.AuthenticationType = OAuthOptions.AuthenticationType;//aqui
            OAuthBearerOptionsMpApp.Description = OAuthOptions.Description;//aqui
            OAuthBearerOptionsMpApp.Provider = new CustomBearerAuthenticationProvider();//aqui

            OAuthBearerAuthenticationExtensions.UseOAuthBearerAuthentication(app, OAuthBearerOptionsMpApp);//aqui

            HttpConfiguration config = new HttpConfiguration();//aqui
            WebApiConfig.Register(config);//aqui
        }

        public class CustomBearerAuthenticationProvider : OAuthBearerAuthenticationProvider//aqui
        {
            public override Task ValidateIdentity(OAuthValidateIdentityContext context)
            {
                var claims = context.Ticket.Identity.Claims;
                if (context.Ticket.Properties.ExpiresUtc.Value > DateTime.UtcNow)
                {
                    if (claims.Count() == 0 || claims.Any(claim => claim.Issuer != "Facebook" && claim.Issuer != "Google" && claim.Issuer != "LOCAL_AUTHORITY"))
                        context.Rejected();
                }
                return Task.FromResult<object>(null);
            }
        }


    }
}