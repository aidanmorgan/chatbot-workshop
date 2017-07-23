using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DDDPerth.Services.Bindings;
using DDDPerthBot.Bot.DependencyInjection;
using Microsoft.Bot.Builder.Dialogs.Internals;

namespace DDDPerthBot.Bot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            {
                // http://docs.autofac.org/en/latest/integration/webapi.html#quick-start
                var builder = new ContainerBuilder();

                // register the Bot Builder module
                builder.RegisterModule(new BotApiModule());

                // Get your HttpConfiguration.
                var config = GlobalConfiguration.Configuration;

                // Register your Web API controllers.
                builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

                // OPTIONAL: Register the Autofac filter provider.
                builder.RegisterWebApiFilterProvider(config);

                // Set the dependency resolver to be Autofac.
                var container = builder.Build();
                config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            }

            // WebApiConfig stuff
            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
            });
        }
    }
}
