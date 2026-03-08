using UnityEngine;
using Views;

namespace Settings
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs / Enemies")]
    public class EnemiesConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyView Prefab { get; private set; }

        [field: SerializeField] public int MaxEnemies { get; private set; } = 10;
        [field: SerializeField] public int Health { get; private set; } = 100;
        [field: SerializeField] public float RespawnCooldown { get; private set; } = 5;
        [field: SerializeField] public float GridSize { get; private set; } = 20;
        [field: SerializeField] public float SpawnRange { get; private set; } = 8;
    }
}