// Code generated by Microsoft (R) AutoRest Code Generator 0.15.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace DDDPerth.Services.Bindings.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Speaker
    {
        /// <summary>
        /// Initializes a new instance of the Speaker class.
        /// </summary>
        public Speaker() { }

        /// <summary>
        /// Initializes a new instance of the Speaker class.
        /// </summary>
        public Speaker(Guid? id = default(Guid?), string name = default(string), string bio = default(string))
        {
            Id = id;
            Name = name;
            Bio = bio;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bio")]
        public string Bio { get; set; }

    }
}