using System;
using System.Collections.Generic;
using Kiki.Models.Data;

namespace Kiki.Models.Metadata
{
    public class Publisher
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Books published by this publisher
        /// </summary>
        public List<AudioBook> Books { get; set; }

        /// <summary>
        /// Series published by this publisher
        /// </summary>
        public List<Series> Series { get; set; }
    }
}