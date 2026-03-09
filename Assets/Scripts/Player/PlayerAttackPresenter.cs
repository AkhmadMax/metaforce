using System;
using Core;
using Core.Models;
using DefaultNamespace;
using Settings;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace Presenters
{
    public class PlayerAttackPresenter : IStartable, IDisposable
    {
        private readonly PlayerModel _model;
        private readonly PlayerConfig _config;
        private readonly EnemiesFinder _targetFinder;
        private readonly PlayerView _view;

        private readonly CompositeDisposable _disposables = new();

        public PlayerAttackPresenter(PlayerModel model, PlayerConfig config, EnemiesFinder targetFinder, PlayerView view)
        {
            this._model = model;
            this._config = config;
            this._targetFinder = targetFinder;
            this._view = view;
        }

        public void Start()
        {
            _model.IsMoving
                .Where(moving => !moving)
                .SelectMany(_ =>
                    Observable.Interval(TimeSpan.FromSeconds(1f / _config.AttackSpeed))
                        .TakeUntil(_model.IsMoving.Where(m => m)))
                .Subscribe(_ =>
                {
                    var target = _targetFinder.GetClosest(_view.Transform, _config.AttackRadius);
                    if (target != null)
                    {
                        _view.ShowLaser(target.Targetable.Transform);
                        target.Damageable.TakeDamage(Mathf.CeilToInt(_config.AttackDamage));

                        if (target.Damageable.IsDead.Value)
                        {
                            _model.RegisterKill();
                            _view.HideLaser();
                        }
                    }
                    else
                    {
                        _view.HideLaser();
                    }
                })
                .AddTo(_disposables);

            _model.IsMoving
                .Where(moving => moving)
                .Subscribe(_ => _view.HideLaser())
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}