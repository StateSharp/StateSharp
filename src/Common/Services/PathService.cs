using System.Collections.Generic;

namespace StateSharp.Core.Services
{
    internal static class PathService
    {
        internal static List<string> SplitPath(string path)
        {
            var result = new List<string>();
            foreach (var split in path.Split("."))
            {
                if (split.EndsWith(']'))
                {
                    result.Add(split.Substring(0, split.IndexOf('[')));
                    result.Add(split.Substring(split.IndexOf('[')));
                }
                else
                {
                    result.Add(split);
                }
            }
            return result;
        }
    }
}