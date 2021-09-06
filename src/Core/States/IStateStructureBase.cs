namespace StateSharp.Core.States
{
    public interface IStateStructureBase : IStateBase
    {
        internal object GetState();
    }
}
