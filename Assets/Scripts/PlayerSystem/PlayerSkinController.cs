using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinController : MonoBehaviour
{
    [SerializeField]
    private List<SkinScoreData> _skinDatas;

    private SkinScoreData _nextData;
    private SkinScoreData _currentData;

    private void Start()
    {
        ScoreManager.OnScoreChangedNewValue -= OnScoreChangedNewValue; 
        ScoreManager.OnScoreChangedNewValue += OnScoreChangedNewValue;
        _currentData = _skinDatas[0];
        
        OnScoreChangedNewValue(ScoreManager.Instance.CurrentScore);
    }

    private void OnDestroy()
    {
        ScoreManager.OnScoreChangedNewValue -= OnScoreChangedNewValue;
    }

    private void OnScoreChangedNewValue(float score)
    {
        if (score < _nextData.ScoreGoal && score >= _currentData.ScoreGoal) return;
        
        _currentData.Skin.SetActive(false);
        
        for (int i =0; i<= _skinDatas.Count; i++)
        {
            if (score >= _skinDatas[i].ScoreGoal) continue;

            _nextData = _skinDatas[i];
            _currentData = _skinDatas[i - 1];
            _currentData.Skin.SetActive(true);
            break;
        }
    }
    
    [Serializable]
    public struct SkinScoreData
    {
        public float ScoreGoal;
        public GameObject Skin;
    }
}
