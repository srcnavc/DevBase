using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndlessMapBase : MonoBehaviour
{

    public static EndlessMapBase ins;
    public static Action<string> OnActiveZoneChanged;
    public int howManyTimesMapMoved = 0;

    [SerializeField] string activeZoneId;
    [SerializeField] NavMeshSurface surface;
    [SerializeField] int currentLevel;
    [SerializeField] List<GameObject> inSceneObjects = new List<GameObject>();
    [SerializeField] List<GameObject> levels = new List<GameObject>();
    [SerializeField] MapComponents activeZone;
    GameObject tempGo;
    GameObject currentZone;
    private void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("LevelData", 0);

        if (ins == null)
            ins = this;

        InitializeMap();
    }

    private void Start()
    {
        
    }
    
    public string ActiveZoneId
    {
        get => activeZoneId;
        set
        {
            if (!value.Equals(activeZoneId))
            {
                activeZoneId = value;
                OnActiveZoneChanged?.Invoke(activeZoneId);
            }
        }
    }

    public MapComponents ActiveZone { get => activeZone; set => activeZone = value; }

    /*private Vector3 NextLocation(MapComponents comp1, MapComponents comp2)
    {
        tempFloat = comp1.transform.position.z
            + comp1.Renderer.bounds.extents.z
            + comp2.Renderer.bounds.extents.z;

        return new Vector3(0f, 0f, tempFloat);
    }*/

    public void InitializeMap()
    {
        inSceneObjects.Clear();
        
        if (levels.Count <= 0)
        {
            Debug.LogError("Level list is empty.");
            return;
        }

        tempGo = Instantiate(levels[currentLevel % levels.Count]);
        tempGo.name = levels[currentLevel % levels.Count].name;
        tempGo.GetComponent<MapComponents>().ZoneId = tempGo.name;
        tempGo.transform.position = Vector3.zero;

        PlayerPrefs.SetInt("LevelData", currentLevel);

        inSceneObjects.Add(tempGo);

        SetCurrentZone(inSceneObjects[0]);
        surface.BuildNavMesh();

    }

    public void NextZone()
    {
        //SetCurrentZone(inSceneObjects[inSceneObjects.IndexOf(currentZone) + 1]);
    }

    public MapComponents GetNextZone()
    {
        return levels[(levels.IndexOf(currentZone) + 1) % levels.Count].GetComponent<MapComponents>();
    }
    public void UpdateNavmesh()
    {
        surface.UpdateNavMesh(surface.navMeshData);
    }

    public void BuildNavMesh()
    {
        surface.BuildNavMesh();
    }

    private void SetCurrentZone(GameObject go)
    {
        currentZone = go;
        ActiveZone = currentZone.GetComponent<MapComponents>();
        ActiveZoneId = ActiveZone.ZoneId;
    }

    public void DeletePrevious()
    {
        tempGo = inSceneObjects[0];
        inSceneObjects.Remove(tempGo);
        tempGo.SetActive(false);
        Destroy(tempGo);
        
        howManyTimesMapMoved++;
        //UpdateNavmesh();
    }

    public GameObject LoadLevel()
    {
        if (levels.Count > 0)
        {
            currentLevel++;
            tempGo = Instantiate(levels[currentLevel % levels.Count]);
            tempGo.name = levels[currentLevel % levels.Count].name;
            tempGo.GetComponent<MapComponents>().ZoneId = tempGo.name;

            tempGo.transform.position = ActiveZone.SpawnPoint.position;
            UpdateNavmesh(); 
            PlayerPrefs.SetInt("LevelData", currentLevel);
        }
        else
        {
            Debug.LogError("Level list is empty.");
            return null;
        }

        inSceneObjects.Add(tempGo);

        tempGo.GetComponent<MapComponents>().dynamicMapPosition = activeZone.SpawnPoint; ;
        SetCurrentZone(inSceneObjects[(inSceneObjects.IndexOf(currentZone) + 1)]);
        
        return tempGo;
    }
}
