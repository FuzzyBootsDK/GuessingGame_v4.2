using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame_v4._2
{
    internal class RandomNumber
    {
        public static int NewRandomNumber(int minValue, int maxValue)
        {
            Random randomNumber = new();
            int newRandomNumber = randomNumber.Next(minValue, maxValue);
            return newRandomNumber;
        }
    }
}
