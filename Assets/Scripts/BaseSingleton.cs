using UnityEngine;

public class BaseSingleton<T>: MonoBehaviour where T : BaseSingleton<T>
{ 
    public static T Instance { private set; get; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = (T) this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
