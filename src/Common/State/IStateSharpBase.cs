using System.Collections.Generic;

namespace StateSharp.Common.State
{
    public interface IStateSharpBase
    {
        internal StateSharpType Type { get; }
        internal IStateSharpBase Parent { get; }
        internal string Key { get; }
        internal string GetPath(List<IStateSharpBase> callers);
        string GetPath();
    }
}