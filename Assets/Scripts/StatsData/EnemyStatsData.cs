using UnityEngine;

namespace Diabloid
{
    [CreateAssetMenu(fileName = "EnemyData", menuName ="StatsData/Enemy")]
    public class EnemyStatsData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;

        [Range(1, 1000)] public int HP;
        [Range(1, 100)] public float Damage;

        [Range(0.5f, 1)] public float EffectiveDistance;
        [Range(0.5f, 1)] public float Radius;

        public int MinGold;
        public int MaxGold;

        public GameObject Prefab;

    }
}