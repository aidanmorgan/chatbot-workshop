using System;
using System.Threading.Tasks;
using DDDPerthBot.Bot.DependencyInjection;
using DDDPerthBot.Bot.Services;
using DDDPerthBot.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace DDDPerthBot.Bot.Dialogs
{
    #warning Replace these values with your own LUIS values if you want to use LUIS
    [LuisModel("f7e32880-8175-481b-9005-90bc6fb336e8", "d34f0cf004364e748c5aa234a0622432")]
    [Serializable]
    public class ConferenceRootDialog : LuisDialog<object>
    {
        private const int QnAMakerMinimumScore = 50;
        private readonly IBotApiFactory _apiFactory;
        private readonly IChatFragmentService _chatFragmentService;
        private readonly IQnAMakerService _qnAMakerService;

        public ConferenceRootDialog(IBotApiFactory apiFactory, IQnAMakerService qnAMakerService,
            IChatFragmentService chatFragmentService)
        {
            _apiFactory = apiFactory;
            _qnAMakerService = qnAMakerService;
            _chatFragmentService = chatFragmentService;
        }

        public ConferenceRootDialog(IBotApiFactory apiFactory, ILuisService service)
            : base(service)
        {
            _apiFactory = apiFactory;
        }


        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            // we have no response from Luis, so lets try QnA maker before spitting the dummy
            var response = await _qnAMakerService.ExecuteAsync(result.Query);

            if (response != null && response.Score >= QnAMakerMinimumScore)
                await context.PostAsync(response.Answer);
            else
                await context.PostAsync(_chatFragmentService.RandomNoAnswerFragment());

            context.Wait(MessageReceived);
        }


        [LuisIntent(LuisIntents.SpeakerDetails)]
        public async Task SpeakerDetails(IDialogContext context, LuisResult result)
        {
            if (result.Entities.Count > 0)
            {
                var bestMatchEntity = result.Entities[0].Entity;
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
                var bestMatchEntity = result.Entities[0].Entity;
                context.Call(new SessionDetailsDialog(_apiFactory, bestMatchEntity), ResumeAfterSessionDetails);
            }
            else
            {
                await context.SayAsync("I'm sorry, I don't know of any Speaker by that name.");
                context.Wait(MessageReceived);
            }
        }

        private async Task ResumeAfterSessionDetails(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            await result;
            context.Wait(MessageReceived);
        }

        private async Task ResumeAfterSpeakerDetails(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            await result;
            context.Wait(MessageReceived);
        }


        protected override Task MessageReceived(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            return base.MessageReceived(context, item);
        }
    }
}