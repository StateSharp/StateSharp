namespace StateSharp.Core
{
    public static class StateManagerConstructor
    {
        public static IStateManager<T> New<T>() where T : class
        {
            return new StateManager<T>();
        }
    }
}