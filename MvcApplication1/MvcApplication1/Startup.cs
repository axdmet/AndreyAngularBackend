    using System;
    using System.Web.Http;
    using Microsoft.Owin;
    using Microsoft.Owin.Cors;
    using Microsoft.Owin.Logging;
    using Microsoft.Owin.Security.OAuth;
    using MvcApplication1;
    using Owin;

public class Startup
    {
      //  public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
       // public static OAuthAuthorizationServerOptions OAuthServerOptions { get; set; }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
		    WebApiConfig.Register(config);
		    

            app.UseCors(CorsOptions.AllowAll);
			this.ConfigureOAuth(app);


           GlobalConfiguration.Configuration.EnsureInitialized();
			app.UseWebApi(config);
        }

		public void ConfigureOAuth(IAppBuilder app)
		{
			//OAuthBearerOptions = new OAuthBearerAuthenticationOptions
			//{
			//	Provider = new QueryStringOAuthBearerProvider("token")
			//};

			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
				Provider =new SimpleAuthorizationServerProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(OAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
    }
