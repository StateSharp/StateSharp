using StateSharp.Common.State;

namespace UnitTests.State
{
    public class GameState
    {
        public IStateSharpDictionary<IStateSharpObject<PlayerState>> Players { get; private set; }
    }
}