using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using MvcApplication1.DTO;
using MvcApplication1.Poco;
using System.Web.Http.Cors;

namespace MvcApplication1
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
            var cors = new EnableCorsAttribute("*", "*", "*"); // ToDo: put production site
            config.EnableCors(cors);

			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new {id = RouteParameter.Optional}
				);

			config.EnsureInitialized();


			// Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
			// To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
			// For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
			//config.EnableQuerySupport();

			// To disable tracing in your application, please comment out or remove the following line of code
			// For more information, refer to: http://www.asp.net/web-api
			config.EnableSystemDiagnosticsTracing();

			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<UserEntry, UserDto>();
				cfg.CreateMap<Role, RoleDto>();
				cfg.CreateMap<Topic, TopicDto>();
                cfg.CreateMap<Message, MessageDto>();
			});

            
           
            
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            //app.UseCors(CorsOptions.AllowAll);


			
			

		}
	}
}
