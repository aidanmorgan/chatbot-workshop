using System;
using System.Threading.Tasks;
using DDDPerthBot.Bot.DependencyInjection;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace DDDPerthBot.Bot.Dialogs
{
    [Serializable]
    public class SessionDetailsDialog : IDialog<IMessageActivity>
    {
        private readonly IBotApiFactory _apiFactory;
        private readonly string _searchTerm;


        public SessionDetailsDialog(IBotApiFactory api, string searchTerm)
        {
            _apiFactory = api;
            _searchTerm = searchTerm;
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Done<IMessageActivity>(null);
            return Task.CompletedTask;
        }
    }
}