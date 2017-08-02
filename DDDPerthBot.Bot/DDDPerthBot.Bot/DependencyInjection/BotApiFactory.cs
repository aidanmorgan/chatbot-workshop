using System;
using DDDPerth.Services.Bindings;

namespace DDDPerthBot.Bot.DependencyInjection
{
    public interface IBotApiFactory
    {
        IDDDPerthBotAPI CreateApi();
    }

    [Serializable]
    public class BotApiFactory : IBotApiFactory
    {
        private readonly string _url;

        public BotApiFactory(string url)
        {
            _url = url;
        }

        public IDDDPerthBotAPI CreateApi()
        {
            return new DDDPerthBotAPI(new Uri(_url));
        }
    }
}