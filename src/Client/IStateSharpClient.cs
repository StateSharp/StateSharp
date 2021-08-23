namespace StateSharp.Client
{
    public interface IStateSharpClient<out T>
    {
        T State { get; }
    }
}