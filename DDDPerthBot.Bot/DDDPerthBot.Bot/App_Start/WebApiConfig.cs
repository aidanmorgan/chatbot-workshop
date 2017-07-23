using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DDDPerthBot.Bot.DependencyInjection;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DDDPerthBot.Bot
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

        }
    }
}
