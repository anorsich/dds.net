using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bridge.Domain;

namespace Dds.Net.Dto
{
    public class FutureTricks
    {
        public FutureTricks()
        {
            FutureCards = new Deck();
        }

        /// <summary>
        /// Number of searched nodes 
        /// </summary>
        public int Nodes { get; set; }

        /// <summary>
        /// No of alternative cards
        /// </summary>
        public int Cards { get; set; }

        public Deck FutureCards { get; set; }

        public List<int> Scores { get; set; }
    }
}
