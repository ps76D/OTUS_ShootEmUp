namespace Infrastructure.Listeners
{
    public interface IUpdatableListener : IGameStateListener
    {
        void CustomUpdate();
    }
}