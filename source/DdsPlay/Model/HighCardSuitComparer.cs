// -----------------------------------------------------------------------
// <copyright file="CardSuiteComparer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace DdsPlay.Model
{
    /// <summary>
    /// Comparer for uptown sorting (Ace High).
    /// </summary>
    public class HighCardSuitComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            if (x.Suit != y.Suit)
            {
                if (x.Suit < y.Suit)
                    return -1;
                else return 1;
            }
            else
            {
                return y.CompareTo(x);
            }
        }
    }
}
