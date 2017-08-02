using Newtonsoft.Json;

namespace DDDPerthBot.QnAMaker
{
    public class QnAMakerRequest
    {
        public QnAMakerRequest(string question)
        {
            Question = question;
        }

        public QnAMakerRequest(string question, int top)
        {
            Question = question;
            Top = top;
        }

        [JsonProperty(PropertyName = "question")]
        public string Question { get; private set; }

        [JsonProperty(PropertyName = "top")]
        public int Top { get; private set; } = 1;
    }
}