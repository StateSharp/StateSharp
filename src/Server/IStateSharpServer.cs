namespace StateSharp.Server
{
    public interface IStateSharpServer<out T>
    {
        T State { get; }
        void Start();
        void Stop();
    }
}