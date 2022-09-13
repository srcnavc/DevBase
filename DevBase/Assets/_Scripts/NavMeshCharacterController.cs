using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshCharacterController : MovementBase
{
    [SerializeField] float speed;
    [SerializeField] bool isRotationActive;
    [SerializeField] float rotation = 1f;
    NavMeshAgent agent;
    Vector3 directionVector;
    IinputBridge input;
    
    public float Speed { get => speed; set => speed = value; } 
    public bool IsRotationActive { get => isRotationActive; set => isRotationActive = value; }
    public float Rotation { get => rotation; set => rotation = value; }

    public override void SetDestination(Vector3 position)
    {
        if (agent != null)
            agent.SetDestination(position);
    }

    private void Awake() => agent = GetComponent<NavMeshAgent>();

    private void Start() => input = FindObjectsOfType<MonoBehaviour>().OfType<IinputBridge>().First();

    private void Update()
    {
        if (input.moveInput.magnitude == 0)
            return;
        
        directionVector.x = input.moveInput.x;
        directionVector.z = input.moveInput.y;
        directionVector.Normalize();
        
        agent.Move(Speed * Time.deltaTime * directionVector);

        // Rotation --------------------------------------------------------------------------->
        if (IsRotationActive)
            transform.rotation = Quaternion.LookRotation(directionVector * Rotation);// Quaternion.Lerp(transform.rotation, Quaternion.Euler(directionVector),rotationSpeed);
        // Rotation --------------------------------------------------------------------------->
    }
}
