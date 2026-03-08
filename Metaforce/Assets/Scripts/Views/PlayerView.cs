using System;
using UnityEngine;

namespace Core
{
    public class PlayerView :  MonoBehaviour
    {
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector2 delta, float speed)
        {
            _characterController.Move(new Vector3(delta.x, 0, delta.y) *  speed);
        }
    }
}