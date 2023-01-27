namespace Diabloid
{
    public interface ISaveLoadService
    {
        PlayerProgress LoadProgress();
        void SaveProgress();
    }
}