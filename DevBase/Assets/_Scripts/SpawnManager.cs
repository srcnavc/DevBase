using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager ins;
    public List<Spawner> spawners = new List<Spawner>();
    private bool isEnabled;
    [SerializeField] GameObject prefab;
    [SerializeField] float delay;
    [SerializeField] int spawnLimit;
    float timer = 0f;
    private static List<GameObject> EnemyList = new List<GameObject>();
    GameObject tempGo;
    bool isCalledFirstTime = false;
    Spawner tempSpawner;
    public bool IsEnabled { get => isEnabled; set => isEnabled = value; }

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    private void Start()
    {
        spawners.AddRange(FindObjectsOfType<Spawner>(true));
    }

    // Update is called once per frame
    void Update()
    {
        if(IsEnabled)//if (GameStateManager.GetState() == GameState.play)
        {
            if (EnemyList.Count < spawnLimit && timer + delay <= Time.time)
                SpawnWithDelay();
        }
    }
    
    private void LateUpdate()
    {
        if (!isCalledFirstTime)
            isCalledFirstTime = true;

        if (Input.GetKeyDown(KeyCode.K))
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    private void SpawnWithDelay(bool fromPool = false)
    {
        timer = Time.time;
        tempSpawner = spawners[Random.Range(0, spawners.Count)];

        while(!tempSpawner.IsEnabled)
            tempSpawner = spawners[Random.Range(0, spawners.Count)];

        if(fromPool)
            tempGo = tempSpawner.SpawnFromPool(tempSpawner.transform);
        else
            tempGo = tempSpawner.Spawn();
        
        tempGo.name = tempGo.name + "_" + Time.time;
        EnemyList.Add(tempGo);
    }

    public static void RemoveFromActiveEnemyList(GameObject go)
    {
        if(EnemyList.Contains(go))
            EnemyList.Remove(go);
    }
}
