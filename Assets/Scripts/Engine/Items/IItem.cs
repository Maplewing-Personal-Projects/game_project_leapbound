using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public interface IItem
    {
        string Id { get; }
        Rectangle AreaRange { get; }
        GamePlayEngine.State Get(GamePlayEngine.State currentState);
    }
}
