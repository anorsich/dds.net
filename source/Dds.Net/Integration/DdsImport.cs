using System.Runtime.InteropServices;

// add these references to load an external dll

namespace Dds.Net.Integration
{
    internal class DdsImport
    {
        [DllImport("dds.dll")]
        public static extern int CalcDDtablePBN(DdTableDealPbn tableDealPbn, ref DdTableResults tablep);

        [DllImport("dds.dll")]
        public static extern int SolveBoardPBN(DealPbn dealPBN, int target, int solutions, int mode, ref FutureTricks futureTricks, int threadIndex);
    }
}
