using StateSharp.Common.State;

namespace StateSharp.Server
{
    public interface IStateSharpServer<T> : IStateSharpObject<T>
    {
        void Start();
        void Stop();
    }
}