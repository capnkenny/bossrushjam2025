using UnityEngine;

public class Boss_Jump : MonoBehaviour
{
    public delegate void AnimEndFunc();

    [SerializeField] public Transform attackOffset;
    public float attackRange = 1f;
    public float jumpTime = 2f;
    public float internalTimer = 0.0f;
    private float startTime;
    private Vector3 origPos;
    public AnimEndFunc endFunction;
    public AnimEndFunc startFunction;

    public void Attack()
    {
        if(internalTimer == 0.0f)
        {
            startTime = Time.time;
            origPos = transform.position;
            if(startFunction != null) startFunction();
        }

        internalTimer += Time.deltaTime;
        internalTimer = internalTimer % jumpTime;
        
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = (transform.position + attackOffset.position) * 0.5F;
        //center -= new Vector3(0, 1, 0);
        Vector3 riseRelCenter = origPos - center;
        Vector3 setRelCenter = attackOffset.position - center;
        var pos = Vector3.Slerp(riseRelCenter, setRelCenter, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
