namespace StateSharp.Common.Event
{
    public interface IStateSharpEvent
    {
        string Path { get; }
        object OldValue { get; }
        object NewValue { get; }
    }
}