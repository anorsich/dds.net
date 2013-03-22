using System.Collections.Generic;
using System.IO;

namespace PBN
{
    public class PbnParser
    {
        public static PbnParseResult ParseGame(string input)
        {
            var reader = new StringReader(input);
            var result = new PbnParseResult();
            PbnValue lastPbnValue = null;
            while (true)
            {
                var line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                if (line.StartsWith("["))
                {
                    var key = line.Substring(1, line.IndexOf(' ') - 1);
                    var start = line.IndexOf('"') + 1;
                    var end = line.LastIndexOf('"');
                    var value = line.Substring(start, end - start);
                    lastPbnValue = new PbnValue {Value = value};
                    result.Values.Add(key, lastPbnValue);
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    if (lastPbnValue != null)
                    {
                        var isEmpty = lastPbnValue.Body == null;
                        if (!isEmpty)
                        {
                            lastPbnValue.Body += "\r\n";
                        }
                        lastPbnValue.Body += line;
                    }
                }
                else
                {
                    lastPbnValue = null;
                }
            }
            return result;
        }


        public static IEnumerable<string[]> ParsePlay(string input)
        {
            var reader = new StringReader(input);
            while (true)
            {
                var line = reader.ReadLine();
                if (line == null)
                {
                    yield break;
                }
                yield return line.Split(' ');
            }
        }
    }

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

    public class PbnValue
    {
        public string Value { get; set; }
        public string Body { get; set; }
    }
}
