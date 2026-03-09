using System;
using System.Collections;
using UniRx;
using UnityEngine;

using Core.Interfaces;

namespace Views
{
    public class EnemyView : MonoBehaviour, ITargetable
    {
        private Coroutine _moveRoutine;
        private readonly Subject<Unit> _onArrived = new();
        public IObservable<Unit> OnArrived => _onArrived;
        
        public Transform Transform => transform;
        

        public void SetDestination(Vector3 destination)
        {
            Stop();
            _moveRoutine = StartCoroutine(MoveTowards(destination));
        }

        private IEnumerator MoveTowards(Vector3 destination)
        {
            
            while(Vector3.Distance(Transform.position, destination) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime);
                yield return null;
            }
            _onArrived.OnNext(Unit.Default);

        }

        public void Stop()
        {
            if(_moveRoutine != null) StopCoroutine(_moveRoutine);
        }

        private void OnDestroy()
        {
            _onArrived.Dispose();
        }
    }
}