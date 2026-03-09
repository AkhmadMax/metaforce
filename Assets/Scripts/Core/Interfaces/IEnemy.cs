namespace Core.Interfaces
{
    public interface IEnemy
    {
        ITargetable Targetable { get; }
        IDamageable Damageable { get; }
    }
}