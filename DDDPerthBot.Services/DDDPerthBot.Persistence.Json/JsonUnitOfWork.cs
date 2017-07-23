using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDDPerthBot.Model;
using Newtonsoft.Json;

namespace DDDPerthBot.Persistence.Json
{
    public class JsonUnitOfWork : IUnitOfWork
    {
        private readonly IRepository<Session> _sessionsRepository;
        private readonly IRepository<Speaker> _speakersRepository;

        public JsonUnitOfWork(string speakerJson, string sessionJson)
        {
            var speakers = JsonConvert.DeserializeObject<Speaker[]>(speakerJson);
            var sessions = JsonConvert.DeserializeObject<Session[]>(sessionJson);

            _sessionsRepository = new JsonRepository<Session>(sessions.ToList());
            _speakersRepository = new JsonRepository<Speaker>(speakers.ToList());
        }

        public IRepository<Session> Sessions => _sessionsRepository;
        public IRepository<Speaker> Speakers => _speakersRepository;
    }
}
