using UniRx;
using UnityEngine;

namespace Metaforce.Player
{
    public class PlayerModel
    {
        public ReactiveProperty<bool> IsMoving { get; } = new(false);
    }
}