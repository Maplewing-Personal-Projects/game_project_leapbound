namespace Maplewing.LeapBound.Engine
{
    public interface ICommand 
    {
        GamePlayEngine.State Execute(GamePlayEngine.State state);
    }
}