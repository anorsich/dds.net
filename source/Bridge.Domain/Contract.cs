using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Bridge.Domain
{
    public class Contract
    {
        public Contract()
        {
            Trump = Trump.NoTrump;
        }

        public int Value { get; set; }

        public Trump Trump { get; set; }

        public PlayerPosition PlayerPosition { get; set; }

        public override string ToString()
        {
            return String.Format("{0}:{1}{2}", PlayerPosition, Value, Trump);
        }

        public Contract(string contract, PlayerPosition declarer)
        {
            Value = int.Parse(contract[0].ToString());
            PlayerPosition = declarer;
            var suit = Suit.Suits.FirstOrDefault(x => x.ShortName == new string(contract[1], 1));
            if (suit !=null)
            {
                Trump = new Trump(suit);
            }
            else
            {
                Trump = Trump.NoTrump;
            }
        }
    }
}
