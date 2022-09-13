using System;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float rayLenght = 2.5f; 
    [SerializeField] float radius = 2.5f; 
    [SerializeField] float distanceToConsideredAsOnGround = 2.5f; 
    [SerializeField] bool onnGround;
    [SerializeField] bool gizmosOn = false;

    public static Action<bool> OnGround;
    public UnityEvent onGround;
    public UnityEvent onAir;
    
    public bool IsGrounded => onnGround;
    RaycastHit[] hits;
    
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        hits = Physics.SphereCastAll(transform.position, radius, -transform.up, rayLenght, layerMask);
        
        if (hits.Length > 0 && hits[0].distance <= distanceToConsideredAsOnGround)
        {
            // on Ground
            if (!onnGround)
                SwitchOnGround();
        }
        else
        {
            // On Air
            if (onnGround)
                SwitchOnGround();
        }
    }

    private void SwitchOnGround()
    {
        onnGround = !onnGround;
        OnGround?.Invoke(onnGround);

        if (onnGround && GameStateManager.GetState() == GameState.play)
            onGround?.Invoke();
        else if(GameStateManager.GetState() == GameState.play)
            onAir?.Invoke();
    }

    private void OnDrawGizmos()
    {
        if (gizmosOn)
        {
            Gizmos.color = Color.blue;
            Ray ray = new Ray(transform.position, (rayLenght * -transform.up));
            Gizmos.DrawRay(ray);
            Gizmos.DrawSphere(transform.position - (rayLenght * transform.up), radius);
        }
    }
}
