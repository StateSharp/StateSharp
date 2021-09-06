namespace StateSharp.Core.States
{
    public interface IStateObjectBase : IStateBase
    {
        internal object GetState();
    }
}
