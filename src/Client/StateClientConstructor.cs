namespace StateSharp.Networking.Client
{
    public static class StateClientConstructor
    {
        public static IStateClient<T> New<T>() where T : class
        {
            return new StateClient<T>();
        }
    }
}
