using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
   [SerializeField]
   private Animator _animator;

   private readonly int onStartHash = Animator.StringToHash("OnStart");
   private readonly int onEndHash = Animator.StringToHash("OnEnd");

   private void Start()
   {
      PlayerMoveController.OnStartMove -= OnStartMove;
      PlayerMoveController.OnEndMove -= OnEndMove;
      
      PlayerMoveController.OnStartMove += OnStartMove;
      PlayerMoveController.OnEndMove += OnEndMove;
   }

   private void OnDestroy()
   {
      PlayerMoveController.OnStartMove -= OnStartMove;
      PlayerMoveController.OnEndMove -= OnEndMove;
   }

   private void OnStartMove()
   {
      _animator.SetBool(onStartHash, true);
   }

   private void OnEndMove()
   {
      _animator.SetBool(onEndHash, true);
   }
}
