using StateSharp.Core;

namespace StateSharp.Networking.Client
{
    public interface IStateClient<T> : IStateManager<T> where T : class
    {
        bool Connected { get; }
        void Connect(string hostname, int port);
        void Close();
    }
}
