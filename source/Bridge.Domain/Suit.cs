using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.Domain
{
    public class Suit : IEquatable<Suit>
    {
        public static Suit Spades = new Suit(0, "Spades", "S");
        public static Suit Hearts = new Suit(1, "Hearts", "H");
        public static Suit Diamonds = new Suit(2, "Diamonds", "D");
        public static Suit Clubs = new Suit(3, "Clubs", "C");
        public static List<Suit> Suits = new List<Suit>()
        {
            Spades,
            Hearts,
            Diamonds,
            Clubs
        };

        /// <summary>
        /// Order is important for the double dummy solver.
        /// </summary>
        public int Order { get; private set; }

        public string FullName { get; private set; }

        public string ShortName { get; private set; }

        public Suit(int order)
            : this(Suits.Single(x => x.Order == order))
        {
        }

        internal Suit(Suit suit)
            : this(suit.Order, suit.FullName, suit.ShortName)
        {

        }

        internal Suit(int order, string name, string shortName)
        {
            Order = order;
            FullName = name;
            ShortName = shortName.ToUpper();
        }

        public bool Equals(Suit other)
        {
            return Order == other.Order;
        }

        public override string ToString()
        {
            switch (ShortName)
            {
                case "S": return "♠";
                case "H": return "♥";
                case "C": return "♣";
                case "D": return "♦";
                default: return ShortName;
            }
        }

        public static bool operator ==(Suit x, Suit y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Suit x, Suit y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            return Equals((Suit)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Order;
                hashCode = (hashCode * 397) ^ (FullName != null ? FullName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
