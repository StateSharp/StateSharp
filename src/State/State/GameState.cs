using StateSharp.Core.State;

namespace State.State
{
    public class GameState
    {
        public IStateDictionary<IStateObject<PlayerState>> Players { get; private set; }
    }
}