using System.Collections.Generic;

namespace PBN
{
    public class PbnParseResult
    {
        private class PbnKeys
        {
            public const string Dealer = "Dealer";
            public const string Auction = "Auction";
            public const string Deal = "Deal";
            public const string Contract = "Contract";
            public const string Result = "Result";
            public const string Play = "Play";
        }

        private PbnValue this[string i]
        {
            get
            {
                if (Values.ContainsKey(i))
                {
                    return Values[i];
                }
                return new PbnValue();
            }
        }

        public Dictionary<string, PbnValue> Values { get; set; }

        public PbnParseResult()
        {
            Values = new Dictionary<string, PbnValue>();
        }

        public string Dealer
        {
            get { return this[PbnKeys.Dealer].Value; }
        }

        public string Deal
        {
            get { return this[PbnKeys.Deal].Value; }
        }

        public string Contract
        {
            get { return this[PbnKeys.Contract].Value; }
        }

        public string Result
        {
            get { return this[PbnKeys.Result].Value; }
        }

        public string FirstPlayer
        {
            get { return this[PbnKeys.Play].Value; }
        }

        public string Play
        {
            get { return this[PbnKeys.Play].Body; }
        }

        public string Auction
        {
            get { return this[PbnKeys.Auction].Body; }
        }
    }
}