using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMobManager : MonoBehaviour, IDoDoorMath
{
    public static CharacterMobManager Ins;
    public Action<int> OnMobCountChanged;
    public static Action<GameObject> OnMobSpawn;
    [HideInInspector] public bool isPlayer;
    [SerializeField] Transform container;
    [SerializeField] PoolInfo mobPrefabInfo;
    public List<MobController> mobs;
    [SerializeField] int lineMultiplier;
    [SerializeField] int startMobCount;
    [SerializeField] CurrencySC relatedProperty;
    [SerializeField] Vector3 randomSpawnRange;
    [SerializeField] float lineDistance;
    [SerializeField] float mobMaximumNumber;
    [SerializeField] bool randomSpawn;
    
    int counter = 0;
    Vector3 tempRandom;
    int mobIndex;
    int mobLineNumber;
    Vector3 desiredPosition;

    public int GetValue => mobs.Count;

    public int LineMultiplier { get => lineMultiplier; set => lineMultiplier = value; }
    public float MobMaximumNumber { get => mobMaximumNumber; set => mobMaximumNumber = value; }
    public float LineDistance { get => lineDistance; set => lineDistance = value; }

    // For Door Math
    public void UseNewValue(int amount, DoMathType type)
    {
        switch (type)
        {
            case DoMathType.Addition:
                CreateMob(amount);
                break;
            case DoMathType.Subtraction:
                if(mobs.Count < amount)
                    amount = mobs.Count;
                
                for (int i = amount - 1; i >= 0; i--)
                {
                    if (mobs.Count > 0)
                        mobs[i].Kill(Vector3.zero);
                    else
                        break;
                }
                break;
            case DoMathType.Divisition:
                if (mobs.Count < 0)
                    return;
                
                int count = mobs.Count;
                for (int i = 0; i < count - (count / amount); i++)
                {
                    if (mobs.Count > 0)
                        mobs[count - i - 1].Kill(Vector3.zero);
                    else
                        break;
                }
                break;
            case DoMathType.Multiply:
                CreateMob((mobs.Count * amount) - mobs.Count);
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        if(transform.CompareTag("Player") && Ins == null)
            Ins = this;
    }

    void Start()
    {
        if (relatedProperty != null)
            CreateMob(relatedProperty.Value);
        else
            CreateMob(startMobCount);
    }

    void Update()
    {
        Test();
    }

    private void KillAll()
    {
        for (int i = 0; i < mobs.Count; i++)
            mobs[i].Kill(Vector3.zero);
    }
    
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CreateMob(50);
        if (Input.GetKey(KeyCode.K))
            KillAll();
    }

    public void AddMob(MobController mob)
    {
        mobs.Add(mob);
        OnMobCountChanged?.Invoke(GetValue);
    }

    public void RemoveMob(MobController mob)
    {
        mobs.Remove(mob);
        OnMobCountChanged?.Invoke(GetValue);
    }

    public void RemoveAllMob()
    {
        KillAll();
    }

    public Vector3 GetMobDesiredPosition(MobController mob)
    {
        mobIndex = mobs.IndexOf(mob);
      
        mobLineNumber = 1;
        while (mobIndex > 0)
        {
            if (mobIndex > mobLineNumber * LineMultiplier)
            {
                mobIndex -= mobLineNumber * LineMultiplier;
                mobLineNumber++;
            }
            else break;

        }

        desiredPosition = LineDistance * mobLineNumber * Vector3.forward;
        desiredPosition = Quaternion.Euler(0, (360 * mobIndex) / (mobLineNumber * LineMultiplier), 0) * desiredPosition;
        
        return desiredPosition ;
    }


    public IEnumerator CreateMobCoroutine(int count)
    {
        if (mobs.Count >= MobMaximumNumber)
            yield break;

        counter = 0;

        while (counter < count)
        {
            if (mobs.Count < MobMaximumNumber)
            {
                GameObject TempMob = PoolManager.Fetch(mobPrefabInfo.PoolName);

                TempMob.transform.parent = container;
                TempMob.SetActive(true);
                TempMob.GetComponent<MobController>().IsDeath = false;
                
                AddMob(TempMob.GetComponent<MobController>());
                TempMob.GetComponent<MobController>().Master = this;
                TempMob.transform.position = container.position;

                OnMobSpawn?.Invoke(TempMob);

                counter++;

                yield return new WaitForSeconds(0.01f);
            }
            else
                StopCoroutine(CreateMobCoroutine(count));
        }
    }

    public void CreateMob(int count)
    {
        if (count > 10)
        {
            StartCoroutine(CreateMobCoroutine(count));
            return;
        }

        if (mobs.Count >= MobMaximumNumber)
            return;

        for (int i = 0; i < count; i++)
        {
            if (mobs.Count < MobMaximumNumber)
            {
                GameObject TempMob = PoolManager.Fetch(mobPrefabInfo.PoolName);

                TempMob.transform.parent = container;
                TempMob.SetActive(true);
                TempMob.GetComponent<MobController>().IsDeath = false;
                
                AddMob(TempMob.GetComponent<MobController>());
                TempMob.GetComponent<MobController>().Master = this;

                TempMob.transform.position = container.position;

                OnMobSpawn?.Invoke(TempMob);
            }
            else
                break;
        }
    }
    
    private Vector3 GetRandomPositionInCircle()
    {
        tempRandom = UnityEngine.Random.insideUnitSphere;
        
        return new Vector3(tempRandom.x * randomSpawnRange.x, tempRandom.y * randomSpawnRange.y, tempRandom.z * randomSpawnRange.z);
    }
}
public enum CharacterStatus
{ 
ready = 0 , 
dead = 1, 
finished = 2
}
