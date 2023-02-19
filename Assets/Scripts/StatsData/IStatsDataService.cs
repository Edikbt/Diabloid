namespace Diabloid
{
    public interface IStatsDataService
    {
        void Load();
        EnemyStatsData Enemy(EnemyTypeId typeId);
        LevelData Level(string sceneName);
    }
}