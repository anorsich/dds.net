using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dds.Net
{
    public class DdsCalcDDtableException : Exception
    {
        public int ErrorCode { get; set; }

        public DdsCalcDDtableException(int code)
        {
            ErrorCode = code;
        }
    }
}
