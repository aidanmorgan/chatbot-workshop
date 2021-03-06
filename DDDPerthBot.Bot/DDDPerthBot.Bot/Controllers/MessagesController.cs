﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DDDPerthBot.Bot.DependencyInjection;
using DDDPerthBot.Bot.Dialogs;
using DDDPerthBot.Bot.Services;
using DDDPerthBot.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace DDDPerthBot.Bot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly IChatFragmentService _chatFragmentService;
        private readonly IQnAMakerService _qnaMakerService;
        private readonly IBotApiFactory _scope;

        public MessagesController(IBotApiFactory scope, IQnAMakerService qnaMakerService,
            IChatFragmentService chatFragmentService)
        {
            _scope = scope;
            _qnaMakerService = qnaMakerService;
            _chatFragmentService = chatFragmentService;
        }

        /// <summary>
        ///     POST: api/Messages
        ///     Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
                await Conversation.SendAsync(activity,
                    () => new ConferenceRootDialog(_scope, _qnaMakerService, _chatFragmentService));
            else
                HandleSystemMessage(activity);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}