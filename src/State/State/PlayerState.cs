using StateSharp.Common.State;

namespace State.State
{
    public class PlayerState
    {
        public IStateSharpStructure<Vector3> Position { get; private set; }
    }
}