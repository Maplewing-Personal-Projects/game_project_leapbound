using System;

namespace Maplewing.LeapBound.Engine
{
    public static class RandomValue
    {
        private static Random _random = new Random();

        // from: https://stackoverflow.com/questions/3365337/best-way-to-generate-a-random-float-in-c-sharp
        public static float GetFloat(float minValue, float maxValue)
            => (float)(_random.NextDouble() * (maxValue - minValue) + minValue);

        public static int GetInt(int minValue, int maxValue)
            => _random.Next(minValue, maxValue);

        public static bool GetBool()
            => _random.Next() % 2 == 0;
    }
}