using System;
using UniRx;

namespace Metaforce.Core
{
    public interface IScoreService
    {
        IReadOnlyReactiveProperty<int> Score { get; }
        public void AddScore();

    }
}