using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dds.Net.Dto
{
    public class Card
    {
        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public Rank Rank { get; set; }

        public Suit Suit { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}", Rank, Suit);
        }
    }
}
