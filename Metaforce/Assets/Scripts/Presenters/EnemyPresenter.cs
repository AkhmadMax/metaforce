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
            var origin = EnemyView.Transform.position;
            var horizontal = Random.value > 0.5f;
            var distance = Random.Range(_enemiesConfig.MinPatrolDistance, _enemiesConfig.MaxPatrolDistance);
            var halfGrid = _enemiesConfig.GridSize / 2f;

            var offset = horizontal
                ? new Vector3(distance, 0f, 0f)
                : new Vector3(0f, 0f, distance);

            var pointA = origin + offset;
            var pointB = origin - offset;

            pointA.x = Mathf.Clamp(pointA.x, -halfGrid, halfGrid);
            pointA.z = Mathf.Clamp(pointA.z, -halfGrid, halfGrid);
            pointB.x = Mathf.Clamp(pointB.x, -halfGrid, halfGrid);
            pointB.z = Mathf.Clamp(pointB.z, -halfGrid, halfGrid);

            if (Random.value > 0.5f)
            {
                // Third point: turn the corner
                var perpDistance = Random.Range(_enemiesConfig.MinPatrolDistance, _enemiesConfig.MaxPatrolDistance);
                var perpOffset = horizontal
                    ? new Vector3(0f, 0f, perpDistance)
                    : new Vector3(perpDistance, 0f, 0f);

                var pointC = pointB + perpOffset;
                pointC.x = Mathf.Clamp(pointC.x, -halfGrid, halfGrid);
                pointC.z = Mathf.Clamp(pointC.z, -halfGrid, halfGrid);

                _patrolPoints = new[] { pointA, pointB, pointC };
            }
            else
            {
                _patrolPoints = new[] { pointA, pointB };
            }
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