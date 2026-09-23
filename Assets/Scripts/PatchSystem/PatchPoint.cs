using UnityEngine;

public class PatchPoint : MonoBehaviour
{
    [SerializeField]
    private PatchPoint nextPoint;

    [Min(0),SerializeField]
    private float width;
    
    public float Distance { private set; get; }
    public float Angle { private set; get; }
    public float Width => width;
    public PatchPoint NextPoint => nextPoint;


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Distance = nextPoint ? (nextPoint.transform.position - transform.position).magnitude : 0;
        Angle = nextPoint ? Vector3.SignedAngle(nextPoint.transform.position, transform.forward, Vector3.up) : 0; 

        var targetDistance =  transform.forward * Distance;
        Gizmos.DrawLine(transform.position,transform.position +targetDistance);
        
        var leftPoint = transform.position - transform.right * width;
        Gizmos.DrawLine(leftPoint, leftPoint + targetDistance);
        
        var rightPoint = transform.position + transform.right * width;
        Gizmos.DrawLine(rightPoint, rightPoint + targetDistance);
        
        Gizmos.DrawSphere(transform.position,0.1f);
    }
#endif
}