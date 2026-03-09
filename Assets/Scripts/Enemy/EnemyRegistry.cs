using System;
using System.Collections.Generic;
using Metaforce.Core;
using UniRx;
using VContainer.Unity;

namespace Metaforce.Enemy
{
    public class EnemyRegistry : IEnemyRegistry, IInitializable, IDisposable
    {
        public IReadOnlyList<IEnemy> AliveEnemies => _aliveEnemies;

        private readonly EnemySpawnService _spawnService;
        private readonly List<IEnemy> _aliveEnemies = new();
        private readonly CompositeDisposable _disposables = new();
        
        public EnemyRegistry(EnemySpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public void Initialize()
        {
            _spawnService.OnEnemyCreated
                .Subscribe(TrackEnemy)
                .AddTo(_disposables);
        }

        private void TrackEnemy(IEnemy enemy)
        {
            enemy.Damageable.IsDead
                .Subscribe(dead =>
                {
                    if (dead)
                        _aliveEnemies.Remove(enemy);
                    else
                        _aliveEnemies.Add(enemy);
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
