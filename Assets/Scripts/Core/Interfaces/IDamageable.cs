using UniRx;

namespace Metaforce.Core
{
    public interface IDamageable
    {
        IReadOnlyReactiveProperty<bool> IsDead { get; }
        void TakeDamage(int damage);
    }
}