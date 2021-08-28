using StateSharp.Core.States;

namespace State.State
{
    public class PlayerState
    {
        public IStateStructure<Vector3> Position { get; private set; }
    }
}