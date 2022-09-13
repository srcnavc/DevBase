using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MobController : MonoBehaviour
{
    public Action<CharacterMobManager> OnMasterChanged;
    public UnityEvent onSpawn;
    [SerializeField] float mobReactionTime = 0.5f;
    [SerializeField] float lastReaction;
    [SerializeField] MovementBase movement;
    [SerializeField] float mobReactionTimeMin;
    [SerializeField] float mobReactionTimeMax;
    private CharacterMobManager master;
    private bool isDeath = false;
    
    Vector3 destination;
    
    public bool IsDeath { get => isDeath; set => isDeath = value; }
    public CharacterMobManager Master
    {
        get => master; 
        set
        {
            master = value;
            OnMasterChanged?.Invoke(master);
        }
    }

    private void Awake()
    {
        CharacterMobManager.OnMobSpawn += OnSpawn;
    }

    private void OnDestroy()
    {
        CharacterMobManager.OnMobSpawn -= OnSpawn;
    }

    void Update()
    {
        if (IsDeath)
            return;
        if (lastReaction + mobReactionTime < Time.time)
        {
            mobReactionTime = UnityEngine.Random.Range(mobReactionTimeMin, mobReactionTimeMax);

            lastReaction = Time.time;

            destination = Master.transform.position + Master.GetMobDesiredPosition(this);

            if (!IsDeath)
                movement.SetDestination(destination);
        }
    }

    private void OnSpawn(GameObject go)
    {
        if (go != gameObject)
            return;

        onSpawn?.Invoke();
    }

    public void Kill(Vector3 hit)
    {
        IsDeath = true;
        Master.RemoveMob(this);
        transform.parent = null;
    }
}
