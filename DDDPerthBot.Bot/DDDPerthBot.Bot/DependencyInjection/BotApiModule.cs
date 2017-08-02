using Autofac;
using DDDPerthBot.Bot.Services;
using DDDPerthBot.QnAMaker;

namespace DDDPerthBot.Bot.DependencyInjection
{
    public class BotApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);


            builder.RegisterInstance(new BotApiFactory("http://dddperthbot-services.azurewebsites.net"))
                .As<IBotApiFactory>();

            #warning Replace these values with your own Q&A maker values if you want to use Q&A maker
            builder.RegisterInstance(new QnAMakerService("581616c0-2f16-4953-925d-cbe20ce9da15", "51f4cbd93f684d7dbcdddd224865caf5"))
                .As<IQnAMakerService>();

            builder.RegisterType<ChatFragmentService>().As<IChatFragmentService>();
        }
    }
}