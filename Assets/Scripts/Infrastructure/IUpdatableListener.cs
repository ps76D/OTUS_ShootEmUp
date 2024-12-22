using Infrastructure.DI;

namespace Infrastructure
{
    public interface IUpdatableListener : IGameStateListener
    {
        void CustomUpdate();
    }
}