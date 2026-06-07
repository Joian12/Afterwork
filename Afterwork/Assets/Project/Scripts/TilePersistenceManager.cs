using System.Collections.Generic;
using System.IO;
using UnityEngine;
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
    }
    
    public void AddTileSurfaceData(TileSurfaceData tileSurfaceData)
    {
        // just update it if it exists already
        if (_tileSurfaceDataList.Contains(tileSurfaceData)) 
        {
            int index = _tileSurfaceDataList.FindIndex(x => x.TileId == tileSurfaceData.TileId);
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
    public TileSurfaceType SurfaceType;
}

[System.Serializable]
public class TileSurfaceDataWrapper
{
    public List<TileSurfaceData> TileSurfaceDataList;
}