namespace StateSharp.Core
{
    public static class StateManagerConstructor
    {
        public static IStateManager<T> New<T>()
        {
            return new StateManager<T>();
        }
    }
}