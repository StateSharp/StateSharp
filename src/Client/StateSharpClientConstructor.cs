namespace StateSharp.Client
{
    public static class StateSharpClientConstructor
    {
        public static IStateSharpClient<T> New<T>()
        {
            return new StateSharpClient<T>();
        }
    }
}