using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.Domain
{
    public class Trump : IEquatable<Trump>
    {
        public static Trump Spades = new Trump(Suit.Spades);
        public static Trump Hearts = new Trump(Suit.Hearts);
        public static Trump Diamonds = new Trump(Suit.Diamonds);
        public static Trump Clubs = new Trump(Suit.Clubs);
        public static Trump NoTrump = new Trump(new Suit(4, "NoTrump", "NT"));
        public static List<Trump> Trumps = new List<Trump>() { Spades, Hearts, Diamonds, Clubs, NoTrump };

        internal Suit Suit { get; set; }

        public int Order
        {
            get { return Suit.Order; }
        }

        public string ShortName
        {
            get { return Suit.ShortName; }
        }

        public string FullName
        {
            get { return Suit.FullName; }
        }

        public Trump(int order)
            : this(Trumps.Single(x => x.Order == order).Suit)
        {

        }

        internal Trump(Suit suit)
        {
            Suit = suit;
        }

        public bool Equals(Trump other)
        {
            return other.Suit == this.Suit;
        }

        public override string ToString()
        {
            return Suit.ToString();
        }

        public static bool operator ==(Trump x, Trump y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Trump x, Trump y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            return Equals((Trump)obj);
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
