namespace Asynkrone.UnityTelegramGame.Security
{
    public interface IObfuscation {
        public long Obfuscate(long playerId, long score);
    }
}