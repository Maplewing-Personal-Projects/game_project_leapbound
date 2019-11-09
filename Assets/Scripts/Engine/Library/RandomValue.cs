using System;

namespace Maplewing.LeapBound.Engine
{
    public static class RandomValue
    {
        private static Random _random = new Random();

        public static int GetInt(int minValue, int maxValue)
            => _random.Next(minValue, maxValue);

        public static bool GetBool()
            => _random.Next() % 2 == 0;
    }
}