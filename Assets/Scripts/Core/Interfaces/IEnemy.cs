namespace Metaforce.Core
{
    public interface IEnemy
    {
        ITargetable Targetable { get; }
        IDamageable Damageable { get; }
    }
}