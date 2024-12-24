namespace Infrastructure.Listeners
{
    public interface IStartGameListener : IGameStateListener
    {
        void StartGame();
    }
}