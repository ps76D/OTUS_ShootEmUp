using Components;
using Infrastructure.Listeners;

namespace GameManager.Listeners
{
    public interface ICharacterHitPointsListener : IGameListener
    {
        void InGame(HitPointsComponent hitPointsComponent);
    }
}