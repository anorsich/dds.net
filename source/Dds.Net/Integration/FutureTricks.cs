using System.Runtime.InteropServices;

namespace Dds.Net.Integration
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct FutureTricks
    {
        /// <summary>
        /// /* Number of searched nodes */
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int nodes;

        /// <summary>
        /// /*  No of alternative cards  */
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int cards;

        /// <summary>
        /// /* 0=Spades, 1=Hearts, 2=Diamonds, 3=Clubs */
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public int[] suit;

        /// <summary>
        /// /* 2-14 for 2 through Ace */ 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public int[] rank;

        /// <summary>
        /// /* Bit string of ranks for equivalent lower rank cards. The decimal value range between 4 (=2) and 8192 (King=rank 13). 
        ///  When there are several ”equals”, the value is the sum of each ”equal”. */
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public int[] equals;

        /// <summary>
        /// /* -1 indicates that target was not reached, otherwise target or max numbe of tricks */ 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public int[] score;
    }
}