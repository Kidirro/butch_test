using System;
using UnityEngine;

public class SlideUIArea : MonoBehaviour
{

    [SerializeField]
    private SlideType currentType;

    private static event Action<SlideType> OnSlideChanged = delegate(SlideType type) {  }; 

    private void Start()
    {
        PlayerMoveController.OnStartMove -= OnStartMove;
        PlayerMoveController.OnEndMove -= OnEndMove;
        
        PlayerMoveController.OnStartMove += OnStartMove;
        PlayerMoveController.OnEndMove += OnEndMove;
        
        OnSlideChanged += ChangeSlide;
        
        gameObject.SetActive(currentType == SlideType.Tutorial);
    }

    private void OnDestroy()
    {
        PlayerMoveController.OnStartMove -= OnStartMove;
        PlayerMoveController.OnEndMove -= OnEndMove;
    }

    private void OnEndMove()
    {
        gameObject.SetActive(currentType==SlideType.Finish);
    }

    private void OnStartMove()
    {
        gameObject.SetActive(currentType==SlideType.Gameplay);
    }

    private void ChangeSlide(SlideType type)
    {
        gameObject.SetActive(type == currentType);
    }
    
    public static void ChangeSlid(SlideType type)
    {
        OnSlideChanged.Invoke(type);
    }

    public enum SlideType
    {
        Tutorial,
        Gameplay,
        Finish,
        LevelProgress
    }
}
