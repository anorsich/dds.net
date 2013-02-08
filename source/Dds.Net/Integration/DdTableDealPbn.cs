using System.Runtime.InteropServices;

namespace Dds.Net.Integration
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DdTableDealPbn
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public char[] cards;

        /// <summary>
        /// Constructor. Initialises the local cards array and converts the char array parameter into an array of length 80.
        /// </summary>
        /// <param name="pbnCards">Char array containing a PBN code. Can be of indeterminate length.</param>
        public DdTableDealPbn(char[] pbnCards)
        {
            // init local array
            cards = new char[80];
            // copy pbnCards array into local array, making sure it has an upper bound of 80 in the 1st dimension.
            for (int i = 0; i < pbnCards.Length; i++)
                cards[i] = pbnCards[i];
        }
    }
}
