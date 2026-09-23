using System.Collections;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PickableScoreText : MonoBehaviour
{
    [SerializeField]
    private Color positiveColor;

    [SerializeField]
    private Color negativeColor;
    
    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private float delayBeforeAnimation;
    
    [SerializeField]
    private float alphaSpeed;

    [SerializeField]
    private float flySpeed;
    
    [SerializeField]
    private ParticleSystem positiveParticles;
    
    [SerializeField]
    private ParticleSystem negativeParticles;

    private float _scoreDisplay;
    private Coroutine _animationCoroutine;
    
    private void Start()
    {
        ScoreManager.OnScoreChanged -= OnScoreChangedNewValue; 
        ScoreManager.OnScoreChanged += OnScoreChangedNewValue; 
        
        text.text = ScoreManager.Instance.CurrentScore.ToString();
    }

    private void OnDestroy()
    {
        ScoreManager.OnScoreChanged -= OnScoreChangedNewValue;
    }

    private void OnScoreChangedNewValue(float score)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
            _animationCoroutine = null;

            if (_scoreDisplay < 0 && score > 0)
            {
                _scoreDisplay = 0;
            }
        }

        _scoreDisplay += score;
        _animationCoroutine = StartCoroutine(AnimationProcess());

    }

    private IEnumerator AnimationProcess()
    {
        text.text = "";
        if (_scoreDisplay > 0)
        {
            text.text = "+";
        }

        text.text += _scoreDisplay;

        if (_scoreDisplay > 0)
        {
            text.color = positiveColor;
            positiveParticles.Play();
        }
        else
        {
            text.color = negativeColor;
            negativeParticles.Play();
        }
        
        text.transform.localPosition = Vector3.zero;
        text.alpha = 1;

        yield return new WaitForSeconds(delayBeforeAnimation);

        while (text.alpha>0)
        {
            text.alpha -= Time.deltaTime * alphaSpeed;
            text.transform.localPosition += Vector3.up * Time.deltaTime * flySpeed;

            yield return null;
        }

        _scoreDisplay = 0;
    }
}
