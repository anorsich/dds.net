using System.Runtime.InteropServices;

namespace Dds.Net.Integration
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DdTableResults
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] // 4 * 5 = 20
            /* Because we had to convert this to a 1-dimensional array, the order is somewhat messed up.
         * What follows is an index-map of the array corresponding to meaningful keys. The indexes are zero-based. (1-20 = 0-19)
         *      S   H   D   C   NT
         *  N   0   4   8   12  16
         *  E   1   5   9   13  17
         *  S   2   6   10  14  18
         *  W   3   7   11  15  19
         */
            public int[] resTable;
    }
}