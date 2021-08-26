namespace StateSharp.Core.State
{
    public interface IStateObject<T> : IStateObjectBase where T : class
    {
        T State { get; }

        T Set();
    }
}