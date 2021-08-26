namespace StateSharp.Client
{
    public interface IStateClient<out T> where T : class
    {
        T State { get; }
    }
}