using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dds.Net.Dto
{
    public class FutureTricks
    {
        public FutureTricks()
        {
            FutureCards = new List<Card>();
        }

        /// <summary>
        /// Number of searched nodes 
        /// </summary>
        public int Nodes { get; set; }

        /// <summary>
        /// No of alternative cards
        /// </summary>
        public int Cards { get; set; }

        public List<Card> FutureCards { get; set; }
    }
}
