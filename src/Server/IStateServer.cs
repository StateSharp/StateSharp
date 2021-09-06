using StateSharp.Core;

namespace StateSharp.Networking.Server
{
    public interface IStateServer<T> : IStateManager<T> where T : class
    {
        void Start();
        bool Pending();
        void AcceptClient();
        void AcceptClients();
        void Stop();
    }
}
