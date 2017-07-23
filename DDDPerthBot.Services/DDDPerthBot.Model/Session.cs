using System;
using System.Collections.Generic;
using System.Text;

namespace DDDPerthBot.Model
{
    public class Session : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Abstract { get; set; }

        
    }
}
