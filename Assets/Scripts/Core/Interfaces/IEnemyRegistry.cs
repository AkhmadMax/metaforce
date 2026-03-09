using System.Collections.Generic;

namespace Metaforce.Core
{
    public interface IEnemyRegistry
    {
        IReadOnlyList<IEnemy> AliveEnemies { get; }
    }
}