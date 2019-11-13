using LaYumba.Functional;
using System.Linq;

namespace Maplewing.LeapBound.Engine
{
    public class PlayerAttackCommand : ICommand
    {
        GamePlayEngine.State ICommand.Execute(GamePlayEngine.State state)
            => state.With(
                s => s.Bullets,
                state.Bullets.Concat(new IBullet[] { state.Player.Attack() }).ToArray());
    }
}