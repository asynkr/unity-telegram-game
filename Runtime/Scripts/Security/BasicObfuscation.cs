using System;

namespace Asynkrone.UnityTelegramGame.Security
{
    public class BasicObfuscation : IObfuscation
    {
        private long[] BIG_PRIMES;
        public delegate int PlayerIdToTokenIndex(long playerId);
        private PlayerIdToTokenIndex playerIdToTokenIndex; 

        /// <summary>
        /// Provides a basic obfuscation of the score,
        /// so that it will be difficult to cheat.
        /// <br/>
        /// The obfuscation relies on the server app and the Unity app
        /// both knowing the player id and the tokens.
        /// <br/>
        /// One can't rely solely on player id because it is shared with no obfuscation to the game.
        /// One can't rely solely on 1 big prime because it's easy to trace back to the prime knowing the score.
        /// </summary>
        /// <param name="bigNumbers">The big prime numbers</param>
        /// <param name="playerIdToTokenIndex">The method to go from player Id to a token in big primes</param>
        public BasicObfuscation(long[] bigPrimes, PlayerIdToTokenIndex playerIdToTokenIndex)
        {
            this.BIG_PRIMES = bigPrimes;
            this.playerIdToTokenIndex = playerIdToTokenIndex;
        }
        public BasicObfuscation(long[] bigPrimes) : this(bigPrimes, BasicPlayerIdToTokenIndex) { }

        private static int SumDigits(long value)
        {
            if (value < 10) return (int)value;

            long sum = 0;
            while (value != 0)
            {
                long rem;
                value = Math.DivRem(value, 10, out rem);
                sum += rem;
            }

            if (sum >= 10)
                sum = SumDigits(sum);

            return (int)sum;
        }
        public static int BasicPlayerIdToTokenIndex(long playerId)
        {
            return SumDigits(playerId) - 1;
        }

        public long Obfuscate(long playerId, long score)
        {
            int index = playerIdToTokenIndex(playerId);

            long token = BIG_PRIMES[index];
            return score * token;
        }
    }
}