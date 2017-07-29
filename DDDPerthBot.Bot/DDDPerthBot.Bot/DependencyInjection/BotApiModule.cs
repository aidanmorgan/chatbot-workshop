using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using DDDPerth.Services.Bindings;
using DDDPerthBot.Bot.Services;
using Microsoft.Bot.Builder.Internals.Fibers;
using DDDPerthBot.QnAMaker;

namespace DDDPerthBot.Bot.DependencyInjection
{
    public class BotApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // http://localhost:2391
            builder.RegisterInstance(new BotApiFactory("http://localhost:2391")).As<IBotApiFactory>();
            builder.RegisterInstance(new QnAMakerService("581616c0-2f16-4953-925d-cbe20ce9da15", "51f4cbd93f684d7dbcdddd224865caf5")).As<IQnAMakerService>();

            builder.RegisterType<ChatFragmentService>().As<IChatFragmentService>();
        }
    }
}