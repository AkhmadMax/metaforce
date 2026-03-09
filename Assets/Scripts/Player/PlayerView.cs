using System;
using UnityEngine;

namespace Metaforce.Player
{
    public class PlayerView :  MonoBehaviour
    {
        [SerializeField] private LineRenderer _laser;

        public Transform Transform => transform;
        private CharacterController _characterController;
        private Transform _laserTarget;


        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector2 delta, float speed)
        {
            _characterController.Move(new Vector3(delta.x, 0, delta.y) *  speed * Time.deltaTime);
        }
        
        public void ShowLaser(Transform target)
        {
            _laserTarget = target;
            _laser.enabled = true;
        }

        public void HideLaser()
        {
            _laserTarget = null;
            _laser.enabled = false;
        }

        private void LateUpdate()
        {
            if (_laserTarget == null) return;

            _laser.SetPosition(0, transform.position);
            _laser.SetPosition(1, _laserTarget.position);
        }
    }
}