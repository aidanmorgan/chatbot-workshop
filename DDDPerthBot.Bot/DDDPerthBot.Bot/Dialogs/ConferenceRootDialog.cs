using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using DDDPerth.Services.Bindings;
using DDDPerthBot.Bot.DependencyInjection;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace DDDPerthBot.Bot.Dialogs
{
    // TODO : change these to your own LUIS model.
    [LuisModel("f7e32880-8175-481b-9005-90bc6fb336e8", "d34f0cf004364e748c5aa234a0622432")]
    [Serializable]
    public class ConferenceRootDialog : LuisDialog<object>
    {
        private readonly IBotApiFactory _apiFactory;

        public ConferenceRootDialog(IBotApiFactory apiFactory)
        {
            this._apiFactory = apiFactory;
        }

        public ConferenceRootDialog(IBotApiFactory apiFactory, ILuisService service)
            : base(service)
        {
            _apiFactory = apiFactory;
        }


        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry I did not understand your request.)";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }


        [LuisIntent(LuisIntents.SpeakerDetails)]
        public async Task SpeakerDetails(IDialogContext context, LuisResult result)
        {
            if (result.Entities.Count > 0)
            {
                string bestMatchEntity = result.Entities[0].Entity;
                context.Call(new SpeakerDetailsDialog(_apiFactory, bestMatchEntity), ResumeAfterSpeakerDetails);
            }
            else
            {
                await context.SayAsync("I'm sorry, I don't know of any Speaker by that name.");
                context.Wait(MessageReceived);
            }
        }

        [LuisIntent(LuisIntents.SessionDetails)]
        public async Task SessionDetails(IDialogContext context, LuisResult result)
        {
            if (result.Entities.Count > 0)
            {
                string bestMatchEntity = result.Entities[0].Entity;
                context.Call(new SessionDetailsDialog(_apiFactory, bestMatchEntity), ResumeAfterSessionDetails);
            }
            else
            {
                await context.SayAsync("I'm sorry, I don't know of any Speaker by that name.");
                context.Wait(MessageReceived);
            }
        }

        private Task ResumeAfterSessionDetails(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            context.Done<object>(null);
            return Task.CompletedTask;
        }

        private Task ResumeAfterSpeakerDetails(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            context.Done<object>(null);
            return Task.CompletedTask;
        }


        protected override Task MessageReceived(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            return base.MessageReceived(context, item);
        }
    }
}