using UniRx;
using UnityEngine;

namespace Metaforce.Player
{
    public class PlayerModel
    {
        public ReactiveProperty<bool> IsMoving { get; } = new(false);
        public ReactiveProperty<int> KillCount { get; } = new(0);

        public void RegisterKill()
        {
            KillCount.Value++;
        }
    }
}