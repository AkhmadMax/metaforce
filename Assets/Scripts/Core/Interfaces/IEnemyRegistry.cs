using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IEnemyRegistry
    {
        IReadOnlyList<IEnemy> ActiveEnemies { get; }
    }
}