using BigramParser.Core.Extensions;
using BigramParser.Core.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace BigramParser.Core.Parsers
{
    public class BigramTextParser : INgramParser
    {
        public string Input { get; set; }
        public Dictionary<string, int> Histogram { get; set; } = new Dictionary<string, int>();

        public BigramTextParser(string input)
        {
            Input = ScrubInput(input);
        }

        public string ScrubInput(string input)
        {
            return input
                .ScrubNonLettersAndDigits()
                .Trim()
                .ToLower();
        }

        public Dictionary<string, int> Parse()
        {
            var words = Input.Split(' ').ToList();
            if(words.Count > 1)
            {
                int lastIndex = words.Count - 1;
                for(int i = 0; i < words.Count; ++i)
                {
                    if(i < lastIndex)
                    {
                        var bigramKey = words[i] + " " + words[i + 1];
                        if(Histogram.ContainsKey(bigramKey))
                        {
                            Histogram[bigramKey] = Histogram[bigramKey] + 1;
                        }
                        else
                        {
                            Histogram.Add(bigramKey, 1);
                        }
                    }
                }
            }

            return Histogram;
        }        
    }
}
