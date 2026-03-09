using System;
using System.Collections.Generic;
using Metaforce.Core;
using UniRx;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Metaforce.Enemy
{
    public class EnemySpawnService : IStartable, IDisposable
    {
        private readonly Func<Vector3, EnemyPresenter> _createEnemy;
        private readonly EnemiesConfig _config;
        private readonly Subject<IEnemy> _onEnemyCreated = new();
        private readonly List<EnemyPresenter> _enemies = new();
        private readonly CompositeDisposable _disposables = new();

        public IObservable<IEnemy> OnEnemyCreated => _onEnemyCreated;

        public EnemySpawnService(Func<Vector3, EnemyPresenter> createEnemy, EnemiesConfig config)
        {
            _createEnemy = createEnemy;
            _config = config;
        }

        public void Start()
        {
            for (int i = 0; i < _config.MaxEnemies; i++)
                SpawnEnemy(GetRandomPosition());
        }

        private void SpawnEnemy(Vector3 position)
        {
            var presenter = _createEnemy(position);
            presenter.Start();
            _enemies.Add(presenter);
            _onEnemyCreated.OnNext(presenter);

            presenter.Damageable.IsDead
                .Where(dead => dead)
                .Delay(TimeSpan.FromSeconds(_config.RespawnCooldown))
                .Subscribe(_ => presenter.Respawn(GetRandomPosition()))
                .AddTo(_disposables);
        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                Random.Range(-_config.SpawnRange, _config.SpawnRange), 0f, Random.Range(-_config.SpawnRange, _config.SpawnRange));
        }

        public void Dispose()
        {
            foreach (var enemy in _enemies)
                enemy.Dispose();
            _disposables.Dispose();
            _onEnemyCreated.Dispose();
        }
    }
}