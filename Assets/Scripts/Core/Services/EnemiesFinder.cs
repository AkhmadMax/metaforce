using Core.Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemiesFinder
    {
        private readonly EnemySpawnService _spawnService;

        public EnemiesFinder(EnemySpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public IEnemy GetClosest(Transform origin, float radius)
        {
            IEnemy closest = null;
            var minDistance = float.MaxValue;

            foreach (var enemy in _spawnService.ActiveEnemies)
            {
                if (enemy.Damageable.IsDead.Value) continue;

                var distance = Vector3.Distance(
                    origin.position, enemy.Targetable.Transform.position);

                if (distance > radius) continue;
                if (distance >= minDistance) continue;

                minDistance = distance;
                closest = enemy;
            }

            return closest;
        }
    }
}