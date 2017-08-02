using System.Threading.Tasks;

namespace DDDPerthBot.QnAMaker
{
    public interface IQnAMakerService
    {
        Task<QnAResult> ExecuteAsync(string request);
    }
}