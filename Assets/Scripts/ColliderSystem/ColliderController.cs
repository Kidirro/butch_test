using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : BaseSingleton<ColliderController>
{
    private int _playerHash;
    
    public void RegisterPlayerCollider(Collider playerCollider)
    {
        _playerHash = playerCollider.GetHashCode();
    }

    public bool CheckColliderHash(Collider collider)
    {
        return collider.GetHashCode() == _playerHash;
    }
}
