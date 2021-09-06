using StateSharp.Core.States;

namespace StateSharp.Tests.State.State
{
    public class PlayerState
    {
        public IStateStructure<Vector3> Position { get; private set; }
    }
}
