using System;
using UnityEngine;

public class InputController : BaseSingleton<InputController>
{
    public static event Action<float> OnHorizontalDrag = delegate(float f) {  };

    private bool _isDrag;
    private Vector2 _previousPoint; 
    
    
    private void Update()
    {
        if (IsStartDrag()) return;
        if (IsEndDrag()) return;
        if (!_isDrag) return;
        
        var currentPoint = Input.mousePosition;
        OnHorizontalDrag.Invoke(currentPoint.x - _previousPoint.x);
        _previousPoint = currentPoint;
    }

    private bool IsEndDrag()
    {
        if (!Input.GetMouseButtonUp(0) || _isDrag == false) return false;
        _isDrag = false;
        return true;

    }

    private bool IsStartDrag()
    {
        if (!Input.GetMouseButtonDown(0) || _isDrag) return false;
        _isDrag = true;
        _previousPoint = Input.mousePosition;
        return true;

    }
}
