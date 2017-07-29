using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPerthBot.QnAMaker
{
    public interface IQnAMakerService
    {
        Task<QnAResult> ExecuteAsync(string request);
    }
}
