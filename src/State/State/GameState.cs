using StateSharp.Common.State;

namespace State.State
{
    public class GameState
    {
        public IStateSharpDictionary<IStateSharpObject<PlayerState>> Players { get; private set; }
    }
}