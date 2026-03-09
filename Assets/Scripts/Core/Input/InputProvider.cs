using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Metaforce.Core
{
    public class InputProvider : IInputProvider, IStartable, IDisposable
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

            _inputActions.Move.performed += OnMovePerformed;
            _inputActions.Move.canceled += OnMoveCanceled;
        }

        public void Dispose()
        {
            _inputActions.Move.performed -= OnMovePerformed;
            _inputActions.Move.canceled -= OnMoveCanceled;
            _moveSubject.Dispose();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _moveSubject.OnNext(context.ReadValue<Vector2>());
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _moveSubject.OnNext(Vector2.zero);
        }
    }
}