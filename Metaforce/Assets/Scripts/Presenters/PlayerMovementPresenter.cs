using System;
using Core;
using Core.Interfaces;
using Core.Models;
using Settings;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Presenters
{
    public class PlayerMovementPresenter : IStartable, ITickable, IDisposable
    {
        private readonly CompositeDisposable _disposables = new();

        private readonly IInputProvider _inputProvider;
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly PlayerConfig _playerConfig;

        private Vector2 _currentDelta;
        
        public PlayerMovementPresenter(IInputProvider inputProvider, PlayerModel playerModel, PlayerView playerView, PlayerConfig  playerConfig)
        {
            _inputProvider = inputProvider;
            _playerModel = playerModel;
            _playerView = playerView;
            _playerConfig = playerConfig;
        }
        
        public void Start()
        {
            _inputProvider.OnMove
                .Subscribe(Move)
                .AddTo(_disposables);        
        }
        
        public void Move(Vector2 delta)
        {
            _currentDelta = delta;
            _playerModel._isMoving.Value = delta.sqrMagnitude > 0.01f;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void Tick()
        {
            if (_currentDelta.sqrMagnitude > 0.01f)
            {
                _playerView.Move(_currentDelta, _playerConfig.Speed);

            }
        }
    }
}