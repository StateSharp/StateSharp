namespace StateSharp.Core.State
{
    public interface IStateObject<T> : IStateObjectBase
    {
        T State { get; }
    }
}