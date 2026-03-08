using System;
using Core.Models;
using Settings;
using UniRx;
using UnityEngine;
using VContainer.Unity;
using Views;
using Random = UnityEngine.Random;

namespace Presenters
{
    public class EnemyPresenter : IStartable, IDisposable
    {
        public readonly EnemyModel EnemyModel;
        public readonly EnemyView EnemyView;
        private readonly EnemiesConfig _enemiesConfig;
        
        private Vector3[] _patrolPoints;
        private int _currentIndex;
        private readonly CompositeDisposable _disposables = new();

        public EnemyPresenter(EnemyModel enemyModel, EnemyView enemyView, EnemiesConfig enemiesConfig)
        {

            EnemyModel = enemyModel;
            EnemyView = enemyView;
            _enemiesConfig = enemiesConfig;
        }

        public void Start()
        {
            InitPatrolPoints();
            SetupPatrolLoop();
        }

        public void Respawn(Vector3 position)
        {
            
        }

        private void InitPatrolPoints()
        {
            float half = _enemiesConfig.GridSize / 2f;
            _patrolPoints = new[]
            {
                new Vector3(Random.Range(-half, half), 0f, Random.Range(-half, half)),
                new Vector3(Random.Range(-half, half), 0f, Random.Range(-half, half)),
            };
        }
        
        private void SetupPatrolLoop()
        {
            EnemyView.SetDestination(_patrolPoints[0]);

            EnemyView.OnArrived
                .Delay(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    _currentIndex = (_currentIndex + 1) % _patrolPoints.Length;
                    EnemyView.SetDestination(_patrolPoints[_currentIndex]);
                })
                .AddTo(_disposables);
        }
        
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}