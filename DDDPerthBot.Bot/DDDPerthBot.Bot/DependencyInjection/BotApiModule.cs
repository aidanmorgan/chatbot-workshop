using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using DDDPerth.Services.Bindings;
using Microsoft.Bot.Builder.Internals.Fibers;

namespace DDDPerthBot.Bot.DependencyInjection
{
    public class BotApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            // http://localhost:2391
            builder.RegisterInstance(new BotApiFactory("http://localhost:2391")).As<IBotApiFactory>();
        }
    }
}