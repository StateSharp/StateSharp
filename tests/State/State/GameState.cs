using StateSharp.Core.States;

namespace State.State
{
    public class GameState
    {
        public IStateString Name { get; private set; }
        public IStateStructure<int> Score { get; private set; }
        public IStateObject<PlayerState> LocalPlayer { get; private set; }
        public IStateDictionary<IStateObject<PlayerState>> RemotePlayers { get; private set; }
    }
}
