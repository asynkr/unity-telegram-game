using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Asynkrone.UnityTelegramGame.Security
{
    public interface IObfuscation {
        public long Obfuscate(long playerId, long score);
    }
}