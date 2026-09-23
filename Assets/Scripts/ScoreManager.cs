using System;
using UnityEngine;

public class ScoreManager : BaseSingleton<ScoreManager>
{
    public static event Action<float> OnScoreChangedNewValue = delegate(float f) {  }; 
    public static event Action<float> OnScoreChanged = delegate(float f) {  };

    public float CurrentScore { private set; get; }

    private void Start()
    {
        CurrentScore = 0;
    }

    public void AddScore(float score)
    {
        CurrentScore =Mathf.Max(0, CurrentScore+score);
        OnScoreChangedNewValue.Invoke(CurrentScore);
        OnScoreChanged.Invoke(score);
    }
}
