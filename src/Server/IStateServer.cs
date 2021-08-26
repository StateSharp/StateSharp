namespace StateSharp.Server
{
    public interface IStateServer<out T> where T : class
    {
        T State { get; }
        void Start();
        void Stop();
    }
}