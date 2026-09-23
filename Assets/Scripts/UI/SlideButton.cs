using UnityEngine;

public class SlideButton : MonoBehaviour
{
   [SerializeField]
   private SlideUIArea.SlideType type;

   public void ChangeSlide()
   {
      SlideUIArea.ChangeSlid(type);
   }
}
