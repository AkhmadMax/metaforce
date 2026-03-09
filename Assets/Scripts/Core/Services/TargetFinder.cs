using Presenters;
using UnityEngine;

namespace DefaultNamespace
{
    public class TargetFinder
    {
        private readonly EnemySpawnService _spawnService;

        public TargetFinder(EnemySpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public EnemyPresenter GetClosest(Transform origin, float radius)
        {
            EnemyPresenter closest = null;
            var minDistance = float.MaxValue;

            foreach (var enemy in _spawnService.ActiveEnemies)
            {
                if (enemy.EnemyModel.IsDead.Value) continue;

                var distance = Vector3.Distance(
                    origin.position, enemy.EnemyView.Transform.position);

                if (distance > radius) continue;
                if (distance >= minDistance) continue;

                minDistance = distance;
                closest = enemy;
            }

            return closest;
        }
    }
}