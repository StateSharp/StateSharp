namespace StateSharp.Common.Event
{
    public class StateSharpEvent : IStateSharpEvent
    {
        public string Path { get; }
        public object OldValue { get; }
        public object NewValue { get; }

        public StateSharpEvent(string path, object oldValue, object newValue)
        {
            Path = path;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}