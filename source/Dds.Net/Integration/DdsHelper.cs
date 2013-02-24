using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dds.Net
{
    internal class DdsHelper
    {
        /// <summary>
        /// Converts Portable Bridge Notation File from string to 80 chars array.
        /// Pbn code example: E:AT5.AJT.A632.KJ7 Q763.KQ9.KQJ94.T 942.87653..98653 KJ8.42.T875.AQ42
        /// </summary>
        /// <param name="pbn">Pbn code</param>
        /// <returns>80 chars array</returns>
        internal static char[] PbnStringToChars(string pbn)
        {
            var result = new char[80];
            for (int i = 0; i < pbn.Length; i++)
                result[i] = pbn[i];

            return result;
        }
    }
}
