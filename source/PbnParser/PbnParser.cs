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
}
