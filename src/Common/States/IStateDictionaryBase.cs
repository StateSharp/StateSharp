using System.Collections.Generic;

namespace StateSharp.Core.States
{
    public interface IStateDictionaryBase : IStateBase
    {
        internal IReadOnlyDictionary<string, object> GetState();
    }
}
