using UniRx;
using UnityEngine;

namespace Core.Models
{
    public class PlayerModel
    {
        public ReactiveProperty<bool> _isMoving = new ReactiveProperty<bool>();
        public ReactiveProperty<int> _frags = new ReactiveProperty<int>();
    }
}