namespace Maplewing.LeapBound.Engine
{
    public interface IItem
    {
        string Id { get; }
        GamePlayEngine.State Get(GamePlayEngine.State currentState);
    }
}
