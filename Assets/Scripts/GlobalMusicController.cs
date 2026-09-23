using UnityEngine;

//При подключении музыки может понадобится
public class GlobalMusicController : MonoBehaviour
{
    [SerializeField]
    private AudioClip finishGameClip;
    
    
    private void Start()
    {
        PlayerMoveController.OnStartMove -= OnStartMove;
        PlayerMoveController.OnEndMove -= OnEndMove;
      
        PlayerMoveController.OnStartMove += OnStartMove;
        PlayerMoveController.OnEndMove += OnEndMove;
    }

    private void OnEndMove()
    {
        AudioController.Instance.TryPlayAudio(finishGameClip);
    }

    private void OnStartMove()
    {
        //TODO
    }

    private void OnDestroy()
    {
        PlayerMoveController.OnStartMove -= OnStartMove;
        PlayerMoveController.OnEndMove -= OnEndMove;
    }

}
