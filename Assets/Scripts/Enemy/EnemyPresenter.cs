using System;
using Metaforce.Core;
using UniRx;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Metaforce.Enemy
{
    public class EnemyPresenter : IStartable, IDisposable, IEnemy
    {
        private readonly EnemyModel _enemyModel;
        private readonly EnemyView _enemyView;

        public IDamageable Damageable => _enemyModel;
        public ITargetable Targetable => _enemyView;
        private readonly EnemiesConfig _enemiesConfig;
        
        private Vector3[] _patrolPoints;
        private int _currentIndex;
        private readonly CompositeDisposable _disposables = new();

        public EnemyPresenter(EnemyModel enemyModel, EnemyView enemyView, EnemiesConfig enemiesConfig)
        {

            _enemyModel = enemyModel;
            _enemyView = enemyView;
            _enemiesConfig = enemiesConfig;
        }

        public void Start()
        {
            InitPatrolPoints();
            SetupPatrolLoop();
            SetupDeathHandler();
            BeginPatrol();
        }

        private void SetupDeathHandler()
        {
            _enemyModel.IsDead
                .Where(dead => dead)
                .Subscribe(_ =>
                {
                    _enemyView.Stop();
                    _enemyView.gameObject.SetActive(false);
                })
                .AddTo(_disposables);
        }

        public void Respawn(Vector3 position)
        {
            _enemyView.transform.position = position;
            _enemyView.gameObject.SetActive(true);
            _enemyModel.Reset(_enemiesConfig.Health);
            _currentIndex = 0;
            InitPatrolPoints();
            BeginPatrol();
        }
        
        private void BeginPatrol()
        {
            _enemyView.SetDestination(_patrolPoints[0]);
        }

        private void InitPatrolPoints()
        {
            var origin = _enemyView.Transform.position;
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
            _enemyView.OnArrived
                .Subscribe(_ =>
                {
                    _currentIndex = (_currentIndex + 1) % _patrolPoints.Length;
                    _enemyView.SetDestination(_patrolPoints[_currentIndex]);
                })
                .AddTo(_disposables);
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}