// -----------------------------------------------------------------------
// <copyright file="Game.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DdsPlay.Model
{
    public class Game
    {
        private List<Card> cards = new List<Card>();
        public List<Card> Cards
        {
            get
            {
                return cards;
            }
        }

        private List<Deck> decks = new List<Deck>();
        public List<Deck> Decks
        {
            get
            {
                return decks;
            }
        }

        internal HighCardSuitComparer CardSuitComparer = new HighCardSuitComparer();
    }}
