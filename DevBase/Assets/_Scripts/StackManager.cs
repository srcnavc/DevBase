using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public static StackManager ins;
    public Action OnStackIncrease;
    public StackDirection stackDirection;
    [SerializeField] Transform parent;
    [SerializeField] Vector3Int stackSize;
    [SerializeField] Vector3Int currentLocation;
    [SerializeField] Vector3 tileSize;
    [SerializeField] int currentStackSize;
    [SerializeField] PoolInfo stackPrefabInfo;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] bool dontUseCurrency;
    [SerializeField] Vector3 holderOffset;
    [SerializeField] protected List<GameObject> items = new List<GameObject>();
    [Tooltip("Press \"C\" to cheat.")]
    [SerializeField] int cheatCount = 100;
    [SerializeField] List<Transform> containers = new List<Transform>();
    public int CurrentStackCount => items.Count;
    Vector3 tempVec3;
    GameObject tempGo;
    IStackItem tempStackItem;
    int itemIndex = 0;
    

    public enum StackDirection
    {
        X,
        Y,
        Z
    }

    
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Base Stack");
        CreateContainers();

        currentLocation = Vector3Int.zero;

    }

    private void CreateContainers()
    {
        for (int i = 0; i < stackSize.x; i++)
        {
            tempGo = new GameObject();//new GameObject("Stack Container - " + (i + 1));
            
            tempGo.transform.parent = parent;
            tempVec3 = Vector3.zero;
            tempVec3.x += (i * tileSize.x);
            
            tempGo.transform.localPosition = tempVec3;
            containers.Add(tempGo.transform);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < cheatCount; i++)
            {
                AddItemWithScaling(transform.position, Vector3.one, Vector3.one);

            }
        }
    }

    private void OnCurrencyChangedValue(int count)
    {
        /*if (items.Count > count)
        {
            tempInt = items.Count - count;
            for (int i = tempInt - 1; i >= 0; i--)
                RemoveItem(true);
        }
        else if (items.Count < count)
        {
            tempInt = count - items.Count;
            for (int i = 0; i < tempInt; i++)
                AddItemWithScaling(transform.position, Vector3.one, Vector3.one, true);
        }*/
    }

    public virtual GameObject GetLastStackItem()
    {
        if (items.Count > 0)
            return items[^1]; // Means Last member of the list
        else
            return null;
    }

    public void AddItemWithScaling(Vector3 from, Vector3 startScale, Vector3 targetScale, bool isCurrencyCallback = false)
    {
        if (!isCurrencyCallback && !dontUseCurrency)
            relatedCurrency.Value++;

        // Get GameObject from Pool
        tempGo = PoolManager.Fetch(stackPrefabInfo.PoolName);
        // Get Stack interface
        tempStackItem = tempGo.GetComponent<IStackItem>();
        // Set new Location of The object
        tempStackItem.Location = new Vector3Int(currentLocation.x, currentLocation.y, currentLocation.z);
        // Set offset between stack items by stack direcktion
        if(stackDirection == StackDirection.Z)
            tempStackItem.ZPositionOffset = tileSize.z;
        else if(stackDirection == StackDirection.Y)
            tempStackItem.YPositionOffset = tileSize.y;
        
        if (items.Count >= stackSize.x)
        {
            tempStackItem.BeforeMe = items[items.Count - stackSize.x];
            tempGo.transform.localRotation = items[items.Count - stackSize.x].transform.localRotation;
        }
        else
        {
            tempStackItem.BeforeMe = containers[currentLocation.x % containers.Count].gameObject;
            tempGo.transform.localRotation = containers[currentLocation.x % containers.Count].localRotation;
        }

        tempGo.transform.localPosition = TargetLocalPosition(tempStackItem.BeforeMe.transform.position);//tempGo.transform.parent.InverseTransformPoint(from);
        //tempStackItem.TargetLocalPosition = TargetLocalPosition(tempStackItem.BeforeMe.transform.position);

        items.Add(tempGo);

        OnStackIncrease?.Invoke();

        tempStackItem.StartMovingWithScaling(startScale, targetScale);
        tempGo.SetActive(true);

        IncreaseStackSize();
    }
    private Vector3 TargetLocalPosition(Vector3 beforeMePos)
    {
        tempVec3 = parent.position;
        tempVec3.y = beforeMePos.y + tileSize.y;

        if (stackDirection == StackDirection.Z)
            tempVec3.y = beforeMePos.z + tileSize.z;
        else if (stackDirection == StackDirection.Y)
            tempVec3.y = beforeMePos.y + tileSize.y;

        return parent.InverseTransformPoint(tempVec3);
    }

    

    private void IncreaseStackSize()
    {
        currentStackSize++;
        currentLocation.x++;

        if (currentLocation.x >= stackSize.x)
        {
            currentLocation.x = 0;
            currentLocation.z++;
            if (currentLocation.z >= stackSize.z)
            {
                currentLocation.z = 0;
                currentLocation.y++;
                if (currentLocation.y >= stackSize.y)
                {
                    currentLocation.y = 0;
                }
            }
        }
    }

    public void RemoveItem(bool isCurrencyCallback = false)
    {
        if (items.Count >= 1)
            RemoveItem(items[items.Count - 1], isCurrencyCallback);
    } 

    public void Scatter(GameObject go)
    {
        tempStackItem = go.GetComponent<IStackItem>();
        if (items.Contains(go))
        {
            itemIndex = items.IndexOf(go);

            for (int i = items.Count - 1; i >= itemIndex; i--)
            {
                tempStackItem = items[i].GetComponent<IStackItem>();
                tempStackItem.Scatter();
                RemoveItem(items[i]);
            }
        }
        
    } 

    public void ScatterAll()
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            tempStackItem = items[i].GetComponent<IStackItem>();
            tempStackItem.Scatter();
            RemoveItem(items[i]);
        }
    }  

    public void RemoveAll()
    {
        for (int i = items.Count - 1; i >= 0; i--)
            RemoveItem(items[i]);

        items.Clear();
    }  

    public void RemoveItem(GameObject go, bool isCurrencyCallback = false)
    {
        tempStackItem = go.GetComponent<IStackItem>();
        if (items.Contains(go))
        {
            if (!isCurrencyCallback && !dontUseCurrency)
                relatedCurrency.Value--;

            itemIndex = items.IndexOf(go);
            currentLocation = tempStackItem.Location;
            currentStackSize = items.Count - itemIndex;

            items.Remove(go);
            go.GetComponent<PoolObject>().Release();

            for (int i = itemIndex; i < items.Count; i++)
            {

                tempStackItem = items[i].GetComponent<IStackItem>();
                tempStackItem.Location = currentLocation;
                
                if (stackDirection == StackDirection.Z)
                    tempStackItem.ZPositionOffset = tileSize.z;
                else if (stackDirection == StackDirection.Y)
                    tempStackItem.YPositionOffset = tileSize.y;
                
                if (i >= stackSize.x)
                    tempStackItem.BeforeMe = items[i - stackSize.x];
                else
                    tempStackItem.BeforeMe = containers[currentLocation.x % containers.Count].gameObject;

                IncreaseStackSize();
            }
        }

        
    }
    private Vector3 Getlocation(Vector3Int pos)
    {
        tempVec3.x = (tileSize.x / 2f) * (pos.x + 1);
        tempVec3.z = (tileSize.z / 2f) * (pos.z - 1);
        tempVec3.y = (tileSize.y / 2f) * (pos.y + 1);
        tempVec3 += holderOffset;
        return tempVec3;
    }
}
