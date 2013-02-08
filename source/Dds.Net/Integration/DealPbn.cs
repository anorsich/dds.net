using System.Runtime.InteropServices;

namespace Dds.Net.Integration
{
    internal struct DealPbn
    {
        public int trump;

        public int first;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] currentTrickSuit;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] currentTrickRank;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public char[] remainCards;
    }
}