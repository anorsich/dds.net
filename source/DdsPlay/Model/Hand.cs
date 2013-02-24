// -----------------------------------------------------------------------
// <copyright file="Hand.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace DdsPlay.Model
{
    /// <summary>
    /// Class to represent a single hand of cards in a multiplayer card game.
    /// </summary>
    public class Hand
    {
        List<Card> CardsInHand { get; set; }
        List<Card> CardsPlayed { get; set; }

        void Sort()
        {
        }
    }
}
