using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager ins;
    [SerializeField] SaveData currentSave;
    
    private bool IsSaveFileExists => File.Exists(GetPath());
    private string GetPath()
    {
        return Application.persistentDataPath + "/gamedata.data";
    }
    public SaveData CurrentSave { get => currentSave; }

    public void Save(SaveData data)
    {
        string stringSaveData = JsonUtility.ToJson(data);

        if (!IsSaveFileExists)
            CreateSaveFile();

        File.WriteAllText(GetPath(), stringSaveData);

    }

    public void ResetSaveData()
    {
        File.WriteAllText(GetPath(), "");
    }

    public void Save()
    {
        string stringSaveData = JsonUtility.ToJson(CurrentSave);

        if (!IsSaveFileExists)
            CreateSaveFile();

        File.WriteAllText(GetPath(), stringSaveData);

    }

    private void CreateSaveFile()
    {
        FileStream fs = File.Create(GetPath());
        fs.Close();
    }

    private void Awake()
    {
        if (ins == null)
            ins = this;

        Load();
    }

    
    public void Load()
    {
        if (!IsSaveFileExists)
            CreateSaveFile();
        else
        {
            string jsonStr = GetSaveFileContent();

            if (jsonStr.Trim() != "")
                currentSave = JsonUtility.FromJson<SaveData>(jsonStr);
        }
    }

    public void Load(SaveData data)
    {
        currentSave = data;
    }

    private string GetSaveFileContent()
    {
        return File.ReadAllText(GetPath());
    }
   
}

