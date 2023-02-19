using UnityEngine;

namespace Diabloid
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(EnemyStatsData enemyStatsData, Transform parent);
    }
}