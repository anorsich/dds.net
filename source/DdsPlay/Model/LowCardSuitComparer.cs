// -----------------------------------------------------------------------
// <copyright file="LowCardSuitComparer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace DdsPlay.Model
{
    /// <summary>
    /// Comparer for downtown sorting. (Ace Low)
    /// </summary>
    public class LowCardSuitComparer : IComparer<Card>
    {
        #region IComparer<Card> Members

        public int Compare(Card x, Card y)
        {
            if (x.Suit == y.Suit)
            {
                return x.CompareTo(y);
            }

            if (x.Suit < y.Suit)
                return -1;
            else
                return 1;
        }

        #endregion
    }
}
