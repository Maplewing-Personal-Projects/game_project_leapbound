using LaYumba.Functional;
using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public class CoinItem : IItem
    {
        private const int MAX_MONEY_VALUE = 300;
        private const int MIN_MONEY_VALUE = 100;

        private Rectangle _areaRange;

        string IItem.Id { get; } = "coin";
        Rectangle IItem.AreaRange { get => _areaRange; }

        GamePlayEngine.State IItem.Get(GamePlayEngine.State currentState)
            => currentState.With(
                state => state.Money,
                currentState.Money + RandomValue.GetInt(MIN_MONEY_VALUE, MAX_MONEY_VALUE));


        public CoinItem(Vector2D position)
            => _areaRange = new Rectangle(
                position,
                new Vector2D(0.2f, 0.2f));
    }
}
