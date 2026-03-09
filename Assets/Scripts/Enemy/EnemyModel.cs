using System;
using Metaforce.Core;
using UniRx;
using UnityEngine;

namespace Metaforce.Enemy
{
    public class EnemyModel : IDamageable
    {
        public IReadOnlyReactiveProperty<int> CurrentHp => _currentHp;
        public IReadOnlyReactiveProperty<bool> IsDead { get; }
        
        private IReactiveProperty<int> _currentHp;


        public EnemyModel(int hp)
        {
            _currentHp = new ReactiveProperty<int>(hp);
            IsDead = CurrentHp.Select(value => value <= 0).ToReactiveProperty();

        }
        
        public void TakeDamage(int damage)
        {
            _currentHp.Value = Mathf.Max(0, _currentHp.Value - damage);
        }

        public void Reset(int health)
        {
            _currentHp.Value = health;
        }
    }
}