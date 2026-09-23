using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiColliderObject : BaseColliderObject
{
    [SerializeField]
    private List<GameObject> _destroyList;

    [SerializeField]
    private float _score;
    protected override void ColliderAction()
    {
        ScoreManager.Instance.AddScore(_score);
        foreach (var tempObject in _destroyList)
        {
            Destroy(tempObject);
        }
        Destroy(gameObject);
    }
}