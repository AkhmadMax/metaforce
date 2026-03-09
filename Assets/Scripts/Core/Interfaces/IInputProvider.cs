using System;
using UnityEngine;

namespace Core.Interfaces
{
    public interface IInputProvider
    {
        IObservable<Vector2> OnMove { get; }
    }
}