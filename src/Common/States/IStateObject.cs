namespace StateSharp.Core.States
{
    public interface IStateObject<T> : IStateObjectBase where T : class
    {
        T State { get; }

        T Set();
    }
}