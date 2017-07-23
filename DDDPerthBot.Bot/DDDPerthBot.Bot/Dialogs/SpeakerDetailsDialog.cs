﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using DDDPerth.Services.Bindings;
using DDDPerth.Services.Bindings.Models;
using DDDPerthBot.Bot.DependencyInjection;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace DDDPerthBot.Bot.Dialogs
{
    [Serializable]
    public class SpeakerDetailsDialog : IDialog<IMessageActivity>
    {
        private readonly IBotApiFactory _apiFactory;
        private readonly string _searchTerm;


        public SpeakerDetailsDialog(IBotApiFactory api, string searchTerm)
        {
            this._apiFactory = api;
            this._searchTerm = searchTerm;
        }

        public async Task StartAsync(IDialogContext context)
        {
            try
            {
                var details = await _apiFactory.CreateApi().ApiSpeakersQGetAsync(_searchTerm);

                if (details.Count == 0)
                {
                    await context.SayAsync("I couldn't find a Speaker with the name you're looking for.");
                    context.Done<object>(null);
                }
                else if (details.Count == 1)
                {
                    var message = context.MakeMessage();

                    var herocard = new HeroCard()
                    {
                        Title = details[0].Name,
                        Text = details[0].Bio
                    };

                    message.Attachments.Add(herocard.ToAttachment());

                    await context.PostAsync(message);
                    context.Done<IMessageActivity>(null);
                    
                }
                else
                {
                    var carousel = CreateCarousel(context, details);
                    await context.PostAsync(carousel);
                    context.Wait(MessageReceivedAsync);
                }
            }
            catch (HttpRequestException x)
            {
                await context.PostAsync("I'm sorry, we're encountering technical difficulties. Please try again later.");
                context.Done<IMessageActivity>(null);
            }
        }


        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var response = await result;

            var details = await _apiFactory.CreateApi().ApiSpeakersQGetAsync(response.Text);

            var herocard = new HeroCard()
            {
                Title = details[0].Name,
                Text = details[0].Bio
            };

            var message = context.MakeMessage();
            message.Attachments.Add(herocard.ToAttachment());

            await context.PostAsync(message);
            context.Done<object>(null);
        }

        private IMessageActivity CreateCarousel(IDialogContext context, IList<Speaker> speakers)
        {
            var message = context.MakeMessage();
            message.Text = "Which speaker did you mean?";
            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;


            foreach (var speaker in speakers)
            {
                var herocard = new HeroCard()
                {
                    Buttons = new List<CardAction>() { new CardAction(ActionTypes.PostBack, speaker.Name, value: speaker.Name) }
                };

                message.Attachments.Add(herocard.ToAttachment());
            }
            return message;
        }
    }
}