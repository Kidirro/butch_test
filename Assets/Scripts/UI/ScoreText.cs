using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    private void Start()
    {
        ScoreManager.OnScoreChangedNewValue -= OnScoreChangedNewValue; 
        ScoreManager.OnScoreChangedNewValue += OnScoreChangedNewValue; 
        
        text.text = ScoreManager.Instance.CurrentScore.ToString();
    }

    private void OnDestroy()
    {
        ScoreManager.OnScoreChangedNewValue -= OnScoreChangedNewValue;
    }

    private void OnScoreChangedNewValue(float score)
    {
        text.text = score.ToString();
    }
}
