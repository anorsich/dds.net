using System;

namespace DdsPlay.Model
{
    public class Card : IComparable<Card>
    {
        public Card(CardRank rank, CardSuit suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }

        public Card(int cardRank, CardSuit suit)
        {
            this.Rank = (CardRank)cardRank;
            this.Suit = suit;
        }

        public CardRank Rank { get; set; }

        public CardSuit Suit { get; set; }

        public CardColor Color
        {
            get
            {
                if ((Suit == CardSuit.Spades) || (Suit == CardSuit.Clubs))
                    return CardColor.Black;
                else
                    return CardColor.Red;
            }
        }

        public int Number
        {
            get
            {
                return (int)Rank;
            }
        }

        public string NumberString
        {
            get
            {
                switch (Rank)
                {
                    case CardRank.Ace:
                        return "A";
                    case CardRank.Jack:
                        return "J";
                    case CardRank.Queen:
                        return "Q";
                    case CardRank.King:
                        return "K";
                    default:
                        return Number.ToString();
                }
            }
        }

        public string SuitString
        {
            get
            {
                switch (Suit)
                {
                    case CardSuit.Spades: return "♠";
                    case CardSuit.Hearts: return "♥";
                    case CardSuit.Clubs: return "♣";
                    case CardSuit.Diamonds: return "♦";
                    default: return Suit.ToString();
                }
            }
        }

        public bool Visible { get; set; }

        public bool Enabled { get; set; }

        public bool IsDragable { get; set; }

        public int CompareTo(Card other)
        {
            int value1 = this.Number;
            int value2 = other.Number;

            if (Card.IsAceBiggest)
            {
                if (value1 == 1)
                    value1 = 14;

                if (value2 == 1)
                    value2 = 14;
            }

            if (value1 > value2)
                return 1;
            else if (value1 < value2)
                return -1;
            else
                return 0;
        }

        public override string ToString()
        {
            return this.NumberString + "" + this.SuitString;
        }

        public EventHandler VisibleChanged { get; set; }

        public EventHandler DeckChanged { get; set; }
    }
}
