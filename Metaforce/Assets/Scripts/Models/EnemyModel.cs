using UniRx;
using UnityEngine;

namespace Core.Models
{
    public class EnemyModel
    {
        private ReactiveProperty<int> CurrentHp { get; }
        public IReadOnlyReactiveProperty<bool> IsDead { get; }


        public EnemyModel(int hp)
        {
            CurrentHp = new ReactiveProperty<int>(hp);
            IsDead = CurrentHp.Select(hp => hp <= 0).ToReactiveProperty();

        }
        
        public void TakeDamage(int damage)
        {
            CurrentHp.Value = Mathf.Max(0, CurrentHp.Value - damage);
        }
    }
}