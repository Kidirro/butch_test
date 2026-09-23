using UnityEngine;

public class AnimatorTriggerCollider : BaseColliderObject
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private string _trigger;
    
    protected override void ColliderAction()
    {
        _animator.SetTrigger(_trigger);
    }
}
