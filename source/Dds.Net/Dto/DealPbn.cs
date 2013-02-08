using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dds.Net.Dto
{
    public class DealPbn
    {
        public DealPbn()
        {
            CurrentTrickCards = new List<Card>();
        }

        public Trump Trump { get; set; }

        /// <summary>
        /// Leading hand for the trick
        /// </summary>
        public Player TrickDealer { get; set; }

        public List<Card> CurrentTrickCards { get; set; }

        public string RemainCardsPbn { get; set; }
    }
}
