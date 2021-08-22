namespace StateSharp.Common.State
{
    public abstract class StateSharpBase
    {
        internal StateSharpBase Parent { get; }

        protected StateSharpBase(StateSharpBase parent)
        {
            Parent = parent;
        }
    }
}