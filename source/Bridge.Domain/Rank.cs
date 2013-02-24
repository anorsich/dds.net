using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Bridge.Domain
{
    public class Rank : IEquatable<Rank>
    {
        public static Rank Two = new Rank(2, "2", "Two");
        public static Rank Three = new Rank(3, "3", "Three");
        public static Rank Four = new Rank(4, "4", "Four");
        public static Rank Five = new Rank(5, "5", "Five");
        public static Rank Six = new Rank(6, "6", "Six");
        public static Rank Seven = new Rank(7, "7", "Seven");
        public static Rank Eight = new Rank(8, "8", "Eight");
        public static Rank Nine = new Rank(9, "9", "Nine");
        public static Rank Ten = new Rank(10, "T", "Ten");
        public static Rank Jack = new Rank(11, "J", "Jack");
        public static Rank Queen = new Rank(12, "Q", "Queen");
        public static Rank King = new Rank(13, "K", "King");
        public static Rank Ace = new Rank(14, "A", "Ace");

        public static List<Rank> Ranks = new List<Rank>() { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };

        public int Score { get; private set; }

        public string ShortName { get; private set; }

        public string FullName { get; private set; }

        public Rank(int score)
            : this(Ranks.Single(x => x.Score == score))
        {
        }

        public Rank(string letter)
            : this(Ranks.Single(x => x.ShortName == letter))
        {
        }

        public Rank(char pbnLetter)
            : this(pbnLetter.ToString(CultureInfo.InvariantCulture))
        {
        }

        internal Rank(Rank rank)
        {
            Score = rank.Score;
            ShortName = rank.ShortName;
            FullName = rank.FullName;
        }

        internal Rank(int score, string pbnLetter, string fullName)
        {
            Score = score;
            ShortName = pbnLetter.ToUpper();
            FullName = fullName;
        }

        public bool Equals(Rank other)
        {
            return Score == other.Score;
        }

        public override string ToString()
        {
            return ShortName;
        }

        public static bool operator ==(Rank x, Rank y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Rank x, Rank y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rank)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Score;
                hashCode = (hashCode * 397) ^ (FullName != null ? FullName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }


}
