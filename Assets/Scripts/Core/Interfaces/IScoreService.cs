using System;

namespace Metaforce.Core
{
    public interface IScoreService
    {
        event Action<int> ScoreChanged;
        
        int CurrentScore { get; }
        void AddScore();
        void SubtractScore();
    }
}