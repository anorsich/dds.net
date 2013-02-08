using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dds.Net
{
    public class DdsSolveBoardException : Exception
    {
        public int ErrorCode { get; set; }

        public Dictionary<int, string> ErrorCodeMessageMapping
        {
            get
            {
                return new Dictionary<int, string>()
                           {
                               {-1,"Unknown fault" },
                               {-2,"No of cards = 0" },
                               {-3,"target > Number of tricks left" },
                               {-4,"Duplicated cards" },
                               {-5,"target < -1" },
                               {-7,"target >13" },
                               {-8,"solutions < 1" },
                               {-9,"solutions > 3" },
                               {-10,"No of cards > 52" },
                               {-11,"Not used" },
                               {-12,"Suit or rank value out of range for deal.currentTrickSuit or deal.currentTrickRank." },
                               {-13,"Card already played in the current trick is also defined as a remaining card to play." },
                               {-14,"Wrong number of remaining cards for a hand" },
                               {-15,"threadIndex < 0 or >=noOfThreads, noOfThreads is the configured maximum number of threads" }
                           };
            }
        }


        public DdsSolveBoardException(int code)
        {
            ErrorCode = code;

            if (!ErrorCodeMessageMapping.ContainsKey(code))
            {
                ErrorCode = ErrorCodeMessageMapping.Keys.First();
            }
        }

        public override string Message
        {
            get
            {
                return ErrorCodeMessageMapping[ErrorCode];
            }
        }
    }
}
