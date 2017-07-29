using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace TravelAgentBot
{
    [Serializable]
    public class FlightBookingDialog : IDialog<object>
    {
        private LocationAndTime _origin;
        private LocationAndTime _destination;

        public Task StartAsync(IDialogContext context)
        {
            context.Call(new LocationTimeDialog(TravelDirection.Origin), AfterOriginDetails);
            return Task.CompletedTask;
        }


        private async Task AfterOriginDetails(IDialogContext context, IAwaitable<LocationAndTime> result)
        {
            _origin = await result;
            context.Call(new LocationTimeDialog(TravelDirection.Destination), AfterDestinationDetails);
        }


        private async Task AfterDestinationDetails(IDialogContext context, IAwaitable<LocationAndTime> result)
        {
            _destination = await result;
            context.Call(new TravellerDetailsDialog(), AfterTravellerDetails);
        }


        private Task AfterTravellerDetails(IDialogContext context, IAwaitable<object> result)
        {
            // there's more to be done here....
            return Task.CompletedTask;
        }
    }

    [Serializable]
    public class LocationTimeDialog : IDialog<LocationAndTime>
    {
        private readonly TravelDirection _travelDirection;
        private string _location;
        private string _when;

        public LocationTimeDialog(TravelDirection travelDirection)
        {
            _travelDirection = travelDirection;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.SayAsync("Where?");
            context.Wait(DestinationNameProvided);
        }

        private async Task DestinationNameProvided(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var answer = await result;

            _location = answer.Text;

            // maybe we call a web-service here to turn a location into a valid airport code
            if (!IsLocationValid(_location))
            {
                await context.SayAsync("What? Try Again!");
                context.Wait(DestinationNameProvided);
            }
            else
            {
                await context.SayAsync("When?");
                context.Wait(DepartureTimeProvided);
            }
        }

        private async Task DepartureTimeProvided(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var answer = await result;

            this._when = answer.Text;

            DateTimeOffset? time = ConvertWhenToDateTimeOffset(_when);

            if (!time.HasValue)
            {
                await context.SayAsync("I'm sorry, I don't recognise that time.");
                context.Wait(DepartureTimeProvided);
            }
            else
            {
                context.Done(new LocationAndTime()
                {
                    Location = this._location,
                    When = time.Value
                });
            }
        }

        private DateTimeOffset? ConvertWhenToDateTimeOffset(string @when)
        {
            // obviously this needs to be a lot smarter..
            // HINT: there is a LUIS model for handling time
            return DateTimeOffset.UtcNow;
        }

        private bool IsLocationValid(string location)
        {
            // obviously this needs to be a lot smarter...
            // HINT: Call a web-service to process the location as a valid destination?
            return true;
        }
    }


    [Serializable]
    public class TravellerDetailsDialog :IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            throw new NotImplementedException();
        }
    }



    public enum TravelDirection
    {
        Origin,
        Destination
    }

    [Serializable]
    public class LocationAndTime
    {
        public string Location { get; set; }
        public DateTimeOffset When { get; set; }
    }
}
