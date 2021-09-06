namespace StateSharp.Core.Events
{
    public class StateEvent : IStateEvent
    {
        public StateEvent(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
