// -----------------------------------------------------------------------
// <copyright file="Deck.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DdsPlay.Model
{
    /// <summary>
    /// Class to represent a deck of cards, or a hand of cards, or any
    /// other collection of cards used in the game.  I.e. the dealer's
    /// deck, or a stack of cards in a solitairre game, or a discard pile.
    /// </summary>
    public class Deck
    {
        #region Properties

        /// <summary>
        /// Gets the cards collection for this deck.
        /// </summary>
        public List<Card> Cards
        {
            get { return _cards; }
        }
        private List<Card> _cards = new List<Card>();

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Deck"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        private bool _enabled = true;

        /// <summary>
        /// Gets the game instance to which this deck is associated.
        /// </summary>
        public Game Game
        {
            get { return _game; }
        }
        private Game _game;

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

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [sort changed].
        /// </summary>
        public event EventHandler SortChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Deck"/> class.
        /// </summary>
        /// <param name="game">The game.</param>
        public Deck(Game game)
        {
            this._game = game;
            this._game.Decks.Add(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Deck"/> class.
        /// </summary>
        /// <param name="numberOfDecks">The number of decks.</param>
        /// <param name="uptoNumber">The upto number.</param>
        /// <param name="game">The game.</param>
        public Deck(int numberOfDecks, int uptoNumber, Game game)
            : this(game)
        {
            for (int deck = 0; deck < numberOfDecks; deck++)
            {
                for (int suit = 1; suit <= 4; suit++)
                {
                    for (int number = 1; number <= uptoNumber; number++)
                    {
                        Cards.Add(new Card(number, (CardSuit)suit, this));
                    }
                }
            }

            Shuffle();
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Determines whether [has] [the specified number].
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="suit">The suit.</param>
        /// <returns>
        ///   <c>true</c> if [has] [the specified number]; otherwise, <c>false</c>.
        /// </returns>
        public bool Has(int number, CardSuit suit)
        {
            return Has((CardRank)number, suit);
        }

        /// <summary>
        /// Determines whether [has] [the specified rank].
        /// </summary>
        /// <param name="rank">The rank.</param>
        /// <param name="suit">The suit.</param>
        /// <returns>
        ///   <c>true</c> if [has] [the specified rank]; otherwise, <c>false</c>.
        /// </returns>
        public bool Has(CardRank rank, CardSuit suit)
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
        public Card GetCard(int number, CardSuit suit)
        {
            return GetCard((CardRank)number, suit);
        }

        /// <summary>
        /// Gets the first matching card in the deck matching the
        /// specified suit and rank.
        /// </summary>
        /// <param name="rank">The rank to match.</param>
        /// <param name="suit">The suit to match.</param>
        /// <returns>The matching card.</returns>
        public Card GetCard(CardRank rank, CardSuit suit)
        {
            return Cards.FirstOrDefault(card => (card.Rank == rank) && (card.Suit == suit));
        }

        #endregion

        #region Sort Methods

        /// <summary>
        /// Shuffles the the deck's cards list one time.
        /// </summary>
        public void Shuffle()
        {
            Shuffle(1);
        }

        /// <summary>
        /// Shuffles the deck's cards list specified number of times.
        /// </summary>
        /// <param name="times">The number of times.</param>
        public void Shuffle(int times)
        {
            for (int time = 0; time < times; time++)
            {
                for (int i = 0; i < Cards.Count; i++)
                {
                    Cards[i].Shuffle();
                }
            }

            if (SortChanged != null)
                SortChanged(this, null);
        }

        /// <summary>
        /// Sorts the cards in the deck  using the Game's suit comparer.
        /// </summary>
        public void Sort()
        {
            Cards.Sort(Game.CardSuitComparer);

            if (SortChanged != null)
                SortChanged(this, null);
        }
        
        #endregion

        #region Draw Cards Methods

        /// <summary>
        /// Draws/Moves the specified amount of cards to the specified deck.
        /// </summary>
        /// <param name="toDeck">To deck.</param>
        /// <param name="count">The count.</param>
        public void Draw(Deck toDeck, int count)
        {
            for (var i = 0; i < count; i++)
            {
                TopCard.Deck = toDeck;
            }
        }

        #endregion

        #region Generic Methods

        /// <summary>
        /// Method to toggle the visibility of all cards in the deck.
        /// </summary>
        public void FlipAllCards()
        {
            foreach (var t in Cards)
            {
                t.Visible = !t.Visible;
            }
        }

        /// <summary>
        /// Method to set the value of Enabled to the provided value on
        /// all of the cards in the deck.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        public void EnableAllCards(bool enable)
        {
            foreach (var t in Cards)
            {
                t.Enabled = enable;
            }
        }

        /// <summary>
        /// Method to set the value of the IsDragable on all of the cards
        /// in the deck.
        /// </summary>
        /// <param name="isDragable">if set to <c>true</c> [is dragable].</param>
        public void MakeAllCardsDragable(bool isDragable)
        {
            foreach (var t in Cards)
            {
                t.IsDragable = isDragable;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var output = new StringBuilder();

            output.Append("[" + Environment.NewLine);

            foreach (var t in Cards)
            {
                output.Append(t.ToString() + Environment.NewLine);
            }

            output.Append("]" + Environment.NewLine);

            return output.ToString();
        }

        #endregion
    }
}
