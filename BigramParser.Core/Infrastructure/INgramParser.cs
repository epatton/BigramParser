using System;
using System.Collections.Generic;
using System.Text;

namespace BigramParser.Core.Infrastructure
{
    public interface INgramParser
    {
        string Input { get; set; }
        Dictionary<string, int> Histogram { get; set; }

        string ScrubInput(string input);
        Dictionary<string, int> Parse();
    }
}
