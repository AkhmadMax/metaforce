using UniRx;

namespace Core.Interfaces
{
    public interface IDamageable
    {
        IReadOnlyReactiveProperty<bool> IsDead { get; }
        void TakeDamage(int damage);
    }
}