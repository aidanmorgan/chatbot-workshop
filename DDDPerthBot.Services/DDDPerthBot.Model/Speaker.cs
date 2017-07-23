using System;
using System.Collections.Generic;

namespace DDDPerthBot.Model
{
    public class Speaker : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Bio { get; set; }

        public string Twitter { get; set; }
    }
}
