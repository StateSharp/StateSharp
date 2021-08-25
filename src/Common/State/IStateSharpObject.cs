namespace StateSharp.Common.State
{
    public interface IStateSharpObject<T> : IStateSharpObjectBase
    {
        T State { get; }
    }
}