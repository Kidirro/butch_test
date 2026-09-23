using UnityEngine;

public class ScoreCollider : BaseColliderObject
{
    [SerializeField]
    private float _score;
    protected override void ColliderAction()
    {
        ScoreManager.Instance.AddScore(_score);
        Destroy(gameObject);
    }
}
