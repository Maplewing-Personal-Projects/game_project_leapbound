using LaYumba.Functional;

namespace Maplewing.LeapBound.Engine
{
    public class CoinItem : IItem
    {
        private const int MAX_MONEY_VALUE = 300;
        private const int MIN_MONEY_VALUE = 100;

        string IItem.Id { get; } = "coin";

        GamePlayEngine.State IItem.Get(GamePlayEngine.State currentState)
            => currentState.With(
                state => state.Money, 
                currentState.Money + RandomValue.Get(MIN_MONEY_VALUE, MAX_MONEY_VALUE));
    }
}
