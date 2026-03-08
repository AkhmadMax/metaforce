using System;

namespace Core.Interfaces
{
    public interface IScoreService
    {
        event Action<int> ScoreChanged;
        
        int CurrentScore { get; }
        void AddScore();
        void SubtractScore();
    }
}