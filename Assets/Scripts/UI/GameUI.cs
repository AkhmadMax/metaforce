using Metaforce.Core;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

public class GameUI : MonoBehaviour
{
    [Inject] private IScoreService _scoreService;
    
    public TMP_Text scoreText;

    private void Start()
    {
        _scoreService.Score
            .Subscribe(score => scoreText.text = score.ToString())
            .AddTo(this);
    }
}
