using System;
using Core.Interfaces;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace Core
{
    public class InputProvider : IInputProvider, IStartable
    {
        private readonly Subject<Vector2> _moveSubject = new();
        private readonly InputActionsReference _inputActions;

        public IObservable<Vector2> OnMove => _moveSubject;

        public InputProvider(InputActionsReference actionsReference)
        {
            _inputActions = actionsReference;
        }

        public void Start()
        {
            _inputActions.EnablePlayerMap();
            
            _inputActions.Move.performed += context => _moveSubject.OnNext(context.ReadValue<Vector2>());
            _inputActions.Move.canceled += _ => _moveSubject.OnNext(Vector2.zero);
        }
    }
}