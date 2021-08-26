using StateSharp.Core.State;

namespace State.State
{
    public class GameState
    {
        public IStateStructure<int> Score { get; private set; }
        public IStateObject<PlayerState> LocalPlayer { get; private set; }
        public IStateDictionary<IStateObject<PlayerState>> RemotePlayers { get; private set; }
    }
}