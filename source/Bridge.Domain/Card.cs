using System;

namespace Bridge.Domain
{
    public class Card : IComparable<Card>, IEquatable<Card>
    {
        public Card(Rank rank, Suit suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }

        public Card(int cardRank, Suit suit)
        {
            this.Rank = new Rank(cardRank);
            this.Suit = suit;
        }

        public PlayerPosition PlayerPosition { get; set; }

        public Rank Rank { get; set; }

        public Suit Suit { get; set; }

        public CardColor Color
        {
            get
            {
                if ((Suit == Suit.Spades) || (Suit == Suit.Clubs))
                    return CardColor.Black;
                else
                    return CardColor.Red;
            }
        }

        public int CompareTo(Card other)
        {
            int value1 = this.Rank.Score;
            int value2 = other.Rank.Score;

            if (value1 > value2)
                return 1;
            else if (value1 < value2)
                return -1;
            else
                return 0;
        }

        public bool Equals(Card other)
        {
            return Rank == other.Rank && Suit == other.Suit;
        }

        public override string ToString()
        {
            return this.Rank.ToString() + this.Suit.ToString();
        }

        public static bool operator ==(Card x, Card y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Card x, Card y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            return Equals((Card)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (PlayerPosition != null ? PlayerPosition.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Rank != null ? Rank.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Suit != null ? Suit.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
