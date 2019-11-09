using System;

namespace Maplewing.LeapBound.Engine
{
    public static class RandomValue
    {
        private static Random _random = new Random();

        public static int Get(int minValue, int maxValue)
            => _random.Next(minValue, maxValue); 
    }
}