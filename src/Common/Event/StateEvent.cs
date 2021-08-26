namespace StateSharp.Core.Event
{
    public class StateEvent : IStateEvent
    {
        public string Path { get; }
        public object OldValue { get; }
        public object NewValue { get; }

        public StateEvent(string path, object oldValue, object newValue)
        {
            Path = path;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}