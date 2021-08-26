namespace StateSharp.Core.Event
{
    public interface IStateEvent
    {
        string Path { get; }
        object OldValue { get; }
        object NewValue { get; }
    }
}