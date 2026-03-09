using System;
using UniRx;

namespace Metaforce.Core
{
    public class ScoreService : IScoreService
    {
        public IReadOnlyReactiveProperty<int> Score => _score;
        private readonly IReactiveProperty<int> _score = new IntReactiveProperty(0);
        
        public void AddScore()
        {
            _score.Value++;
        }
    }
}
