namespace StateSharp.Client
{
    public interface IStateClient<out T>
    {
        T State { get; }
    }
}