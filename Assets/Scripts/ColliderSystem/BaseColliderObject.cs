using UnityEngine;

public abstract class BaseColliderObject : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioClip;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (ColliderController.Instance.CheckColliderHash(collision.collider) == false) return;

        if (_audioClip != null)
        {
            AudioController.Instance.TryPlayAudio(_audioClip);
        }
        
        ColliderAction();
    }

    protected abstract void ColliderAction();
}
