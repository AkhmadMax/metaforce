using System;
using UnityEngine;

namespace Metaforce.Core
{
    public interface IInputProvider
    {
        IObservable<Vector2> OnMove { get; }
    }
}