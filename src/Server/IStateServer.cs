namespace StateSharp.Server
{
    public interface IStateServer<out T>
    {
        T State { get; }
        void Start();
        void Stop();
    }
}