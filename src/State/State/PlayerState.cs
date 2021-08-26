using StateSharp.Core.State;

namespace State.State
{
    public class PlayerState
    {
        public IStateStructure<Vector3> Position { get; private set; }
    }
}