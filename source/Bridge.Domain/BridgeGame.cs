using System.Collections.Generic;
using System.Linq;
using Bridge.Domain.Utils;

namespace Bridge.Domain
{
    public class BridgeGame
    {
        private Trick _currentTrick;
        
        public BridgeGame(Dictionary<PlayerPosition, Deck> state, PlayerPosition declarer)
        {
            GameState = state;
            Tricks = new List<Trick>();
            Declarer = declarer;
            Dummy = BridgeHelper.GetNextPlayerPosition(BridgeHelper.GetNextPlayerPosition(declarer));
            Contract = new Contract();
            _currentTrick = new Trick() { TrickDealer = Declarer };
        }

        public Dictionary<PlayerPosition, Deck> GameState { get; private set; }

        public List<Trick> Tricks { get; private set; }

        public PlayerPosition Declarer { get; private set; }

        public PlayerPosition Dummy { get; private set; }

        public Contract Contract { get; private set; }

        public Trick CurrentTrick
        {
            get { return _currentTrick; }
        }

        public int CardsRemaning
        {
            get { return GameState.Values.Sum(x => x.Count); }
        }

        public PlayerPosition PlayCard(Card card, PlayerPosition playerPosition)
        {
            var nextPlayer = BridgeHelper.GetNextPlayerPosition(playerPosition);

            if (_currentTrick.Deck.Count == 0)
            {
                _currentTrick = new Trick();
                _currentTrick.TrickDealer = playerPosition;
            }


            if (_currentTrick.Deck.Count <= 4)
            {
                card.PlayerPosition = playerPosition;
                _currentTrick.Deck.Cards.Add(card);
            }

            if (_currentTrick.Deck.Count == 4)
            {
                Tricks.Add(_currentTrick);
                var winner = FindWinner(_currentTrick, Contract.Trump);
                _currentTrick.TrickWinner = winner;
                nextPlayer = winner;
                _currentTrick = new Trick() { TrickDealer = winner };
            }

            GameState[playerPosition].RemoveCard(card);

            return nextPlayer;
        }

        #region Helper properties

        public int NorthSouthTricksMade
        {
            get { return Tricks.Count(x => x.TrickWinner == PlayerPosition.North || x.TrickWinner == PlayerPosition.South); }
        }

        public int EastWestTricksMade
        {
            get { return Tricks.Count(x => x.TrickWinner == PlayerPosition.East || x.TrickWinner == PlayerPosition.West); }
        }

        #endregion

        private PlayerPosition FindWinner(Trick trick, Trump trump)
        {
            var highestTrump = trick.Deck.Cards.Where(c => c.Suit.Order == trump.Order).OrderByDescending(c => c.Rank.Score);
            var highestInTrickDealerSuit = trick.Deck.Cards.Where(c => c.Suit.Order == trick.TrickDealerSuit.Order).OrderByDescending(c => c.Rank.Score);
            return highestTrump.Any()
                       ? highestTrump.First().PlayerPosition
                       : highestInTrickDealerSuit.First().PlayerPosition;
        }
    }
}
