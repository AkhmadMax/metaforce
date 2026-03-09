using UnityEngine;

namespace Metaforce.Core
{
    public class EnemiesFinder
    {
        private readonly IEnemyRegistry _registry;

        public EnemiesFinder(IEnemyRegistry registry)
        {
            _registry = registry;
        }

        public IEnemy GetClosest(Transform origin, float radius)
        {
            IEnemy closest = null;
            var minDistance = float.MaxValue;

            foreach (var enemy in _registry.AliveEnemies)
            {
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