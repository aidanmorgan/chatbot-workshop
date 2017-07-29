using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DDDPerthBot.Bot.Services
{
    public interface IChatFragmentService
    {
        string RandomNoAnswerFragment();
    }

    [Serializable]
    public class ChatFragmentService : IChatFragmentService
    {
        public const string NoAnswerFragmentFile = "NoAnswerFragments.txt";

        // intentionally marking this as non-serialized as we want to reload this each time.
        [NonSerialized]
        private IList<string> _noAnswerFragments;
        private IList<string> NoAnswerFragments
        {
            get
            {
                if (_noAnswerFragments == null)
                {
                    _noAnswerFragments = ReadFile(NoAnswerFragmentFile)
                        .Split('\n')
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Select(x => x.Trim())
                        .ToList();
                }

                return _noAnswerFragments;
            }
        }

        private readonly Random _random;

        public ChatFragmentService()
        {
            _random = new Random();
        }

        public string RandomNoAnswerFragment()
        {
            return NoAnswerFragments[_random.Next(0, NoAnswerFragments.Count - 1)];
        }

        private static string ReadFile(string filename)
        {
            var pathToFile =$"{HttpRuntime.AppDomainAppPath}{Path.DirectorySeparatorChar}Fragments{Path.DirectorySeparatorChar}{filename}";

            string fileContent;

            using (StreamReader reader = File.OpenText(pathToFile))
            {
                fileContent = reader.ReadToEnd();
            }

            return fileContent;

        }
    }
}
