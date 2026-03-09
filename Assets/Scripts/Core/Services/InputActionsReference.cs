using UnityEngine.InputSystem;

namespace Core
{
    public class InputActionsReference
    {
        private readonly InputActionMap _playerMap;

        public InputAction Move { get; }

        public InputActionsReference(InputActionAsset asset)
        {
            _playerMap = asset.FindActionMap("Player");
            Move = _playerMap.FindAction("Movement");
        }

        public void EnablePlayerMap() => _playerMap.Enable();
        public void DisablePlayerMap() => _playerMap.Disable();
    }
}