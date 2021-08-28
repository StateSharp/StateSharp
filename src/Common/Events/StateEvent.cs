namespace StateSharp.Core.Events
{
    public class StateEvent : IStateEvent
    {
        public string Path { get; }

        public StateEvent(string path)
        {
            Path = path;
        }
    }
}