using System;

namespace Aries.Utility
{
    public class RandomExt
    {
        public static int GetRandomNumber(int min, int max)
        {
            var buffer = Guid.NewGuid().ToByteArray();
            var iSeed = BitConverter.ToInt32(buffer, 0);
            var r = new Random(iSeed);
            return r.Next(min, max + 1);
        }


    }
}
