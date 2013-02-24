using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge.Domain
{
    /// <summary>
    /// Class to represent a deck of cards, or a hand of cards, or any
    /// other collection of cards used in the game.  I.e. the dealer's
    /// deck, or a stack of cards in a solitairre game, or a discard pile.
    /// </summary>
    public class Deck
    {
        public Deck()
        {
            Cards = new List<Card>();
        }

        /// <summary>
        /// Gets the cards collection for this deck.
        /// </summary>
        public List<Card> Cards { get; set; }

        /// <summary>
        /// Gets the top card.
        /// </summary>
        public Card TopCard
        {
            get { return Cards.Count > 0 ? Cards[Cards.Count - 1] : null; }
        }

        /// <summary>
        /// Gets the bottom card.
        /// </summary>
        public Card BottomCard
        {
            get { return Cards.Count > 0 ? Cards[0] : null; }
        }

        public Card CardWithHighestRank
        {
            get { return Cards.OrderByDescending(x => x.Rank.Score).ToList()[0]; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has cards.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has cards; otherwise, <c>false</c>.
        /// </value>
        public bool HasCards
        {
            get { return Cards.Count > 0; }
        }

        public void RemoveCard(Card card)
        {
            var cardToRemove = Cards.Single(x => x == card);
            Cards.Remove(cardToRemove);
        }
        #region Get Methods

        /// <summary>
        /// Determines whether [has] [the specified number].
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="suit">The suit.</param>
        /// <returns>
        ///   <c>true</c> if [has] [the specified number]; otherwise, <c>false</c>.
        /// </returns>
        public bool Has(int number, Suit suit)
        {
            return Has(new Rank(number), suit);
        }

        /// <summary>
        /// Determines whether [has] [the specified rank].
        /// </summary>
        /// <param name="rank">The rank.</param>
        /// <param name="suit">The suit.</param>
        /// <returns>
        ///   <c>true</c> if [has] [the specified rank]; otherwise, <c>false</c>.
        /// </returns>
        public bool Has(Rank rank, Suit suit)
        {
            if (GetCard(rank, suit) != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the card in the deck matching the specified integer
        /// rank value and the specified suit.
        /// </summary>
        /// <param name="number">The number of the rank to match.</param>
        /// <param name="suit">The suit to match.</param>
        /// <returns>The matching card.</returns>
        public Card GetCard(int number, Suit suit)
        {
            return GetCard(new Rank(number), suit);
        }

        /// <summary>
        /// Gets the first matching card in the deck matching the
        /// specified suit and rank.
        /// </summary>
        /// <param name="rank">The rank to match.</param>
        /// <param name="suit">The suit to match.</param>
        /// <returns>The matching card.</returns>
        public Card GetCard(Rank rank, Suit suit)
        {
            return Cards.FirstOrDefault(card => (card.Rank == rank) && (card.Suit == suit));
        }

        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Count);
            sb.Append(":");
            foreach (var card in Cards)
            {
                sb.Append(card);
            }

            return sb.ToString();
        }

        public int Count { get { return Cards.Count; } }
    }
}
