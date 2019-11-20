using System.Collections.Generic;

namespace BigramParser.Tests.Helpers
{
    public static class EqualityChecks
    {
        public static bool DictionariesAreEqual(
            Dictionary<string, int> d1,
            Dictionary<string, int> d2)
        {
            bool equal = false;
            if (d1.Count == d2.Count)
            {
                equal = true;
                foreach (var pair in d1)
                {
                    int value;
                    if (d2.TryGetValue(pair.Key, out value))
                    {
                        if (value != pair.Value)
                        {
                            equal = false;
                            break;
                        }
                    }
                    else
                    {
                        equal = false;
                        break;
                    }
                }
            }

            return equal;
        }
    }
}
