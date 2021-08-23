namespace StateSharp.Common
{
    public static class StateSharpManagerConstructor
    {
        public static IStateSharpManager<T> New<T>()
        {
            return new StateSharpManager<T>();
        }
    }
}