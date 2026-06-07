using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TilePersistenceManager : MonoBehaviour
{
    public static TilePersistenceManager Instance { get; private set; }
    
    private readonly string _tileSurfaceDataPath = "TileSurfaceData.json";
    
    private List<TileSurfaceData> _tileSurfaceDataList = new();
    
    [SerializeField] private Button _saveButton; // test
    
    private void Awake()
    {
        Instance = this;
        
        _saveButton.onClick.AddListener(SaveTileSurfaceData);
        
        LoadTileSurfaceData();
    }
    
    public void AddTileSurfaceData(TileSurfaceData tileSurfaceData)
    {
        // just update it if it exists already
        int index = _tileSurfaceDataList.FindIndex(x => x.TilePos == tileSurfaceData.TilePos);
        if (index != -1) 
        {
            _tileSurfaceDataList[index] = tileSurfaceData;
            return;
        }
        
        _tileSurfaceDataList.Add(tileSurfaceData);
    }
    
    private void SaveTileSurfaceData()
    {
        TileSurfaceDataWrapper tileSurfaceDataWrapper = new();
        tileSurfaceDataWrapper.TileSurfaceDataList = _tileSurfaceDataList;
        
        string json = JsonUtility.ToJson(tileSurfaceDataWrapper, true);
        File.WriteAllText(_tileSurfaceDataPath, json);
    }
    
    public void LoadTileSurfaceData()
    {
        if (File.Exists(_tileSurfaceDataPath) == false)
        {
            return;
        }
        
        string json = File.ReadAllText(_tileSurfaceDataPath);
        TileSurfaceDataWrapper tileSurfaceDataWrapper = JsonUtility.FromJson<TileSurfaceDataWrapper>(json);
        _tileSurfaceDataList = tileSurfaceDataWrapper.TileSurfaceDataList;
    }
    
    public List<TileSurfaceData> GetTileSurfaceDataList()
    {
        return _tileSurfaceDataList;
    }

    private void OnApplicationQuit()
    {   
        SaveTileSurfaceData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveTileSurfaceData();
        }
    }
}

[System.Serializable]
public class TileSurfaceData
{
    public int TileId;
    public int TilePos;
    public string TileName;
    public TileSurfaceType TileSurfaceType;
}

[System.Serializable]
public class TileSurfaceDataWrapper
{
    public List<TileSurfaceData> TileSurfaceDataList;
}