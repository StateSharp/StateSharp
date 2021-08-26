namespace StateSharp.Client
{
    public static class StateClientConstructor
    {
        public static IStateClient<T> New<T>()
        {
            return new StateClient<T>();
        }
    }
}