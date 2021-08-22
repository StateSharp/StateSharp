using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateSharp.Common.State;

namespace StateSharp.Common.Services
{
    internal static class PathService
    {
        public static string GetPath(IReadOnlyList<IStateSharpBase> callers)
        {
            var builder = new StringBuilder();
            var type = StateSharpType.Object;
            builder.Append(callers.Last().Key);
            foreach (var caller in callers.Reverse().Skip(1))
            {
                switch (type)
                {
                    case StateSharpType.Dictionary:
                        builder.Append($"[{caller.Key}]");
                        break;
                    case StateSharpType.Object:
                        builder.Append($".{caller.Key}");
                        break;
                    case StateSharpType.Structure:
                        builder.Append($".{caller.Key}");
                        break;
                    default:
                        throw new ArgumentException($"Unknown StateSharpType {type}");
                }
                type = caller.Type;
            }
            return builder.ToString();
        }
    }
}