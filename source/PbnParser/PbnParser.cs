using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PBN
{
    public class PbnParser
    {
        public static string GetNextPosition(string position)
        {
            switch (position)
            {
                case "S":
                    return "W";
                case "W":
                    return "N";
                case "N":
                    return "E";
                case "E":
                    return "S";
                default:
                    throw new ArgumentOutOfRangeException(position);
            }
        }

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

        public static IEnumerable<HandParseResult> ParseHands(string input)
        {
            var position = input.Substring(0, 1);
            var hands = input.Substring(2).Split(' ');
            foreach (var hand in hands)
            {
                yield return new HandParseResult
                {
                    Cards = ParseHand(hand).ToList(),
                    Position = position
                };
                position = GetNextPosition(position);
            }
        }

        static readonly string[] Suits = new[] { "S", "H", "D", "C" };

        private static IEnumerable<string> ParseHand(string input)
        {
            var cards = input.Split('.');
            var suit = 0;
            foreach (var suitCards in cards)
            {
                foreach (char t in suitCards)
                {
                    yield return Suits[suit] + t;
                }
                suit++;
            }
        }

        public class HandParseResult
        {
            public string Position { get; set; }

            public List<string> Cards { get; set; }
        }
    }
}
