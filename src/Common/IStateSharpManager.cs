namespace StateSharp.Common
{
    public interface IStateSharpManager<out T>
    {
        T State { get; }
    }
}